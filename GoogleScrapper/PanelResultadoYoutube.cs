using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleScrapper
{
    public class PanelResultadoYoutube: Panel
    {
        private Label TituloVideoLB = new Label();
        private PictureBox ImagenVideoPicBx = new PictureBox();
        public PanelResultadoYoutube(int ancho, int alto,string tituloVideo,string LinkImagen)
        {
            this.Width = ancho;
            this.Height = alto;
            this.ImagenVideoPicBx.SizeMode = PictureBoxSizeMode.StretchImage;
            this.ImagenVideoPicBx.ImageLocation = LinkImagen != "" ? LinkImagen : "C:\\Users\\julim\\OneDrive\\Pictures\\Saved Pictures\\146.jpg";
            this.ImagenVideoPicBx.Dock = DockStyle.Top;
            this.ImagenVideoPicBx.Height = (int)(ancho * 0.8);
            this.TituloVideoLB.Text = tituloVideo;
            this.TituloVideoLB.Dock = DockStyle.Bottom;
            this.TituloVideoLB.Height = (int)(ancho * 0.2);
            this.Controls.Add(TituloVideoLB);
            this.Controls.Add(ImagenVideoPicBx);
        }
    }
}
