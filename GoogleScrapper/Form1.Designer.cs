namespace GoogleScrapper
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.VideoTag = new System.Windows.Forms.TabPage();
            this.NumeroPagTotalesLabel = new System.Windows.Forms.Label();
            this.PaginasVisitadasLabel = new System.Windows.Forms.Label();
            this.VideoNumPagprogressBar = new System.Windows.Forms.ProgressBar();
            this.VideoPorPaginaLabel = new System.Windows.Forms.Label();
            this.VideoPorPaginaprogressBar = new System.Windows.Forms.ProgressBar();
            this.LinkVideosLB = new System.Windows.Forms.ListBox();
            this.resultadoVideoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.NumMinResultVideoNumeric = new System.Windows.Forms.NumericUpDown();
            this.FechaVideoCB = new System.Windows.Forms.ComboBox();
            this.DuracionVideoCB = new System.Windows.Forms.ComboBox();
            this.BuscarVideoBTN = new System.Windows.Forms.Button();
            this.SoloListaExtYtdlCB = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BuscarVideoTB = new System.Windows.Forms.TextBox();
            this.ImagenTag = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.VideoTag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultadoVideoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinResultVideoNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.VideoTag);
            this.tabControl1.Controls.Add(this.ImagenTag);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1174, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // VideoTag
            // 
            this.VideoTag.Controls.Add(this.NumeroPagTotalesLabel);
            this.VideoTag.Controls.Add(this.PaginasVisitadasLabel);
            this.VideoTag.Controls.Add(this.VideoNumPagprogressBar);
            this.VideoTag.Controls.Add(this.VideoPorPaginaLabel);
            this.VideoTag.Controls.Add(this.VideoPorPaginaprogressBar);
            this.VideoTag.Controls.Add(this.LinkVideosLB);
            this.VideoTag.Controls.Add(this.label2);
            this.VideoTag.Controls.Add(this.NumMinResultVideoNumeric);
            this.VideoTag.Controls.Add(this.FechaVideoCB);
            this.VideoTag.Controls.Add(this.DuracionVideoCB);
            this.VideoTag.Controls.Add(this.BuscarVideoBTN);
            this.VideoTag.Controls.Add(this.SoloListaExtYtdlCB);
            this.VideoTag.Controls.Add(this.label1);
            this.VideoTag.Controls.Add(this.BuscarVideoTB);
            this.VideoTag.Location = new System.Drawing.Point(4, 29);
            this.VideoTag.Name = "VideoTag";
            this.VideoTag.Padding = new System.Windows.Forms.Padding(3);
            this.VideoTag.Size = new System.Drawing.Size(1166, 417);
            this.VideoTag.TabIndex = 0;
            this.VideoTag.Text = "Video";
            this.VideoTag.UseVisualStyleBackColor = true;
            // 
            // NumeroPagTotalesLabel
            // 
            this.NumeroPagTotalesLabel.AutoSize = true;
            this.NumeroPagTotalesLabel.Location = new System.Drawing.Point(667, 67);
            this.NumeroPagTotalesLabel.Name = "NumeroPagTotalesLabel";
            this.NumeroPagTotalesLabel.Size = new System.Drawing.Size(226, 20);
            this.NumeroPagTotalesLabel.TabIndex = 14;
            this.NumeroPagTotalesLabel.Text = "Numero de Paginas encontradas:";
            // 
            // PaginasVisitadasLabel
            // 
            this.PaginasVisitadasLabel.AutoSize = true;
            this.PaginasVisitadasLabel.Location = new System.Drawing.Point(537, 105);
            this.PaginasVisitadasLabel.Name = "PaginasVisitadasLabel";
            this.PaginasVisitadasLabel.Size = new System.Drawing.Size(125, 20);
            this.PaginasVisitadasLabel.TabIndex = 13;
            this.PaginasVisitadasLabel.Text = "Paginas Visitadas:";
            // 
            // VideoNumPagprogressBar
            // 
            this.VideoNumPagprogressBar.Location = new System.Drawing.Point(687, 101);
            this.VideoNumPagprogressBar.Name = "VideoNumPagprogressBar";
            this.VideoNumPagprogressBar.Size = new System.Drawing.Size(471, 28);
            this.VideoNumPagprogressBar.Step = 1;
            this.VideoNumPagprogressBar.TabIndex = 12;
            // 
            // VideoPorPaginaLabel
            // 
            this.VideoPorPaginaLabel.AutoSize = true;
            this.VideoPorPaginaLabel.Location = new System.Drawing.Point(117, 105);
            this.VideoPorPaginaLabel.Name = "VideoPorPaginaLabel";
            this.VideoPorPaginaLabel.Size = new System.Drawing.Size(130, 20);
            this.VideoPorPaginaLabel.TabIndex = 11;
            this.VideoPorPaginaLabel.Text = "Videos Por Pagina:";
            // 
            // VideoPorPaginaprogressBar
            // 
            this.VideoPorPaginaprogressBar.Location = new System.Drawing.Point(277, 105);
            this.VideoPorPaginaprogressBar.Maximum = 10;
            this.VideoPorPaginaprogressBar.Name = "VideoPorPaginaprogressBar";
            this.VideoPorPaginaprogressBar.Size = new System.Drawing.Size(254, 25);
            this.VideoPorPaginaprogressBar.Step = 1;
            this.VideoPorPaginaprogressBar.TabIndex = 10;
            // 
            // LinkVideosLB
            // 
            this.LinkVideosLB.DataSource = this.resultadoVideoBindingSource;
            this.LinkVideosLB.DisplayMember = "Title";
            this.LinkVideosLB.FormattingEnabled = true;
            this.LinkVideosLB.ItemHeight = 20;
            this.LinkVideosLB.Location = new System.Drawing.Point(6, 136);
            this.LinkVideosLB.Name = "LinkVideosLB";
            this.LinkVideosLB.Size = new System.Drawing.Size(1152, 264);
            this.LinkVideosLB.TabIndex = 9;
            this.LinkVideosLB.ValueMember = "URLVideo";
            this.LinkVideosLB.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // resultadoVideoBindingSource
            // 
            this.resultadoVideoBindingSource.DataSource = typeof(GoogleScrapper.ResultadoVideo);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(727, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nro minimo de resultados:";
            // 
            // NumMinResultVideoNumeric
            // 
            this.NumMinResultVideoNumeric.Location = new System.Drawing.Point(918, 14);
            this.NumMinResultVideoNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumMinResultVideoNumeric.Name = "NumMinResultVideoNumeric";
            this.NumMinResultVideoNumeric.Size = new System.Drawing.Size(79, 27);
            this.NumMinResultVideoNumeric.TabIndex = 6;
            this.NumMinResultVideoNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FechaVideoCB
            // 
            this.FechaVideoCB.FormattingEnabled = true;
            this.FechaVideoCB.Items.AddRange(new object[] {
            "De Cualquier Fecha",
            "Ultima Hora",
            "Ultimas 24 Horas",
            "Ultima Semana",
            "Ultimo Mes",
            "Ultimo Año",
            "Personalizar"});
            this.FechaVideoCB.Location = new System.Drawing.Point(391, 59);
            this.FechaVideoCB.Name = "FechaVideoCB";
            this.FechaVideoCB.Size = new System.Drawing.Size(252, 28);
            this.FechaVideoCB.TabIndex = 5;
            this.FechaVideoCB.Text = "Fecha";
            // 
            // DuracionVideoCB
            // 
            this.DuracionVideoCB.FormattingEnabled = true;
            this.DuracionVideoCB.Items.AddRange(new object[] {
            "Todas las duraciones",
            "Corto (de 0 a 4 min)",
            "Mediana (de 4 a 20 min)",
            "Larga (20 min o más)"});
            this.DuracionVideoCB.Location = new System.Drawing.Point(64, 59);
            this.DuracionVideoCB.Name = "DuracionVideoCB";
            this.DuracionVideoCB.Size = new System.Drawing.Size(264, 28);
            this.DuracionVideoCB.TabIndex = 4;
            this.DuracionVideoCB.Text = "Duracion";
            // 
            // BuscarVideoBTN
            // 
            this.BuscarVideoBTN.Location = new System.Drawing.Point(8, 101);
            this.BuscarVideoBTN.Name = "BuscarVideoBTN";
            this.BuscarVideoBTN.Size = new System.Drawing.Size(94, 29);
            this.BuscarVideoBTN.TabIndex = 3;
            this.BuscarVideoBTN.Text = "Buscar";
            this.BuscarVideoBTN.UseVisualStyleBackColor = true;
            this.BuscarVideoBTN.Click += new System.EventHandler(this.BuscarVideoBTN_Click);
            // 
            // SoloListaExtYtdlCB
            // 
            this.SoloListaExtYtdlCB.AutoSize = true;
            this.SoloListaExtYtdlCB.Checked = true;
            this.SoloListaExtYtdlCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SoloListaExtYtdlCB.Location = new System.Drawing.Point(475, 14);
            this.SoloListaExtYtdlCB.Name = "SoloListaExtYtdlCB";
            this.SoloListaExtYtdlCB.Size = new System.Drawing.Size(207, 24);
            this.SoloListaExtYtdlCB.TabIndex = 2;
            this.SoloListaExtYtdlCB.Text = "Solo Lista Extractores yt-dl";
            this.SoloListaExtYtdlCB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar:";
            // 
            // BuscarVideoTB
            // 
            this.BuscarVideoTB.Location = new System.Drawing.Point(64, 12);
            this.BuscarVideoTB.Name = "BuscarVideoTB";
            this.BuscarVideoTB.Size = new System.Drawing.Size(326, 27);
            this.BuscarVideoTB.TabIndex = 0;
            // 
            // ImagenTag
            // 
            this.ImagenTag.Location = new System.Drawing.Point(4, 29);
            this.ImagenTag.Name = "ImagenTag";
            this.ImagenTag.Padding = new System.Windows.Forms.Padding(3);
            this.ImagenTag.Size = new System.Drawing.Size(1166, 417);
            this.ImagenTag.TabIndex = 1;
            this.ImagenTag.Text = "Imagen";
            this.ImagenTag.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.VideoTag.ResumeLayout(false);
            this.VideoTag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultadoVideoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinResultVideoNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage VideoTag;
        private TabPage ImagenTag;
        private ComboBox DuracionVideoCB;
        private Button BuscarVideoBTN;
        private CheckBox SoloListaExtYtdlCB;
        private Label label1;
        private TextBox BuscarVideoTB;
        private ComboBox FechaVideoCB;
        private Label label2;
        private NumericUpDown NumMinResultVideoNumeric;
        private ListBox LinkVideosLB;
        private BindingSource resultadoVideoBindingSource;
        private ProgressBar VideoPorPaginaprogressBar;
        private Label PaginasVisitadasLabel;
        private ProgressBar VideoNumPagprogressBar;
        private Label VideoPorPaginaLabel;
        private Label NumeroPagTotalesLabel;
    }
}