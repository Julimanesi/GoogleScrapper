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
            TituloLB = new Label();
            panel1 = new Panel();
            ImagenGuardadaLB = new Label();
            InformacionLB = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)ImagenPB).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ImagenPB
            // 
            ImagenPB.Location = new Point(0, 62);
            ImagenPB.Name = "ImagenPB";
            ImagenPB.Size = new Size(618, 386);
            ImagenPB.TabIndex = 0;
            ImagenPB.TabStop = false;
            ImagenPB.Click += ImagenPB_Click;
            // 
            // TituloLB
            // 
            TituloLB.Dock = DockStyle.Top;
            TituloLB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TituloLB.Location = new Point(0, 0);
            TituloLB.Name = "TituloLB";
            TituloLB.Size = new Size(618, 59);
            TituloLB.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(ImagenGuardadaLB);
            panel1.Location = new Point(-64, 666);
            panel1.Name = "panel1";
            panel1.Size = new Size(682, 38);
            panel1.TabIndex = 3;
            // 
            // ImagenGuardadaLB
            // 
            ImagenGuardadaLB.AutoSize = true;
            ImagenGuardadaLB.ForeColor = Color.Red;
            ImagenGuardadaLB.Location = new Point(76, 10);
            ImagenGuardadaLB.Name = "ImagenGuardadaLB";
            ImagenGuardadaLB.Size = new Size(261, 20);
            ImagenGuardadaLB.TabIndex = 4;
            ImagenGuardadaLB.Text = "Url Imagen Guardada al Portapapeles!";
            ImagenGuardadaLB.Visible = false;
            // 
            // InformacionLB
            // 
            InformacionLB.BackColor = SystemColors.ControlLight;
            InformacionLB.Location = new Point(0, 448);
            InformacionLB.Name = "InformacionLB";
            InformacionLB.ReadOnly = true;
            InformacionLB.Size = new Size(618, 216);
            InformacionLB.TabIndex = 5;
            InformacionLB.Text = "";
            // 
            // InfoYoutubeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(618, 705);
            Controls.Add(InformacionLB);
            Controls.Add(ImagenPB);
            Controls.Add(panel1);
            Controls.Add(TituloLB);
            MaximumSize = new Size(700, 756);
            Name = "InfoYoutubeForm";
            Text = "Informacion del Elemento";
            Load += InfoYoutubeForm_Load;
            ((System.ComponentModel.ISupportInitialize)ImagenPB).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox ImagenPB;
        private RichTextBox InformacionLB;
        private Label TituloLB;
        private Panel panel1;
        private Label ImagenGuardadaLB;
    }
}