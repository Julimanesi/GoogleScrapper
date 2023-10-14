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
            components = new System.ComponentModel.Container();
            tabControl1 = new TabControl();
            VideoTag = new TabPage();
            panelResultado = new Panel();
            DescargVideosBTN = new Button();
            SeleccionarTodosBTN = new Button();
            ResultadosTotalesLabel = new Label();
            AgregarEnviarSMplayerBTN = new Button();
            panelProgreso = new Panel();
            ResultadosActualesProgressBar = new ProgressBar();
            NumResultadosActulabel = new Label();
            VideoNumPagprogressBar = new ProgressBar();
            PaginasVisitadasLabel = new Label();
            NumeroPagTotalesLabel = new Label();
            LinkVideosLB = new ListBox();
            resultadoVideoBindingSource = new BindingSource(components);
            panel1 = new Panel();
            FechaFinlabel = new Label();
            FechaIniciolabel = new Label();
            label1 = new Label();
            BuscarVideoTB = new TextBox();
            SoloListaExtYtdlCKBX = new CheckBox();
            FechaFinDTP = new DateTimePicker();
            BuscarVideoBTN = new Button();
            FechaInicioDTP = new DateTimePicker();
            DuracionVideoCOMBX = new ComboBox();
            AltaCalidadCKBX = new CheckBox();
            FechaVideoCOMBX = new ComboBox();
            label2 = new Label();
            NumMinResultVideoNM = new NumericUpDown();
            YoutubeTag = new TabPage();
            ResultadosYouTubeFlowLayPanel = new FlowLayoutPanel();
            BotoneraYoutube = new Panel();
            ResultadosPorPaginaYouTbLabel = new Label();
            GuardarResultadosBTN = new Button();
            ObtenerVideosCanalBTN = new Button();
            DescargVideosYouTbBTN = new Button();
            SeleccionarTodosYouTubeBTN = new Button();
            ResultadosTotalesYouTbLabel = new Label();
            AgregarEnviarSMplayerYoutbBTN = new Button();
            ObtenerVideosListaReprBTN = new Button();
            NumColumnasResultNM = new NumericUpDown();
            label7 = new Label();
            MainYTPanel = new Panel();
            ObtenerVideosIDCanalBTN = new Button();
            label22 = new Label();
            IDCanalTXBX = new TextBox();
            label18 = new Label();
            IdCanalTBX = new TextBox();
            ObtenerVideosIDListReprBTN = new Button();
            label16 = new Label();
            BuscarYoutubeBTN = new Button();
            AbrirResultadosBTN = new Button();
            label21 = new Label();
            FiltroVideoYTPanel = new Panel();
            TipoVideoComboBox = new ComboBox();
            DuracionYoutubeVideoCBX = new ComboBox();
            label8 = new Label();
            SubtitulosComboBox = new ComboBox();
            label12 = new Label();
            DefinicionComboBox = new ComboBox();
            label13 = new Label();
            label14 = new Label();
            VideoCategoriaComboBox = new ComboBox();
            label15 = new Label();
            TipoCanalComboBox = new ComboBox();
            IDListaReprodTXBX = new TextBox();
            label19 = new Label();
            IdiomaComboBox = new ComboBox();
            label17 = new Label();
            TipoBusquedaComboBox = new ComboBox();
            label11 = new Label();
            SafeSearchComboBox = new ComboBox();
            label10 = new Label();
            OrdenComboBox = new ComboBox();
            label9 = new Label();
            PaisComboBox = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            BuscarYoutubVideoTB = new TextBox();
            FinYoutbDTP = new DateTimePicker();
            InicioYoutbDTP = new DateTimePicker();
            label6 = new Label();
            MaxResultYoutubeNM = new NumericUpDown();
            ImagenTag = new TabPage();
            Descargatag = new TabPage();
            label20 = new Label();
            DescargaDirecVideosBTN = new Button();
            URLsDDVideosRTB = new RichTextBox();
            VerificarVideosbackgrWorker = new System.ComponentModel.BackgroundWorker();
            ObtenerVideosYTPanel = new Panel();
            FiltroBusquedaYTPanel = new Panel();
            tabControl1.SuspendLayout();
            VideoTag.SuspendLayout();
            panelResultado.SuspendLayout();
            panelProgreso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resultadoVideoBindingSource).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumMinResultVideoNM).BeginInit();
            YoutubeTag.SuspendLayout();
            BotoneraYoutube.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumColumnasResultNM).BeginInit();
            MainYTPanel.SuspendLayout();
            FiltroVideoYTPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MaxResultYoutubeNM).BeginInit();
            Descargatag.SuspendLayout();
            ObtenerVideosYTPanel.SuspendLayout();
            FiltroBusquedaYTPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(VideoTag);
            tabControl1.Controls.Add(YoutubeTag);
            tabControl1.Controls.Add(ImagenTag);
            tabControl1.Controls.Add(Descargatag);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1179, 724);
            tabControl1.TabIndex = 0;
            // 
            // VideoTag
            // 
            VideoTag.Controls.Add(panelResultado);
            VideoTag.Controls.Add(panelProgreso);
            VideoTag.Controls.Add(LinkVideosLB);
            VideoTag.Controls.Add(panel1);
            VideoTag.Location = new Point(4, 29);
            VideoTag.Name = "VideoTag";
            VideoTag.Padding = new Padding(3);
            VideoTag.Size = new Size(1171, 691);
            VideoTag.TabIndex = 0;
            VideoTag.Text = "General Videos";
            VideoTag.UseVisualStyleBackColor = true;
            // 
            // panelResultado
            // 
            panelResultado.Controls.Add(DescargVideosBTN);
            panelResultado.Controls.Add(SeleccionarTodosBTN);
            panelResultado.Controls.Add(ResultadosTotalesLabel);
            panelResultado.Controls.Add(AgregarEnviarSMplayerBTN);
            panelResultado.Location = new Point(3, 237);
            panelResultado.Name = "panelResultado";
            panelResultado.Size = new Size(1165, 47);
            panelResultado.TabIndex = 20;
            panelResultado.Visible = false;
            // 
            // DescargVideosBTN
            // 
            DescargVideosBTN.Location = new Point(367, 7);
            DescargVideosBTN.Name = "DescargVideosBTN";
            DescargVideosBTN.Size = new Size(244, 29);
            DescargVideosBTN.TabIndex = 18;
            DescargVideosBTN.Text = "Descargar Videos Seleccionados";
            DescargVideosBTN.UseVisualStyleBackColor = true;
            DescargVideosBTN.Click += DescargVideosBTN_Click;
            // 
            // SeleccionarTodosBTN
            // 
            SeleccionarTodosBTN.Location = new Point(673, 7);
            SeleccionarTodosBTN.Name = "SeleccionarTodosBTN";
            SeleccionarTodosBTN.Size = new Size(163, 29);
            SeleccionarTodosBTN.TabIndex = 17;
            SeleccionarTodosBTN.Text = "Seleccionar Todos";
            SeleccionarTodosBTN.UseVisualStyleBackColor = true;
            SeleccionarTodosBTN.Click += SeleccionarTodosButton_Click;
            // 
            // ResultadosTotalesLabel
            // 
            ResultadosTotalesLabel.AutoSize = true;
            ResultadosTotalesLabel.Location = new Point(937, 11);
            ResultadosTotalesLabel.Name = "ResultadosTotalesLabel";
            ResultadosTotalesLabel.Size = new Size(131, 20);
            ResultadosTotalesLabel.TabIndex = 16;
            ResultadosTotalesLabel.Text = "ResultadosTotales:";
            // 
            // AgregarEnviarSMplayerBTN
            // 
            AgregarEnviarSMplayerBTN.Location = new Point(5, 6);
            AgregarEnviarSMplayerBTN.Name = "AgregarEnviarSMplayerBTN";
            AgregarEnviarSMplayerBTN.Size = new Size(301, 30);
            AgregarEnviarSMplayerBTN.TabIndex = 15;
            AgregarEnviarSMplayerBTN.Text = "Agregar/Enviar seleccionados a Smplayer";
            AgregarEnviarSMplayerBTN.UseVisualStyleBackColor = true;
            AgregarEnviarSMplayerBTN.Click += AgregarEnviarSmplayer_Click;
            // 
            // panelProgreso
            // 
            panelProgreso.Controls.Add(ResultadosActualesProgressBar);
            panelProgreso.Controls.Add(NumResultadosActulabel);
            panelProgreso.Controls.Add(VideoNumPagprogressBar);
            panelProgreso.Controls.Add(PaginasVisitadasLabel);
            panelProgreso.Controls.Add(NumeroPagTotalesLabel);
            panelProgreso.Location = new Point(3, 147);
            panelProgreso.Name = "panelProgreso";
            panelProgreso.Size = new Size(1165, 84);
            panelProgreso.TabIndex = 19;
            panelProgreso.Visible = false;
            // 
            // ResultadosActualesProgressBar
            // 
            ResultadosActualesProgressBar.Location = new Point(255, 39);
            ResultadosActualesProgressBar.Name = "ResultadosActualesProgressBar";
            ResultadosActualesProgressBar.Size = new Size(289, 29);
            ResultadosActualesProgressBar.TabIndex = 16;
            // 
            // NumResultadosActulabel
            // 
            NumResultadosActulabel.AutoSize = true;
            NumResultadosActulabel.Location = new Point(5, 44);
            NumResultadosActulabel.Name = "NumResultadosActulabel";
            NumResultadosActulabel.Size = new Size(223, 20);
            NumResultadosActulabel.TabIndex = 15;
            NumResultadosActulabel.Text = "Numero de Resultados Actuales:";
            // 
            // VideoNumPagprogressBar
            // 
            VideoNumPagprogressBar.Location = new Point(699, 40);
            VideoNumPagprogressBar.Name = "VideoNumPagprogressBar";
            VideoNumPagprogressBar.Size = new Size(454, 28);
            VideoNumPagprogressBar.Step = 1;
            VideoNumPagprogressBar.TabIndex = 12;
            // 
            // PaginasVisitadasLabel
            // 
            PaginasVisitadasLabel.AutoSize = true;
            PaginasVisitadasLabel.Location = new Point(541, 44);
            PaginasVisitadasLabel.Name = "PaginasVisitadasLabel";
            PaginasVisitadasLabel.Size = new Size(125, 20);
            PaginasVisitadasLabel.TabIndex = 13;
            PaginasVisitadasLabel.Text = "Paginas Visitadas:";
            // 
            // NumeroPagTotalesLabel
            // 
            NumeroPagTotalesLabel.AutoSize = true;
            NumeroPagTotalesLabel.Location = new Point(5, 12);
            NumeroPagTotalesLabel.Name = "NumeroPagTotalesLabel";
            NumeroPagTotalesLabel.Size = new Size(226, 20);
            NumeroPagTotalesLabel.TabIndex = 14;
            NumeroPagTotalesLabel.Text = "Numero de Paginas encontradas:";
            // 
            // LinkVideosLB
            // 
            LinkVideosLB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LinkVideosLB.DataSource = resultadoVideoBindingSource;
            LinkVideosLB.DisplayMember = "Title";
            LinkVideosLB.FormattingEnabled = true;
            LinkVideosLB.HorizontalScrollbar = true;
            LinkVideosLB.ItemHeight = 20;
            LinkVideosLB.Location = new Point(3, 282);
            LinkVideosLB.Name = "LinkVideosLB";
            LinkVideosLB.SelectionMode = SelectionMode.MultiExtended;
            LinkVideosLB.Size = new Size(1165, 344);
            LinkVideosLB.TabIndex = 9;
            LinkVideosLB.ValueMember = "URLVideo";
            LinkVideosLB.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            LinkVideosLB.DoubleClick += LinVideoDoubleClick;
            // 
            // resultadoVideoBindingSource
            // 
            resultadoVideoBindingSource.DataSource = typeof(ResultadoVideo);
            // 
            // panel1
            // 
            panel1.Controls.Add(FechaFinlabel);
            panel1.Controls.Add(FechaIniciolabel);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(BuscarVideoTB);
            panel1.Controls.Add(SoloListaExtYtdlCKBX);
            panel1.Controls.Add(FechaFinDTP);
            panel1.Controls.Add(BuscarVideoBTN);
            panel1.Controls.Add(FechaInicioDTP);
            panel1.Controls.Add(DuracionVideoCOMBX);
            panel1.Controls.Add(AltaCalidadCKBX);
            panel1.Controls.Add(FechaVideoCOMBX);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(NumMinResultVideoNM);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1165, 144);
            panel1.TabIndex = 21;
            // 
            // FechaFinlabel
            // 
            FechaFinlabel.AutoSize = true;
            FechaFinlabel.Location = new Point(678, 107);
            FechaFinlabel.Name = "FechaFinlabel";
            FechaFinlabel.Size = new Size(73, 20);
            FechaFinlabel.TabIndex = 20;
            FechaFinlabel.Text = "Fecha Fin:";
            FechaFinlabel.Visible = false;
            // 
            // FechaIniciolabel
            // 
            FechaIniciolabel.AutoSize = true;
            FechaIniciolabel.Location = new Point(168, 110);
            FechaIniciolabel.Name = "FechaIniciolabel";
            FechaIniciolabel.Size = new Size(90, 20);
            FechaIniciolabel.TabIndex = 19;
            FechaIniciolabel.Text = "Fecha Inicio:";
            FechaIniciolabel.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 16);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 1;
            label1.Text = "Buscar:";
            // 
            // BuscarVideoTB
            // 
            BuscarVideoTB.Location = new Point(61, 13);
            BuscarVideoTB.Name = "BuscarVideoTB";
            BuscarVideoTB.Size = new Size(550, 27);
            BuscarVideoTB.TabIndex = 0;
            // 
            // SoloListaExtYtdlCKBX
            // 
            SoloListaExtYtdlCKBX.AutoSize = true;
            SoloListaExtYtdlCKBX.Checked = true;
            SoloListaExtYtdlCKBX.CheckState = CheckState.Checked;
            SoloListaExtYtdlCKBX.Location = new Point(631, 15);
            SoloListaExtYtdlCKBX.Name = "SoloListaExtYtdlCKBX";
            SoloListaExtYtdlCKBX.Size = new Size(207, 24);
            SoloListaExtYtdlCKBX.TabIndex = 2;
            SoloListaExtYtdlCKBX.Text = "Solo Lista Extractores yt-dl";
            SoloListaExtYtdlCKBX.UseVisualStyleBackColor = true;
            // 
            // FechaFinDTP
            // 
            FechaFinDTP.Location = new Point(757, 102);
            FechaFinDTP.Name = "FechaFinDTP";
            FechaFinDTP.Size = new Size(295, 27);
            FechaFinDTP.TabIndex = 18;
            FechaFinDTP.Visible = false;
            // 
            // BuscarVideoBTN
            // 
            BuscarVideoBTN.Location = new Point(5, 103);
            BuscarVideoBTN.Name = "BuscarVideoBTN";
            BuscarVideoBTN.Size = new Size(94, 29);
            BuscarVideoBTN.TabIndex = 3;
            BuscarVideoBTN.Text = "Buscar";
            BuscarVideoBTN.UseVisualStyleBackColor = true;
            BuscarVideoBTN.Click += BuscarVideoBTN_Click;
            // 
            // FechaInicioDTP
            // 
            FechaInicioDTP.Location = new Point(264, 105);
            FechaInicioDTP.Name = "FechaInicioDTP";
            FechaInicioDTP.Size = new Size(293, 27);
            FechaInicioDTP.TabIndex = 17;
            FechaInicioDTP.Visible = false;
            // 
            // DuracionVideoCOMBX
            // 
            DuracionVideoCOMBX.FormattingEnabled = true;
            DuracionVideoCOMBX.Items.AddRange(new object[] { "Todas las duraciones", "Corto (de 0 a 4 min)", "Mediana (de 4 a 20 min)", "Larga (20 min o más)" });
            DuracionVideoCOMBX.Location = new Point(61, 60);
            DuracionVideoCOMBX.Name = "DuracionVideoCOMBX";
            DuracionVideoCOMBX.Size = new Size(264, 28);
            DuracionVideoCOMBX.TabIndex = 4;
            DuracionVideoCOMBX.Text = "Duracion";
            // 
            // AltaCalidadCKBX
            // 
            AltaCalidadCKBX.AutoSize = true;
            AltaCalidadCKBX.Location = new Point(673, 62);
            AltaCalidadCKBX.Name = "AltaCalidadCKBX";
            AltaCalidadCKBX.Size = new Size(113, 24);
            AltaCalidadCKBX.TabIndex = 16;
            AltaCalidadCKBX.Text = "Alta Calidad";
            AltaCalidadCKBX.UseVisualStyleBackColor = true;
            // 
            // FechaVideoCOMBX
            // 
            FechaVideoCOMBX.FormattingEnabled = true;
            FechaVideoCOMBX.Items.AddRange(new object[] { "De Cualquier Fecha", "Ultima Hora", "Ultimas 24 Horas", "Ultima Semana", "Ultimo Mes", "Ultimo Año", "Personalizar" });
            FechaVideoCOMBX.Location = new Point(388, 60);
            FechaVideoCOMBX.Name = "FechaVideoCOMBX";
            FechaVideoCOMBX.Size = new Size(252, 28);
            FechaVideoCOMBX.TabIndex = 5;
            FechaVideoCOMBX.Text = "Fecha";
            FechaVideoCOMBX.SelectedIndexChanged += FechaVideoCB_Changed;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(883, 17);
            label2.Name = "label2";
            label2.Size = new Size(185, 20);
            label2.TabIndex = 7;
            label2.Text = "Nro minimo de resultados:";
            // 
            // NumMinResultVideoNM
            // 
            NumMinResultVideoNM.Location = new Point(1074, 15);
            NumMinResultVideoNM.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumMinResultVideoNM.Name = "NumMinResultVideoNM";
            NumMinResultVideoNM.Size = new Size(79, 27);
            NumMinResultVideoNM.TabIndex = 6;
            NumMinResultVideoNM.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // YoutubeTag
            // 
            YoutubeTag.Controls.Add(ResultadosYouTubeFlowLayPanel);
            YoutubeTag.Controls.Add(BotoneraYoutube);
            YoutubeTag.Controls.Add(MainYTPanel);
            YoutubeTag.Location = new Point(4, 29);
            YoutubeTag.Name = "YoutubeTag";
            YoutubeTag.Size = new Size(1171, 691);
            YoutubeTag.TabIndex = 2;
            YoutubeTag.Text = "YouTube";
            YoutubeTag.UseVisualStyleBackColor = true;
            // 
            // ResultadosYouTubeFlowLayPanel
            // 
            ResultadosYouTubeFlowLayPanel.AutoScroll = true;
            ResultadosYouTubeFlowLayPanel.AutoSize = true;
            ResultadosYouTubeFlowLayPanel.Dock = DockStyle.Fill;
            ResultadosYouTubeFlowLayPanel.Location = new Point(0, 517);
            ResultadosYouTubeFlowLayPanel.Name = "ResultadosYouTubeFlowLayPanel";
            ResultadosYouTubeFlowLayPanel.Size = new Size(1171, 174);
            ResultadosYouTubeFlowLayPanel.TabIndex = 0;
            // 
            // BotoneraYoutube
            // 
            BotoneraYoutube.Controls.Add(ResultadosPorPaginaYouTbLabel);
            BotoneraYoutube.Controls.Add(GuardarResultadosBTN);
            BotoneraYoutube.Controls.Add(ObtenerVideosCanalBTN);
            BotoneraYoutube.Controls.Add(DescargVideosYouTbBTN);
            BotoneraYoutube.Controls.Add(SeleccionarTodosYouTubeBTN);
            BotoneraYoutube.Controls.Add(ResultadosTotalesYouTbLabel);
            BotoneraYoutube.Controls.Add(AgregarEnviarSMplayerYoutbBTN);
            BotoneraYoutube.Controls.Add(ObtenerVideosListaReprBTN);
            BotoneraYoutube.Controls.Add(NumColumnasResultNM);
            BotoneraYoutube.Controls.Add(label7);
            BotoneraYoutube.Dock = DockStyle.Top;
            BotoneraYoutube.Location = new Point(0, 373);
            BotoneraYoutube.Name = "BotoneraYoutube";
            BotoneraYoutube.Size = new Size(1171, 144);
            BotoneraYoutube.TabIndex = 23;
            BotoneraYoutube.Visible = false;
            // 
            // ResultadosPorPaginaYouTbLabel
            // 
            ResultadosPorPaginaYouTbLabel.AutoSize = true;
            ResultadosPorPaginaYouTbLabel.Location = new Point(259, 105);
            ResultadosPorPaginaYouTbLabel.Name = "ResultadosPorPaginaYouTbLabel";
            ResultadosPorPaginaYouTbLabel.Size = new Size(157, 20);
            ResultadosPorPaginaYouTbLabel.TabIndex = 51;
            ResultadosPorPaginaYouTbLabel.Text = "Resultados Por Pagina:";
            // 
            // GuardarResultadosBTN
            // 
            GuardarResultadosBTN.Location = new Point(744, 54);
            GuardarResultadosBTN.Name = "GuardarResultadosBTN";
            GuardarResultadosBTN.Size = new Size(194, 29);
            GuardarResultadosBTN.TabIndex = 19;
            GuardarResultadosBTN.Text = "Guardar Ultimo Resultado";
            GuardarResultadosBTN.UseVisualStyleBackColor = true;
            GuardarResultadosBTN.Click += GuardarResultadosBTN_Click;
            // 
            // ObtenerVideosCanalBTN
            // 
            ObtenerVideosCanalBTN.Location = new Point(339, 6);
            ObtenerVideosCanalBTN.Name = "ObtenerVideosCanalBTN";
            ObtenerVideosCanalBTN.Size = new Size(207, 29);
            ObtenerVideosCanalBTN.TabIndex = 50;
            ObtenerVideosCanalBTN.Text = "Obtener videos de Canal";
            ObtenerVideosCanalBTN.UseVisualStyleBackColor = true;
            ObtenerVideosCanalBTN.Click += ObtenerVideosCanalBTN_Click;
            // 
            // DescargVideosYouTbBTN
            // 
            DescargVideosYouTbBTN.Location = new Point(315, 55);
            DescargVideosYouTbBTN.Name = "DescargVideosYouTbBTN";
            DescargVideosYouTbBTN.Size = new Size(244, 29);
            DescargVideosYouTbBTN.TabIndex = 18;
            DescargVideosYouTbBTN.Text = "Descargar Videos Seleccionados";
            DescargVideosYouTbBTN.UseVisualStyleBackColor = true;
            DescargVideosYouTbBTN.Click += DescargVideosYouTbBTN_Click;
            // 
            // SeleccionarTodosYouTubeBTN
            // 
            SeleccionarTodosYouTubeBTN.Location = new Point(565, 54);
            SeleccionarTodosYouTubeBTN.Name = "SeleccionarTodosYouTubeBTN";
            SeleccionarTodosYouTubeBTN.Size = new Size(163, 29);
            SeleccionarTodosYouTubeBTN.TabIndex = 17;
            SeleccionarTodosYouTubeBTN.Text = "Seleccionar Todos";
            SeleccionarTodosYouTubeBTN.UseVisualStyleBackColor = true;
            SeleccionarTodosYouTubeBTN.Click += SeleccionarTodosYouTubeBTN_Click;
            // 
            // ResultadosTotalesYouTbLabel
            // 
            ResultadosTotalesYouTbLabel.AutoSize = true;
            ResultadosTotalesYouTbLabel.Location = new Point(504, 105);
            ResultadosTotalesYouTbLabel.Name = "ResultadosTotalesYouTbLabel";
            ResultadosTotalesYouTbLabel.Size = new Size(135, 20);
            ResultadosTotalesYouTbLabel.TabIndex = 16;
            ResultadosTotalesYouTbLabel.Text = "Resultados Totales:";
            // 
            // AgregarEnviarSMplayerYoutbBTN
            // 
            AgregarEnviarSMplayerYoutbBTN.Location = new Point(8, 54);
            AgregarEnviarSMplayerYoutbBTN.Name = "AgregarEnviarSMplayerYoutbBTN";
            AgregarEnviarSMplayerYoutbBTN.Size = new Size(301, 30);
            AgregarEnviarSMplayerYoutbBTN.TabIndex = 15;
            AgregarEnviarSMplayerYoutbBTN.Text = "Agregar/Enviar seleccionados a Smplayer";
            AgregarEnviarSMplayerYoutbBTN.UseVisualStyleBackColor = true;
            AgregarEnviarSMplayerYoutbBTN.Click += AgregarEnviarSMplayerYoutbBTN_Click;
            // 
            // ObtenerVideosListaReprBTN
            // 
            ObtenerVideosListaReprBTN.Location = new Point(8, 6);
            ObtenerVideosListaReprBTN.Name = "ObtenerVideosListaReprBTN";
            ObtenerVideosListaReprBTN.Size = new Size(297, 29);
            ObtenerVideosListaReprBTN.TabIndex = 49;
            ObtenerVideosListaReprBTN.Text = "Obtener videos de lista de Reproduccion";
            ObtenerVideosListaReprBTN.UseVisualStyleBackColor = true;
            ObtenerVideosListaReprBTN.Click += ObtenerVideosListaReprBTN_Click;
            // 
            // NumColumnasResultNM
            // 
            NumColumnasResultNM.Location = new Point(177, 103);
            NumColumnasResultNM.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumColumnasResultNM.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumColumnasResultNM.Name = "NumColumnasResultNM";
            NumColumnasResultNM.Size = new Size(62, 27);
            NumColumnasResultNM.TabIndex = 21;
            NumColumnasResultNM.Value = new decimal(new int[] { 3, 0, 0, 0 });
            NumColumnasResultNM.ValueChanged += NumColumnasResultNM_ValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.InfoText;
            label7.Location = new Point(15, 105);
            label7.Name = "label7";
            label7.Size = new Size(156, 20);
            label7.TabIndex = 22;
            label7.Text = "Numero de Columnas:";
            // 
            // MainYTPanel
            // 
            MainYTPanel.BackColor = Color.Transparent;
            MainYTPanel.Controls.Add(FiltroBusquedaYTPanel);
            MainYTPanel.Controls.Add(ObtenerVideosYTPanel);
            MainYTPanel.Dock = DockStyle.Top;
            MainYTPanel.Location = new Point(0, 0);
            MainYTPanel.Name = "MainYTPanel";
            MainYTPanel.Size = new Size(1171, 373);
            MainYTPanel.TabIndex = 22;
            // 
            // ObtenerVideosIDCanalBTN
            // 
            ObtenerVideosIDCanalBTN.Location = new Point(718, 11);
            ObtenerVideosIDCanalBTN.Name = "ObtenerVideosIDCanalBTN";
            ObtenerVideosIDCanalBTN.Size = new Size(319, 29);
            ObtenerVideosIDCanalBTN.TabIndex = 55;
            ObtenerVideosIDCanalBTN.Text = "Obtener videos de ID Canal";
            ObtenerVideosIDCanalBTN.UseVisualStyleBackColor = true;
            ObtenerVideosIDCanalBTN.Click += ObtenerVideosIDCanalBTN_Click;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(7, 15);
            label22.Name = "label22";
            label22.Size = new Size(87, 20);
            label22.TabIndex = 53;
            label22.Text = "Id de Canal:";
            // 
            // IDCanalTXBX
            // 
            IDCanalTXBX.Location = new Point(100, 12);
            IDCanalTXBX.Name = "IDCanalTXBX";
            IDCanalTXBX.Size = new Size(592, 27);
            IDCanalTXBX.TabIndex = 54;
            IDCanalTXBX.TextChanged += IDCanalTXBX_TextChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(362, 125);
            label18.Name = "label18";
            label18.Size = new Size(87, 20);
            label18.TabIndex = 44;
            label18.Text = "Id de Canal:";
            // 
            // IdCanalTBX
            // 
            IdCanalTBX.Location = new Point(455, 122);
            IdCanalTBX.Name = "IdCanalTBX";
            IdCanalTBX.Size = new Size(224, 27);
            IdCanalTBX.TabIndex = 43;
            // 
            // ObtenerVideosIDListReprBTN
            // 
            ObtenerVideosIDListReprBTN.Location = new Point(718, 45);
            ObtenerVideosIDListReprBTN.Name = "ObtenerVideosIDListReprBTN";
            ObtenerVideosIDListReprBTN.Size = new Size(319, 29);
            ObtenerVideosIDListReprBTN.TabIndex = 51;
            ObtenerVideosIDListReprBTN.Text = "Obtener videos de ID lista de Reproduccion";
            ObtenerVideosIDListReprBTN.UseVisualStyleBackColor = true;
            ObtenerVideosIDListReprBTN.Click += ObtenerVideosIDListReprBTN_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(4, 125);
            label16.Name = "label16";
            label16.Size = new Size(104, 20);
            label16.TabIndex = 40;
            label16.Text = "Tipo de Canal:";
            // 
            // BuscarYoutubeBTN
            // 
            BuscarYoutubeBTN.Location = new Point(5, 86);
            BuscarYoutubeBTN.Name = "BuscarYoutubeBTN";
            BuscarYoutubeBTN.Size = new Size(94, 29);
            BuscarYoutubeBTN.TabIndex = 3;
            BuscarYoutubeBTN.Text = "Buscar";
            BuscarYoutubeBTN.UseVisualStyleBackColor = true;
            BuscarYoutubeBTN.Click += BuscarYoutubeBTN_Click;
            // 
            // AbrirResultadosBTN
            // 
            AbrirResultadosBTN.Location = new Point(3, 3);
            AbrirResultadosBTN.Name = "AbrirResultadosBTN";
            AbrirResultadosBTN.Size = new Size(94, 29);
            AbrirResultadosBTN.TabIndex = 52;
            AbrirResultadosBTN.Text = "Abrir";
            AbrirResultadosBTN.UseVisualStyleBackColor = true;
            AbrirResultadosBTN.Click += AbrirResultadosBTN_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(7, 49);
            label21.Name = "label21";
            label21.Size = new Size(197, 20);
            label21.TabIndex = 45;
            label21.Text = "Id de Lista de Reproduccion:";
            // 
            // FiltroVideoYTPanel
            // 
            FiltroVideoYTPanel.Controls.Add(TipoVideoComboBox);
            FiltroVideoYTPanel.Controls.Add(DuracionYoutubeVideoCBX);
            FiltroVideoYTPanel.Controls.Add(label8);
            FiltroVideoYTPanel.Controls.Add(SubtitulosComboBox);
            FiltroVideoYTPanel.Controls.Add(label12);
            FiltroVideoYTPanel.Controls.Add(DefinicionComboBox);
            FiltroVideoYTPanel.Controls.Add(label13);
            FiltroVideoYTPanel.Controls.Add(label14);
            FiltroVideoYTPanel.Controls.Add(VideoCategoriaComboBox);
            FiltroVideoYTPanel.Controls.Add(label15);
            FiltroVideoYTPanel.Dock = DockStyle.Bottom;
            FiltroVideoYTPanel.Location = new Point(0, 160);
            FiltroVideoYTPanel.Name = "FiltroVideoYTPanel";
            FiltroVideoYTPanel.Size = new Size(1171, 82);
            FiltroVideoYTPanel.TabIndex = 47;
            // 
            // TipoVideoComboBox
            // 
            TipoVideoComboBox.FormattingEnabled = true;
            TipoVideoComboBox.Location = new Point(115, 9);
            TipoVideoComboBox.Name = "TipoVideoComboBox";
            TipoVideoComboBox.Size = new Size(216, 28);
            TipoVideoComboBox.TabIndex = 37;
            // 
            // DuracionYoutubeVideoCBX
            // 
            DuracionYoutubeVideoCBX.FormattingEnabled = true;
            DuracionYoutubeVideoCBX.Items.AddRange(new object[] { "Todas las duraciones", "Corto (de 0 a 4 min)", "Mediana (de 4 a 20 min)", "Larga (20 min o más)" });
            DuracionYoutubeVideoCBX.Location = new Point(780, 9);
            DuracionYoutubeVideoCBX.Name = "DuracionYoutubeVideoCBX";
            DuracionYoutubeVideoCBX.Size = new Size(217, 28);
            DuracionYoutubeVideoCBX.TabIndex = 4;
            DuracionYoutubeVideoCBX.Text = "Duracion";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(702, 12);
            label8.Name = "label8";
            label8.Size = new Size(72, 20);
            label8.TabIndex = 23;
            label8.Text = "Duracion:";
            // 
            // SubtitulosComboBox
            // 
            SubtitulosComboBox.FormattingEnabled = true;
            SubtitulosComboBox.Location = new Point(573, 43);
            SubtitulosComboBox.Name = "SubtitulosComboBox";
            SubtitulosComboBox.Size = new Size(191, 28);
            SubtitulosComboBox.TabIndex = 30;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(489, 46);
            label12.Name = "label12";
            label12.Size = new Size(78, 20);
            label12.TabIndex = 31;
            label12.Text = "Subtitulos:";
            // 
            // DefinicionComboBox
            // 
            DefinicionComboBox.FormattingEnabled = true;
            DefinicionComboBox.Location = new Point(457, 9);
            DefinicionComboBox.Name = "DefinicionComboBox";
            DefinicionComboBox.Size = new Size(211, 28);
            DefinicionComboBox.TabIndex = 32;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(373, 12);
            label13.Name = "label13";
            label13.Size = new Size(80, 20);
            label13.TabIndex = 33;
            label13.Text = "Definicion:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 46);
            label14.Name = "label14";
            label14.Size = new Size(141, 20);
            label14.TabIndex = 35;
            label14.Text = "Categoria de Video:";
            // 
            // VideoCategoriaComboBox
            // 
            VideoCategoriaComboBox.FormattingEnabled = true;
            VideoCategoriaComboBox.Location = new Point(153, 43);
            VideoCategoriaComboBox.Name = "VideoCategoriaComboBox";
            VideoCategoriaComboBox.Size = new Size(285, 28);
            VideoCategoriaComboBox.TabIndex = 36;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(5, 12);
            label15.Name = "label15";
            label15.Size = new Size(104, 20);
            label15.TabIndex = 38;
            label15.Text = "Tipo de video:";
            // 
            // TipoCanalComboBox
            // 
            TipoCanalComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TipoCanalComboBox.FormattingEnabled = true;
            TipoCanalComboBox.Location = new Point(114, 121);
            TipoCanalComboBox.Name = "TipoCanalComboBox";
            TipoCanalComboBox.Size = new Size(229, 28);
            TipoCanalComboBox.TabIndex = 39;
            // 
            // IDListaReprodTXBX
            // 
            IDListaReprodTXBX.Location = new Point(213, 46);
            IDListaReprodTXBX.Name = "IDListaReprodTXBX";
            IDListaReprodTXBX.Size = new Size(479, 27);
            IDListaReprodTXBX.TabIndex = 50;
            IDListaReprodTXBX.TextChanged += IDListaReprodTXBX_TextChanged;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(540, 49);
            label19.Name = "label19";
            label19.Size = new Size(59, 20);
            label19.TabIndex = 46;
            label19.Text = "Idioma:";
            // 
            // IdiomaComboBox
            // 
            IdiomaComboBox.FormattingEnabled = true;
            IdiomaComboBox.Location = new Point(607, 46);
            IdiomaComboBox.Name = "IdiomaComboBox";
            IdiomaComboBox.Size = new Size(259, 28);
            IdiomaComboBox.TabIndex = 45;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(789, 7);
            label17.Name = "label17";
            label17.Size = new Size(132, 20);
            label17.TabIndex = 42;
            label17.Text = "Tipo de Busqueda:";
            // 
            // TipoBusquedaComboBox
            // 
            TipoBusquedaComboBox.FormattingEnabled = true;
            TipoBusquedaComboBox.Location = new Point(927, 4);
            TipoBusquedaComboBox.Name = "TipoBusquedaComboBox";
            TipoBusquedaComboBox.Size = new Size(232, 28);
            TipoBusquedaComboBox.TabIndex = 41;
            TipoBusquedaComboBox.SelectedIndexChanged += TipoBusquedaComboBox_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(794, 89);
            label11.Name = "label11";
            label11.Size = new Size(127, 20);
            label11.TabIndex = 29;
            label11.Text = "Busqueda Segura:";
            // 
            // SafeSearchComboBox
            // 
            SafeSearchComboBox.FormattingEnabled = true;
            SafeSearchComboBox.Location = new Point(927, 85);
            SafeSearchComboBox.Name = "SafeSearchComboBox";
            SafeSearchComboBox.Size = new Size(232, 28);
            SafeSearchComboBox.TabIndex = 28;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(289, 49);
            label10.Name = "label10";
            label10.Size = new Size(53, 20);
            label10.TabIndex = 27;
            label10.Text = "Orden:";
            // 
            // OrdenComboBox
            // 
            OrdenComboBox.FormattingEnabled = true;
            OrdenComboBox.Location = new Point(348, 46);
            OrdenComboBox.Name = "OrdenComboBox";
            OrdenComboBox.Size = new Size(168, 28);
            OrdenComboBox.TabIndex = 26;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(884, 49);
            label9.Name = "label9";
            label9.Size = new Size(37, 20);
            label9.TabIndex = 25;
            label9.Text = "Pais:";
            // 
            // PaisComboBox
            // 
            PaisComboBox.FormattingEnabled = true;
            PaisComboBox.Location = new Point(927, 47);
            PaisComboBox.Name = "PaisComboBox";
            PaisComboBox.Size = new Size(232, 28);
            PaisComboBox.TabIndex = 24;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(399, 91);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 20;
            label3.Text = "Fecha Fin:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 91);
            label4.Name = "label4";
            label4.Size = new Size(90, 20);
            label4.TabIndex = 19;
            label4.Text = "Fecha Inicio:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(112, 7);
            label5.Name = "label5";
            label5.Size = new Size(55, 20);
            label5.TabIndex = 1;
            label5.Text = "Buscar:";
            // 
            // BuscarYoutubVideoTB
            // 
            BuscarYoutubVideoTB.Location = new Point(168, 4);
            BuscarYoutubVideoTB.Name = "BuscarYoutubVideoTB";
            BuscarYoutubVideoTB.Size = new Size(615, 27);
            BuscarYoutubVideoTB.TabIndex = 0;
            // 
            // FinYoutbDTP
            // 
            FinYoutbDTP.Location = new Point(478, 86);
            FinYoutbDTP.Name = "FinYoutbDTP";
            FinYoutbDTP.Size = new Size(295, 27);
            FinYoutbDTP.TabIndex = 18;
            // 
            // InicioYoutbDTP
            // 
            InicioYoutbDTP.Location = new Point(100, 86);
            InicioYoutbDTP.Name = "InicioYoutbDTP";
            InicioYoutbDTP.Size = new Size(293, 27);
            InicioYoutbDTP.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1, 49);
            label6.Name = "label6";
            label6.Size = new Size(188, 20);
            label6.TabIndex = 7;
            label6.Text = "Nro maximo de resultados:";
            // 
            // MaxResultYoutubeNM
            // 
            MaxResultYoutubeNM.Location = new Point(192, 47);
            MaxResultYoutubeNM.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            MaxResultYoutubeNM.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            MaxResultYoutubeNM.Name = "MaxResultYoutubeNM";
            MaxResultYoutubeNM.Size = new Size(79, 27);
            MaxResultYoutubeNM.TabIndex = 6;
            MaxResultYoutubeNM.TextAlign = HorizontalAlignment.Right;
            MaxResultYoutubeNM.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // ImagenTag
            // 
            ImagenTag.Location = new Point(4, 29);
            ImagenTag.Name = "ImagenTag";
            ImagenTag.Padding = new Padding(3);
            ImagenTag.Size = new Size(1171, 691);
            ImagenTag.TabIndex = 1;
            ImagenTag.Text = "Imagen";
            ImagenTag.UseVisualStyleBackColor = true;
            // 
            // Descargatag
            // 
            Descargatag.Controls.Add(label20);
            Descargatag.Controls.Add(DescargaDirecVideosBTN);
            Descargatag.Controls.Add(URLsDDVideosRTB);
            Descargatag.Location = new Point(4, 29);
            Descargatag.Name = "Descargatag";
            Descargatag.Size = new Size(1171, 691);
            Descargatag.TabIndex = 3;
            Descargatag.Text = "Descarga Directa";
            Descargatag.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(9, 22);
            label20.Name = "label20";
            label20.Size = new Size(114, 20);
            label20.TabIndex = 2;
            label20.Text = "URLs de Videos:";
            // 
            // DescargaDirecVideosBTN
            // 
            DescargaDirecVideosBTN.Location = new Point(9, 250);
            DescargaDirecVideosBTN.Name = "DescargaDirecVideosBTN";
            DescargaDirecVideosBTN.Size = new Size(205, 29);
            DescargaDirecVideosBTN.TabIndex = 1;
            DescargaDirecVideosBTN.Text = "Descargar Videos/musica";
            DescargaDirecVideosBTN.UseVisualStyleBackColor = true;
            DescargaDirecVideosBTN.Click += DescargaDirecVideosBTN_Click;
            // 
            // URLsDDVideosRTB
            // 
            URLsDDVideosRTB.Location = new Point(9, 59);
            URLsDDVideosRTB.Name = "URLsDDVideosRTB";
            URLsDDVideosRTB.Size = new Size(902, 169);
            URLsDDVideosRTB.TabIndex = 0;
            URLsDDVideosRTB.Text = "";
            // 
            // VerificarVideosbackgrWorker
            // 
            VerificarVideosbackgrWorker.WorkerReportsProgress = true;
            VerificarVideosbackgrWorker.DoWork += BWVerificarVideo_Dowork;
            VerificarVideosbackgrWorker.ProgressChanged += BWVerificarVideo_Progreso;
            VerificarVideosbackgrWorker.RunWorkerCompleted += BWVerificarVideo_Resultado;
            // 
            // ObtenerVideosYTPanel
            // 
            ObtenerVideosYTPanel.BackColor = Color.YellowGreen;
            ObtenerVideosYTPanel.Controls.Add(label22);
            ObtenerVideosYTPanel.Controls.Add(ObtenerVideosIDCanalBTN);
            ObtenerVideosYTPanel.Controls.Add(IDListaReprodTXBX);
            ObtenerVideosYTPanel.Controls.Add(label21);
            ObtenerVideosYTPanel.Controls.Add(IDCanalTXBX);
            ObtenerVideosYTPanel.Controls.Add(BuscarYoutubeBTN);
            ObtenerVideosYTPanel.Controls.Add(ObtenerVideosIDListReprBTN);
            ObtenerVideosYTPanel.Dock = DockStyle.Bottom;
            ObtenerVideosYTPanel.Location = new Point(0, 248);
            ObtenerVideosYTPanel.Name = "ObtenerVideosYTPanel";
            ObtenerVideosYTPanel.Size = new Size(1171, 125);
            ObtenerVideosYTPanel.TabIndex = 56;
            // 
            // FiltroBusquedaYTPanel
            // 
            FiltroBusquedaYTPanel.Controls.Add(AbrirResultadosBTN);
            FiltroBusquedaYTPanel.Controls.Add(label18);
            FiltroBusquedaYTPanel.Controls.Add(MaxResultYoutubeNM);
            FiltroBusquedaYTPanel.Controls.Add(IdCanalTBX);
            FiltroBusquedaYTPanel.Controls.Add(label6);
            FiltroBusquedaYTPanel.Controls.Add(label16);
            FiltroBusquedaYTPanel.Controls.Add(InicioYoutbDTP);
            FiltroBusquedaYTPanel.Controls.Add(FinYoutbDTP);
            FiltroBusquedaYTPanel.Controls.Add(FiltroVideoYTPanel);
            FiltroBusquedaYTPanel.Controls.Add(BuscarYoutubVideoTB);
            FiltroBusquedaYTPanel.Controls.Add(TipoCanalComboBox);
            FiltroBusquedaYTPanel.Controls.Add(label5);
            FiltroBusquedaYTPanel.Controls.Add(label19);
            FiltroBusquedaYTPanel.Controls.Add(label4);
            FiltroBusquedaYTPanel.Controls.Add(IdiomaComboBox);
            FiltroBusquedaYTPanel.Controls.Add(label3);
            FiltroBusquedaYTPanel.Controls.Add(label17);
            FiltroBusquedaYTPanel.Controls.Add(PaisComboBox);
            FiltroBusquedaYTPanel.Controls.Add(TipoBusquedaComboBox);
            FiltroBusquedaYTPanel.Controls.Add(label9);
            FiltroBusquedaYTPanel.Controls.Add(label11);
            FiltroBusquedaYTPanel.Controls.Add(OrdenComboBox);
            FiltroBusquedaYTPanel.Controls.Add(SafeSearchComboBox);
            FiltroBusquedaYTPanel.Controls.Add(label10);
            FiltroBusquedaYTPanel.Dock = DockStyle.Top;
            FiltroBusquedaYTPanel.Location = new Point(0, 0);
            FiltroBusquedaYTPanel.Name = "FiltroBusquedaYTPanel";
            FiltroBusquedaYTPanel.Size = new Size(1171, 242);
            FiltroBusquedaYTPanel.TabIndex = 57;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1179, 724);
            Controls.Add(tabControl1);
            MinimumSize = new Size(1197, 771);
            Name = "MainForm";
            Text = "Multimedia Scrapper";
            Load += MainForm_Load;
            Resize += Ajustar_imagenes;
            tabControl1.ResumeLayout(false);
            VideoTag.ResumeLayout(false);
            panelResultado.ResumeLayout(false);
            panelResultado.PerformLayout();
            panelProgreso.ResumeLayout(false);
            panelProgreso.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)resultadoVideoBindingSource).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumMinResultVideoNM).EndInit();
            YoutubeTag.ResumeLayout(false);
            YoutubeTag.PerformLayout();
            BotoneraYoutube.ResumeLayout(false);
            BotoneraYoutube.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumColumnasResultNM).EndInit();
            MainYTPanel.ResumeLayout(false);
            FiltroVideoYTPanel.ResumeLayout(false);
            FiltroVideoYTPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MaxResultYoutubeNM).EndInit();
            Descargatag.ResumeLayout(false);
            Descargatag.PerformLayout();
            ObtenerVideosYTPanel.ResumeLayout(false);
            ObtenerVideosYTPanel.PerformLayout();
            FiltroBusquedaYTPanel.ResumeLayout(false);
            FiltroBusquedaYTPanel.PerformLayout();
            ResumeLayout(false);
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
        private TabPage YoutubeTag;
        private Panel MainYTPanel;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox BuscarYoutubVideoTB;
        private DateTimePicker FinYoutbDTP;
        private Button BuscarYoutubeBTN;
        private DateTimePicker InicioYoutbDTP;
        private ComboBox DuracionYoutubeVideoCBX;
        private Label label6;
        private NumericUpDown MaxResultYoutubeNM;
        private Panel BotoneraYoutube;
        private Button DescargVideosYouTbBTN;
        private Button SeleccionarTodosYouTubeBTN;
        private Label ResultadosTotalesYouTbLabel;
        private Button AgregarEnviarSMplayerYoutbBTN;
        private FlowLayoutPanel ResultadosYouTubeFlowLayPanel;
        private NumericUpDown NumColumnasResultNM;
        private Label label7;
        private Label label8;
        private Label label9;
        private ComboBox PaisComboBox;
        private Label label10;
        private ComboBox OrdenComboBox;
        private Label label11;
        private ComboBox SafeSearchComboBox;
        private Label label12;
        private ComboBox SubtitulosComboBox;
        private Label label13;
        private ComboBox DefinicionComboBox;
        private Label label14;
        private ComboBox VideoCategoriaComboBox;
        private ComboBox TipoVideoComboBox;
        private Label label15;
        private ComboBox TipoCanalComboBox;
        private Label label17;
        private ComboBox TipoBusquedaComboBox;
        private Label label18;
        private TextBox IdCanalTBX;
        private Label label19;
        private ComboBox IdiomaComboBox;
        private Panel FiltroVideoYTPanel;
        private Label label16;
        private Button ObtenerVideosCanalBTN;
        private Button ObtenerVideosListaReprBTN;
        private TabPage Descargatag;
        private Label label20;
        private Button DescargaDirecVideosBTN;
        private RichTextBox URLsDDVideosRTB;
        private Label label21;
        private TextBox IDListaReprodTXBX;
        private Button ObtenerVideosIDListReprBTN;
        private Button GuardarResultadosBTN;
        private Button AbrirResultadosBTN;
        private Label ResultadosPorPaginaYouTbLabel;
        private Button ObtenerVideosIDCanalBTN;
        private Label label22;
        private TextBox IDCanalTXBX;
        private Panel ObtenerVideosYTPanel;
        private Panel FiltroBusquedaYTPanel;
    }
}