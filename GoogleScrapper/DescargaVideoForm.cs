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

namespace GoogleScrapper
{
    public partial class DescargaVideoForm : Form
    {
        private string ListaUrls { get; set; } = "";
        private int Total { get; set; }
        private int CantActualDesc { get; set; } = 0;
        private string Directoy { get; set; } = "";
        private bool MostrandoDetalles = false;
        private string comprimirVideo = "--postprocessor-args \"-c:v h264_nvenc -preset:v p7 -tune:v hq -rc:v vbr -cq:v 32 -b:v 0 -profile:v high\"";
        private static BackgroundWorker BWDescargaVideo = new BackgroundWorker();

        public DescargaVideoForm(string ListaUrls, int count, string Directoy)
        {
            InitializeComponent();
            this.ListaUrls = ListaUrls;
            this.Total = count;
            this.Directoy = Directoy;
            SalidaRTextBox.Text = "";
            VideosDescargadosLabel.Text = $"Videos Descargados: 0/{Total}";
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
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                //VideoScrapper videoScrapper = (VideoScrapper)e.Argument;
                Regex rgx = new Regex(@"\[download\]\s+(?<porcentaje>\d+)", RegexOptions.IgnoreCase);
                int progreso = 0;
            
                using (Process ytdl = new Process())
                {
                    ytdl.StartInfo.FileName = "yt-dlp.exe";
                    if (!SoloAudioCKBX.Checked)
                    {
                        ytdl.StartInfo.Arguments = $"{(ComprimirVideoCKBX.Checked ? comprimirVideo : "")} -P \"{Directoy}\"  --check-formats -f mp4 -o \"%(title)s.%(ext)s\" {ListaUrls} --add-metadata";
                    }
                    else
                    {   //Descargar solo musica/audio
                        ytdl.StartInfo.Arguments = $" --postprocessor-args \"-c:v h264_nvenc\" -P \"{Directoy}\" --no-playlist -x --audio-format mp3 --audio-quality 320K -o \"%(title)s.%(ext)s\" {ListaUrls} --add-metadata";
                    }
                    ytdl.StartInfo.UseShellExecute = false;
                    ytdl.StartInfo.CreateNoWindow = true;
                    ytdl.StartInfo.RedirectStandardOutput = true;
                    ytdl.StartInfo.RedirectStandardError = true;
                    ytdl.Start();
                    while (!ytdl.StandardOutput.EndOfStream)
                    {
                        string? linea = ytdl.StandardOutput.ReadLine();
                        if(linea != null) 
                        {
                            MatchCollection matches = rgx.Matches(linea);
                            if(matches.Count > 0)
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
                    if (procesoOutput != null)
                    {
                        if (procesoOutput.Contains("ERROR",StringComparison.InvariantCultureIgnoreCase))
                        {
                            Estadolabel.Text = "Estado: Error al Descargar:" + procesoOutput.Substring(procesoOutput.IndexOf("Error",StringComparison.InvariantCultureIgnoreCase));
                        }
                        else
                        {
                            Estadolabel.Text = "Estado: Video Descargado!";
                        }
                    }
                    else
                    {
                        Estadolabel.Text = "Estado: Video Descargado!";
                    }
                }
            }catch(Exception ex)
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
                DestinoLabel.Text = "Destino: " + matches[0].Groups["destino"].Value;
            }
        }
        private void ObtenerEstadoDescarga(string salida)
        {
            string pattern = @"\[(?<estado>\w+)\]";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(salida);
            if (matches.Count > 0)
            {
                Estadolabel.Text = "Estado: " + parsearEstado(matches[0].Groups["estado"].Value);
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

                    DetalleDescargaLabel.Text = $"Detalle Descarga: Porcentaje: {porcentaje}. Tamaño actual: {tamanioact}. Velocidad de descarga: {velocDesc}. Tiempo faltante estimado: {tiempofalt}.";
                    List<string> salidaLineas = SalidaRTextBox.Lines.ToList();
                    int indexremover = salidaLineas.Count - 2;
                    if (indexremover > 2)
                    {
                        salidaLineas.RemoveAt(indexremover);
                        SalidaRTextBox.Lines = salidaLineas.ToArray();
                    }
                }
                SalidaRTextBox.Text += salida + "\n";
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Descargar la Lista de Reproducion");
            }
        }

        private string parsearEstado(string estado)
        {
            switch(estado)
            {
                case "download":
                    estado = "Descargando";
                    break;
                case "info":
                    estado = "Obteniendo Información";
                    break;
                case "Metadata":
                    estado = "Agregando metadatos";
                    if(!Estadolabel.Text.Contains("metadatos"))
                        CantActualDesc++;
                    break;
                default:
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
    }
}
