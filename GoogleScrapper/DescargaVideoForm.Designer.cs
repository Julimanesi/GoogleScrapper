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
            this.SalidaRTextBox = new System.Windows.Forms.RichTextBox();
            this.ProgresoDescargaPB = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.VideosDescargadosLabel = new System.Windows.Forms.Label();
            this.DetalleDescargaLabel = new System.Windows.Forms.Label();
            this.MostrarDetallesBTN = new System.Windows.Forms.Button();
            this.Estadolabel = new System.Windows.Forms.Label();
            this.DestinoLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SalidaRTextBox
            // 
            this.SalidaRTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SalidaRTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SalidaRTextBox.Location = new System.Drawing.Point(0, 0);
            this.SalidaRTextBox.Name = "SalidaRTextBox";
            this.SalidaRTextBox.ReadOnly = true;
            this.SalidaRTextBox.Size = new System.Drawing.Size(897, 169);
            this.SalidaRTextBox.TabIndex = 0;
            this.SalidaRTextBox.Text = "";
            this.SalidaRTextBox.Visible = false;
            // 
            // ProgresoDescargaPB
            // 
            this.ProgresoDescargaPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProgresoDescargaPB.Location = new System.Drawing.Point(190, 116);
            this.ProgresoDescargaPB.Name = "ProgresoDescargaPB";
            this.ProgresoDescargaPB.Size = new System.Drawing.Size(574, 20);
            this.ProgresoDescargaPB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 116);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Progreso de la descarga:";
            // 
            // VideosDescargadosLabel
            // 
            this.VideosDescargadosLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VideosDescargadosLabel.AutoSize = true;
            this.VideosDescargadosLabel.Location = new System.Drawing.Point(12, 24);
            this.VideosDescargadosLabel.Margin = new System.Windows.Forms.Padding(3);
            this.VideosDescargadosLabel.Name = "VideosDescargadosLabel";
            this.VideosDescargadosLabel.Size = new System.Drawing.Size(145, 20);
            this.VideosDescargadosLabel.TabIndex = 7;
            this.VideosDescargadosLabel.Text = "Videos descargados:";
            // 
            // DetalleDescargaLabel
            // 
            this.DetalleDescargaLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DetalleDescargaLabel.Location = new System.Drawing.Point(12, 142);
            this.DetalleDescargaLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DetalleDescargaLabel.Name = "DetalleDescargaLabel";
            this.DetalleDescargaLabel.Size = new System.Drawing.Size(752, 59);
            this.DetalleDescargaLabel.TabIndex = 6;
            this.DetalleDescargaLabel.Text = "Detalle Descarga:";
            // 
            // MostrarDetallesBTN
            // 
            this.MostrarDetallesBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MostrarDetallesBTN.Location = new System.Drawing.Point(12, 207);
            this.MostrarDetallesBTN.Name = "MostrarDetallesBTN";
            this.MostrarDetallesBTN.Size = new System.Drawing.Size(141, 29);
            this.MostrarDetallesBTN.TabIndex = 5;
            this.MostrarDetallesBTN.Text = "Mostrar Detalles";
            this.MostrarDetallesBTN.UseVisualStyleBackColor = true;
            this.MostrarDetallesBTN.Click += new System.EventHandler(this.MostrarDetallesBTN_Click);
            // 
            // Estadolabel
            // 
            this.Estadolabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Estadolabel.Location = new System.Drawing.Point(12, 50);
            this.Estadolabel.Margin = new System.Windows.Forms.Padding(3);
            this.Estadolabel.Name = "Estadolabel";
            this.Estadolabel.Size = new System.Drawing.Size(873, 43);
            this.Estadolabel.TabIndex = 4;
            this.Estadolabel.Text = "Estado:";
            // 
            // DestinoLabel
            // 
            this.DestinoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DestinoLabel.Location = new System.Drawing.Point(12, 90);
            this.DestinoLabel.Margin = new System.Windows.Forms.Padding(3);
            this.DestinoLabel.Name = "DestinoLabel";
            this.DestinoLabel.Size = new System.Drawing.Size(873, 20);
            this.DestinoLabel.TabIndex = 3;
            this.DestinoLabel.Text = "Destino:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.VideosDescargadosLabel);
            this.panel1.Controls.Add(this.DetalleDescargaLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.MostrarDetallesBTN);
            this.panel1.Controls.Add(this.ProgresoDescargaPB);
            this.panel1.Controls.Add(this.Estadolabel);
            this.panel1.Controls.Add(this.DestinoLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 260);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.SalidaRTextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 260);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(897, 169);
            this.panel2.TabIndex = 5;
            // 
            // DescargaVideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 429);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(915, 440);
            this.Name = "DescargaVideoForm";
            this.Text = "Descargando Videos";
            this.Load += new System.EventHandler(this.DescargaVideoForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

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
    }
}