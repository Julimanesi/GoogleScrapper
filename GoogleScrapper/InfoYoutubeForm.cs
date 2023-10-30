using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleScrapper
{
    public partial class InfoYoutubeForm : Form
    {
        public TipoInfo Tipo { get; set; }
        public Video? Video { get; set; }
        public Channel? Canal { get; set; }
        public Playlist? Lista { get; set; }
        public string LinkImagen { get; set; } = string.Empty;

        public InfoYoutubeForm(TipoInfo tipoInfo, Video? video = null, Channel? canal = null, Playlist? lista = null)
        {
            InitializeComponent();
            Tipo = tipoInfo;
            Video = video;
            Canal = canal;
            Lista = lista;
        }

        private void InfoYoutubeForm_Load(object sender, EventArgs e)
        {
            switch (Tipo)
            {
                case TipoInfo.video:
                    CargarInfoVideo();
                    break;
                case TipoInfo.canal:
                    CargarInfoCanal();
                    break;
                case TipoInfo.Lista:
                    CargarInfoLista();
                    break;
            }
        }

        private void CargarInfoVideo()
        {
            try
            {
                if (Video != null)
                {
                    TituloLB.Text = $"{Video?.Snippet?.Title}\nTipo: Video | Fecha de Publicacion: {Video?.Snippet?.PublishedAtDateTimeOffset?.LocalDateTime.ToString("dd/MM/yyy")} | Estado: {Video?.Status?.UploadStatus} | ";
                    InformacionLB.Text = $"Etiquetas: {String.Join(", ", Video?.Snippet?.Tags ?? new List<string>())}\nCanal: {Video?.Snippet.ChannelTitle} | Duracion: {Video?.ContentDetails?.Duration.Replace("PT", "")/*.Replace("M", ":").Replace("S", "")*/} | ";
                    InformacionLB.Text += $"Definicion: {Video?.ContentDetails?.Definition} | Rating: {Video?.ContentDetails?.ContentRating?.IncaaRating} | ";
                    InformacionLB.Text += $"Vistas: {Video?.Statistics?.ViewCount}  | Likes: {Video?.Statistics?.LikeCount} | Dislikes: {Video?.Statistics?.DislikeCount}";
                    InformacionLB.Text += $"\nDescripcion: {Video?.Snippet?.Description}";
                    string tamanio = Video?.FileDetails?.FileSize > 1024 * 1024 ? $"{(Video?.FileDetails?.FileSize / (1024 * 1024))}MB" : $"{(Video?.FileDetails?.FileSize / (1024))}KB";
                    InformacionLB.Text += $"\nDatos de Archivo: Formato: {Video?.FileDetails?.Container} | Tasa de Bits: {Video?.FileDetails?.BitrateBps} | Tamaño: {tamanio}";
                    ImagenPB.ImageLocation = Video?.Snippet?.Thumbnails?.High?.Url;
                    LinkImagen = $"{Video?.Snippet?.Thumbnails?.High?.Url}";
                }
            }
            catch (Exception e)
            {

            }
        }

        private void CargarInfoCanal()
        {
            try
            {
                if (Canal != null)
                {
                    TituloLB.Text = $"{Canal.Snippet.Title}\nTipo: Canal | Fecha de Publicacion:{Canal.Snippet.PublishedAtDateTimeOffset?.LocalDateTime.ToString("dd/MM/yyy")} | ";
                    InformacionLB.Text += $"Pais: {Canal.Snippet.Country} | ";
                    InformacionLB.Text += $"Vistas: {Canal.Statistics.ViewCount}  | Nro de Videos: {Canal.Statistics.VideoCount} |";
                    InformacionLB.Text += $"\nDescripcion: {Canal.Snippet.Description}";
                    ImagenPB.ImageLocation = Canal?.Snippet?.Thumbnails?.High?.Url;
                    LinkImagen = $"{Canal?.Snippet.Thumbnails.High.Url}";
                }
            }
            catch (Exception e)
            {

            }
        }

        private void CargarInfoLista()
        {
            //TODO Implementar CargarInfoLista
        }

        private void ImagenPB_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"{LinkImagen}");
            ImagenGuardadaLB.Visible = true;
        }

        public enum TipoInfo { video, Lista, canal }
    }
}
