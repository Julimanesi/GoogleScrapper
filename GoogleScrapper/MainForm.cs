using System.Diagnostics;
using System.ComponentModel;
using System.Security.Policy;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Google.Apis.YouTube.v3.Data;
using static System.Windows.Forms.LinkLabel;
using System.Linq;
using Google.Apis.YouTube.v3;
using System.Globalization;
using static Google.Apis.YouTube.v3.SearchResource.ListRequest;
using System.Windows.Forms;

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
        private YoutubeApi? YoutubeApi;
        public static PanelYoutube? ItemResultadoSeleccionado { get; set; }

        private string ResultadoBusqueda { get; set; } = "";
        private string ResultadoListaVideos { get; set; } = "";

        private string PathDirectorioResultadoBusqueda { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Resultados de Busqueda");

        public MainForm()
        {
            InitializeComponent();
            FechaInicioDTP.Value = DateTime.Now.AddYears(-1);
            FechaFinDTP.Value = DateTime.Now;
            FechaInicioDTP.MaxDate = DateTime.Now;
            FechaFinDTP.MaxDate = DateTime.Now;
            InicioYoutbDTP.Value = DateTime.Now.AddYears(-50);
            FinYoutbDTP.Value = DateTime.Now;
            InicioYoutbDTP.MaxDate = DateTime.Now;
            FinYoutbDTP.MaxDate = DateTime.Now;
            EjecucionProcesos.Inicializar();
            DuracionYoutubeVideoCBX.DataSource = Enum.GetValues(typeof(SearchResource.ListRequest.VideoDurationEnum));
            var allregions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Where(x => !x.Equals(CultureInfo.InvariantCulture))
            .Where(x => !x.IsNeutralCulture).Select(x => new RegionInfo(x.LCID)).DistinctBy(x => x.GeoId).OrderBy(x => x.EnglishName).ToList();
            PaisComboBox.DataSource = allregions;
            PaisComboBox.ValueMember = "Name";
            PaisComboBox.DisplayMember = "EnglishName";
            PaisComboBox.SelectedIndex = allregions.FindIndex(x => x.Name == "US");
            OrdenComboBox.DataSource = Enum.GetValues(typeof(SearchResource.ListRequest.OrderEnum));
            SafeSearchComboBox.DataSource = Enum.GetValues(typeof(SearchResource.ListRequest.SafeSearchEnum));
            SubtitulosComboBox.DataSource = Enum.GetValues(typeof(SearchResource.ListRequest.VideoCaptionEnum));
            DefinicionComboBox.DataSource = Enum.GetValues(typeof(SearchResource.ListRequest.VideoDefinitionEnum));
            TipoVideoComboBox.DataSource = Enum.GetValues(typeof(SearchResource.ListRequest.VideoTypeEnum));
            TipoCanalComboBox.DataSource = Enum.GetValues(typeof(SearchResource.ListRequest.ChannelTypeEnum));
            var Categorias = JsonConvert.DeserializeObject<Google.Apis.YouTube.v3.Data.VideoCategoryListResponse>(File.ReadAllText(@"C:\Users\julim\OneDrive\Documentos\Visual Studio 2022\Windows Form\GoogleScrapper\GoogleScrapper\youtubeApiCategVideoUs.json"));
            var ListCategorias = Categorias.Items.Select(x => new { x.Id, x.Snippet.Title }).OrderBy(x => x.Title).ToList();
            ListCategorias.Add(new { Id = "", Title = "Ninguna" });
            VideoCategoriaComboBox.DataSource = ListCategorias;
            VideoCategoriaComboBox.ValueMember = "Id";
            VideoCategoriaComboBox.DisplayMember = "Title";
            VideoCategoriaComboBox.SelectedIndex = ListCategorias.FindIndex(x => x.Id == "");
            var ListaTipoBusqueda = new[] {
                new { Nombre = "Video", Valor = "video" },
                new { Nombre = "Lista de Reproduccion", Valor = "playlist" },
                new { Nombre = "Canal", Valor = "channel" }
            };
            TipoBusquedaComboBox.DataSource = ListaTipoBusqueda;
            TipoBusquedaComboBox.ValueMember = "Valor";
            TipoBusquedaComboBox.DisplayMember = "Nombre";
            var alllanguages = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
            IdiomaComboBox.DataSource = alllanguages;
            IdiomaComboBox.DisplayMember = "DisplayName";
            IdiomaComboBox.ValueMember = "TwoLetterISOLanguageName";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.Icon = new Icon(@"C:\Users\julim\OneDrive\Documentos\Visual Studio 2022\Windows Form\GoogleScrapper\GoogleScrapper\multimedia_player_16922.ico");
        }

        public void DescargarVideos(string listaVideos, int cant, char tipo)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                switch (tipo)
                {
                    case 'G':
                        DescargVideosBTN.Enabled = false;
                        DescargVideosBTN.Text = "Descargando Videos...";
                        break;
                    case 'Y':
                        DescargVideosYouTbBTN.Enabled = false;
                        DescargVideosYouTbBTN.Text = "Descargando Videos...";
                        break;
                    case 'D':
                        DescargaDirecVideosBTN.Enabled = false;
                        DescargaDirecVideosBTN.Text = "Descargando Videos...";
                        break;
                }

                //EjecucionProcesos.DescargarListaReproduc(GetLinVideosInALine(), LinkVideosLB.SelectedItems.Count, folderDialog.SelectedPath);
                DescargaVideoForm descargaVideoForm = new DescargaVideoForm(listaVideos, cant, folderDialog.SelectedPath);
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
            DescargarVideos(GetLinVideosInALine(), LinkVideosLB.SelectedItems.Count, 'G');
        }

        private void DescargaVideoForm_Closed(object? sender, System.EventArgs e)
        {
            DescargVideosBTN.Enabled = true;
            DescargVideosBTN.Text = "Descargar Videos Seleccionados";
            DescargVideosYouTbBTN.Enabled = true;
            DescargVideosYouTbBTN.Text = "Descargar Videos Seleccionados";
            DescargaDirecVideosBTN.Enabled = true;
            DescargaDirecVideosBTN.Text = "Descargar Videos/musica";
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

        private async void BuscarYoutubeBTN_Click(object sender, EventArgs e)
        {
            try
            {
                YoutubeApi = new YoutubeApi(BuscarYoutubVideoTB.Text, (int)MaxResultYoutubeNM.Value, InicioYoutbDTP.Value, FinYoutbDTP.Value, PaisComboBox.SelectedValue.ToString(), (VideoDurationEnum)DuracionYoutubeVideoCBX.SelectedItem, (OrderEnum)OrdenComboBox.SelectedItem, (SafeSearchEnum)SafeSearchComboBox.SelectedItem, (VideoCaptionEnum)SubtitulosComboBox.SelectedItem, (VideoDefinitionEnum)DefinicionComboBox.SelectedItem, (VideoTypeEnum)TipoVideoComboBox.SelectedItem, (string)VideoCategoriaComboBox.SelectedValue, (string)TipoBusquedaComboBox.SelectedValue, (ChannelTypeEnum)TipoCanalComboBox.SelectedItem, (string)IdiomaComboBox.SelectedValue, (string)PaisComboBox.SelectedValue);
                await YoutubeApi.Run();
                //var searchListResponse = YoutubeApi.ListaRespuesta;
                //var searchListResponse = JsonConvert.DeserializeObject<Google.Apis.YouTube.v3.Data.SearchListResponse>(File.ReadAllText(@"C:\Users\julim\OneDrive\Documentos\Visual Studio 2022\Windows Form\GoogleScrapper\GoogleScrapper\youtubeApiTest.json"));
                //CargarResultados(searchListResponse);
                CargarResultados(YoutubeApi.ListaRespuesta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al hacer la Busqueda");
            }
        }

        public void CargarResultados(SearchListResponse? searchListResponse)
        {
            if (searchListResponse != null && searchListResponse.Items.Any())
            {
                ItemResultadoSeleccionado = null;
                panelResultadoYoutubes.Clear();
                ResultadosYouTubeFlowLayPanel.Controls.Clear();
                int ancho = (ResultadosYouTubeFlowLayPanel.Width - (ResultadosYouTubeFlowLayPanel.Margin.Horizontal * (int)NumColumnasResultNM.Value + 21)) / (int)NumColumnasResultNM.Value;

                foreach (var searchResult in searchListResponse.Items)
                {
                    PanelYoutube panelResultado = new PanelYoutube(ancho, (int)(ancho / NroAureo), searchResult, panelyoutubeClick);
                    panelResultadoYoutubes.Add(panelResultado);
                }
                ResultadosYouTubeFlowLayPanel.Controls.AddRange(panelResultadoYoutubes.ToArray());
                BotoneraYoutube.Visible = true;
                ResultadosPorPaginaYouTbLabel.Text = "Resultados Por Pagina: " + searchListResponse.PageInfo.ResultsPerPage;
                ResultadosTotalesYouTbLabel.Text = "Resultados Totales: " + searchListResponse.PageInfo.TotalResults;

                NombreAchivoUltResultTXBX.Text = BuscarVideoBTN.Text;
                CambiarVisibilidadBotonesObtenerVideos();
                ResultadoBusqueda = JsonConvert.SerializeObject(searchListResponse);
            }
        }
        public void CargarResultados(PlaylistItemListResponse? searchListResponse)
        {
            if (searchListResponse != null && searchListResponse.Items.Any())
            {
                NombreAchivoUltResultTXBX.Text = ItemResultadoSeleccionado != null ? ItemResultadoSeleccionado.TituloVideoLB.Text : "Sin nombre";
                ItemResultadoSeleccionado = null;
                panelResultadoYoutubes.Clear();
                ResultadosYouTubeFlowLayPanel.Controls.Clear();
                int ancho = (ResultadosYouTubeFlowLayPanel.Width - (ResultadosYouTubeFlowLayPanel.Margin.Horizontal * (int)NumColumnasResultNM.Value + 21)) / (int)NumColumnasResultNM.Value;

                foreach (var searchResult in searchListResponse.Items.Where(x => x.Status.PrivacyStatus != "private" && x.Snippet.Title != "Deleted video"))
                {
                    try
                    {
                        PanelYoutube panelResultado = new PanelYoutube(ancho, (int)(ancho / NroAureo), searchResult, panelyoutubeClick);
                        panelResultadoYoutubes.Add(panelResultado);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                ResultadosYouTubeFlowLayPanel.Controls.AddRange(panelResultadoYoutubes.ToArray());
                BotoneraYoutube.Visible = true;
                ResultadosPorPaginaYouTbLabel.Text = "Resultados Por Pagina: " + searchListResponse.PageInfo.ResultsPerPage;
                ResultadosTotalesYouTbLabel.Text = "Resultados Totales: " + searchListResponse.PageInfo.TotalResults;

                CambiarVisibilidadBotonesObtenerVideos();
                ResultadoListaVideos = JsonConvert.SerializeObject(searchListResponse);
            }
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

        private void AgregarEnviarSMplayerYoutbBTN_Click(object sender, EventArgs e)
        {
            EjecucionProcesos.EnviarListaReproducASMPlayer(string.Join(" ", panelResultadoYoutubes.Where(j => j.seleccionado).Select(x => x.Link)));
        }

        private void DescargVideosYouTbBTN_Click(object sender, EventArgs e)
        {
            var listaLinks = panelResultadoYoutubes.Where(j => j.seleccionado).Select(x => x.Link);
            DescargarVideos(string.Join(" ", listaLinks), listaLinks.Count(), 'Y');
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

        private void TipoBusquedaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TipoBusquedaComboBox.SelectedIndex)
            {
                case 0:
                    FiltroVideoYTPanel.Visible = true;
                    break;
                case 2:
                    FiltroVideoYTPanel.Visible = false;
                    break;
                case 1:
                    FiltroVideoYTPanel.Visible = false;
                    break;
            }
        }

        private async void ObtenerVideosListaReprBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemResultadoSeleccionado != null && ItemResultadoSeleccionado.TipoResultado == TipoResultado.lista)
                {
                    var resultado = await YoutubeApi.GetPlaylistItems(ItemResultadoSeleccionado.ID);
                    if (resultado != null)
                    {
                        CargarResultados(resultado);
                        ResultadoListaVideos = JsonConvert.SerializeObject(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos de la Lista de Reproduccion");
            }
        }

        private void ObtenerVideosCanalBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemResultadoSeleccionado != null && ItemResultadoSeleccionado.TipoResultado == TipoResultado.lista)
                {
                    ObtenerVideosDesdeIDCanal(ItemResultadoSeleccionado.ID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos del Canal");
            }
        }

        private void IDListaReprodTXBX_TextChanged(object sender, EventArgs e)
        {
            ObtenerVideosIDListReprBTN.Visible = IDListaReprodTXBX.Text.Length > 0 && IDListaReprodTXBX.Text != "" && IDListaReprodTXBX.Text != " ";
        }

        private async void ObtenerVideosIDListReprBTN_Click(object sender, EventArgs e)
        {
            try
            {
                var resultado = await YoutubeApi.GetPlaylistItems(IDListaReprodTXBX.Text);
                if (resultado != null)
                {
                    CargarResultados(resultado);
                    ResultadoListaVideos = JsonConvert.SerializeObject(resultado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos de la Lista de Reproduccion");
            }
        }

        private void ObtenerVideosIDCanalBTN_Click(object sender, EventArgs e)
        {
            try
            {
                ObtenerVideosDesdeIDCanal(IDCanalTXBX.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos desde el IdCanal");
            }
        }
        private void ObtenerVideosNombreCanalBTN_Click(object sender, EventArgs e)
        {
            try 
            { 
                ObtenerVideosDesdeNombreCanal(NombreCanalTXBX.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos desde el Nombre del Canal");
            }
        }

        private void IDCanalTXBX_TextChanged(object sender, EventArgs e)
        {
            ObtenerVideosIDCanalBTN.Visible = IDCanalTXBX.Text.Length > 0 && IDCanalTXBX.Text != "" && IDCanalTXBX.Text != " ";
        }
        private void NombreCanalTXBX_TextChanged(object sender, EventArgs e)
        {
            ObtenerVideosNombreCanalBTN.Visible = NombreCanalTXBX.Text.Length > 0 && NombreCanalTXBX.Text != "" && NombreCanalTXBX.Text != " ";
        }

        private void GuardarResultadosBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(PathDirectorioResultadoBusqueda))
                {
                    Directory.CreateDirectory(PathDirectorioResultadoBusqueda);
                }
                //FileDialog fileDialog = new SaveFileDialog();
                //fileDialog.Filter = "*.json"; 
                //fileDialog.AddExtension = true;
                //if (fileDialog.ShowDialog() == DialogResult.OK)
                //{
                //    System.IO.File.WriteAllText(pathDirectorio + fileDialog.FileName, ResultadoBusqueda);
                //}
                if (ResultadoBusqueda.Length > 0)
                {
                    System.IO.File.WriteAllText(Path.Combine(PathDirectorioResultadoBusqueda, $"{NombreAchivoUltResultTXBX.Text}.rbjson"), ResultadoBusqueda, System.Text.Encoding.UTF8);
                }
                if (ResultadoListaVideos.Length > 0)
                {
                    System.IO.File.WriteAllText(Path.Combine(PathDirectorioResultadoBusqueda, $"{NombreAchivoUltResultTXBX.Text}.rljson"), ResultadoListaVideos, System.Text.Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Guardar los Resultados");
            }
        }
        private void AbrirResultadosBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(PathDirectorioResultadoBusqueda))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.InitialDirectory = PathDirectorioResultadoBusqueda;
                    ofd.Filter = "Archivos de Resultado de Busqueda (*.rbjson)|*.rbjson|Archivos de Lista de Videos (*.rljson)|*.rljson"; //All files (*.*)|*.*";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        var tipo = Path.GetExtension(ofd.FileName);
                        if (tipo != null)
                        {
                            switch (tipo)
                            {
                                case ".rbjson"://Busqueda
                                    var searchListResponse = JsonConvert.DeserializeObject<SearchListResponse>(File.ReadAllText(ofd.FileName, System.Text.Encoding.UTF8));
                                    CargarResultados(searchListResponse);
                                    break;
                                case ".rljson"://Lista
                                    var searchListVideoResponse = JsonConvert.DeserializeObject<PlaylistItemListResponse>(File.ReadAllText(ofd.FileName, System.Text.Encoding.UTF8));
                                    CargarResultados(searchListVideoResponse);
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("El archivo no tiene el formato correcto", "Error al abrir el archivo");
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(PathDirectorioResultadoBusqueda);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir el archivo");
            }
        }
        public async void ObtenerVideosDesdeIDCanal(string IdCanal)
        {
            try
            {
                var resultado = await YoutubeApi.GetCanalInfo(IdCanal);
                if (resultado != null && resultado.Items != null && resultado.Items.Count > 0)
                {
                    var listasRelacionadas = resultado.Items[0].ContentDetails.RelatedPlaylists;
                    var resultadoVideos = await YoutubeApi.GetPlaylistItems(listasRelacionadas.Uploads);
                    CargarResultados(resultadoVideos);
                    ResultadoListaVideos = JsonConvert.SerializeObject(resultadoVideos);
                }
                else
                {
                    MessageBox.Show("No se encontro el Canal", "Error al Obtener los Videos desde el IdCanal");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos desde el IdCanal");
            }
        }

        public async void ObtenerVideosDesdeNombreCanal(string NombreCanal)
        {
            try
            {
                var resultado = await YoutubeApi.GetCanalInfo(NombreCanal,false);
                if (resultado != null && resultado.Items != null && resultado.Items.Count > 0)
                {
                    var listasRelacionadas = resultado.Items[0].ContentDetails.RelatedPlaylists;
                    var resultadoVideos = await YoutubeApi.GetPlaylistItems(listasRelacionadas.Uploads);
                    CargarResultados(resultadoVideos);
                    ResultadoListaVideos = JsonConvert.SerializeObject(resultadoVideos);
                }
                else
                {
                    MessageBox.Show("No se encontro el Canal", "Error al Obtener los Videos desde el Nombre del Canal");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos desde el Nombre del Canal");
            }
        }

        private void SeleccionSimpleCKBX_CheckedChanged(object sender, EventArgs e)
        {
            CambiarVisibilidadBotonesObtenerVideos();
        }

        private void ObtenerInformacionBTN_Click(object sender, EventArgs e)
        {
            ///TODO Implementar Funcionalidad Obtener Informacion
        }

        private void CambiarVisibilidadBotonesObtenerVideos()
        {
            ObtenerVideosListaReprBTN.Visible = SeleccionSimpleCKBX.Checked && ItemResultadoSeleccionado != null && ItemResultadoSeleccionado.TipoResultado == TipoResultado.lista;
            ObtenerVideosCanalBTN.Visible = SeleccionSimpleCKBX.Checked && ItemResultadoSeleccionado != null && ItemResultadoSeleccionado.TipoResultado == TipoResultado.canal;
            ObtenerInformacionBTN.Visible = SeleccionSimpleCKBX.Checked && ItemResultadoSeleccionado != null;
        }
        private void panelyoutubeClick(object sender, EventArgs e)
        {
            try
            {
                if (SeleccionSimpleCKBX.Checked)
                {
                    foreach (var item in panelResultadoYoutubes.Where(x => x != ItemResultadoSeleccionado))
                    {
                        item.Deseleccionar();
                    }
                }
                CambiarVisibilidadBotonesObtenerVideos();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Descargar Videos Directamente

        private void DescargaDirecVideosBTN_Click(object sender, EventArgs e)
        {
            DescargarVideos(string.Join(" ", URLsDDVideosRTB.Lines), URLsDDVideosRTB.Lines.Count(), 'D');
        }

        #endregion



        
    }
}