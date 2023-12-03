using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Internal;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GoogleScrapper.DescargaVideoForm;

namespace GoogleScrapper
{
    public partial class EdicionVideosForm : Form
    {
        private bool MostrandoDetalles = false;
        private List<Destino> Destinos = new List<Destino>();
        private BackgroundWorker BWEditarVideos = new BackgroundWorker();
        private TipoEdicion Tipo { get; set; }
        public enum TipoEdicion { comprimir, Thumbnails }
        private string UrlThumbnails { get; set; } = string.Empty;

        public EdicionVideosForm(List<Destino> destinos, TipoEdicion tipoEdicion)
        {
            InitializeComponent();
            Destinos = destinos;
            Tipo = tipoEdicion;
            switch (Tipo)
            {
                case TipoEdicion.Thumbnails:
                    AccionLB.Text = "Agregar Thumbnails";
                    URLExtraLB.Visible = true;
                    URLExtraLB.Text = "URL Thumbnail:";
                    URLExtraTXBX.Visible = true;
                    break;
                case TipoEdicion.comprimir:
                    AccionLB.Text = "Comprimir Videos";
                    break;
            }
            BWEditarVideos.WorkerReportsProgress = true;
            BWEditarVideos.WorkerSupportsCancellation = true;
            BWEditarVideos.DoWork += new DoWorkEventHandler(BWEditarVideos_Dowork);
            BWEditarVideos.ProgressChanged += new ProgressChangedEventHandler(BWEditarVideos_Progreso);
            BWEditarVideos.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWEditarVideos_Resultado);
        }

        private void EditarBTN_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Tipo)
                {
                    case TipoEdicion.comprimir:
                        EjecucionProcesos.ComprimirVideo(Destinos,Estadolabel, ProgresoTotalPB, SalidaRTextBox);
                        break;
                    case TipoEdicion.Thumbnails:
                        BWEditarVideos.RunWorkerAsync();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Editar");
            }
        }

        private void BWEditarVideos_Dowork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                if (URLExtraTXBX.Text != "")
                {
                    var destino = Destinos.ElementAt(0);
                    destino.URLThumbnail = URLExtraTXBX.Text;
                    EjecucionProcesos.AgregarThumbnails(worker, e, Destinos);
                }
                else
                {
                    MessageBox.Show("Debe agregar una URL del Thumbnails", "Error al Editar Videos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Editar Videos");
            }
        }

        private void BWEditarVideos_Progreso(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                ProgresoTotalPB.Value = e.ProgressPercentage;
                if (e.UserState != null)
                {
                    string salida = (string)e.UserState;
                    Estadolabel.Text = $"Estado: {salida}";
                    SalidaRTextBox.Text += $"{salida}\n";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BWEditarVideos_Resultado(object sender, RunWorkerCompletedEventArgs e)
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
                            Estadolabel.Text = "Estado: Error al Editar Videos:" + procesoOutput.Substring(procesoOutput.IndexOf("Error", StringComparison.InvariantCultureIgnoreCase));
                        }
                        else
                        {
                            Estadolabel.Text = $"Estado: Videos Editados!";
                        }
                    }
                    else
                    {
                        Estadolabel.Text = $"Estado: Videos Editados!";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Editar Videos");
            }
            finally
            {
                ProgresoTotalPB.Value = 100;
                VideosEditadosLabel.Text = "Finalizado!";
                BackgroundWorker worker = sender as BackgroundWorker;
                worker.CancelAsync();
            }
        }

        private void MostrarDetallesBTN_Click(object sender, EventArgs e)
        {

            SalidaRTextBox.Visible = !MostrandoDetalles;
            if (SalidaRTextBox.Visible)
            {
                this.SetBounds(this.Location.X, this.Location.Y, 818, 586);
            }
            else
            {
                this.SetBounds(this.Location.X, this.Location.Y, 818, 413);
            }
            MostrandoDetalles = !MostrandoDetalles;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void GetURLThumbnailsFormYtUrl(string url)
        {
            var videoInfo = await YoutubeApi.GetVideoInfo(YoutubeApi.ParsearIdDesdeURLVideo(url));
            if (videoInfo != null)
            {
                UrlThumbnails = videoInfo.Snippet.Thumbnails.High.Url;
            }
            else
            {
                MessageBox.Show("No se pudo obtener la informacion del video de YouTube", "Error al Editar Videos");
            }
        }

    }
}
