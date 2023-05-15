namespace FFXIIIMusicVolumeSlider.AppClasses
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.AboutPictureBox = new System.Windows.Forms.PictureBox();
            this.AboutOKbutton = new System.Windows.Forms.Button();
            this.CreditLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AboutPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AboutPictureBox
            // 
            this.AboutPictureBox.Location = new System.Drawing.Point(36, 12);
            this.AboutPictureBox.Name = "AboutPictureBox";
            this.AboutPictureBox.Size = new System.Drawing.Size(100, 80);
            this.AboutPictureBox.TabIndex = 0;
            this.AboutPictureBox.TabStop = false;
            // 
            // AboutOKbutton
            // 
            this.AboutOKbutton.Location = new System.Drawing.Point(50, 141);
            this.AboutOKbutton.Name = "AboutOKbutton";
            this.AboutOKbutton.Size = new System.Drawing.Size(75, 23);
            this.AboutOKbutton.TabIndex = 1;
            this.AboutOKbutton.Text = "OK";
            this.AboutOKbutton.UseVisualStyleBackColor = true;
            this.AboutOKbutton.Click += new System.EventHandler(this.AboutOKbutton_Click);
            // 
            // CreditLabel
            // 
            this.CreditLabel.AutoSize = true;
            this.CreditLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.CreditLabel.Location = new System.Drawing.Point(15, 110);
            this.CreditLabel.Name = "CreditLabel";
            this.CreditLabel.Size = new System.Drawing.Size(144, 15);
            this.CreditLabel.TabIndex = 2;
            this.CreditLabel.Text = "App Progammer : Surihix";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 176);
            this.Controls.Add(this.CreditLabel);
            this.Controls.Add(this.AboutOKbutton);
            this.Controls.Add(this.AboutPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.AboutPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AboutPictureBox;
        private System.Windows.Forms.Button AboutOKbutton;
        private System.Windows.Forms.Label CreditLabel;
    }
}