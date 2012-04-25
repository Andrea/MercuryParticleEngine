namespace ProjectMercury.Editor.UI
{
    partial class SplashForm
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
            this.components = new System.ComponentModel.Container();
            this.uxVersionLabel = new System.Windows.Forms.Label();
            this.uxCloseTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // uxVersionLabel
            // 
            this.uxVersionLabel.AutoSize = true;
            this.uxVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.uxVersionLabel.ForeColor = System.Drawing.Color.LightGray;
            this.uxVersionLabel.Location = new System.Drawing.Point(545, 156);
            this.uxVersionLabel.Name = "uxVersionLabel";
            this.uxVersionLabel.Size = new System.Drawing.Size(79, 13);
            this.uxVersionLabel.TabIndex = 0;
            this.uxVersionLabel.Text = "uxVersionLabel";
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ProjectMercury.Editor.Properties.Resources.MercurySplash;
            this.ClientSize = new System.Drawing.Size(720, 240);
            this.ControlBox = false;
            this.Controls.Add(this.uxVersionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label uxVersionLabel;
        private System.Windows.Forms.Timer uxCloseTimer;
    }
}