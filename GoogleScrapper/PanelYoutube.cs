using Google.Apis.YouTube.v3.Data;
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
        public Label TituloVideoLB = new Label();
        private Label DescripcionVideoLB = new Label();
        private Label OtrosDatosVideoLB = new Label();

        private PictureBox ImagenVideoPicBx = new PictureBox();
        public bool seleccionado { get; set; } = false;
        public static string BaseUrlYouTube { get; } = "https://www.youtube.com/watch?v=";
        public static string BaseUrlYouTubeChannel { get; } = "https://www.youtube.com/channel/";
        public static string BaseUrlYouTubePlaylist { get; } = "https://www.youtube.com/playlist?list=";
        public string Link { get; set; } = "";
        public string ID { get; set; } = "";
        public string IDCanalPropietario { get; set; } = "";
        public TipoResultado TipoResultado { get; set; }
        public SearchResult? ResultadoBusqueda { get; set; } = null!;
        public PlaylistItem ? ResultadoListaItem { get; set; } = null!;

        public PanelYoutube(int ancho, int altoimagen, Google.Apis.YouTube.v3.Data.SearchResult resultado, EventHandler click)
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
            this.ImagenVideoPicBx.Click += click;
            this.ImagenVideoPicBx.DoubleClick += AlDobleClick;

            this.TituloVideoLB.Text = resultado.Snippet.Title;
            this.TituloVideoLB.Dock = DockStyle.Top;
            this.TituloVideoLB.AutoSize = false;
            this.TituloVideoLB.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloVideoLB.ForeColor = SystemColors.InfoText;
            this.TituloVideoLB.Size = new Size(ancho, (int)(altoRestante * 0.4M));
            this.TituloVideoLB.Click += panelyoutubeClick;
            this.TituloVideoLB.Click += click; ;
            this.TituloVideoLB.DoubleClick += AlDobleClick;

            this.OtrosDatosVideoLB.Dock = DockStyle.Top;
            this.OtrosDatosVideoLB.AutoSize = true;
            this.OtrosDatosVideoLB.MaximumSize = new Size(ancho, (int)(altoRestante * 0.2M));
            this.OtrosDatosVideoLB.Text = $"Fecha: {resultado.Snippet.PublishedAtDateTimeOffset?.ToString("dd/MM/yyyy")} | Canal: {resultado.Snippet.ChannelTitle}";
            this.OtrosDatosVideoLB.Click += panelyoutubeClick;
            this.OtrosDatosVideoLB.Click += click;
            this.OtrosDatosVideoLB.DoubleClick += AlDobleClick;

            this.DescripcionVideoLB.Dock = DockStyle.Top;
            this.DescripcionVideoLB.AutoSize = true;
            this.DescripcionVideoLB.MaximumSize = new Size(ancho, (int)(altoRestante * 0.4M));
            this.DescripcionVideoLB.Text = resultado.Snippet.Description;
            this.DescripcionVideoLB.Click += panelyoutubeClick;
            this.DescripcionVideoLB.Click += click;
            this.DescripcionVideoLB.DoubleClick += AlDobleClick;

            this.Controls.Add(DescripcionVideoLB);
            this.Controls.Add(OtrosDatosVideoLB);
            this.Controls.Add(ImagenVideoPicBx);
            this.Controls.Add(TituloVideoLB);
            //youtube#
            string tipo = resultado.Id.Kind.Split('#')[1];
            switch (tipo)
            {
                case "video":
                    Link = BaseUrlYouTube + resultado.Id.VideoId;
                    this.TipoResultado = TipoResultado.video;
                    ID = resultado.Id.VideoId;
                    IDCanalPropietario = resultado.Snippet.ChannelId;
                    break;
                case "channel":
                    Link = BaseUrlYouTubeChannel + resultado.Id.ChannelId;
                    this.TipoResultado = TipoResultado.canal;
                    ID = resultado.Id.ChannelId;
                    break;
                case "playlist":
                    Link = BaseUrlYouTubePlaylist + resultado.Id.PlaylistId;
                    this.TipoResultado = TipoResultado.lista;
                    ID = resultado.Id.PlaylistId;
                    IDCanalPropietario = resultado.Snippet.ChannelId;
                    break;
            }
            this.TituloVideoLB.Text = $"({TipoResultado}){TituloVideoLB.Text}";
            ResultadoBusqueda = resultado;
        }
        public PanelYoutube(int ancho, int altoimagen, PlaylistItem resultado,EventHandler click)
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
            this.ImagenVideoPicBx.Click += click;
            this.ImagenVideoPicBx.DoubleClick += AlDobleClick;

            this.TituloVideoLB.Text = resultado.Snippet.Title;
            this.TituloVideoLB.Dock = DockStyle.Top;
            this.TituloVideoLB.AutoSize = false;
            this.TituloVideoLB.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.TituloVideoLB.ForeColor = SystemColors.InfoText;
            this.TituloVideoLB.Size = new Size(ancho, (int)(altoRestante * 0.4M));
            this.TituloVideoLB.Click += panelyoutubeClick;
            this.TituloVideoLB.Click += click;
            this.TituloVideoLB.DoubleClick += AlDobleClick;

            this.OtrosDatosVideoLB.Dock = DockStyle.Top;
            this.OtrosDatosVideoLB.AutoSize = true;
            this.OtrosDatosVideoLB.MaximumSize = new Size(ancho, (int)(altoRestante * 0.2M));
            this.OtrosDatosVideoLB.Text = $"Fecha: {resultado.Snippet.PublishedAtDateTimeOffset?.ToString("dd/MM/yyyy")} | Canal: {resultado.Snippet.ChannelTitle}";
            this.OtrosDatosVideoLB.Click += panelyoutubeClick;
            this.OtrosDatosVideoLB.Click += click;
            this.OtrosDatosVideoLB.DoubleClick += AlDobleClick;

            this.DescripcionVideoLB.Dock = DockStyle.Top;
            this.DescripcionVideoLB.AutoSize = true;
            this.DescripcionVideoLB.MaximumSize = new Size(ancho, (int)(altoRestante * 0.4M));
            this.DescripcionVideoLB.Text = resultado.Snippet.Description;
            this.DescripcionVideoLB.Click += panelyoutubeClick;
            this.DescripcionVideoLB.Click += click;
            this.DescripcionVideoLB.DoubleClick += AlDobleClick;

            this.Controls.Add(DescripcionVideoLB);
            this.Controls.Add(OtrosDatosVideoLB);
            this.Controls.Add(ImagenVideoPicBx);
            this.Controls.Add(TituloVideoLB);

            Link = BaseUrlYouTube + resultado.Snippet.ResourceId.VideoId;
            ID = resultado.Snippet.ResourceId.VideoId;
            IDCanalPropietario = resultado.Snippet.VideoOwnerChannelId;
            this.TipoResultado = TipoResultado.video;
            this.TituloVideoLB.Text = $"({TipoResultado}){TituloVideoLB.Text}";
            ResultadoListaItem = resultado;
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
            try
            {
                OnSeleccionado();
                Clipboard.SetText(Link);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error al copiar el Link al Portapapeles");
            }
        }

        public void OnSeleccionado()
        {
            if (seleccionado)
            {
                this.BorderStyle = BorderStyle.None;
                this.BackColor = Color.Transparent;
                MainForm.ItemResultadoSeleccionado = null;
            }
            else
            {
                this.BorderStyle = BorderStyle.Fixed3D;
                this.BackColor = Color.SkyBlue;
                MainForm.ItemResultadoSeleccionado = this;
            }
            this.seleccionado = !this.seleccionado;
        }

        public void Deseleccionar()
        {
            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.Transparent;
            this.seleccionado = false;
        }

        private void AlDobleClick(object sender, EventArgs e)
        {
            try
            {
                switch (this.TipoResultado)
                {
                    case TipoResultado.video:
                        EjecucionProcesos.ReproducirUnVideo(Link);
                        break;
                    case TipoResultado.canal:
                        break;
                    case TipoResultado.lista:
                        EjecucionProcesos.EnviarListaReproducASMPlayer(Link);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al intentar Reproducir el/los Video/s del Link");
            }
        }
    }
    public enum TipoResultado { video,canal, lista }
}
