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
            button2 = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)ImagenPB).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ImagenPB
            // 
            ImagenPB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ImagenPB.Location = new Point(0, 62);
            ImagenPB.MinimumSize = new Size(631, 315);
            ImagenPB.Name = "ImagenPB";
            ImagenPB.Size = new Size(631, 315);
            ImagenPB.TabIndex = 0;
            ImagenPB.TabStop = false;
            // 
            // InformacionLB
            // 
            InformacionLB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InformacionLB.Location = new Point(0, 380);
            InformacionLB.Name = "InformacionLB";
            InformacionLB.Size = new Size(631, 110);
            InformacionLB.TabIndex = 1;
            // 
            // TituloLB
            // 
            TituloLB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TituloLB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            TituloLB.Location = new Point(0, 0);
            TituloLB.Name = "TituloLB";
            TituloLB.Size = new Size(631, 59);
            TituloLB.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(0, 493);
            panel1.Name = "panel1";
            panel1.Size = new Size(631, 67);
            panel1.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(128, 16);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(12, 16);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // InfoYoutubeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(631, 561);
            Controls.Add(panel1);
            Controls.Add(InformacionLB);
            Controls.Add(ImagenPB);
            Controls.Add(TituloLB);
            MinimumSize = new Size(597, 560);
            Name = "InfoYoutubeForm";
            Text = "Informacion del Elemento";
            Load += InfoYoutubeForm_Load;
            ((System.ComponentModel.ISupportInitialize)ImagenPB).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox ImagenPB;
        private Label InformacionLB;
        private Label TituloLB;
        private Panel panel1;
        private Button button2;
        private Button button1;
    }
}