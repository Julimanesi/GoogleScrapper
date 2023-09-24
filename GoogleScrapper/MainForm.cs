using System.Diagnostics;
using System.ComponentModel;
using System.Security.Policy;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Google.Apis.YouTube.v3.Data;
using static System.Windows.Forms.LinkLabel;
using System.Linq;

namespace GoogleScrapper
{
    public partial class MainForm : Form
    {
        public static string URLGoogle = "https://www.google.com/search?q=";
        private int NroMinimoResultados = 1;
        public static decimal NroAureo { get; } = 1.61803M;
        private List<string> LinksVideosYoutube = new List<string>();
        private List<PanelYoutube> panelResultadoYoutubes = new List<PanelYoutube>();
        private bool TodosPanelesYoutubeSeleccionados = false;

        public MainForm()
        {
            InitializeComponent();
            FechaInicioDTP.Value = DateTime.Now.AddYears(-1);
            FechaFinDTP.Value = DateTime.Now;
            FechaInicioDTP.MaxDate = DateTime.Now;
            FechaFinDTP.MaxDate = DateTime.Now;
            EjecucionProcesos.Inicializar();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.Icon = new Icon(@"C:\Users\julim\OneDrive\Documentos\Visual Studio 2022\Windows Form\GoogleScrapper\GoogleScrapper\multimedia_player_16922.ico");
        }

        public void DescargarVideos(string listaVideos) 
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                DescargVideosBTN.Enabled = false;
                DescargVideosBTN.Text = "Descargando Videos...";
                //EjecucionProcesos.DescargarListaReproduc(GetLinVideosInALine(), LinkVideosLB.SelectedItems.Count, folderDialog.SelectedPath);
                DescargaVideoForm descargaVideoForm = new DescargaVideoForm(listaVideos, LinkVideosLB.SelectedItems.Count, folderDialog.SelectedPath);
                descargaVideoForm.FormClosed += DescargaVideoForm_Closed;
                descargaVideoForm.Activate();
                descargaVideoForm.Show();
            }
        }

        #region Google Video Scrapper

        #region Eventos Controles
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
            NroMinimoResultados = (int)NumMinResultVideoNM.Value;
            panelProgreso.Visible = true;
            VideoScrapper videoScrapper = new VideoScrapper(BuscarVideoTB.Text, DuracionVideoCOMBX.SelectedIndex, FechaVideoCOMBX.SelectedIndex, AltaCalidadCKBX.Checked, FechaInicioDTP.Value, FechaFinDTP.Value);
            VerificarVideosbackgrWorker.RunWorkerAsync(videoScrapper);
            BuscarVideoBTN.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResultadoVideo seleccionado = (ResultadoVideo)resultadoVideoBindingSource.Current;
            if (seleccionado != null)
                Clipboard.SetText(seleccionado.URLVideo);
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
                MessageBox.Show("Error al hacer doble click en el video");
            }
        }

        private void AgregarEnviarSmplayer_Click(object sender, EventArgs e)
        {
            EjecucionProcesos.EnviarListaReproducASMPlayer(GetLinVideosInALine());
        }

        private void FechaVideoCB_Changed(object sender, EventArgs e)
        {
            if (FechaVideoCOMBX.SelectedIndex == 6)
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

        private void DescargVideosBTN_Click(object sender, EventArgs e)
        {
            DescargarVideos(GetLinVideosInALine());
        }

        private void DescargaVideoForm_Closed(object? sender, System.EventArgs e)
        {
            DescargVideosBTN.Enabled = true;
            DescargVideosBTN.Text = "Descargar Videos Seleccionados";
            if (sender != null)
            {
                DescargaVideoForm descargaVideoForm = (DescargaVideoForm)sender;
                descargaVideoForm.Dispose();
            }
        }
        #endregion

        private List<ResultadoVideo> ObtenerVideosReproduciblesPorPagina(VideoScrapper videoScrapper, List<string> ListaEstractores, int pagina)
        {
            var resultadoVideosReproducibles = new ConcurrentBag<ResultadoVideo>();
            try
            {
                List<ResultadoVideo> resultadoVideos = videoScrapper.ObtenerLinksVideos(pagina);
                if (SoloListaExtYtdlCKBX.Checked)
                    //resultadoVideos = resultadoVideos.Where(x => ListaEstractores.Any(c => x.URLVideo.Split(".")[1].ToLower() == c.ToLower())).ToList();
                    resultadoVideos = resultadoVideos.Where(x => x.URLVideo.Contains("http") && ListaEstractores.Any(c => new Uri(x.URLVideo).Host.Split('.')[new Uri(x.URLVideo).Host.Split('.').Length - 2] == c.ToLower())).ToList();

                Parallel.ForEach(resultadoVideos, resultado =>
                {
                    if (EjecucionProcesos.YtdlPuedeReproducir(resultado.URLVideo))
                        resultadoVideosReproducibles.Add(resultado);
                });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, $"Error al Obtener los Videos Reproducibles de la Pagina: {pagina}");
            }
            return resultadoVideosReproducibles.ToList();
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

        #region Background Workers
        private void BWVerificarVideo_Dowork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            VideoScrapper videoScrapper = (VideoScrapper)e.Argument;
            int nroPaginasTotales = videoScrapper.GetNroPaginas(worker);
            worker.ReportProgress(0, new ProgresoTotal(0, 0, nroPaginasTotales));
            List<string> ListaEstractores = EjecucionProcesos.GetListaExtractores();
            List<ResultadoVideo> resultadoVideos = new List<ResultadoVideo>();
            int nroPagina = 0;
            do
            {
                resultadoVideos.AddRange(ObtenerVideosReproduciblesPorPagina(videoScrapper, ListaEstractores, nroPagina++));
                resultadoVideos = resultadoVideos.DistinctBy(x => x.URLVideo).ToList();
                worker.ReportProgress((int)(((decimal)nroPagina / nroPaginasTotales) * 100), new ProgresoTotal(resultadoVideos.Count, nroPagina, nroPaginasTotales));
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
        #endregion

        #endregion

        #region Youtube Video Scrapper

        private void BuscarYoutubeBTN_Click(object sender, EventArgs e)
        {
            ResultadosYouTubeFlowLayPanel.Controls.Clear();
            int ancho = (ResultadosYouTubeFlowLayPanel.Width - (ResultadosYouTubeFlowLayPanel.Margin.Horizontal * (int)NumColumnasResultNM.Value + 21)) / (int)NumColumnasResultNM.Value;
            var searchListResponse = JsonConvert.DeserializeObject<Google.Apis.YouTube.v3.Data.SearchListResponse>(File.ReadAllText(@"C:\Users\julim\OneDrive\Documentos\Visual Studio 2022\Windows Form\GoogleScrapper\GoogleScrapper\youtubeApiTest.json"));
            foreach (var searchResult in searchListResponse.Items)
            {
                PanelYoutube panelResultado = new PanelYoutube(ancho, (int)(ancho / NroAureo), searchResult);
                panelResultadoYoutubes.Add(panelResultado);
            }
            ResultadosYouTubeFlowLayPanel.Controls.AddRange(panelResultadoYoutubes.ToArray());
            BotoneraYoutube.Visible = true;
        }
        private void Ajustar_imagenes(object sender, EventArgs e)
        {
            RenderizarResultados((int)NumColumnasResultNM.Value);
        }
        private void NumColumnasResultNM_ValueChanged(object sender, EventArgs e)
        {
            RenderizarResultados((int)NumColumnasResultNM.Value);
        }
        private void RenderizarResultados(int CantColum = 3)
        {
            int ancho = (ResultadosYouTubeFlowLayPanel.Width - (ResultadosYouTubeFlowLayPanel.Margin.Horizontal * CantColum + 21)) / CantColum;
            foreach (PanelYoutube panelResultado in panelResultadoYoutubes)
            {
                panelResultado.VolverARenderizar(ancho, (int)(ancho / NroAureo));
            }
        }

        #endregion

        private void AgregarEnviarSMplayerYoutbBTN_Click(object sender, EventArgs e)
        {
            EjecucionProcesos.EnviarListaReproducASMPlayer(string.Join(" ", panelResultadoYoutubes.Where(j => j.seleccionado).Select(x => x.Link)));
        }

        private void DescargVideosYouTbBTN_Click(object sender, EventArgs e)
        {
            DescargarVideos(string.Join(" ", panelResultadoYoutubes.Where(j=>j.seleccionado).Select(x=>x.Link)));
        }

        private void SeleccionarTodosYouTubeBTN_Click(object sender, EventArgs e)
        {
            foreach (PanelYoutube panelResultado in panelResultadoYoutubes)
            {
                panelResultado.seleccionado = TodosPanelesYoutubeSeleccionados;
                panelResultado.OnSeleccionado();
            }
            if (TodosPanelesYoutubeSeleccionados)
            {
                SeleccionarTodosYouTubeBTN.Text = "Seleccionar todos";
            }
            else
            {
                SeleccionarTodosYouTubeBTN.Text = "Deseleccionar todos";
            }
            TodosPanelesYoutubeSeleccionados = !TodosPanelesYoutubeSeleccionados;
        }
    }
}