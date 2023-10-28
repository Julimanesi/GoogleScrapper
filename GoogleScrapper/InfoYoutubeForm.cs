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
    public partial class InfoYoutubeForm : Form
    {
        public TipoInfo Tipo { get; set; }
        public Video? Video{ get; set; }
        public Channel? Canal { get; set; }
        public Playlist? Lista { get; set; }

        public InfoYoutubeForm(TipoInfo tipoInfo, Video? video = null, Channel? canal = null, Playlist? lista= null)
        {
            InitializeComponent();
            Tipo = tipoInfo;
            Video = video;
            Canal = canal;
            Lista = lista;
        }

        private void InfoYoutubeForm_Load(object sender, EventArgs e)
        {

        }

        private void CargarInfoVideo()
        {
            //TODO Implementar CargarInfoVideo
        }

        private void CargarInfoCanal()
        {
            //TODO Implementar CargarInfoCanal
        }

        private void CargarInfoLista()
        {
            //TODO Implementar CargarInfoLista
        }

        public enum TipoInfo{video,Lista,canal }
    }
}
