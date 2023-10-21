namespace GoogleScrapper
{
    partial class DescargaVideoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SalidaRTextBox = new RichTextBox();
            ProgresoDescargaPB = new ProgressBar();
            label1 = new Label();
            VideosDescargadosLabel = new Label();
            DetalleDescargaLabel = new Label();
            MostrarDetallesBTN = new Button();
            Estadolabel = new Label();
            DestinoLabel = new Label();
            panel1 = new Panel();
            AgregarThumbnailCKBX = new CheckBox();
            ObtenerDatosMusicaCKBX = new CheckBox();
            SoloAudioCKBX = new CheckBox();
            ComprimirVideoCKBX = new CheckBox();
            DescargarBTN = new Button();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // SalidaRTextBox
            // 
            SalidaRTextBox.BackColor = SystemColors.ControlLightLight;
            SalidaRTextBox.Dock = DockStyle.Fill;
            SalidaRTextBox.Location = new Point(0, 0);
            SalidaRTextBox.Name = "SalidaRTextBox";
            SalidaRTextBox.ReadOnly = true;
            SalidaRTextBox.Size = new Size(887, 0);
            SalidaRTextBox.TabIndex = 0;
            SalidaRTextBox.Text = "";
            SalidaRTextBox.Visible = false;
            // 
            // ProgresoDescargaPB
            // 
            ProgresoDescargaPB.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ProgresoDescargaPB.Location = new Point(190, 221);
            ProgresoDescargaPB.Name = "ProgresoDescargaPB";
            ProgresoDescargaPB.Size = new Size(685, 20);
            ProgresoDescargaPB.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(12, 221);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(172, 20);
            label1.TabIndex = 2;
            label1.Text = "Progreso de la descarga:";
            // 
            // VideosDescargadosLabel
            // 
            VideosDescargadosLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            VideosDescargadosLabel.AutoSize = true;
            VideosDescargadosLabel.Location = new Point(190, 124);
            VideosDescargadosLabel.Margin = new Padding(3);
            VideosDescargadosLabel.Name = "VideosDescargadosLabel";
            VideosDescargadosLabel.Size = new Size(145, 20);
            VideosDescargadosLabel.TabIndex = 7;
            VideosDescargadosLabel.Text = "Videos descargados:";
            // 
            // DetalleDescargaLabel
            // 
            DetalleDescargaLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DetalleDescargaLabel.Location = new Point(12, 247);
            DetalleDescargaLabel.Margin = new Padding(3);
            DetalleDescargaLabel.Name = "DetalleDescargaLabel";
            DetalleDescargaLabel.Size = new Size(863, 59);
            DetalleDescargaLabel.TabIndex = 6;
            DetalleDescargaLabel.Text = "Detalle de la Descarga:";
            // 
            // MostrarDetallesBTN
            // 
            MostrarDetallesBTN.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            MostrarDetallesBTN.Location = new Point(12, 312);
            MostrarDetallesBTN.Name = "MostrarDetallesBTN";
            MostrarDetallesBTN.Size = new Size(141, 29);
            MostrarDetallesBTN.TabIndex = 5;
            MostrarDetallesBTN.Text = "Mostrar Detalles";
            MostrarDetallesBTN.UseVisualStyleBackColor = true;
            MostrarDetallesBTN.Click += MostrarDetallesBTN_Click;
            // 
            // Estadolabel
            // 
            Estadolabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Estadolabel.Location = new Point(12, 155);
            Estadolabel.Margin = new Padding(3);
            Estadolabel.Name = "Estadolabel";
            Estadolabel.Size = new Size(863, 43);
            Estadolabel.TabIndex = 4;
            Estadolabel.Text = "Estado:";
            // 
            // DestinoLabel
            // 
            DestinoLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DestinoLabel.Location = new Point(12, 195);
            DestinoLabel.Margin = new Padding(3);
            DestinoLabel.Name = "DestinoLabel";
            DestinoLabel.Size = new Size(863, 20);
            DestinoLabel.TabIndex = 3;
            DestinoLabel.Text = "Destino:";
            // 
            // panel1
            // 
            panel1.Controls.Add(AgregarThumbnailCKBX);
            panel1.Controls.Add(ObtenerDatosMusicaCKBX);
            panel1.Controls.Add(SoloAudioCKBX);
            panel1.Controls.Add(ComprimirVideoCKBX);
            panel1.Controls.Add(DescargarBTN);
            panel1.Controls.Add(VideosDescargadosLabel);
            panel1.Controls.Add(DetalleDescargaLabel);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(MostrarDetallesBTN);
            panel1.Controls.Add(ProgresoDescargaPB);
            panel1.Controls.Add(Estadolabel);
            panel1.Controls.Add(DestinoLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(887, 365);
            panel1.TabIndex = 4;
            // 
            // AgregarThumbnailCKBX
            // 
            AgregarThumbnailCKBX.AutoSize = true;
            AgregarThumbnailCKBX.Location = new Point(12, 34);
            AgregarThumbnailCKBX.Name = "AgregarThumbnailCKBX";
            AgregarThumbnailCKBX.Size = new Size(159, 24);
            AgregarThumbnailCKBX.TabIndex = 13;
            AgregarThumbnailCKBX.Text = "Agregar Thumbnail";
            AgregarThumbnailCKBX.UseVisualStyleBackColor = true;
            // 
            // ObtenerDatosMusicaCKBX
            // 
            ObtenerDatosMusicaCKBX.AutoSize = true;
            ObtenerDatosMusicaCKBX.Location = new Point(172, 73);
            ObtenerDatosMusicaCKBX.Name = "ObtenerDatosMusicaCKBX";
            ObtenerDatosMusicaCKBX.Size = new Size(199, 24);
            ObtenerDatosMusicaCKBX.TabIndex = 12;
            ObtenerDatosMusicaCKBX.Text = "Obtener Datos de Musica";
            ObtenerDatosMusicaCKBX.UseVisualStyleBackColor = true;
            ObtenerDatosMusicaCKBX.Visible = false;
            ObtenerDatosMusicaCKBX.CheckedChanged += ObtenerDatosMusicaCKBX_CheckedChanged;
            // 
            // SoloAudioCKBX
            // 
            SoloAudioCKBX.AutoSize = true;
            SoloAudioCKBX.Location = new Point(172, 34);
            SoloAudioCKBX.Name = "SoloAudioCKBX";
            SoloAudioCKBX.Size = new Size(105, 24);
            SoloAudioCKBX.TabIndex = 11;
            SoloAudioCKBX.Text = "Solo Audio";
            SoloAudioCKBX.UseVisualStyleBackColor = true;
            SoloAudioCKBX.CheckedChanged += SoloAudioCKBX_CheckedChanged;
            // 
            // ComprimirVideoCKBX
            // 
            ComprimirVideoCKBX.AutoSize = true;
            ComprimirVideoCKBX.Location = new Point(11, 73);
            ComprimirVideoCKBX.Name = "ComprimirVideoCKBX";
            ComprimirVideoCKBX.Size = new Size(155, 24);
            ComprimirVideoCKBX.TabIndex = 9;
            ComprimirVideoCKBX.Text = "Comprimir Videos ";
            ComprimirVideoCKBX.UseVisualStyleBackColor = true;
            // 
            // DescargarBTN
            // 
            DescargarBTN.Location = new Point(12, 120);
            DescargarBTN.Name = "DescargarBTN";
            DescargarBTN.Size = new Size(152, 29);
            DescargarBTN.TabIndex = 8;
            DescargarBTN.Text = "Comenzar Descarga";
            DescargarBTN.UseVisualStyleBackColor = true;
            DescargarBTN.Click += DescargarBTN_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(SalidaRTextBox);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 365);
            panel2.Name = "panel2";
            panel2.Size = new Size(887, 0);
            panel2.TabIndex = 5;
            // 
            // DescargaVideoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(887, 358);
            Controls.Add(panel2);
            Controls.Add(panel1);
            MinimumSize = new Size(905, 405);
            Name = "DescargaVideoForm";
            Text = "Descargando Videos";
            Load += DescargaVideoForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox SalidaRTextBox;
        private ProgressBar ProgresoDescargaPB;
        private Label label1;
        private Label DestinoLabel;
        private Label Estadolabel;
        private Button MostrarDetallesBTN;
        private Label DetalleDescargaLabel;
        private Label VideosDescargadosLabel;
        private Panel panel1;
        private Panel panel2;
        private Button DescargarBTN;
        private CheckBox ComprimirVideoCKBX;
        private CheckBox SoloAudioCKBX;
        private CheckBox ObtenerDatosMusicaCKBX;
        private CheckBox AgregarThumbnailCKBX;
    }
}