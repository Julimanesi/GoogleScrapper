using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static GoogleScrapper.DescargaVideoForm;

namespace GoogleScrapper
{
    public class EjecucionProcesos
    {
        private string ArchivoListaReprodcAux = "ListaReproducAux.m3u";

        private static BackgroundWorker BWSmplayer = new BackgroundWorker();

        public static void Inicializar()
        {
            BWSmplayer.DoWork += new DoWorkEventHandler(BWSmplayer_Dowork);
        }

        #region Youtube-dl

        public static bool YtdlPuedeReproducir(string Url)
        {
            try
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
                    if (!ytdl.WaitForExit(1000 * 5))
                    {
                        ytdl.Kill();
                        return false;
                    }
                    var procesoOutput = ytdl.StandardError.ReadToEnd();
                    resp = procesoOutput == null || !procesoOutput.Contains("ERROR");
                    
                }
                return resp;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<string> GetListaExtractores()
        {
            List<string> extractores = new List<string>();
            try
            {
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error al Obtener la lista de Extractores");
            }
            return extractores;
        }

        public static void DescargarVideos(BackgroundWorker worker , DoWorkEventArgs e, bool SoloAudio,bool ComprimirVideo, string Directoy,string ListaUrls,bool AgregarThumbnail,bool ObtenerDatosMusica)
        {
            try
            {
                Regex rgx = new Regex(@"\[download\]\s+(?<porcentaje>\d+)", RegexOptions.IgnoreCase);
                int progreso = 0;

                using (Process ytdl = new Process())
                {
                    ytdl.StartInfo.FileName = "yt-dlp.exe";
                    if (!SoloAudio)
                    {                                                                                                  
                        ytdl.StartInfo.Arguments = $"{(ComprimirVideo ? DescargaVideoForm.ComandoComprimirVideo : "")} -P \"{Directoy}\" {(ComprimirVideo ? "--check-formats -f mp4" : "")} -o \"%(title)s.%(ext)s\" {ListaUrls} --add-metadata {(AgregarThumbnail ? "--compat-options embed-thumbnail-atomicparsley" : "")}";
                    }
                    else
                    {   //Descargar solo musica/audio
                        string nombreArchivoMusica = ObtenerDatosMusica ? "\"%(title)s.%(ext)s\"" : "\"%(track)s.%(ext)s\"";
                        ytdl.StartInfo.Arguments = $" --postprocessor-args \"-c:v h264_nvenc\" -P \"{Directoy}\" --no-playlist -x --audio-format mp3 --audio-quality 320K -o {nombreArchivoMusica} {ListaUrls} --add-metadata";
                    }
                    ytdl.StartInfo.UseShellExecute = false;
                    ytdl.StartInfo.CreateNoWindow = true;
                    ytdl.StartInfo.RedirectStandardOutput = true;
                    ytdl.StartInfo.RedirectStandardError = true;
                    ytdl.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    ytdl.StartInfo.StandardErrorEncoding = Encoding.UTF8;
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
        }

        #endregion

        #region SMPlayer 

        public static void ReproducirUnVideo(string URLVideo)
        {
            try
            {
                if (!BWSmplayer.IsBusy)
                {
                    BWSmplayer.RunWorkerAsync();
                }

                using (Process smplayer = new Process())
                {
                    smplayer.StartInfo.FileName = "C:\\Program Files\\SMPlayer\\smplayer.exe";
                    smplayer.StartInfo.Arguments = $" {URLVideo}";
                    smplayer.StartInfo.UseShellExecute = false;
                    smplayer.Start();
                    smplayer.WaitForExit();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error al Reproducir el Video seleccionado en SMPlayer");
            }
        }

        public static void EnviarListaReproducASMPlayer(string ListaUrls)
        {
            try
            {
                if (!BWSmplayer.IsBusy)
                {
                    BWSmplayer.RunWorkerAsync();
                }
                using (Process smplayer = new Process())
                {
                    smplayer.StartInfo.FileName = "C:\\Program Files\\SMPlayer\\smplayer.exe";
                    smplayer.StartInfo.Arguments = $"-add-to-playlist " + ListaUrls;
                    smplayer.StartInfo.UseShellExecute = false;
                    smplayer.Start();
                    smplayer.WaitForExit();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error al Enviar la Lista de Reproducion a SMPlayer");
            }
        }

        #region No usado
        private void ReproducirListaAuxGuardada(BindingSource resultadoVideoBindingSource)
        {
            CrearListaReproducAux(resultadoVideoBindingSource);
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
        private void CrearListaReproducAux(BindingSource resultadoVideoBindingSource)
        {
            List<ResultadoVideo> ListaVideos = (List<ResultadoVideo>)resultadoVideoBindingSource.DataSource;
            if (ListaVideos != null && ListaVideos.Count > 0)
            {
                File.WriteAllLines(ArchivoListaReprodcAux, ListaVideos.Select(x => x.URLVideo));
            }
        }
        #endregion

        #endregion

        #region ffmpeg

        public string GetMetadata(string DireccionArchivo)
        {
            string metadata = string.Empty;
            try
            {
                var extArch = Path.GetExtension(DireccionArchivo);
                string DestinoAux = DireccionArchivo + "aux" + extArch;
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "ffmpeg.exe";
                processStartInfo.Arguments = $"-i \"{DireccionArchivo}\"";
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.StandardOutputEncoding = Encoding.UTF8;
                processStartInfo.StandardErrorEncoding = Encoding.UTF8;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                Process? ffmpeg = Process.Start(processStartInfo);
                metadata = ffmpeg?.StandardError.ReadToEnd() ?? "";
            }
            catch (Exception ex)
            {
                
            }
            return metadata;
        }

        public static void ExtraerSubtitulos(Label EstadoLB, ProgressBar ProgresoPB, string DireccionArchivo,int stream,string Idioma = "esp")
        {
            try
            {
                ProgresoPB.Value = 0;
                EstadoLB.Text = "Extrayendo Subtitulos";

                using (Process ffmpeg = new Process())
                {
                    ffmpeg.StartInfo.FileName = "ffmpeg.exe";
                    ffmpeg.StartInfo.Arguments = $"-i \"{DireccionArchivo}\" -map 0:s:{stream} \"{DireccionArchivo}_{Idioma}.srt\"";
                    ffmpeg.StartInfo.UseShellExecute = false;
                    ffmpeg.StartInfo.CreateNoWindow = true;
                    ffmpeg.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    ffmpeg.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                    ffmpeg.StartInfo.RedirectStandardOutput = true;
                    ffmpeg.StartInfo.RedirectStandardError = true;
                    ffmpeg.Start();
                    var salidaError = ffmpeg?.StandardError.ReadToEnd();
                }
                EstadoLB.Text = "Subtitulos extraidos";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Thumbnails");
            }
        }

        public static void AgregarSubtitulos(BackgroundWorker worker, DoWorkEventArgs e, string DireccionArchivo,string DireccionSub, string Idioma = "esp")
        {
            try
            {
                int progreso = 0;
                worker.ReportProgress(progreso, "Agregar Subtitulos");
                var extArch = Path.GetExtension(DireccionArchivo);
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "ffmpeg.exe";
                processStartInfo.Arguments = $"-i \"{DireccionArchivo}\" -i \"{DireccionSub}\" -c copy -c:s mov_text \"{DireccionArchivo}aux{extArch}\"";
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.StandardOutputEncoding = Encoding.UTF8;
                processStartInfo.StandardErrorEncoding = Encoding.UTF8;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                Process? ffmpeg = Process.Start(processStartInfo);
                var salidaError = ffmpeg?.StandardError.ReadToEnd();
                if (salidaError != null && salidaError != "" && salidaError.Contains("ERROR", StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
                else
                {
                    File.Delete(DireccionArchivo);
                    File.Move($"{DireccionArchivo}aux{extArch}", DireccionArchivo, true);
                }

                e.Result = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Thumbnails");
            }
        }

        public static void AgregarThumbnails(BackgroundWorker worker, DoWorkEventArgs e, List<Destino> Destinos)
        {
            try
            {
                int progreso = 0;
                int cant = 0;
                int erroresCont = 0;
                worker.ReportProgress(progreso, "Agregando Thumbnails");
                foreach (var destino in Destinos)
                {
                    cant++;
                    if (destino != null)
                    {
                        try
                        {
                            var extArch = Path.GetExtension(destino.DireccionArchivo);
                            string DestinoAux = destino.DireccionArchivo + "aux" + extArch;
                            ProcessStartInfo processStartInfo = new ProcessStartInfo();
                            processStartInfo.FileName = "ffmpeg.exe";
                            if (extArch == ".mp3")
                            {
                                processStartInfo.Arguments = $"-i \"{destino.DireccionArchivo}\" -i \"{destino.URLThumbnail}\" -map 0:0 -map 1:0 -c copy -id3v2_version 3 -metadata:s:v title=\"Album cover\" -metadata:s:v comment=\"Cover (front)\" \"{DestinoAux}\"";
                            }
                            else
                            {
                                processStartInfo.Arguments = $"ffmpeg -i \"{destino.DireccionArchivo}\" -i \"{destino.URLThumbnail}\" -map 0 -map 1 -c copy -c:v:1 png -disposition:v:1 attached_pic \"{DestinoAux}\"";
                            }
                            processStartInfo.UseShellExecute = false;
                            processStartInfo.CreateNoWindow = true;
                            processStartInfo.StandardOutputEncoding = Encoding.UTF8;
                            processStartInfo.StandardErrorEncoding = Encoding.UTF8;
                            processStartInfo.RedirectStandardOutput = true;
                            processStartInfo.RedirectStandardError = true;
                            //using (Process ffmpeg = new Process())
                            //{
                            //    ffmpeg.StartInfo.FileName = "ffmpeg.exe";
                            //    ffmpeg.StartInfo.Arguments = $"-i \"{destino}\" -i \"{UrlImagen}\" -map 0:0 -map 1:0 -c copy -id3v2_version 3 -metadata:s:v title=\"Album cover\" -metadata:s:v comment=\"Cover (front)\" \"{destino + "aux.mp3"}\"";
                            //    ffmpeg.StartInfo.UseShellExecute = false;
                            //    ffmpeg.StartInfo.CreateNoWindow = true;
                            //    ffmpeg.StartInfo.RedirectStandardOutput = true;
                            //    ffmpeg.StartInfo.RedirectStandardError = true;
                            //    ffmpeg.Start();
                            //    while (!ffmpeg.StandardOutput.EndOfStream)
                            //    {
                            //        string? linea = ffmpeg.StandardOutput.ReadLine();
                            //        worker.ReportProgress(progreso, linea);
                            //    }
                            //    ffmpeg.Kill();
                            //    salidaError = ffmpeg.StandardError.ReadToEnd();
                            //}
                            Process ffmpeg = Process.Start(processStartInfo);
                            //if (!ffmpeg.WaitForExit(1000 * 5))
                            //{
                            //    ffmpeg.Kill();
                            //    continue;
                            //}
                            var salidaError = ffmpeg.StandardError.ReadToEnd();
                            if (salidaError != null && salidaError != "" && salidaError.Contains("ERROR", StringComparison.InvariantCultureIgnoreCase))
                            {
                                erroresCont++;
                                continue;
                            }
                            else
                            {
                                File.Delete(destino.DireccionArchivo);
                                File.Move(DestinoAux, destino.DireccionArchivo, true);
                            }
                        }catch (Exception ex)
                        {
                            erroresCont++;
                            continue;
                        }
                    }
                    progreso = (int)(((decimal)cant / Destinos.Count) * 100);
                    worker.ReportProgress(progreso, $"Agregando Thumbnails a {destino}");
                }
                var destinoAux = Destinos.FirstOrDefault();
                if (destinoAux != null)
                {
                    string? directorio = Path.GetDirectoryName(destinoAux!.DireccionArchivo);
                    if (directorio != null)
                    {
                        List<string> listTemp = Directory.GetFiles(directorio).ToList();
                        listTemp.Where(x => x.Contains(".temp")).ToList().ForEach(x => File.Delete(x));
                    }
                }
                e.Result = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Thumbnails");
            }
        }

        public static async void ComprimirVideo(List<Destino> Destinos, Label? EstadoLB = null,ProgressBar? ProgresoPB = null,RichTextBox ? SalidaRTBX = null)
        {
            try
            {
                if(ProgresoPB != null)
                    ProgresoPB.Value = 0;
                int cant = 0;
                int erroresCont = 0;
                if (EstadoLB != null)
                    EstadoLB.Text= "Comprimiendo Videos";
                if (SalidaRTBX != null)
                    SalidaRTBX.Text += EstadoLB?.Text ?? "";
                foreach (var destino in Destinos)
                {
                    if (destino != null)
                    {
                        try
                        {
                            var directorio = Path.GetDirectoryName(destino.DireccionArchivo);
                            if (!Directory.Exists(directorio)) continue;
                            using (Process ffmpeg = new Process())
                            {
                                ffmpeg.StartInfo.FileName = "ffmpeg.exe";/*-map 0:a? -map 0:s? -map 0:v*/
                                ffmpeg.StartInfo.Arguments = $"-i \"{destino.DireccionArchivo}\" -c:v h264_nvenc -pix_fmt yuv420p -preset:v p7 -tune:v hq -rc:v vbr -cq:v 32 -b:v 0 -profile:v high  \"{Path.Combine(directorio, destino.Titulo + ".mp4")}\"";
                                ffmpeg.StartInfo.UseShellExecute = false;
                                ffmpeg.StartInfo.CreateNoWindow = true;
                                ffmpeg.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                                ffmpeg.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                                ffmpeg.StartInfo.RedirectStandardOutput = true;
                                ffmpeg.StartInfo.RedirectStandardError = true;
                                ffmpeg.Start();
                                if (ffmpeg == null) continue;
                                ffmpeg.BeginOutputReadLine();
                                ffmpeg.BeginErrorReadLine();
                                if (EstadoLB != null)
                                    EstadoLB.Text = $"Comprimiendo Video: {destino.Titulo}";
                                if (SalidaRTBX != null)
                                    SalidaRTBX.Text += EstadoLB?.Text ?? "";
                                await ffmpeg.WaitForExitAsync();
                                //TODO ver como mostrar el progreso de la compresion y como mostrar los errores
                                //var salidaError = await ffmpeg.StandardError.ReadToEndAsync();
                                //if (salidaError != null && salidaError != "" && salidaError.Contains("ERROR", StringComparison.InvariantCultureIgnoreCase))
                                //{
                                //    erroresCont++;
                                //    worker.ReportProgress(progreso, $"Error al comprimir el Archivo: {salidaError}");
                                //    continue;
                                //}
                            }
                        }
                        catch (Exception ex)
                        {
                            erroresCont++;
                            if (ProgresoPB != null)
                                ProgresoPB.Value = (int)(((decimal)cant + erroresCont / Destinos.Count) * 100);
                            if (EstadoLB != null)
                                EstadoLB.Text = $"Error al comprimir el video:{destino.Titulo}. Error: {ex.Message}";
                            if (SalidaRTBX != null)
                                SalidaRTBX.Text += EstadoLB?.Text ?? "";
                            continue;
                        }
                        File.Delete(destino.DireccionArchivo);
                        cant++;
                        if (ProgresoPB != null)
                            ProgresoPB.Value = (int)(((decimal)cant + erroresCont / Destinos.Count) * 100);
                        if (EstadoLB != null)
                            EstadoLB.Text = $"Video Comprimido: {destino.Titulo}";
                        if (SalidaRTBX != null)
                            SalidaRTBX.Text += EstadoLB?.Text ?? "";
                    }
                }
                if (EstadoLB != null)
                    EstadoLB.Text = "Finalizado";
                if (ProgresoPB != null)
                    ProgresoPB.Value = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Comprimir video");
            }
        }
        #endregion

        #region BackgroundWorker
        private static void BWSmplayer_Dowork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                using (Process smplayer = new Process())
                {
                    smplayer.StartInfo.FileName = "C:\\Program Files\\SMPlayer\\smplayer.exe";
                    smplayer.StartInfo.UseShellExecute = false;
                    smplayer.Start();
                    smplayer.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al ejecutar SMPlayer");
            }
        }

        #endregion

        
    }
}
