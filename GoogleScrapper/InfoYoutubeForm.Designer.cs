namespace GoogleScrapper
{
    partial class InfoYoutubeForm
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
            ImagenPB = new PictureBox();
            InformacionLB = new Label();
            TituloLB = new Label();
            panel1 = new Panel();
            ImagenGuardadaLB = new Label();
            ((System.ComponentModel.ISupportInitialize)ImagenPB).BeginInit();
            SuspendLayout();
            // 
            // ImagenPB
            // 
            ImagenPB.Location = new Point(0, 62);
            ImagenPB.MinimumSize = new Size(631, 315);
            ImagenPB.Name = "ImagenPB";
            ImagenPB.Size = new Size(682, 347);
            ImagenPB.TabIndex = 0;
            ImagenPB.TabStop = false;
            ImagenPB.Click += ImagenPB_Click;
            // 
            // InformacionLB
            // 
            InformacionLB.Location = new Point(0, 412);
            InformacionLB.Name = "InformacionLB";
            InformacionLB.Size = new Size(682, 179);
            InformacionLB.TabIndex = 1;
            // 
            // TituloLB
            // 
            TituloLB.Dock = DockStyle.Top;
            TituloLB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TituloLB.Location = new Point(0, 0);
            TituloLB.Name = "TituloLB";
            TituloLB.Size = new Size(682, 59);
            TituloLB.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Location = new Point(0, 594);
            panel1.Name = "panel1";
            panel1.Size = new Size(682, 38);
            panel1.TabIndex = 3;
            // 
            // ImagenGuardadaLB
            // 
            ImagenGuardadaLB.AutoSize = true;
            ImagenGuardadaLB.ForeColor = Color.Red;
            ImagenGuardadaLB.Location = new Point(409, 7);
            ImagenGuardadaLB.Name = "ImagenGuardadaLB";
            ImagenGuardadaLB.Size = new Size(261, 20);
            ImagenGuardadaLB.TabIndex = 4;
            ImagenGuardadaLB.Text = "Url Imagen Guardada al Portapapeles!";
            ImagenGuardadaLB.Visible = false;
            // 
            // InfoYoutubeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 633);
            Controls.Add(ImagenGuardadaLB);
            Controls.Add(ImagenPB);
            Controls.Add(InformacionLB);
            Controls.Add(panel1);
            Controls.Add(TituloLB);
            MinimumSize = new Size(700, 680);
            Name = "InfoYoutubeForm";
            Text = "Informacion del Elemento";
            Load += InfoYoutubeForm_Load;
            ((System.ComponentModel.ISupportInitialize)ImagenPB).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox ImagenPB;
        private Label InformacionLB;
        private Label TituloLB;
        private Panel panel1;
        private Label ImagenGuardadaLB;
    }
}