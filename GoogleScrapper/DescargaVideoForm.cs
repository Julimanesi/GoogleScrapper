using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using Google.Apis.YouTube.v3.Data;
using Microsoft.VisualBasic.Devices;

namespace GoogleScrapper
{
    public partial class DescargaVideoForm : Form
    {
        private string ListaUrls { get; set; } = "";
        private int Total { get; set; }
        private int CantActualDesc { get; set; } = 0;
        private string Directoy { get; set; } = "";
        private bool MostrandoDetalles = false;
        public static string ComandoComprimirVideo { get; } = "--postprocessor-args \"-c:v h264_nvenc -preset:v p7 -tune:v hq -rc:v vbr -cq:v 32 -b:v 0 -profile:v high\"";
        private BackgroundWorker BWDescargaVideo = new BackgroundWorker();
        private BackgroundWorker BWAgregarThumbnails = new BackgroundWorker();
        private List<Destino> Destinos = new List<Destino>();
        private List<PanelYoutube>? PanelesYoutube { get; set; } = null;
        private string IDActual { get; set; } = "";
        private char Tipo { get; set; } = 'Y';

        public DescargaVideoForm(string ListaUrls, int count, string Directoy, char tipo, List<PanelYoutube>? panelesYoutube = null)
        {
            InitializeComponent();
            this.ListaUrls = ListaUrls;
            this.Total = count;
            this.Directoy = Directoy;
            Tipo = tipo;
            SalidaRTextBox.Text = "";
            VideosDescargadosLabel.Text = $"Videos Descargados: 0/{Total}";
            PanelesYoutube = panelesYoutube;
            BWDescargaVideo.WorkerReportsProgress = true;
            BWDescargaVideo.WorkerSupportsCancellation = true;
            BWDescargaVideo.DoWork += new DoWorkEventHandler(BWDescargaVideo_Dowork);
            BWDescargaVideo.ProgressChanged += new ProgressChangedEventHandler(BWDescargaVideo_Progreso);
            BWDescargaVideo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWDescargaVideo_Resultado);
            BWAgregarThumbnails.WorkerReportsProgress = true;
            BWAgregarThumbnails.WorkerSupportsCancellation = true;
            BWAgregarThumbnails.DoWork += new DoWorkEventHandler(BWAgregarThumbnails_Dowork);
            BWAgregarThumbnails.ProgressChanged += new ProgressChangedEventHandler(BWAgregarThumbnails_Progreso);
            BWAgregarThumbnails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWAgregarThumbnails_Resultado);
        }

        public void DescargarListaReproduc()
        {
            if (!BWDescargaVideo.IsBusy)
            {
                BWDescargaVideo.RunWorkerAsync();
            }
        }

        private void DescargaVideoForm_Load(object sender, EventArgs e)
        {

        }

        private void BWDescargaVideo_Dowork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                EjecucionProcesos.DescargarVideos(worker, e, SoloAudioCKBX.Checked, ComprimirVideoCKBX.Checked, Directoy, ListaUrls, AgregarThumbnailCKBX.Checked, ObtenerDatosMusicaCKBX.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Descargar");
            }
        }

        private void BWDescargaVideo_Progreso(object sender, ProgressChangedEventArgs e)
        {
            ProgresoDescargaPB.Value = e.ProgressPercentage < 100 ? e.ProgressPercentage : 100;
            if (e.UserState != null)
            {
                string salida = (string)e.UserState;
                ObtenerDestinoVideo(salida);
                ObtenerEstadoDescarga(salida);
                ObtenerDetallesProgresoDescarga(salida);
                ObtenerDestinoMusica(salida);
                ObtenerIdRexgex(salida);
                VideosDescargadosLabel.Text = $"Videos Descargados: {CantActualDesc}/{Total}";
            }
        }

        private void BWDescargaVideo_Resultado(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                }
                if (e.Result != null)
                {
                    var procesoOutput = (string)e.Result;
                    string algo = SoloAudioCKBX.Checked ? "Audio" : "Video";
                    if (procesoOutput != null)
                    {
                        if (procesoOutput.Contains("ERROR", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Estadolabel.Text = "Estado: Error al Descargar:" + procesoOutput.Substring(procesoOutput.IndexOf("Error", StringComparison.InvariantCultureIgnoreCase));
                        }
                        else
                        {
                            Estadolabel.Text = $"Estado: {algo} Descargado!";
                        }
                    }
                    else
                    {
                        Estadolabel.Text = $"Estado: {algo} Descargado!";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Descargar la Lista de Reproducion");
            }
            finally
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                worker.CancelAsync();
                AgregarThumbnailsAResultados();
            }
        }

        private void ObtenerDestinoVideo(string salida)
        {
            string pattern = @"\[download\]\s+Destination:\s+(?<destino>.*)";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(salida);
            if (matches.Count > 0)
            {
                string destino = matches[0].Groups["destino"].Value;
                DestinoLabel.Text = "Destino: " + destino;
            }
        }
        private void ObtenerEstadoDescarga(string salida)
        {
            string pattern = @"\[(?<estado>\w+)\](?<descripcion>.*)";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(salida);
            if (matches.Count > 0)
            {
                Estadolabel.Text = "Estado: " + parsearEstado(matches[0].Groups["estado"].Value, matches[0].Groups["descripcion"].Value);
            }
            else if (salida.Contains("Agregando Thumbnails"))
            {
                Estadolabel.Text = salida;
            }
        }

        private void ObtenerDetallesProgresoDescarga(string salida)
        {
            try
            {
                string pattern = @"\[download\]\s+(?<porcentaje>.*)\s*of\s+~?\s(?<tamanioact>.*)\sat\s+(?<veloc>.*)\sETA\s(?<tiempofalt>.*)(\s\()?";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                MatchCollection matches = rgx.Matches(salida);
                if (matches.Count > 0)
                {
                    string porcentaje = matches[0].Groups["porcentaje"].Value;
                    string tamanioact = matches[0].Groups["tamanioact"].Value;
                    string velocDesc = matches[0].Groups["veloc"].Value;
                    string tiempofalt = matches[0].Groups["tiempofalt"].Value;

                    DetalleDescargaLabel.Text = $"Detalle de la Descarga:\n            Porcentaje: {porcentaje}. Tamaño actual: {tamanioact}.\n           Velocidad de descarga: {velocDesc}. Tiempo faltante estimado: {tiempofalt}.";
                    List<string> salidaLineas = SalidaRTextBox.Lines.ToList();
                    int indexremover = salidaLineas.Count - 2;
                    if (indexremover > 2)
                    {
                        salidaLineas.RemoveAt(indexremover);
                        SalidaRTextBox.Lines = salidaLineas.ToArray();
                    }
                }
                SalidaRTextBox.Text += salida + "\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Descargar la Lista de Reproducion");
            }
        }

        private string parsearEstado(string estado, string Descripcion = "")
        {
            switch (estado)
            {
                case "download":
                    estado = "Descargando";
                    break;
                case "info":
                    estado = "Obteniendo Información";
                    break;
                case "Metadata":
                    estado = "Agregando metadatos";
                    if (!Estadolabel.Text.Contains("metadatos"))
                    {
                        CantActualDesc++;
                        int prog = (int)(((decimal)CantActualDesc / Total) * 100);
                        ProgresoTotalPB.Value = prog <= 100 ? prog : 100;
                    }
                    break;
                default:
                    estado = Descripcion;
                    break;
            }
            return estado;
        }

        private void MostrarDetallesBTN_Click(object sender, EventArgs e)
        {

            SalidaRTextBox.Visible = !MostrandoDetalles;
            if (SalidaRTextBox.Visible)
            {
                this.SetBounds(this.Location.X, this.Location.Y, 796, 556);
            }
            else
            {
                this.SetBounds(this.Location.X, this.Location.Y, 796, 405);
            }
            MostrandoDetalles = !MostrandoDetalles;
        }

        private void DescargarBTN_Click(object sender, EventArgs e)
        {
            DescargarListaReproduc();
        }

        private void SoloAudioCKBX_CheckedChanged(object sender, EventArgs e)
        {
            ObtenerDatosMusicaCKBX.Visible = SoloAudioCKBX.Checked;
            ComprimirVideoCKBX.Visible = !SoloAudioCKBX.Checked;
        }

        private void ObtenerDatosMusicaCKBX_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void ObtenerDestinoMusica(string salida)
        {
            string pattern = @"\[ExtractAudio\]\s+Destination:\s+(?<destino>.*)";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(salida);
            if (matches.Count > 0)
            {
                string destino = matches[0].Groups["destino"].Value;
                //significa que no encontro el nombre
                if (destino.Contains("\\.mp3"))
                {
                    if (PanelesYoutube != null)
                    {
                        var panel = PanelesYoutube.Where(x => x.ID == IDActual).FirstOrDefault();
                        if (panel != null)
                        {
                            string? basePath = Path.GetDirectoryName(destino);
                            destino = panel.ResultadoListaItem != null ? panel.ResultadoListaItem.Snippet.Title : destino;
                            destino = Path.Combine(basePath ?? "", destino + ".mp3");
                        }
                    }
                }
                DestinoLabel.Text = "Destino: " + destino;
            }
        }

        private async void AgregarThumbnailsAResultados()
        {
            if (AgregarThumbnailCKBX.Checked)
            {
                if (Tipo == 'Y' && PanelesYoutube != null)
                {
                    foreach (var panel in PanelesYoutube)
                    {
                        string titulo = panel.ResultadoListaItem != null ? panel.ResultadoListaItem.Snippet.Title : (panel.ResultadoBusqueda != null ? panel.ResultadoBusqueda.Snippet.Title : "");
                        string uRLThumbnail = panel.ResultadoListaItem != null ? panel.ResultadoListaItem.Snippet.Thumbnails.High.Url : (panel.ResultadoBusqueda != null ? panel.ResultadoBusqueda.Snippet.Thumbnails.High.Url : "");
                        titulo = titulo.Trim();
                        Destinos.Add(new Destino()
                        {
                            IdVideo = panel.ID,
                            Titulo = titulo,
                            DireccionArchivo = Path.Combine(Directoy, titulo + ".mp3"),
                            URLThumbnail = uRLThumbnail
                        });
                    }
                }
                else if (Tipo == 'D')
                {
                    List<string> listaUrl = ListaUrls.Split(' ').ToList();
                    foreach (var url in listaUrl)
                    {
                        if (url.Contains("youtube.com") && url.Contains("="))
                        {
                            var IdVideo = url.Contains("&") ? url.Split('=')[1].Split("&")[0] : url.Split('=')[1];
                            if (IdVideo == null) continue;

                            var videoInfo = await YoutubeApi.GetVideoInfo(IdVideo);
                            if (videoInfo == null) continue;

                            Destinos.Add(new Destino()
                            {
                                IdVideo = IdVideo,
                                Titulo = videoInfo.Snippet.Title,
                                DireccionArchivo = Path.Combine(Directoy, videoInfo.Snippet.Title.Trim() + ".mp3"),
                                URLThumbnail = videoInfo.Snippet.Thumbnails.High.Url,
                            });
                        }
                    }
                }
                VideosDescargadosLabel.Text = "Agregar Thumbnails:";
                if (!BWAgregarThumbnails.IsBusy)
                {
                    BWAgregarThumbnails.RunWorkerAsync();
                }
            }
            ProgresoTotalPB.Value = 100;
        }

        private void ObtenerIdRexgex(string salida)
        {
            Regex rgx = new Regex(@"\[youtube\]\s+(?<Id>\w+):\s+Downloading.*", RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(salida);
            if (matches.Count > 0)
            {
                IDActual = matches[0].Groups["Id"].Value;
            }
        }

        private void BWAgregarThumbnails_Dowork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                EjecucionProcesos.AgregarThumbnails(worker, e, Destinos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Agregar Thumbnails");
            }
        }

        private void BWAgregarThumbnails_Progreso(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                ProgresoTotalPB.Value = e.ProgressPercentage;
                if (e.UserState != null)
                {
                    string salida = (string)e.UserState;

                    Estadolabel.Text = salida;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void BWAgregarThumbnails_Resultado(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message);
                }
                if (e.Result != null)
                {
                    var procesoOutput = (string)e.Result;
                    if (procesoOutput != null)
                    {
                        if (procesoOutput.Contains("ERROR", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Estadolabel.Text = "Estado: Error al Agregar Thumbnails:" + procesoOutput.Substring(procesoOutput.IndexOf("Error", StringComparison.InvariantCultureIgnoreCase));
                        }
                        else
                        {
                            Estadolabel.Text = $"Estado: Thumbnails Agregadas!";
                        }
                    }
                    else
                    {
                        Estadolabel.Text = $"Estado: Thumbnails Agregadas!";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Agregar Thumbnails");
            }
            finally
            {
                ProgresoTotalPB.Value = 100;
                VideosDescargadosLabel.Text = "Finalizado!";
                BackgroundWorker worker = sender as BackgroundWorker;
                worker.CancelAsync();
            }
        }
        public class Destino
        {
            public string IdVideo { get; set; } = "";
            public string Titulo { get; set; } = "";
            public string DireccionArchivo { get; set; } = "";
            public string URLThumbnail { get; set; } = "";
        }
    }
}
