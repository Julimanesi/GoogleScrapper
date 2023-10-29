using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        public EdicionVideosForm(List<Destino> destinos, TipoEdicion tipoEdicion)
        {
            InitializeComponent();
            Destinos = destinos;
            Tipo = tipoEdicion;
            if (Tipo == TipoEdicion.Thumbnails)
            {
                URLExtraLB.Visible = true;
                URLExtraLB.Text = "URL Thumbnail:";
                URLExtraTXBX.Visible = true;
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
                BWEditarVideos.RunWorkerAsync();
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
                switch (Tipo)
                {
                    case TipoEdicion.comprimir:
                        EjecucionProcesos.ComprimirVideo(worker, e, Destinos);
                        break;
                    case TipoEdicion.Thumbnails:
                        if (URLExtraTXBX.Text != "")
                        {
                            //TODO Probar
                            var destino = Destinos.ElementAt(0);
                            destino.URLThumbnail = URLExtraTXBX.Text;
                            EjecucionProcesos.AgregarThumbnails(worker, e, Destinos);
                        }
                        else
                        {
                            MessageBox.Show("Debe agregar una URL del Thumbnails", "Error al Editar Videos");
                        }
                        break;
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

                    SalidaRTextBox.Text = salida;
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
    }
}
