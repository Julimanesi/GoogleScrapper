using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleScrapper
{
    public partial class PanelYoutube : UserControl
    {
        private Label TituloVideoLB = new Label();
        private Label DescripcionVideoLB = new Label();
        private Label OtrosDatosVideoLB = new Label();

        private PictureBox ImagenVideoPicBx = new PictureBox();
        public bool seleccionado { get; set; } = false;
        private string BaseUrlYouTube { get; } = "https://www.youtube.com/watch?v=";
        public string Link { get; set; } = "";
        public PanelYoutube(int ancho, int altoimagen, Google.Apis.YouTube.v3.Data.SearchResult resultado)
        {
            InitializeComponent();
            this.Width = ancho;
            this.Height = ancho;
            decimal altoRestante = ancho - altoimagen;

            this.ImagenVideoPicBx.SizeMode = PictureBoxSizeMode.StretchImage;
            this.ImagenVideoPicBx.ImageLocation = resultado.Snippet.Thumbnails.High.Url != "" ? resultado.Snippet.Thumbnails.High.Url : "C:\\Users\\julim\\OneDrive\\Pictures\\Saved Pictures\\146.jpg";
            this.ImagenVideoPicBx.Dock = DockStyle.Top;
            this.ImagenVideoPicBx.Height = altoimagen;
            this.ImagenVideoPicBx.Click += panelyoutubeClick;
            this.ImagenVideoPicBx.DoubleClick += AlDobleClick;

            this.TituloVideoLB.Text = resultado.Snippet.Title;
            this.TituloVideoLB.Dock = DockStyle.Top;
            this.TituloVideoLB.AutoSize = false;
            this.TituloVideoLB.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloVideoLB.ForeColor = SystemColors.InfoText;
            this.TituloVideoLB.Size = new Size(ancho, (int)(altoRestante * 0.4M));
            this.TituloVideoLB.Click += panelyoutubeClick;
            this.TituloVideoLB.DoubleClick += AlDobleClick;

            this.OtrosDatosVideoLB.Dock = DockStyle.Top;
            this.OtrosDatosVideoLB.AutoSize = true;
            this.OtrosDatosVideoLB.MaximumSize = new Size(ancho, (int)(altoRestante * 0.2M));
            this.OtrosDatosVideoLB.Text = $"Fecha: {resultado.Snippet.PublishedAtDateTimeOffset?.ToString("dd/MM/yyyy")} | Canal: {resultado.Snippet.ChannelTitle}";
            this.OtrosDatosVideoLB.Click += panelyoutubeClick;
            this.OtrosDatosVideoLB.DoubleClick += AlDobleClick;

            this.DescripcionVideoLB.Dock = DockStyle.Top;
            this.DescripcionVideoLB.AutoSize = true;
            this.DescripcionVideoLB.MaximumSize = new Size(ancho, (int)(altoRestante * 0.4M));
            this.DescripcionVideoLB.Text = resultado.Snippet.Description;
            this.DescripcionVideoLB.Click += panelyoutubeClick;
            this.DescripcionVideoLB.DoubleClick += AlDobleClick;

            this.Controls.Add(DescripcionVideoLB);
            this.Controls.Add(OtrosDatosVideoLB);
            this.Controls.Add(ImagenVideoPicBx);
            this.Controls.Add(TituloVideoLB);
            Link = BaseUrlYouTube + resultado.Id.VideoId;
        }

        public void VolverARenderizar(int ancho, int altoimagen)
        {
            this.Width = ancho;
            this.Height = ancho;
            decimal altoRestante = ancho - altoimagen;
            this.ImagenVideoPicBx.Height = altoimagen;
            this.TituloVideoLB.Size = new Size(ancho, (int)(altoRestante * 0.4M));
            this.OtrosDatosVideoLB.MaximumSize = new Size(ancho, (int)(altoRestante * 0.2M));
            this.DescripcionVideoLB.MaximumSize = new Size(ancho, (int)(altoRestante * 0.4M));
        }

        private void panelyoutubeClick(object sender, EventArgs e)
        {
            OnSeleccionado();
            Clipboard.SetText(Link);
        }

        public void OnSeleccionado()
        {
            if (seleccionado)
            {
                this.BorderStyle = BorderStyle.None;
                this.BackColor = Color.Transparent;
            }
            else
            {
                this.BorderStyle = BorderStyle.Fixed3D;
                this.BackColor = Color.SkyBlue;
            }
            this.seleccionado = !this.seleccionado;
        }

        private void AlDobleClick(object sender, EventArgs e)
        {
            EjecucionProcesos.ReproducirUnVideo(Link);
        }
    }
}
