namespace BindingApplication.MidiSettings
{
    partial class MidiSettingsForm
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
            this.midiSettingsControl1 = new BindingLibrary.MidiSettingsControl();
            this.SuspendLayout();
            // 
            // midiSettingsControl1
            // 
            this.midiSettingsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midiSettingsControl1.Location = new System.Drawing.Point(0, 0);
            this.midiSettingsControl1.MidiEventList = null;
            this.midiSettingsControl1.Name = "midiSettingsControl1";
            this.midiSettingsControl1.Size = new System.Drawing.Size(284, 385);
            this.midiSettingsControl1.TabIndex = 0;
            // 
            // MidiSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 385);
            this.Controls.Add(this.midiSettingsControl1);
            this.Name = "MidiSettingsForm";
            this.Text = "MidiSettingsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private BindingLibrary.MidiSettingsControl midiSettingsControl1;
    }
}