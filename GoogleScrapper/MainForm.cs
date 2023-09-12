using System.Diagnostics;
using System.ComponentModel;
using System.Security.Policy;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace GoogleScrapper
{
    public partial class MainForm : Form
    {
        public static string URLGoogle = "https://www.google.com/search?q=";
        private int NroMinimoResultados = 1;
        public MainForm()
        {
            InitializeComponent();
            FechaInicioDTP.Value = DateTime.Now.AddYears(-1);
            FechaFinDTP.Value = DateTime.Now;
            FechaInicioDTP.MaxDate = DateTime.Now;
            FechaFinDTP.MaxDate = DateTime.Now;
            EjecucionProcesos.Inicializar();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon(@"C:\Users\julim\OneDrive\Documentos\Visual Studio 2022\Windows Form\GoogleScrapper\GoogleScrapper\multimedia_player_16922.ico");
        }
    }
}