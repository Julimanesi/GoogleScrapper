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
        public static string comprimirVideo { get; } = "--postprocessor-args \"-c:v h264_nvenc -preset:v p7 -tune:v hq -rc:v vbr -cq:v 32 -b:v 0 -profile:v high\"";
        private static BackgroundWorker BWDescargaVideo = new BackgroundWorker();
        private List<string> Destinos = new List<string>();
        private List<PanelYoutube>? PanelesYoutube { get; set; } = null;

        public DescargaVideoForm(string ListaUrls, int count, string Directoy, List<PanelYoutube>? panelesYoutube = null)
        {
            InitializeComponent();
            this.ListaUrls = ListaUrls;
            this.Total = count;
            this.Directoy = Directoy;
            SalidaRTextBox.Text = "";
            VideosDescargadosLabel.Text = $"Videos Descargados: 0/{Total}";
            PanelesYoutube = panelesYoutube;
            BWDescargaVideo.WorkerReportsProgress = true;
            BWDescargaVideo.WorkerSupportsCancellation = true;
            BWDescargaVideo.DoWork += new DoWorkEventHandler(BWDescargaVideo_Dowork);
            BWDescargaVideo.ProgressChanged += new ProgressChangedEventHandler(BWDescargaVideo_Progreso);
            BWDescargaVideo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWDescargaVideo_Resultado);
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
            //VideoScrapper videoScrapper = (VideoScrapper)e.Argument;
            try
            {
                Regex rgx = new Regex(@"\[download\]\s+(?<porcentaje>\d+)", RegexOptions.IgnoreCase);
                int progreso = 0;

                using (Process ytdl = new Process())
                {
                    ytdl.StartInfo.FileName = "yt-dlp.exe";
                    if (!SoloAudioCKBX.Checked)
                    {
                        ytdl.StartInfo.Arguments = $"{(ComprimirVideoCKBX.Checked ? comprimirVideo : "")} -P \"{Directoy}\"  --check-formats -f mp4 -o \"%(title)s.%(ext)s\" {ListaUrls} --add-metadata {(AgregarThumbnailCKBX.Checked ? "--compat-options embed-thumbnail-atomicparsley" : "")}";
                    }
                    else
                    {   //Descargar solo musica/audio
                        string nombreArchivoMusica = ObtenerDatosMusicaCKBX.Checked ? "\"%(title)s.%(ext)s\"" : "\"%(track)s.%(ext)s\"";
                        ytdl.StartInfo.Arguments = $" --postprocessor-args \"-c:v h264_nvenc\" -P \"{Directoy}\" --no-playlist -x --audio-format mp3 --audio-quality 320K -o {nombreArchivoMusica} {ListaUrls} --add-metadata";
                    }
                    ytdl.StartInfo.UseShellExecute = false;
                    ytdl.StartInfo.CreateNoWindow = true;
                    ytdl.StartInfo.RedirectStandardOutput = true;
                    ytdl.StartInfo.RedirectStandardError = true;
                    ytdl.Start();
                    while (!ytdl.StandardOutput.EndOfStream)
                    {
                        string? linea = ytdl.StandardOutput.ReadLine();
                        if (linea != null)
                        {
                            MatchCollection matches = rgx.Matches(linea);
                            if (matches.Count > 0)
                            {
                                progreso = int.Parse(matches[0].Groups["porcentaje"].Value);
                            }
                        }
                        worker.ReportProgress(progreso, linea);
                    }
                    ytdl.WaitForExit();
                    e.Result = ytdl.StandardError.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Descargar la Lista de Reproducion");
            }
            try
            {
                if (PanelesYoutube != null && AgregarThumbnailCKBX.Checked)
                {
                    int progreso = 0;
                    worker.ReportProgress(progreso, "Agregando Thumbnails");
                    foreach (string destino in Destinos)
                    {
                        worker.ReportProgress((int)(((decimal)progreso / Destinos.Count) * 100), $"Agregando Thumbnails a {destino}");
                        var panel = PanelesYoutube.Find(x => x.ResultadoListaItem != null && destino.Contains(x.ResultadoListaItem.Snippet.Title) || x.ResultadoBusqueda != null && destino.Contains(x.ResultadoBusqueda.Snippet.Title));
                        if (panel != null)
                        {
                            string UrlImagen = "";
                            if (panel.ResultadoListaItem != null)
                            {
                                UrlImagen = panel.ResultadoListaItem.Snippet.Thumbnails.High.Url;
                            }
                            else if (panel.ResultadoBusqueda != null)
                            {
                                UrlImagen = panel.ResultadoBusqueda.Snippet.Thumbnails.High.Url;
                            }
                            string salidaError = "";
                            using (Process ffmpeg = new Process())
                            {
                                ffmpeg.StartInfo.FileName = "ffmpeg.exe";
                                ffmpeg.StartInfo.Arguments = $"-i \"{destino}\" -i \"{UrlImagen}\" \"-c:v h264_nvenc\" -map 0:0 -map 1:0 -c copy -id3v2_version 3 -metadata:s:v title=\"Album cover\" -metadata:s:v comment=\"Cover (front)\" \"{destino + "aux.mp3"}\"";
                                ffmpeg.StartInfo.UseShellExecute = false;
                                ffmpeg.StartInfo.CreateNoWindow = true;
                                ffmpeg.StartInfo.RedirectStandardOutput = true;
                                ffmpeg.StartInfo.RedirectStandardError = true;
                                ffmpeg.Start();
                                //if (!ffmpeg.WaitForExit(1000 * 10))
                                //{
                                //    ffmpeg.Kill();
                                //}
                                ffmpeg.WaitForExit();
                                salidaError = ffmpeg.StandardError.ReadToEnd();
                            }
                            if (salidaError != null && salidaError != "" && salidaError.Contains("ERROR", StringComparison.InvariantCultureIgnoreCase))
                            {

                            }
                            else
                            {
                                File.Move(destino + "aux.mp3", destino, true);
                                File.Delete(destino + "aux.mp3");
                            }
                        }
                        progreso++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Thumbnails");
            }
        }

        private void BWDescargaVideo_Progreso(object sender, ProgressChangedEventArgs e)
        {
            ProgresoDescargaPB.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                string salida = (string)e.UserState;
                ObtenerDestinoVideo(salida);
                ObtenerEstadoDescarga(salida);
                ObtenerDetallesProgresoDescarga(salida);
                ObtenerDestinoMusica(salida);
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
                DestinoLabel.Text = "Destino: " + destino;
                if (!Destinos.Contains(destino))
                    Destinos.Add(destino);
            }
        }
    }
}
