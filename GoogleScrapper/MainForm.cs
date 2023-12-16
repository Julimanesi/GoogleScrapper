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
using static GoogleScrapper.DescargaVideoForm;
using System;
using System.DirectoryServices;

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
        private List<HistorialBusquedaItem> Historial { get; set; } = new List<HistorialBusquedaItem>();
        private int IndexHistorial { get; set; } = 0;
        public static string UltimoIdLista { get; set; } = string.Empty;
        private string ResultadoSiguientePagToken { get; set; } = "";
        private bool CargadoArchivo { get; set; } = false;
        private string NombreArchivoCargado { get; set; } = "";
        private List<HistorialItemSeleccionado> HistorialItemSeleccionados = new List<HistorialItemSeleccionado>();
        public static string Aviso { get; set; } = string.Empty;

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
            var Categorias = JsonConvert.DeserializeObject<Google.Apis.YouTube.v3.Data.VideoCategoryListResponse>(File.ReadAllText(@"C:\Users\julim\OneDrive\Documentos\Visual Studio 2022\Windows Form\MultimediaScrapper\GoogleScrapper\youtubeApiCategVideoUs.json"));
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
            //this.Icon = new Icon(@"C:\Users\julim\OneDrive\Documentos\Visual Studio 2022\Windows Form\MultimediaScrapper\GoogleScrapper\multimedia_player_16922.ico");
        }

        public void DescargarVideos(string listaVideos, int cant, char tipo, List<PanelYoutube>? panelesYoutube = null)
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
                DescargaVideoForm descargaVideoForm = new DescargaVideoForm(listaVideos, cant, folderDialog.SelectedPath, tipo, panelesYoutube);
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

        #region Eventos 

        private void AbrirResultadosBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(PathDirectorioResultadoBusqueda))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.InitialDirectory = PathDirectorioResultadoBusqueda;
                    ofd.Filter = "Todos los Archivos soportados(*.*json)|*.*json|Archivos de Resultado de Busqueda (*.rbjson)|*.rbjson|Archivos de Lista de Videos (*.rljson)|*.rljson"; //All files (*.*)|*.*";
                    ofd.Multiselect = true;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string file in ofd.FileNames)
                        {
                            var tipo = Path.GetExtension(file);
                            if (tipo != null)
                            {
                                CargadoArchivo = true;
                                NombreArchivoCargado = Path.GetFileNameWithoutExtension(file);
                                switch (tipo)
                                {
                                    case ".rbjson"://Busqueda
                                        var searchListResponse = JsonConvert.DeserializeObject<SearchListResponse>(File.ReadAllText(file, System.Text.Encoding.UTF8));
                                        CargarResultados(searchListResponse);
                                        break;
                                    case ".rljson"://Lista
                                        var searchListVideoResponse = JsonConvert.DeserializeObject<PlaylistItemListResponse>(File.ReadAllText(file, System.Text.Encoding.UTF8));
                                        CargarResultados(searchListVideoResponse);
                                        break;
                                }
                            }
                        }
                        //else
                        //{
                        //    MessageBox.Show("El archivo no tiene el formato correcto", "Error al abrir el archivo");
                        //}
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

        #region Eventos Buscar

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

        private void NombreCanalTXBX_TextChanged(object sender, EventArgs e)
        {
            ObtenerVideosNombreCanalBTN.Visible = NombreCanalTXBX.Text.Length > 0 && NombreCanalTXBX.Text != "" && NombreCanalTXBX.Text != " ";
        }

        private void ObtenerVideosNombreCanalBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (IDCanalTXBX.Text.Contains("@"))
                {
                    MessageBox.Show("La Url contiene el nombre de Usuario y no el nombre del Canal.", "Error al Obtener los Videos desde la Url del Canal");
                }
                else
                {
                    ObtenerVideosDesdeNombreCanal(NombreCanalTXBX.Text);
                }
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

        private void ObtenerVideosIDCanalBTN_Click(object sender, EventArgs e)
        {
            try
            {
                string IdCanal = "";
                if (IDCanalTXBX.Text.Contains("channel/"))
                {
                    IdCanal = IDCanalTXBX.Text.Split("channel/")[1];
                }
                else
                {
                    if (IDCanalTXBX.Text.Contains("@"))
                    {
                        MessageBox.Show("La Url contiene el nombre de Usuario y no la Id del Canal.", "Error al Obtener los Videos desde la Url del Canal");
                    }
                    else
                    {
                        IdCanal = IDCanalTXBX.Text;
                    }
                }
                ObtenerVideosDesdeIDCanal(IdCanal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos desde el IdCanal");
            }
        }

        private void IDListaReprodTXBX_TextChanged(object sender, EventArgs e)
        {
            ObtenerVideosIDListReprBTN.Visible = IDListaReprodTXBX.Text.Length > 0 && IDListaReprodTXBX.Text != "" && IDListaReprodTXBX.Text != " ";
        }

        private void ObtenerVideosIDListReprBTN_Click(object sender, EventArgs e)
        {
            try
            {
                string IdlistaRep = IDListaReprodTXBX.Text;
                if (IdlistaRep.Contains("list="))
                {
                    IdlistaRep = IdlistaRep.Substring(IdlistaRep.IndexOf("list=") + 5);
                    if(IdlistaRep.Contains('&'))
                        IdlistaRep = IdlistaRep.Remove(IdlistaRep.IndexOf('&'));
                }
                else
                {
                    IdlistaRep = IDListaReprodTXBX.Text;
                }
                ObtenerVideosDesdeIDListaReproduccion(IdlistaRep);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos desde el IdListaReproduccion");
            }
        }

        #endregion

        #region Resultados botonera

        #region Obtener 

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
                if (ItemResultadoSeleccionado != null)
                {
                    if (ItemResultadoSeleccionado.TipoResultado == TipoResultado.canal)
                    {
                        ObtenerVideosDesdeIDCanal(ItemResultadoSeleccionado.ID);
                    }
                    else
                    {
                        ObtenerVideosDesdeIDCanal(ItemResultadoSeleccionado.IDCanalPropietario);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos del Canal");
            }
        }

        private async void ObtenerInformacionBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemResultadoSeleccionado != null)
                {
                    InfoYoutubeForm infoYoutubeForm = new InfoYoutubeForm(InfoYoutubeForm.TipoInfo.video);
                    switch (ItemResultadoSeleccionado.TipoResultado)
                    {
                        case TipoResultado.video:
                            var video = await YoutubeApi.GetVideoInfo(ItemResultadoSeleccionado.ID);
                            infoYoutubeForm = new InfoYoutubeForm(InfoYoutubeForm.TipoInfo.video, video);
                            break;
                        case TipoResultado.canal:
                            var canal = await YoutubeApi.GetCanalInfo(ItemResultadoSeleccionado.ID);
                            infoYoutubeForm = new InfoYoutubeForm(InfoYoutubeForm.TipoInfo.canal, null, canal);
                            break;
                        case TipoResultado.lista:
                            var lista = await YoutubeApi.GetPlayListInfo(ItemResultadoSeleccionado.ID);
                            infoYoutubeForm = new InfoYoutubeForm(InfoYoutubeForm.TipoInfo.Lista, null, null, lista);
                            break;
                    }
                    infoYoutubeForm.Activate();
                    infoYoutubeForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener la informacion del elemento.");
            }
        }

        private void VolverCargarApiBTN_Click(object sender, EventArgs e)
        {
            //Al presionar vuelve a cargar desde Api los datos ya cargados 
            if (Historial.Any())
            {
                var actual = Historial.ElementAt(IndexHistorial);
                if (actual != null && actual.Tipo == TipoRespuestaBusqYTApi.ListaVideos)
                {
                    var Id = actual?.InformacionLista?.Id;
                    if (Id != null)
                    {
                        ObtenerVideosDesdeIDListaReproduccion(Id);
                        Historial.Remove(actual);
                        IndexHistorial = Historial.Count - 1;
                    }
                }
            }
        }

        #endregion

        #region seleccion y Smplayer

        private void SeleccionSimpleCKBX_CheckedChanged(object sender, EventArgs e)
        {
            CambiarVisibilidadBotonesObtenerVideos();
        }

        private void AgregarEnviarSMplayerYoutbBTN_Click(object sender, EventArgs e)
        {
            EjecucionProcesos.EnviarListaReproducASMPlayer(string.Join(" ", ObtenerPanelesSeleccionados().Select(x => x.Link)));
        }

        private void DescargVideosYouTbBTN_Click(object sender, EventArgs e)
        {
            try
            {
                List<PanelYoutube> PanelesADescargar = ObtenerPanelesSeleccionados();
                var listaLinks = PanelesADescargar.Select(x => x.Link);
                DescargarVideos(string.Join(" ", listaLinks), listaLinks.Count(), 'Y', PanelesADescargar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Descargar los Videos");
            }
        }

        private void SeleccionarTodosYouTubeBTN_Click(object sender, EventArgs e)
        {
            SeleccionarTodos();
        }

        private void SeleccionarTodos()
        {
            foreach (PanelYoutube panelResultado in panelResultadoYoutubes)
            {
                panelResultado.seleccionado = TodosPanelesYoutubeSeleccionados;
                panelResultado.OnSeleccionado();
                AgregarItemSelecAHistorialDesdePanelYT(panelResultado);
            }
            TodosPanelesYoutubeSeleccionados = !TodosPanelesYoutubeSeleccionados;
            CambiarLabelSeleccionarTodos(TodosPanelesYoutubeSeleccionados);
        }

        private void CambiarLabelSeleccionarTodos(bool todosSeleccionados)
        {
            if (todosSeleccionados)
            {
                SeleccionarTodosYouTubeBTN.Text = "Deseleccionar todos";
            }
            else
            {
                SeleccionarTodosYouTubeBTN.Text = "Seleccionar todos";
            }
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
                AgregarItemSelecAHistorialDesdePanelYT(ItemResultadoSeleccionado);
                AvisoYTPanelLB.Text = Aviso;
            }
            catch (Exception ex)
            {
            }
        }

        private void MantenerSeleccionesCKBX_CheckedChanged(object sender, EventArgs e)
        {
            if (MantenerSeleccionesCKBX.Checked)
            {

            }
            else
            {
                HistorialItemSeleccionados.Clear();
            }
        }

        #endregion

        #region Navegacion

        private async void SiguientePaginaYTResultBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (ResultadoSiguientePagToken != null && UltimoIdLista != string.Empty)
                {
                    CargarResultados(await YoutubeApi.GetPlaylistItems(UltimoIdLista, ResultadoSiguientePagToken));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al obtener la siguiente pagina del resultado");
            }
        }

        private void AdelanteYTBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (Historial.Any() && IndexHistorial < Historial.Count - 1)
                {
                    IndexHistorial++;
                    CargarDesdeHistorial();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AtrasYTBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (Historial.Any() && IndexHistorial > 0)
                {
                    IndexHistorial--;
                    CargarDesdeHistorial();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void PrimeroYTBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (Historial.Any())
                {
                    IndexHistorial = 0;
                    CargarDesdeHistorial();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UltimoYTBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (Historial.Any())
                {
                    IndexHistorial = Historial.Count - 1;
                    CargarDesdeHistorial();
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        private void GuardarResultadosBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(PathDirectorioResultadoBusqueda))
                {
                    Directory.CreateDirectory(PathDirectorioResultadoBusqueda);
                }
                if (Historial.Any())
                {
                    var actual = Historial.ElementAt(IndexHistorial);
                    if (actual != null)
                    {
                        string nombreCompleto = "";
                        string nombreArchivo = NombreArchivoUltResultTXBX.Text;
                        if (actual.Tipo == TipoRespuestaBusqYTApi.Busqueda)
                        {
                            if (nombreArchivo == "")
                            {
                                nombreArchivo = $"Busqueda_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}";
                            }
                            nombreCompleto = Path.Combine(PathDirectorioResultadoBusqueda, $"{nombreArchivo}.rbjson");
                            if (File.Exists(nombreCompleto))
                            {
                                nombreCompleto = Path.Combine(PathDirectorioResultadoBusqueda, $"{nombreArchivo}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.rbjson");
                            }
                            System.IO.File.WriteAllText(nombreCompleto, JsonConvert.SerializeObject(actual.ListaBusquedaRespuesta), System.Text.Encoding.UTF8);
                        }
                        else
                        {
                            if (nombreArchivo == "")
                            {
                                nombreArchivo = $"Lista_Videos_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}";
                            }
                            nombreCompleto = Path.Combine(PathDirectorioResultadoBusqueda, $"{nombreArchivo}.rljson");
                            if (File.Exists(nombreCompleto))
                            {
                                nombreCompleto = Path.Combine(PathDirectorioResultadoBusqueda, $"{nombreArchivo}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.rljson");
                            }
                            System.IO.File.WriteAllText(nombreCompleto, JsonConvert.SerializeObject(actual.ResultadoListaVideos), System.Text.Encoding.UTF8);
                        }
                        NombreArchivoGuardResultYTLB.Text = "Guardado en: " + Path.GetFileNameWithoutExtension(nombreCompleto);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Guardar los Resultados");
            }
        }

        #region Filtrar

        private void FiltrarBusquedaTituloYTTBX_TextChanged(object sender, EventArgs e)
        {
            CargarDesdeHistorial();
        }

        private void FiltrarBusquedaCanalYTTBX_TextChanged(object sender, EventArgs e)
        {
            CargarDesdeHistorial();
        }

        #endregion

        #endregion

        #endregion

        #region funciones Cargar Resultados

        public void CargarResultados(SearchListResponse? searchListResponse, bool guardarEnHistorial = true)
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

                NombreArchivoUltResultTXBX.Text = BuscarVideoBTN.Text;
                CambiarVisibilidadBotonesObtenerVideos();
                if (guardarEnHistorial)
                {
                    Historial.Add(new HistorialBusquedaItem(null, searchListResponse, TipoRespuestaBusqYTApi.Busqueda));
                    IndexHistorial = Historial.Count - 1;
                }
                ResultadoSiguientePagToken = "";
                UltimoIdLista = "";
                SiguientePaginaYTResultBTN.Visible = false;
                AvisoYTPanelLB.Text = "";
                VolverCargarApiBTN.Visible = false;
            }
        }

        public async void CargarResultados(PlaylistItemListResponse? searchListResponse, bool guardarEnHistorial = true)
        {
            if (searchListResponse != null && searchListResponse.Items.Any())
            {
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
                int nroPagTotales = GetNroNroPaginaTotales(searchListResponse);
                CambiarVisibilidadBotonesObtenerVideos();
                ResultadoSiguientePagToken = searchListResponse.NextPageToken;
                UltimoIdLista = searchListResponse.Items[0].Snippet.PlaylistId;
                if (guardarEnHistorial)
                {
                    var historialItem = new HistorialBusquedaItem(searchListResponse, null, TipoRespuestaBusqYTApi.ListaVideos);

                    if (searchListResponse.PrevPageToken != null)
                    {
                        historialItem.NroPagina = GetNroPagina(searchListResponse);
                    }
                    //Obtener el nombre de la lista con la pagina como nombre de archivo
                    #region Obtener nombre de archivo
                    if (searchListResponse.PrevPageToken == null && !CargadoArchivo || (CargadoArchivo && ObtenerDatosDeArchivoCKBX.Checked))
                    {
                        historialItem.InformacionLista = await YoutubeApi.GetPlayListInfo(UltimoIdLista);
                    }
                    else if (!CargadoArchivo)
                    {   //Si el resultado es la siguiente pagina de una lista y no fue cargado de un archivo
                        //no poseo el nombre por lo que guardo la info de la lista obtenida anteriormente para obtener el nombre
                        var actual = Historial.ElementAt(IndexHistorial);
                        if (actual != null)
                        {
                            historialItem.InformacionLista = actual.InformacionLista;
                        }
                    }

                    if (CargadoArchivo && !ObtenerDatosDeArchivoCKBX.Checked)
                    {
                        historialItem.NombreArchivo = NombreArchivoCargado;
                    }
                    else
                    {
                        historialItem.NombreArchivo = GetNombreArchivoPlayList(historialItem);
                    }

                    NombreArchivoUltResultTXBX.Text = historialItem.NombreArchivo ?? "";
                    #endregion

                    Historial.Add(historialItem);
                    IndexHistorial = Historial.Count - 1;

                    if (CargadoArchivo)
                    {
                        NombreArchivoCargado = "";
                        CargadoArchivo = false;
                    }
                    PagContYTLabel.Text = $"Pag {historialItem.NroPagina}/{nroPagTotales}";
                    if (historialItem.InformacionLista != null)
                    {
                        VolverCargarApiBTN.Visible = true;
                    }
                }
                SiguientePaginaYTResultBTN.Visible = true;
                AvisoYTPanelLB.Text = "";
            }
        }

        private void RenderizarResultados(int CantColum = 3)
        {
            int ancho = (ResultadosYouTubeFlowLayPanel.Width - (ResultadosYouTubeFlowLayPanel.Margin.Horizontal * CantColum + 21)) / CantColum;
            foreach (PanelYoutube panelResultado in panelResultadoYoutubes)
            {
                panelResultado.VolverARenderizar(ancho, (int)(ancho / NroAureo));
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

        private void CargarDesdeHistorial()
        {
            var actual = Historial.ElementAt(IndexHistorial);
            int nroItems = 0;
            if (actual != null)
            {
                if (FiltrarBusquedaTituloYTTBX.Text == "" && FiltrarBusquedaCanalYTTBX.Text == "")
                {
                    if (actual.Tipo == TipoRespuestaBusqYTApi.Busqueda)
                    {
                        nroItems = actual.ListaBusquedaRespuesta?.Items.Count ?? 0;
                        CargarResultados(actual.ListaBusquedaRespuesta, false);
                    }
                    else
                    {
                        nroItems = actual.ResultadoListaVideos?.Items.Count ?? 0;
                        CargarResultados(actual.ResultadoListaVideos, false);
                        NombreArchivoUltResultTXBX.Text = actual.NombreArchivo;
                        if (actual.ResultadoListaVideos != null)
                        {
                            int nroPagTotales = GetNroNroPaginaTotales(actual.ResultadoListaVideos);
                            PagContYTLabel.Text = $"Pag {actual.NroPagina}/{nroPagTotales}";
                        }
                    }
                }
                else
                {
                    FiltrarResultados(actual, FiltrarBusquedaTituloYTTBX.Text, FiltrarBusquedaCanalYTTBX.Text);
                }
                //TODO ver que pasa cuando uno carga por primera vez
                //Mantiene las selecciones de las otras paginas y si se presiono el boton seleccionar Todos tambien
                if (MantenerSeleccionesCKBX.Checked && !SeleccionSimpleCKBX.Checked)
                {
                    var panelesSeleccionados = ObtenerPanelesSeleccionadosMantenidosIndexPagina(IndexHistorial);
                    panelesSeleccionados.ForEach(x => x.OnSeleccionado());
                    TodosPanelesYoutubeSeleccionados = panelesSeleccionados.Count == nroItems;
                    CambiarLabelSeleccionarTodos(TodosPanelesYoutubeSeleccionados);
                }
                else
                {
                    TodosPanelesYoutubeSeleccionados = false;
                    CambiarLabelSeleccionarTodos(TodosPanelesYoutubeSeleccionados);
                }
            }
        }

        #endregion

        #region funciones Obtener Videos

        public async void ObtenerVideosDesdeIDCanal(string IdCanal)
        {
            try
            {
                var resultado = await YoutubeApi.GetCanalesInfo(IdCanal);
                if (resultado != null && resultado.Items != null && resultado.Items.Count > 0)
                {
                    var listasRelacionadas = resultado.Items[0].ContentDetails.RelatedPlaylists;
                    var resultadoVideos = await YoutubeApi.GetPlaylistItems(listasRelacionadas.Uploads);
                    CargarResultados(resultadoVideos);
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
                var resultado = await YoutubeApi.GetCanalesInfo(NombreCanal, false);
                if (resultado != null && resultado.Items != null && resultado.Items.Count > 0)
                {
                    var listasRelacionadas = resultado.Items[0].ContentDetails.RelatedPlaylists;
                    var resultadoVideos = await YoutubeApi.GetPlaylistItems(listasRelacionadas.Uploads);
                    CargarResultados(resultadoVideos);
                    NombreArchivoUltResultTXBX.Text = NombreCanal;
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

        public async void ObtenerVideosDesdeIDListaReproduccion(string IdListaRep)
        {
            try
            {
                try
                {
                    var resultado = await YoutubeApi.GetPlaylistItems(IdListaRep);
                    if (resultado != null)
                    {
                        CargarResultados(resultado);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al Obtener los Videos de la Lista de Reproduccion");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener los Videos desde el IdCanal");
            }
        }

        private void CambiarVisibilidadBotonesObtenerVideos()
        {
            ObtenerVideosListaReprBTN.Visible = SeleccionSimpleCKBX.Checked && ItemResultadoSeleccionado != null && ItemResultadoSeleccionado.TipoResultado == TipoResultado.lista;
            ObtenerVideosCanalBTN.Visible = SeleccionSimpleCKBX.Checked && ItemResultadoSeleccionado != null && ItemResultadoSeleccionado.IDCanalPropietario != null && ItemResultadoSeleccionado.IDCanalPropietario != "";
            ObtenerInformacionBTN.Visible = SeleccionSimpleCKBX.Checked && ItemResultadoSeleccionado != null;
            MantenerSeleccionesCKBX.Visible = !SeleccionSimpleCKBX.Checked;
        }

        #endregion

        #region funciones auxiliares

        private string GetNombreArchivoPlayList(HistorialBusquedaItem historialItem)
        {
            try
            {
                return historialItem.InformacionLista != null ? $"{historialItem.InformacionLista.Snippet.Title}_Pag_{historialItem.NroPagina}" : "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private int GetNroPagina(PlaylistItemListResponse searchListResponse)
        {
            try
            {
                return (int)((searchListResponse.Items[0].Snippet.Position ?? 0 + 1) / (searchListResponse.PageInfo.ResultsPerPage ?? 1)) + 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        private int GetNroNroPaginaTotales(PlaylistItemListResponse searchListResponse)
        {
            try
            {
                return (int)Math.Ceiling((decimal)(searchListResponse.PageInfo.TotalResults ?? 0) / (searchListResponse.PageInfo.ResultsPerPage ?? 1));

            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        private void FiltrarResultados(HistorialBusquedaItem actual, string Titulo, string NombreCanal)
        {
            try
            {
                if (actual != null)
                {
                    if (actual.Tipo == TipoRespuestaBusqYTApi.Busqueda)
                    {
                        var listaFiltrada = new SearchListResponse();
                        listaFiltrada.ETag = actual.ListaBusquedaRespuesta.ETag;
                        listaFiltrada.TokenPagination = actual.ListaBusquedaRespuesta.TokenPagination;
                        listaFiltrada.NextPageToken = actual.ListaBusquedaRespuesta.NextPageToken;
                        listaFiltrada.PrevPageToken = actual.ListaBusquedaRespuesta.PrevPageToken;
                        listaFiltrada.Items = actual.ListaBusquedaRespuesta.Items.Where(x => (Titulo != "" ? x.Snippet.Title.Contains(Titulo, StringComparison.InvariantCultureIgnoreCase) : true) && (NombreCanal != "" ? x.Snippet.ChannelTitle.Contains(NombreCanal, StringComparison.InvariantCultureIgnoreCase) : true)).ToList();
                        listaFiltrada.EventId = actual.ListaBusquedaRespuesta.EventId;
                        listaFiltrada.Kind = actual.ListaBusquedaRespuesta.Kind;
                        listaFiltrada.PageInfo = actual.ListaBusquedaRespuesta.PageInfo;
                        listaFiltrada.RegionCode = actual.ListaBusquedaRespuesta.RegionCode;
                        listaFiltrada.VisitorId = actual.ListaBusquedaRespuesta.VisitorId;
                        if (listaFiltrada.Items.Any())
                        {
                            CargarResultados(listaFiltrada, false);
                        }
                        else
                        {
                            ItemResultadoSeleccionado = null;
                            panelResultadoYoutubes.Clear();
                            ResultadosYouTubeFlowLayPanel.Controls.Clear();
                        }
                    }
                    else
                    {
                        var listaFiltrada = new PlaylistItemListResponse();
                        listaFiltrada.ETag = actual.ResultadoListaVideos.ETag;
                        listaFiltrada.TokenPagination = actual.ResultadoListaVideos.TokenPagination;
                        listaFiltrada.NextPageToken = actual.ResultadoListaVideos.NextPageToken;
                        listaFiltrada.PrevPageToken = actual.ResultadoListaVideos.PrevPageToken;
                        listaFiltrada.Items = actual.ResultadoListaVideos.Items.Where(x => (Titulo != "" ? x.Snippet.Title.Contains(Titulo, StringComparison.InvariantCultureIgnoreCase) : true) && (NombreCanal != "" ? x.Snippet.VideoOwnerChannelTitle.Contains(NombreCanal, StringComparison.InvariantCultureIgnoreCase) : true)).ToList();
                        listaFiltrada.EventId = actual.ResultadoListaVideos.EventId;
                        listaFiltrada.Kind = actual.ResultadoListaVideos.Kind;
                        listaFiltrada.PageInfo = actual.ResultadoListaVideos.PageInfo;
                        listaFiltrada.VisitorId = actual.ResultadoListaVideos.VisitorId;
                        if (listaFiltrada.Items.Any())
                        {
                            CargarResultados(listaFiltrada, false);
                        }
                        else
                        {
                            ItemResultadoSeleccionado = null;
                            panelResultadoYoutubes.Clear();
                            ResultadosYouTubeFlowLayPanel.Controls.Clear();
                        }
                        NombreArchivoUltResultTXBX.Text = actual.NombreArchivo;
                        if (actual.ResultadoListaVideos != null)
                        {
                            int nroPagTotales = GetNroNroPaginaTotales(actual.ResultadoListaVideos);
                            PagContYTLabel.Text = $"Pag {actual.NroPagina}/{nroPagTotales}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AgregarItemSelecAHistorialDesdePanelYT(PanelYoutube? panelYoutube)
        {
            if (panelYoutube != null)
            {
                var histItemAux = HistorialItemSeleccionados.FirstOrDefault(x => x.IndexHistorial == IndexHistorial && x.ID == panelYoutube.ID && x.TipoResultado == panelYoutube.TipoResultado);
                if (histItemAux == null)
                {
                    HistorialItemSeleccionados.Add(new HistorialItemSeleccionado()
                    {
                        ID = panelYoutube.ID,
                        IndexHistorial = IndexHistorial,
                        TipoResultado = panelYoutube.TipoResultado
                    });
                }
                else
                {
                    HistorialItemSeleccionados.Remove(histItemAux);
                }
            }
        }

        private List<PanelYoutube> ObtenerPanelesSeleccionadosMantenidosIndexPagina(int indexHistorial, List<PanelYoutube>? panelYoutubesAux = null)
        {
            var historialItemSeleccionadosAux = HistorialItemSeleccionados.Where(x => x.IndexHistorial == indexHistorial).ToList();
            return (panelYoutubesAux ?? panelResultadoYoutubes).Where(x => historialItemSeleccionadosAux.Any(y => y.ID == x.ID) && historialItemSeleccionadosAux.Any(y => y.TipoResultado == x.TipoResultado)).ToList();
        }

        private List<PanelYoutube> ObtenerPanelesSeleccionados()
        {
            List<PanelYoutube> PanelesADescargar = new List<PanelYoutube>();
            try
            {
                //Si Mantengo las selecciones debo descargar los videos seleccionados en varias paginas
                if (MantenerSeleccionesCKBX.Checked)
                {   //Variable auxiliar que almacena todos los Paneles del Historial
                    List<PanelYoutube> PanelesAux = new List<PanelYoutube>();
                    foreach (var searchResult in Historial.SelectMany(x => x?.ResultadoListaVideos?.Items ?? new List<PlaylistItem>()))
                    {
                        try
                        {
                            PanelesAux.Add(new PanelYoutube(1, 1, searchResult, panelyoutubeClick));
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    //De todos los paneles filtro por los que fueron seleccionados en varias paginas
                    for (int i = 0; i < Historial.Count; i++)
                    {
                        PanelesADescargar.AddRange(ObtenerPanelesSeleccionadosMantenidosIndexPagina(i, PanelesAux));
                    }
                }
                else
                {
                    //Sino simplemente los paneles de la pagina actual que fueron seleccionados
                    PanelesADescargar = panelResultadoYoutubes.Where(j => j.seleccionado).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener Paneles Seleccionados");
            }
            return PanelesADescargar;
        }

        #endregion

        #endregion

        #region Descargar/Editar Videos Directamente

        private void DescargaDirecVideosBTN_Click(object sender, EventArgs e)
        {
            DescargarVideos(string.Join(" ", URLsDDVideosRTB.Lines), URLsDDVideosRTB.Lines.Count(), 'D');
        }

        private void ComprimirVideosBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Video Files (*.avi, *.mp4, *.mov, *.mkv, *.flv, *.wmv, *.rm, *.3gp, *.ogv, *.webm)|*.avi;*.mp4;*.mov;*.mkv;*.flv;*.wmv;*.rm;*.3gp;*.ogv;*.webm;";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            List<Destino> Destinos = new List<Destino>();

            foreach (string file in ofd.FileNames)
            {
                Destinos.Add(new Destino()
                {
                    DireccionArchivo = file,
                    Titulo = Path.GetFileNameWithoutExtension(file),
                });
            }

            EdicionVideosForm edicionVideosForm = new EdicionVideosForm(Destinos, EdicionVideosForm.TipoEdicion.comprimir);

            edicionVideosForm.Activate();
            edicionVideosForm.Show();
        }

        private void AgregarThumbnailsBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Musica(*.mp3)|*.mp3|Video Files (*.avi, *.mp4, *.mov, *.mkv, *.flv, *.wmv, *.rm, *.3gp, *.ogv, *.webm)|*.avi;*.mp4;*.mov;*.mkv;*.flv;*.wmv;*.rm;*.3gp;*.ogv;*.webm;";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            List<Destino> Destinos = new List<Destino>();

            Destinos.Add(new Destino()
            {
                DireccionArchivo = ofd.FileName,
                Titulo = Path.GetFileNameWithoutExtension(ofd.FileName),
            });

            EdicionVideosForm edicionVideosForm = new EdicionVideosForm(Destinos, EdicionVideosForm.TipoEdicion.Thumbnails);

            edicionVideosForm.Activate();
            edicionVideosForm.Show();
        }

        private async void ObtenerInfoDirecYTVideoBTN_Click(object sender, EventArgs e)
        {
            try
            {
                InfoYoutubeForm infoYoutubeForm = new InfoYoutubeForm(InfoYoutubeForm.TipoInfo.video);
                var video = await YoutubeApi.GetVideoInfo(YoutubeApi.ParsearIdDesdeURLVideo(URLDDVideoTXBX.Text));
                infoYoutubeForm = new InfoYoutubeForm(InfoYoutubeForm.TipoInfo.video, video);
                infoYoutubeForm.Activate();
                infoYoutubeForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Obtener la Info del Video");
            }
        }

        #endregion
 
    }
    public class HistorialBusquedaItem
    {
        public PlaylistItemListResponse? ResultadoListaVideos { get; set; }
        public SearchListResponse? ListaBusquedaRespuesta { get; set; }
        public TipoRespuestaBusqYTApi Tipo { get; set; }
        public Playlist? InformacionLista { get; set; } = null;
        public int NroPagina { get; set; } = 1;
        public string? NombreArchivo { get; set; }

        public HistorialBusquedaItem(PlaylistItemListResponse? resultadoListaVideos, SearchListResponse? listaBusquedaRespuesta, TipoRespuestaBusqYTApi tipo)
        {
            ResultadoListaVideos = resultadoListaVideos;
            ListaBusquedaRespuesta = listaBusquedaRespuesta;
            Tipo = tipo;
        }
    }
    public enum TipoRespuestaBusqYTApi { Busqueda, ListaVideos }

    public class HistorialItemSeleccionado
    {
        public int IndexHistorial { get; set; } = 1;
        public string ID { get; set; } = "";
        public TipoResultado TipoResultado { get; set; }
    }
}