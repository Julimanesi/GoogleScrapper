﻿namespace GoogleScrapper
{
    partial class MainForm
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
            this.panelResultado = new System.Windows.Forms.Panel();
            this.DescargVideosBTN = new System.Windows.Forms.Button();
            this.SeleccionarTodosBTN = new System.Windows.Forms.Button();
            this.ResultadosTotalesLabel = new System.Windows.Forms.Label();
            this.AgregarEnviarSMplayerBTN = new System.Windows.Forms.Button();
            this.panelProgreso = new System.Windows.Forms.Panel();
            this.ResultadosActualesProgressBar = new System.Windows.Forms.ProgressBar();
            this.NumResultadosActulabel = new System.Windows.Forms.Label();
            this.VideoNumPagprogressBar = new System.Windows.Forms.ProgressBar();
            this.PaginasVisitadasLabel = new System.Windows.Forms.Label();
            this.NumeroPagTotalesLabel = new System.Windows.Forms.Label();
            this.LinkVideosLB = new System.Windows.Forms.ListBox();
            this.resultadoVideoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.FechaFinlabel = new System.Windows.Forms.Label();
            this.FechaIniciolabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BuscarVideoTB = new System.Windows.Forms.TextBox();
            this.SoloListaExtYtdlCKBX = new System.Windows.Forms.CheckBox();
            this.FechaFinDTP = new System.Windows.Forms.DateTimePicker();
            this.BuscarVideoBTN = new System.Windows.Forms.Button();
            this.FechaInicioDTP = new System.Windows.Forms.DateTimePicker();
            this.DuracionVideoCOMBX = new System.Windows.Forms.ComboBox();
            this.AltaCalidadCKBX = new System.Windows.Forms.CheckBox();
            this.FechaVideoCOMBX = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NumMinResultVideoNM = new System.Windows.Forms.NumericUpDown();
            this.ImagenTag = new System.Windows.Forms.TabPage();
            this.VerificarVideosbackgrWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.VideoTag.SuspendLayout();
            this.panelResultado.SuspendLayout();
            this.panelProgreso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultadoVideoBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinResultVideoNM)).BeginInit();
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
            this.tabControl1.Size = new System.Drawing.Size(1179, 662);
            this.tabControl1.TabIndex = 0;
            // 
            // VideoTag
            // 
            this.VideoTag.Controls.Add(this.panelResultado);
            this.VideoTag.Controls.Add(this.panelProgreso);
            this.VideoTag.Controls.Add(this.LinkVideosLB);
            this.VideoTag.Controls.Add(this.panel1);
            this.VideoTag.Location = new System.Drawing.Point(4, 29);
            this.VideoTag.Name = "VideoTag";
            this.VideoTag.Padding = new System.Windows.Forms.Padding(3);
            this.VideoTag.Size = new System.Drawing.Size(1171, 629);
            this.VideoTag.TabIndex = 0;
            this.VideoTag.Text = "Video";
            this.VideoTag.UseVisualStyleBackColor = true;
            // 
            // panelResultado
            // 
            this.panelResultado.Controls.Add(this.DescargVideosBTN);
            this.panelResultado.Controls.Add(this.SeleccionarTodosBTN);
            this.panelResultado.Controls.Add(this.ResultadosTotalesLabel);
            this.panelResultado.Controls.Add(this.AgregarEnviarSMplayerBTN);
            this.panelResultado.Location = new System.Drawing.Point(3, 237);
            this.panelResultado.Name = "panelResultado";
            this.panelResultado.Size = new System.Drawing.Size(1165, 47);
            this.panelResultado.TabIndex = 20;
            this.panelResultado.Visible = false;
            // 
            // DescargVideosBTN
            // 
            this.DescargVideosBTN.Location = new System.Drawing.Point(367, 7);
            this.DescargVideosBTN.Name = "DescargVideosBTN";
            this.DescargVideosBTN.Size = new System.Drawing.Size(244, 29);
            this.DescargVideosBTN.TabIndex = 18;
            this.DescargVideosBTN.Text = "Descargar Videos Seleccionados";
            this.DescargVideosBTN.UseVisualStyleBackColor = true;
            this.DescargVideosBTN.Click += new System.EventHandler(this.DescargVideosBTN_Click);
            // 
            // SeleccionarTodosBTN
            // 
            this.SeleccionarTodosBTN.Location = new System.Drawing.Point(673, 7);
            this.SeleccionarTodosBTN.Name = "SeleccionarTodosBTN";
            this.SeleccionarTodosBTN.Size = new System.Drawing.Size(163, 29);
            this.SeleccionarTodosBTN.TabIndex = 17;
            this.SeleccionarTodosBTN.Text = "Seleccionar Todos";
            this.SeleccionarTodosBTN.UseVisualStyleBackColor = true;
            this.SeleccionarTodosBTN.Click += new System.EventHandler(this.SeleccionarTodosButton_Click);
            // 
            // ResultadosTotalesLabel
            // 
            this.ResultadosTotalesLabel.AutoSize = true;
            this.ResultadosTotalesLabel.Location = new System.Drawing.Point(937, 11);
            this.ResultadosTotalesLabel.Name = "ResultadosTotalesLabel";
            this.ResultadosTotalesLabel.Size = new System.Drawing.Size(131, 20);
            this.ResultadosTotalesLabel.TabIndex = 16;
            this.ResultadosTotalesLabel.Text = "ResultadosTotales:";
            // 
            // AgregarEnviarSMplayerBTN
            // 
            this.AgregarEnviarSMplayerBTN.Location = new System.Drawing.Point(5, 6);
            this.AgregarEnviarSMplayerBTN.Name = "AgregarEnviarSMplayerBTN";
            this.AgregarEnviarSMplayerBTN.Size = new System.Drawing.Size(301, 30);
            this.AgregarEnviarSMplayerBTN.TabIndex = 15;
            this.AgregarEnviarSMplayerBTN.Text = "Agregar/Enviar seleccionados a Smplayer";
            this.AgregarEnviarSMplayerBTN.UseVisualStyleBackColor = true;
            this.AgregarEnviarSMplayerBTN.Click += new System.EventHandler(this.AgregarEnviarSmplayer_Click);
            // 
            // panelProgreso
            // 
            this.panelProgreso.Controls.Add(this.ResultadosActualesProgressBar);
            this.panelProgreso.Controls.Add(this.NumResultadosActulabel);
            this.panelProgreso.Controls.Add(this.VideoNumPagprogressBar);
            this.panelProgreso.Controls.Add(this.PaginasVisitadasLabel);
            this.panelProgreso.Controls.Add(this.NumeroPagTotalesLabel);
            this.panelProgreso.Location = new System.Drawing.Point(3, 147);
            this.panelProgreso.Name = "panelProgreso";
            this.panelProgreso.Size = new System.Drawing.Size(1165, 84);
            this.panelProgreso.TabIndex = 19;
            this.panelProgreso.Visible = false;
            // 
            // ResultadosActualesProgressBar
            // 
            this.ResultadosActualesProgressBar.Location = new System.Drawing.Point(255, 39);
            this.ResultadosActualesProgressBar.Name = "ResultadosActualesProgressBar";
            this.ResultadosActualesProgressBar.Size = new System.Drawing.Size(289, 29);
            this.ResultadosActualesProgressBar.TabIndex = 16;
            // 
            // NumResultadosActulabel
            // 
            this.NumResultadosActulabel.AutoSize = true;
            this.NumResultadosActulabel.Location = new System.Drawing.Point(5, 44);
            this.NumResultadosActulabel.Name = "NumResultadosActulabel";
            this.NumResultadosActulabel.Size = new System.Drawing.Size(223, 20);
            this.NumResultadosActulabel.TabIndex = 15;
            this.NumResultadosActulabel.Text = "Numero de Resultados Actuales:";
            // 
            // VideoNumPagprogressBar
            // 
            this.VideoNumPagprogressBar.Location = new System.Drawing.Point(699, 40);
            this.VideoNumPagprogressBar.Name = "VideoNumPagprogressBar";
            this.VideoNumPagprogressBar.Size = new System.Drawing.Size(454, 28);
            this.VideoNumPagprogressBar.Step = 1;
            this.VideoNumPagprogressBar.TabIndex = 12;
            // 
            // PaginasVisitadasLabel
            // 
            this.PaginasVisitadasLabel.AutoSize = true;
            this.PaginasVisitadasLabel.Location = new System.Drawing.Point(541, 44);
            this.PaginasVisitadasLabel.Name = "PaginasVisitadasLabel";
            this.PaginasVisitadasLabel.Size = new System.Drawing.Size(125, 20);
            this.PaginasVisitadasLabel.TabIndex = 13;
            this.PaginasVisitadasLabel.Text = "Paginas Visitadas:";
            // 
            // NumeroPagTotalesLabel
            // 
            this.NumeroPagTotalesLabel.AutoSize = true;
            this.NumeroPagTotalesLabel.Location = new System.Drawing.Point(5, 12);
            this.NumeroPagTotalesLabel.Name = "NumeroPagTotalesLabel";
            this.NumeroPagTotalesLabel.Size = new System.Drawing.Size(226, 20);
            this.NumeroPagTotalesLabel.TabIndex = 14;
            this.NumeroPagTotalesLabel.Text = "Numero de Paginas encontradas:";
            // 
            // LinkVideosLB
            // 
            this.LinkVideosLB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkVideosLB.DataSource = this.resultadoVideoBindingSource;
            this.LinkVideosLB.DisplayMember = "Title";
            this.LinkVideosLB.FormattingEnabled = true;
            this.LinkVideosLB.HorizontalScrollbar = true;
            this.LinkVideosLB.ItemHeight = 20;
            this.LinkVideosLB.Location = new System.Drawing.Point(3, 282);
            this.LinkVideosLB.Name = "LinkVideosLB";
            this.LinkVideosLB.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LinkVideosLB.Size = new System.Drawing.Size(1165, 344);
            this.LinkVideosLB.TabIndex = 9;
            this.LinkVideosLB.ValueMember = "URLVideo";
            this.LinkVideosLB.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.LinkVideosLB.DoubleClick += new System.EventHandler(this.LinVideoDoubleClick);
            // 
            // resultadoVideoBindingSource
            // 
            this.resultadoVideoBindingSource.DataSource = typeof(GoogleScrapper.ResultadoVideo);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FechaFinlabel);
            this.panel1.Controls.Add(this.FechaIniciolabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.BuscarVideoTB);
            this.panel1.Controls.Add(this.SoloListaExtYtdlCKBX);
            this.panel1.Controls.Add(this.FechaFinDTP);
            this.panel1.Controls.Add(this.BuscarVideoBTN);
            this.panel1.Controls.Add(this.FechaInicioDTP);
            this.panel1.Controls.Add(this.DuracionVideoCOMBX);
            this.panel1.Controls.Add(this.AltaCalidadCKBX);
            this.panel1.Controls.Add(this.FechaVideoCOMBX);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.NumMinResultVideoNM);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1165, 144);
            this.panel1.TabIndex = 21;
            // 
            // FechaFinlabel
            // 
            this.FechaFinlabel.AutoSize = true;
            this.FechaFinlabel.Location = new System.Drawing.Point(678, 107);
            this.FechaFinlabel.Name = "FechaFinlabel";
            this.FechaFinlabel.Size = new System.Drawing.Size(73, 20);
            this.FechaFinlabel.TabIndex = 20;
            this.FechaFinlabel.Text = "Fecha Fin:";
            this.FechaFinlabel.Visible = false;
            // 
            // FechaIniciolabel
            // 
            this.FechaIniciolabel.AutoSize = true;
            this.FechaIniciolabel.Location = new System.Drawing.Point(168, 110);
            this.FechaIniciolabel.Name = "FechaIniciolabel";
            this.FechaIniciolabel.Size = new System.Drawing.Size(90, 20);
            this.FechaIniciolabel.TabIndex = 19;
            this.FechaIniciolabel.Text = "Fecha Inicio:";
            this.FechaIniciolabel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar:";
            // 
            // BuscarVideoTB
            // 
            this.BuscarVideoTB.Location = new System.Drawing.Point(61, 13);
            this.BuscarVideoTB.Name = "BuscarVideoTB";
            this.BuscarVideoTB.Size = new System.Drawing.Size(550, 27);
            this.BuscarVideoTB.TabIndex = 0;
            // 
            // SoloListaExtYtdlCKBX
            // 
            this.SoloListaExtYtdlCKBX.AutoSize = true;
            this.SoloListaExtYtdlCKBX.Checked = true;
            this.SoloListaExtYtdlCKBX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SoloListaExtYtdlCKBX.Location = new System.Drawing.Point(631, 15);
            this.SoloListaExtYtdlCKBX.Name = "SoloListaExtYtdlCKBX";
            this.SoloListaExtYtdlCKBX.Size = new System.Drawing.Size(207, 24);
            this.SoloListaExtYtdlCKBX.TabIndex = 2;
            this.SoloListaExtYtdlCKBX.Text = "Solo Lista Extractores yt-dl";
            this.SoloListaExtYtdlCKBX.UseVisualStyleBackColor = true;
            // 
            // FechaFinDTP
            // 
            this.FechaFinDTP.Location = new System.Drawing.Point(757, 102);
            this.FechaFinDTP.Name = "FechaFinDTP";
            this.FechaFinDTP.Size = new System.Drawing.Size(295, 27);
            this.FechaFinDTP.TabIndex = 18;
            this.FechaFinDTP.Visible = false;
            // 
            // BuscarVideoBTN
            // 
            this.BuscarVideoBTN.Location = new System.Drawing.Point(5, 103);
            this.BuscarVideoBTN.Name = "BuscarVideoBTN";
            this.BuscarVideoBTN.Size = new System.Drawing.Size(94, 29);
            this.BuscarVideoBTN.TabIndex = 3;
            this.BuscarVideoBTN.Text = "Buscar";
            this.BuscarVideoBTN.UseVisualStyleBackColor = true;
            this.BuscarVideoBTN.Click += new System.EventHandler(this.BuscarVideoBTN_Click);
            // 
            // FechaInicioDTP
            // 
            this.FechaInicioDTP.Location = new System.Drawing.Point(264, 105);
            this.FechaInicioDTP.Name = "FechaInicioDTP";
            this.FechaInicioDTP.Size = new System.Drawing.Size(293, 27);
            this.FechaInicioDTP.TabIndex = 17;
            this.FechaInicioDTP.Visible = false;
            // 
            // DuracionVideoCOMBX
            // 
            this.DuracionVideoCOMBX.FormattingEnabled = true;
            this.DuracionVideoCOMBX.Items.AddRange(new object[] {
            "Todas las duraciones",
            "Corto (de 0 a 4 min)",
            "Mediana (de 4 a 20 min)",
            "Larga (20 min o más)"});
            this.DuracionVideoCOMBX.Location = new System.Drawing.Point(61, 60);
            this.DuracionVideoCOMBX.Name = "DuracionVideoCOMBX";
            this.DuracionVideoCOMBX.Size = new System.Drawing.Size(264, 28);
            this.DuracionVideoCOMBX.TabIndex = 4;
            this.DuracionVideoCOMBX.Text = "Duracion";
            // 
            // AltaCalidadCKBX
            // 
            this.AltaCalidadCKBX.AutoSize = true;
            this.AltaCalidadCKBX.Location = new System.Drawing.Point(673, 62);
            this.AltaCalidadCKBX.Name = "AltaCalidadCKBX";
            this.AltaCalidadCKBX.Size = new System.Drawing.Size(113, 24);
            this.AltaCalidadCKBX.TabIndex = 16;
            this.AltaCalidadCKBX.Text = "Alta Calidad";
            this.AltaCalidadCKBX.UseVisualStyleBackColor = true;
            // 
            // FechaVideoCOMBX
            // 
            this.FechaVideoCOMBX.FormattingEnabled = true;
            this.FechaVideoCOMBX.Items.AddRange(new object[] {
            "De Cualquier Fecha",
            "Ultima Hora",
            "Ultimas 24 Horas",
            "Ultima Semana",
            "Ultimo Mes",
            "Ultimo Año",
            "Personalizar"});
            this.FechaVideoCOMBX.Location = new System.Drawing.Point(388, 60);
            this.FechaVideoCOMBX.Name = "FechaVideoCOMBX";
            this.FechaVideoCOMBX.Size = new System.Drawing.Size(252, 28);
            this.FechaVideoCOMBX.TabIndex = 5;
            this.FechaVideoCOMBX.Text = "Fecha";
            this.FechaVideoCOMBX.SelectedIndexChanged += new System.EventHandler(this.FechaVideoCB_Changed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(883, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nro minimo de resultados:";
            // 
            // NumMinResultVideoNM
            // 
            this.NumMinResultVideoNM.Location = new System.Drawing.Point(1074, 15);
            this.NumMinResultVideoNM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumMinResultVideoNM.Name = "NumMinResultVideoNM";
            this.NumMinResultVideoNM.Size = new System.Drawing.Size(79, 27);
            this.NumMinResultVideoNM.TabIndex = 6;
            this.NumMinResultVideoNM.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ImagenTag
            // 
            this.ImagenTag.Location = new System.Drawing.Point(4, 29);
            this.ImagenTag.Name = "ImagenTag";
            this.ImagenTag.Padding = new System.Windows.Forms.Padding(3);
            this.ImagenTag.Size = new System.Drawing.Size(1171, 629);
            this.ImagenTag.TabIndex = 1;
            this.ImagenTag.Text = "Imagen";
            this.ImagenTag.UseVisualStyleBackColor = true;
            // 
            // VerificarVideosbackgrWorker
            // 
            this.VerificarVideosbackgrWorker.WorkerReportsProgress = true;
            this.VerificarVideosbackgrWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWVerificarVideo_Dowork);
            this.VerificarVideosbackgrWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BWVerificarVideo_Progreso);
            this.VerificarVideosbackgrWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BWVerificarVideo_Resultado);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 662);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(1197, 709);
            this.Name = "MainForm";
            this.Text = "Multimedia Scrapper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.VideoTag.ResumeLayout(false);
            this.panelResultado.ResumeLayout(false);
            this.panelResultado.PerformLayout();
            this.panelProgreso.ResumeLayout(false);
            this.panelProgreso.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultadoVideoBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinResultVideoNM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage VideoTag;
        private TabPage ImagenTag;
        private ComboBox DuracionVideoCOMBX;
        private Button BuscarVideoBTN;
        private CheckBox SoloListaExtYtdlCKBX;
        private Label label1;
        private TextBox BuscarVideoTB;
        private ComboBox FechaVideoCOMBX;
        private Label label2;
        private NumericUpDown NumMinResultVideoNM;
        private ListBox LinkVideosLB;
        private BindingSource resultadoVideoBindingSource;
        private Label PaginasVisitadasLabel;
        private ProgressBar VideoNumPagprogressBar;
        private Label NumeroPagTotalesLabel;
        private Button AgregarEnviarSMplayerBTN;
        private CheckBox AltaCalidadCKBX;
        private DateTimePicker FechaFinDTP;
        private DateTimePicker FechaInicioDTP;
        private Panel panelResultado;
        private Panel panelProgreso;
        private Label ResultadosTotalesLabel;
        private Panel panel1;
        private Label FechaFinlabel;
        private Label FechaIniciolabel;
        private Button SeleccionarTodosBTN;
        private Label NumResultadosActulabel;
        private System.ComponentModel.BackgroundWorker VerificarVideosbackgrWorker;
        private ProgressBar ResultadosActualesProgressBar;
        private Button DescargVideosBTN;
    }
}