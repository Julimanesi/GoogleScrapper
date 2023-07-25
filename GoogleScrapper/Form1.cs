using System.Diagnostics;
using System.ComponentModel;
using System.Security.Policy;
using System.Collections.Generic;

namespace GoogleScrapper
{
    public partial class Form1 : Form
    {
        public static string URLGoogle = "https://www.google.com/search?q=";
        private int NroMinimoResultados = 1;
        public Form1()
        {
            InitializeComponent();
            FechaInicioDTP.Value = DateTime.Now.AddYears(-1);
            FechaFinDTP.Value = DateTime.Now;
            FechaInicioDTP.MaxDate = DateTime.Now;
            FechaFinDTP.MaxDate = DateTime.Now;
        }

        private void BuscarVideoBTN_Click(object sender, EventArgs e)
        {
            //Pongo a 0 cada vez que presiono el boton buscar
            resultadoVideoBindingSource.DataSource = new BindingSource();
            VideoNumPagprogressBar.Value = 0;
            NumeroPagTotalesLabel.Text = "Numero de Paginas encontradas: Calculando...";
            PaginasVisitadasLabel.Text = "Paginas Visitadas:";
            NumResultadosActulabel.Text = $"Numero de Resultados Actuales:";
            panelResultado.Visible = false;
            //inicializo variables locales
            if (FechaInicioDTP.Value > FechaFinDTP.Value)
            {
                FechaInicioDTP.Value = FechaFinDTP.Value;
                FechaInicioDTP.Value.AddDays(-7);
            }
            NroMinimoResultados = (int)NumMinResultVideoNumeric.Value;
            panelProgreso.Visible = true;
            VideoScrapper videoScrapper = new VideoScrapper(BuscarVideoTB.Text, DuracionVideoCB.SelectedIndex, FechaVideoCB.SelectedIndex, AltaCalidadCB.Checked, FechaInicioDTP.Value, FechaFinDTP.Value);
            VerificarVideosbackgrWorker.RunWorkerAsync(videoScrapper);
            BuscarVideoBTN.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResultadoVideo seleccionado = (ResultadoVideo)resultadoVideoBindingSource.Current;
            if (seleccionado != null)
                Clipboard.SetText(seleccionado.URLVideo);
        }





        private List<ResultadoVideo> ObtenerVideosReproduciblesPorPagina(VideoScrapper videoScrapper, List<string> ListaEstractores, int pagina)
        {
            
            List<ResultadoVideo> resultadoVideos = videoScrapper.ObtenerLinksVideos(pagina);
            if (SoloListaExtYtdlCB.Checked)
                //resultadoVideos = resultadoVideos.Where(x => ListaEstractores.Any(c => x.URLVideo.Split(".")[1].ToLower() == c.ToLower())).ToList();
                resultadoVideos = resultadoVideos.Where(x =>x.URLVideo.Contains("http") && ListaEstractores.Any(c => new Uri(x.URLVideo).Host.Split('.')[new Uri(x.URLVideo).Host.Split('.').Length-2] == c.ToLower())).ToList();

            List<ResultadoVideo> resultadoVideosReproducibles = new List<ResultadoVideo>();
            try
            {
                foreach (ResultadoVideo resultado in resultadoVideos)
                {
                    if (EjecucionProcesos.YtdlPuedeReproducir(resultado.URLVideo))
                        resultadoVideosReproducibles.Add(resultado);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, $"Error al Obtener los Videos Reproducibles de la Pagina: {pagina}");
            }
            return resultadoVideosReproducibles;
        }

        private void LinVideoDoubleClick(object sender, EventArgs e)
        {
            try
            {
                ResultadoVideo seleccionado = (ResultadoVideo)resultadoVideoBindingSource.Current;
                if (seleccionado != null)
                {
                    EjecucionProcesos.ReproducirUnVideo(seleccionado.URLVideo);
                }
            }
            catch (Exception)
            {

            }
        }



        private void AgregarEnviarSmplayer_Click(object sender, EventArgs e)
        {
            EjecucionProcesos.EnviarListaReproducASMPlayer(GetLinVideosInALine());
        }

        private string GetLinVideosInALine()
        {
            string result = "";
            List<ResultadoVideo> ListaVideos = LinkVideosLB.SelectedItems.Cast<ResultadoVideo>().ToList();
            if (ListaVideos != null && ListaVideos.Count > 0)
            {
                List<string> listaLinkVideosFullPath = ListaVideos.Select(x => new string($"\"{x.URLVideo}\"")).ToList();
                result = string.Join(" ", listaLinkVideosFullPath);
            }
            return result;
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

        private void SeleccionarTodosButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LinkVideosLB.Items.Count; i++)
            {
                LinkVideosLB.SetSelected(i, true);
            }
        }


        private void BWVerificarVideo_Dowork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            VideoScrapper videoScrapper = (VideoScrapper)e.Argument;
            int nroPaginasTotales = videoScrapper.GetNroPaginas(worker);
            worker.ReportProgress(0, new ProgresoTotal(0,0, nroPaginasTotales));
            List<string> ListaEstractores = EjecucionProcesos.GetListaExtractores();
            List<ResultadoVideo> resultadoVideos = new List<ResultadoVideo>();
            int nroPagina = 0;
            do
            {
                resultadoVideos.AddRange(ObtenerVideosReproduciblesPorPagina(videoScrapper, ListaEstractores, nroPagina++));
                resultadoVideos = resultadoVideos.DistinctBy(x => x.URLVideo).ToList();
                worker.ReportProgress((int)(((decimal)nroPagina/nroPaginasTotales) * 100), new ProgresoTotal(resultadoVideos.Count, nroPagina, nroPaginasTotales));
            }
            while (resultadoVideos.Count < NroMinimoResultados && nroPaginasTotales > nroPagina);
            e.Result = resultadoVideos;
        }

        private void BWVerificarVideo_Progreso(object sender, ProgressChangedEventArgs e)
        {
            VideoNumPagprogressBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                ProgresoTotal progreso = (ProgresoTotal)e.UserState;
                NumeroPagTotalesLabel.Text = $"Numero de paginas encontradas: {progreso.NroPaginasEncontradas}";
                PaginasVisitadasLabel.Text = $"Paginas Visitadas:({progreso.NroPaginasVisitadas})";
                ResultadosActualesProgressBar.Value = progreso.NroResultadosActuales < NroMinimoResultados ? (int)(((decimal)progreso.NroResultadosActuales / NroMinimoResultados) * 100) : 100;
                NumResultadosActulabel.Text = $"Numero de Resultados Actuales: {progreso.NroResultadosActuales}";
            }
        }

        private void BWVerificarVideo_Resultado(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            { 
                List<ResultadoVideo> resultadoVideos = (List<ResultadoVideo>)e.Result;
                resultadoVideoBindingSource.DataSource = resultadoVideos;
                panelResultado.Visible = true;
                ResultadosTotalesLabel.Text = $"Resultados Totales: {resultadoVideos.Count}";
            }
            BuscarVideoBTN.Enabled = true;
        }
    }
}