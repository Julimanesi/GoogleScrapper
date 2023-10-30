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
                        ytdl.StartInfo.Arguments = $"{(ComprimirVideo ? DescargaVideoForm.ComandoComprimirVideo : "")} -P \"{Directoy}\"  --check-formats -f mp4 -o \"%(title)s.%(ext)s\" {ListaUrls} --add-metadata {(AgregarThumbnail ? "--compat-options embed-thumbnail-atomicparsley" : "")}";
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
                e.Result = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar Thumbnails");
            }
        }

        public async static void ComprimirVideo(BackgroundWorker worker, DoWorkEventArgs e, List<Destino> Destinos)
        {
            try
            {
                int progreso = 0;
                int cant = 0;
                int erroresCont = 0;
                worker.ReportProgress(progreso, "Comprimiendo Videos");
                foreach (var destino in Destinos)
                {
                    cant++;
                    if (destino != null)
                    {
                        try
                        {
                            var directorio = Path.GetDirectoryName(destino.DireccionArchivo);
                            if (!Directory.Exists(directorio)) continue;

                            ProcessStartInfo processStartInfo = new ProcessStartInfo();
                            processStartInfo.FileName = "ffmpeg.exe";/*-map 0:a? -map 0:s? -map 0:v*/
                            processStartInfo.Arguments = $"-i \"{destino.DireccionArchivo}\" -c:v h264_nvenc -pix_fmt yuv420p -preset:v p7 -tune:v hq -rc:v vbr -cq:v 32 -b:v 0 -profile:v high \"{Path.Combine(directorio,destino.Titulo + ".mp4")}\"";
                            processStartInfo.UseShellExecute = false;
                            processStartInfo.CreateNoWindow = true;
                            processStartInfo.StandardOutputEncoding = Encoding.UTF8;
                            processStartInfo.StandardErrorEncoding = Encoding.UTF8;
                            processStartInfo.RedirectStandardOutput = true;
                            processStartInfo.RedirectStandardError = true;
                            Process? ffmpeg = Process.Start(processStartInfo);
                            if (ffmpeg == null) continue;
                            ffmpeg.BeginOutputReadLine();
                            //ffmpeg.BeginErrorReadLine();
                            worker.ReportProgress(progreso,"Esperando a que termine de comprimir....");
                            //bool error = false;
                            //string? linea = ffmpeg.StandardError.ReadLine();
                            //while (linea != null && linea != "")
                            //{
                            //    worker.ReportProgress(progreso, linea);
                            //    if (linea.Contains("ERROR", StringComparison.InvariantCultureIgnoreCase))
                            //    {
                            //        worker.ReportProgress(progreso, $"Error al comprimir el Archivo");
                            //        ffmpeg.Kill();
                            //        erroresCont++;
                            //        error = true;
                            //        break;
                            //    }
                            //    linea = ffmpeg.StandardError.ReadLine();
                            //}
                            //if (error) continue;
                            //while (!ffmpeg.HasExited)
                            //{
                            //    string? linea = ffmpeg.StandardOutput.ReadLine();
                            //    if (linea != null && linea != "")
                            //    {
                            //        worker.ReportProgress(progreso, linea);
                            //        progreso = int.Parse(matches[0].Groups["porcentaje"].Value);
                            //        if (linea.Contains("ERROR", StringComparison.InvariantCultureIgnoreCase))
                            //        {
                            //            worker.ReportProgress(progreso, $"Error al comprimir el Archivo");
                            //            ffmpeg.Kill();
                            //            erroresCont++;
                            //            continue;
                            //        }
                            //    }
                            //}
                            var salidaError = ffmpeg.StandardError.ReadToEnd();
                            if (salidaError != null && salidaError != "" && salidaError.Contains("ERROR", StringComparison.InvariantCultureIgnoreCase))
                            {
                                erroresCont++;
                                worker.ReportProgress(progreso, $"Error al comprimir el Archivo: {salidaError}");
                                continue;
                            }
                            else
                            {
                                File.Delete(destino.DireccionArchivo);
                                worker.ReportProgress(progreso, "Archivo generado");
                            }
                        }
                        catch (Exception ex)
                        {
                            erroresCont++;
                            continue;
                        }
                    }
                    progreso = (int)(((decimal)cant / Destinos.Count) * 100);
                    worker.ReportProgress(progreso, $"Comprimiendo Video: {destino}");
                }
                e.Result = "";
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
