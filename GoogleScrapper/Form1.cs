using System.Diagnostics;
using System.ComponentModel;

namespace GoogleScrapper
{
    public partial class Form1 : Form
    {
        public static string URLGoogle = "https://www.google.com/search?q=";

        public Form1()
        {
            InitializeComponent();
        }

        private void BuscarVideoBTN_Click(object sender, EventArgs e)
        {
            //Pongo a 0 cada vez que presiono el boton buscar
            resultadoVideoBindingSource.DataSource = new BindingSource();
            VideoNumPagprogressBar.Value = 0;
            VideoPorPaginaprogressBar.Value = 0;
            NumeroPagTotalesLabel.Text= "Numero de Paginas encontradas:";
            VideoPorPaginaLabel.Text = "Videos Por Pagina:";
            PaginasVisitadasLabel.Text = "Paginas Visitadas:";
            //inicializo variables locales
            VideoScrapper videoScrapper = new VideoScrapper(BuscarVideoTB.Text,DuracionVideoCB.SelectedIndex,FechaVideoCB.SelectedIndex);
            List<string> ListaEstractores = GetListaExtractores();
            List<ResultadoVideo> resultadoVideos = new List<ResultadoVideo>();
            int i = 0;

            VideoNumPagprogressBar.Maximum = videoScrapper.GetNroPaginas(ref NumeroPagTotalesLabel);
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
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResultadoVideo seleccionado =(ResultadoVideo)resultadoVideoBindingSource.Current;
            if(seleccionado != null)
                Clipboard.SetText(seleccionado.URLVideo);
        }


        private List<string> GetListaExtractores()
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

                ytdl.WaitForExit();
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
            VideoPorPaginaLabel.Text = $"Videos Por Pagina({resultadoVideos.Count}):";

            List<ResultadoVideo> resultadoVideosReproducibles = new List<ResultadoVideo>();
            foreach (ResultadoVideo resultado in resultadoVideos)
            {
                if (YtdlPuedeReproducir(resultado.URLVideo))
                    resultadoVideosReproducibles.Add(resultado);
                VideoPorPaginaprogressBar.Value++;
            }
            return resultadoVideosReproducibles;
        }
    }
}