namespace GoogleScrapper
{
    partial class EdicionVideosForm
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
            panel1 = new Panel();
            URLExtraLB = new Label();
            URLExtraTXBX = new TextBox();
            ParametrosLB = new Label();
            AccionLB = new Label();
            ProgresoTotalPB = new ProgressBar();
            EditarBTN = new Button();
            VideosEditadosLabel = new Label();
            DetalleDescargaLabel = new Label();
            label1 = new Label();
            MostrarDetallesBTN = new Button();
            ProgresoDescargaPB = new ProgressBar();
            Estadolabel = new Label();
            DestinoLabel = new Label();
            SalidaRTextBox = new RichTextBox();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(URLExtraLB);
            panel1.Controls.Add(URLExtraTXBX);
            panel1.Controls.Add(ParametrosLB);
            panel1.Controls.Add(AccionLB);
            panel1.Controls.Add(ProgresoTotalPB);
            panel1.Controls.Add(EditarBTN);
            panel1.Controls.Add(VideosEditadosLabel);
            panel1.Controls.Add(DetalleDescargaLabel);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(MostrarDetallesBTN);
            panel1.Controls.Add(ProgresoDescargaPB);
            panel1.Controls.Add(Estadolabel);
            panel1.Controls.Add(DestinoLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 365);
            panel1.TabIndex = 6;
            panel1.Paint += panel1_Paint;
            // 
            // URLExtraLB
            // 
            URLExtraLB.AutoSize = true;
            URLExtraLB.Location = new Point(166, 9);
            URLExtraLB.Name = "URLExtraLB";
            URLExtraLB.Size = new Size(68, 20);
            URLExtraLB.TabIndex = 18;
            URLExtraLB.Text = "URLExtra";
            URLExtraLB.Visible = false;
            // 
            // URLExtraTXBX
            // 
            URLExtraTXBX.Location = new Point(326, 9);
            URLExtraTXBX.Name = "URLExtraTXBX";
            URLExtraTXBX.Size = new Size(462, 27);
            URLExtraTXBX.TabIndex = 17;
            URLExtraTXBX.Visible = false;
            // 
            // ParametrosLB
            // 
            ParametrosLB.AutoSize = true;
            ParametrosLB.Location = new Point(12, 9);
            ParametrosLB.Name = "ParametrosLB";
            ParametrosLB.Size = new Size(86, 20);
            ParametrosLB.TabIndex = 16;
            ParametrosLB.Text = "Parametros:";
            // 
            // AccionLB
            // 
            AccionLB.AutoSize = true;
            AccionLB.Location = new Point(12, 97);
            AccionLB.Name = "AccionLB";
            AccionLB.Size = new Size(57, 20);
            AccionLB.TabIndex = 15;
            AccionLB.Text = "Accion:";
            // 
            // ProgresoTotalPB
            // 
            ProgresoTotalPB.Location = new Point(213, 143);
            ProgresoTotalPB.Name = "ProgresoTotalPB";
            ProgresoTotalPB.Size = new Size(575, 20);
            ProgresoTotalPB.TabIndex = 14;
            // 
            // EditarBTN
            // 
            EditarBTN.Location = new Point(213, 93);
            EditarBTN.Name = "EditarBTN";
            EditarBTN.Size = new Size(152, 29);
            EditarBTN.TabIndex = 8;
            EditarBTN.Text = "Comenzar Edicion";
            EditarBTN.UseVisualStyleBackColor = true;
            EditarBTN.Click += EditarBTN_Click;
            // 
            // VideosEditadosLabel
            // 
            VideosEditadosLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            VideosEditadosLabel.AutoSize = true;
            VideosEditadosLabel.Location = new Point(12, 143);
            VideosEditadosLabel.Margin = new Padding(3);
            VideosEditadosLabel.Name = "VideosEditadosLabel";
            VideosEditadosLabel.Size = new Size(119, 20);
            VideosEditadosLabel.TabIndex = 7;
            VideosEditadosLabel.Text = "Videos Editados:";
            // 
            // DetalleDescargaLabel
            // 
            DetalleDescargaLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DetalleDescargaLabel.Location = new Point(12, 261);
            DetalleDescargaLabel.Margin = new Padding(3);
            DetalleDescargaLabel.Name = "DetalleDescargaLabel";
            DetalleDescargaLabel.Size = new Size(785, 59);
            DetalleDescargaLabel.TabIndex = 6;
            DetalleDescargaLabel.Text = "Detalle de la Edicion:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(12, 235);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(161, 20);
            label1.TabIndex = 2;
            label1.Text = "Progreso de la Edicion:";
            // 
            // MostrarDetallesBTN
            // 
            MostrarDetallesBTN.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            MostrarDetallesBTN.Location = new Point(12, 326);
            MostrarDetallesBTN.Name = "MostrarDetallesBTN";
            MostrarDetallesBTN.Size = new Size(141, 29);
            MostrarDetallesBTN.TabIndex = 5;
            MostrarDetallesBTN.Text = "Mostrar Detalles";
            MostrarDetallesBTN.UseVisualStyleBackColor = true;
            MostrarDetallesBTN.Click += MostrarDetallesBTN_Click;
            // 
            // ProgresoDescargaPB
            // 
            ProgresoDescargaPB.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ProgresoDescargaPB.Location = new Point(213, 235);
            ProgresoDescargaPB.Name = "ProgresoDescargaPB";
            ProgresoDescargaPB.Size = new Size(575, 20);
            ProgresoDescargaPB.TabIndex = 1;
            // 
            // Estadolabel
            // 
            Estadolabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Estadolabel.Location = new Point(12, 169);
            Estadolabel.Margin = new Padding(3);
            Estadolabel.Name = "Estadolabel";
            Estadolabel.Size = new Size(785, 43);
            Estadolabel.TabIndex = 4;
            Estadolabel.Text = "Estado:";
            // 
            // DestinoLabel
            // 
            DestinoLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DestinoLabel.Location = new Point(12, 209);
            DestinoLabel.Margin = new Padding(3);
            DestinoLabel.Name = "DestinoLabel";
            DestinoLabel.Size = new Size(785, 20);
            DestinoLabel.TabIndex = 3;
            DestinoLabel.Text = "Destino:";
            // 
            // SalidaRTextBox
            // 
            SalidaRTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SalidaRTextBox.BackColor = SystemColors.ControlLightLight;
            SalidaRTextBox.Location = new Point(0, 371);
            SalidaRTextBox.Name = "SalidaRTextBox";
            SalidaRTextBox.ReadOnly = true;
            SalidaRTextBox.Size = new Size(800, 173);
            SalidaRTextBox.TabIndex = 0;
            SalidaRTextBox.Text = "";
            SalidaRTextBox.Visible = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(SalidaRTextBox);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 539);
            panel2.TabIndex = 7;
            // 
            // EdicionVideosForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 539);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "EdicionVideosForm";
            Text = "EdicionVideosForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ProgressBar ProgresoTotalPB;
        private CheckBox AgregarThumbnailCKBX;
        private CheckBox ObtenerDatosMusicaCKBX;
        private CheckBox SoloAudioCKBX;
        private CheckBox ComprimirVideoCKBX;
        private Button DescargarBTN;
        private Label VideosEditadosLabel;
        private Label DetalleDescargaLabel;
        private Label label1;
        private Button MostrarDetallesBTN;
        private ProgressBar ProgresoDescargaPB;
        private Label Estadolabel;
        private Label DestinoLabel;
        private RichTextBox SalidaRTextBox;
        private Panel panel2;
        private Label AccionLB;
        private Label ParametrosLB;
        private Button EditarBTN;
        private Label URLExtraLB;
        private TextBox URLExtraTXBX;
    }
}