using System.Diagnostics;
using System.ComponentModel;
using System.Security.Policy;

namespace GoogleScrapper
{
    public partial class Form1 : Form
    {
        public static string URLGoogle = "https://www.google.com/search?q=";
        private string ArchivoListaReprodcAux = "ListaReproducAux.m3u";

        public Form1()
        {
            InitializeComponent();
            FechaInicioDTP.Value = DateTime.Now.AddYears(-1);
            FechaFinDTP.Value = DateTime.Now;
            FechaInicioDTP.MaxDate = DateTime.Now;
            FechaFinDTP.MaxDate = DateTime.Now;
        }

        private async void BuscarVideoBTN_Click(object sender, EventArgs e)
        {
            //Pongo a 0 cada vez que presiono el boton buscar
            resultadoVideoBindingSource.DataSource = new BindingSource();
            VideoNumPagprogressBar.Value = 0;
            VideoPorPaginaprogressBar.Value = 0;
            NumeroPagTotalesLabel.Text= "Numero de Paginas encontradas: Calculando...";
            VideoPorPaginaLabel.Text = "Videos Por Pagina:";
            PaginasVisitadasLabel.Text = "Paginas Visitadas:";
            panelResultado.Visible = false;
            //inicializo variables locales
            if (FechaInicioDTP.Value > FechaFinDTP.Value)
            {
                FechaInicioDTP.Value = FechaFinDTP.Value;
                FechaInicioDTP.Value.AddDays(-7);
            }
            panelProgreso.Visible = true;
            VideoScrapper videoScrapper = new VideoScrapper(BuscarVideoTB.Text,DuracionVideoCB.SelectedIndex,FechaVideoCB.SelectedIndex,AltaCalidadCB.Checked,FechaInicioDTP.Value,FechaFinDTP.Value);
            List<string> ListaEstractores = await GetListaExtractores();
            List<ResultadoVideo> resultadoVideos = new List<ResultadoVideo>();
            int i = 0;
            VideoNumPagprogressBar.Maximum = videoScrapper.GetNroPaginas();
            NumeroPagTotalesLabel.Text = $"Numero de paginas encontradas: {VideoNumPagprogressBar.Maximum}";
            do
            {
                resultadoVideos.AddRange(ObtenerVideosReproduciblesPorPagina(videoScrapper, ListaEstractores, i++));
                VideoNumPagprogressBar.Value++;
                resultadoVideos = resultadoVideos.DistinctBy(x=>x.URLVideo).ToList();
                PaginasVisitadasLabel.Text = $"Paginas Visitadas:({i})";
            } 
            while (resultadoVideos.Count < NumMinResultVideoNumeric.Value && VideoNumPagprogressBar.Maximum > i);

            resultadoVideoBindingSource.DataSource = resultadoVideos;
            panelResultado.Visible = true;
            ResultadosTotalesLabel.Text = $"Resultados Totales: {resultadoVideos.Count}";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResultadoVideo seleccionado =(ResultadoVideo)resultadoVideoBindingSource.Current;
            if(seleccionado != null)
                Clipboard.SetText(seleccionado.URLVideo);
        }


        private async Task<List<string>> GetListaExtractores()
        {
            List<string> extractores = new List<string>();
            using (Process ytdl = new Process())
            {
                ytdl.StartInfo.FileName = "yt-dlp.exe";
                ytdl.StartInfo.Arguments = "--list-extractors";
                ytdl.StartInfo.UseShellExecute = false;
                ytdl.StartInfo.CreateNoWindow = true;
                ytdl.StartInfo.RedirectStandardOutput = true;
                ytdl.Start();

                extractores.AddRange(ytdl.StandardOutput.ReadToEnd().Split('\n'));

                //ytdl.WaitForExit();
                await ytdl.WaitForExitAsync();
            }
            return extractores;
        }

        private bool YtdlPuedeReproducir(string Url)
        {
            bool resp = true;
            using (Process ytdl = new Process())
            {
                ytdl.StartInfo.FileName = "yt-dlp.exe";
                ytdl.StartInfo.Arguments = $"-s {Url}";
                ytdl.StartInfo.UseShellExecute = false;
                ytdl.StartInfo.CreateNoWindow = true;
                ytdl.StartInfo.RedirectStandardOutput = true;
                ytdl.StartInfo.RedirectStandardError = true;
                ytdl.Start();

                var procesoOutput = ytdl.StandardError.ReadToEnd();
                resp = procesoOutput == null || !procesoOutput.Contains("ERROR");

                ytdl.WaitForExit();
            }
            return resp;
        }

        private List<ResultadoVideo> ObtenerVideosReproduciblesPorPagina(VideoScrapper videoScrapper, List<string> ListaEstractores ,int pagina)
        {
            VideoPorPaginaprogressBar.Value = 0;
            List<ResultadoVideo> resultadoVideos = videoScrapper.ObtenerLinksVideos(pagina);
            if (SoloListaExtYtdlCB.Checked)
                resultadoVideos = resultadoVideos.Where(x => ListaEstractores.Any(c => x.URLVideo.Split(".")[1].ToLower() == c.ToLower())).ToList();
            
            VideoPorPaginaprogressBar.Maximum = resultadoVideos.Count;
            VideoPorPaginaLabel.Text = $"Videos Por Pagina Actual({resultadoVideos.Count}):";

            List<ResultadoVideo> resultadoVideosReproducibles = new List<ResultadoVideo>();
            foreach (ResultadoVideo resultado in resultadoVideos)
            {
                if (YtdlPuedeReproducir(resultado.URLVideo))
                    resultadoVideosReproducibles.Add(resultado);
                VideoPorPaginaprogressBar.Value++;
            }
            return resultadoVideosReproducibles;
        }

        private void LinVideoDoubleClick(object sender, EventArgs e)
        {
            ResultadoVideo seleccionado = (ResultadoVideo)resultadoVideoBindingSource.Current;
            if (seleccionado != null) 
            {
                using (Process smplayer = new Process())
                {
                    smplayer.StartInfo.FileName = "C:\\Program Files\\SMPlayer\\smplayer.exe";
                    smplayer.StartInfo.Arguments = $" {seleccionado.URLVideo}";
                    smplayer.StartInfo.UseShellExecute = false;
                    smplayer.Start();
                    smplayer.WaitForExit();
                }
            }
        }

        private void CrearListaReproducAux()
        {
            List<ResultadoVideo> ListaVideos = (List<ResultadoVideo>) resultadoVideoBindingSource.DataSource;
            if(ListaVideos!=null && ListaVideos.Count > 0)
            {
                File.WriteAllLines(ArchivoListaReprodcAux, ListaVideos.Select(x => x.URLVideo));
            }
        }

        private void AgregarEnviarSmplayer_Click(object sender, EventArgs e)
        {
            using (Process smplayer = new Process())
            {
                smplayer.StartInfo.FileName = "C:\\Program Files\\SMPlayer\\smplayer.exe";
                smplayer.StartInfo.Arguments = $"-add-to-playlist "+ GetLinVideosInALine();
                smplayer.StartInfo.UseShellExecute = false;
                smplayer.Start();
                smplayer.WaitForExit();
            }
        }

        private string GetLinVideosInALine()
        {
            string result = "";
            List<ResultadoVideo> ListaVideos = (List<ResultadoVideo>)resultadoVideoBindingSource.DataSource;
            if (ListaVideos != null && ListaVideos.Count > 0)
            {
               List<string> listaLinkVideosFullPath= ListaVideos.Select(x => new string($"\"{x.URLVideo}\"")).ToList();
                result = string.Join(" ", listaLinkVideosFullPath);
            }
            return result;
        }

        private void ReproducirListaAuxGuardada()
        {
            CrearListaReproducAux();
            if (File.Exists(ArchivoListaReprodcAux))
            {
                using (Process smplayer = new Process())
                {
                    smplayer.StartInfo.FileName = "C:\\Program Files\\SMPlayer\\smplayer.exe";
                    smplayer.StartInfo.Arguments = $"-add-to-playlist \"{Path.GetFullPath(ArchivoListaReprodcAux)}\"";
                    smplayer.StartInfo.UseShellExecute = false;
                    smplayer.Start();
                    smplayer.WaitForExit();
                }
            }
        }

        private void FechaVideoCB_Changed(object sender, EventArgs e)
        {
            if (FechaVideoCB.SelectedIndex == 6)
            {
                FechaInicioDTP.Visible = true;
                FechaFinDTP.Visible = true;
                FechaIniciolabel.Visible = true;
                FechaFinlabel.Visible = true;
            }
            else
            {
                FechaInicioDTP.Visible = false;
                FechaFinDTP.Visible = false;
                FechaIniciolabel.Visible = false;
                FechaFinlabel.Visible = false;
            }
        }
    }
}