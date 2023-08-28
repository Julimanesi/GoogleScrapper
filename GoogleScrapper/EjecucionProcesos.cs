using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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
        public static void DescargarListaReproduc(string ListaUrls, int count, string Directoy)
        {
            //try
            //{
            //    using (Process ytdl = new Process())
            //    {
            //        ytdl.StartInfo.FileName = "yt-dlp.exe";
            //        ytdl.StartInfo.Arguments = $" --postprocessor-args \"-c:v h264_nvenc -preset:v p7 -tune:v hq -rc:v vbr -cq:v 19 -b:v 0 -profile:v high\"  -P {Directoy} -o \"%(title)s.%(ext)s\" {ListaUrls} --add-metadata"; //-f mp4
            //        ytdl.StartInfo.UseShellExecute = false;
            //        ytdl.StartInfo.CreateNoWindow = true;
            //        ytdl.StartInfo.RedirectStandardOutput = true;
            //        ytdl.Start();
            //        string salida = ytdl.StandardOutput.ReadToEnd();
            //        ytdl.WaitForExit();
            //        MessageBox.Show(salida, "Salida de descarga");
            //        //if (!ytdl.WaitForExit(1000 *20 * count))
            //        //{
            //        //    ytdl.Kill();
            //        //    return;
            //        //}
            //    }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message, "Error al Descargar la Lista de Reproducion");
            //}
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

        private void CrearListaReproducAux(BindingSource resultadoVideoBindingSource)
        {
            List<ResultadoVideo> ListaVideos = (List<ResultadoVideo>)resultadoVideoBindingSource.DataSource;
            if (ListaVideos != null && ListaVideos.Count > 0)
            {
                File.WriteAllLines(ArchivoListaReprodcAux, ListaVideos.Select(x => x.URLVideo));
            }
        }
    }
}
