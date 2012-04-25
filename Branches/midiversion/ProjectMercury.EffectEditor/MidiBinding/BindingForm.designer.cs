namespace WinFormsGraphicsDevice
{
    partial class BindingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

   

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bindingSettingsControl = new BindingLibrary.BindingSettingsControl();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configMnuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.midiConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingSettingsControl
            // 
            this.bindingSettingsControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.bindingSettingsControl.Location = new System.Drawing.Point(0, 24);
            this.bindingSettingsControl.Name = "bindingSettingsControl";
            this.bindingSettingsControl.Size = new System.Drawing.Size(526, 469);
            this.bindingSettingsControl.TabIndex = 7;
            this.bindingSettingsControl.SourceEditButtonClick += new System.EventHandler(this.bindingSettingsControl_SourceEditButtonClick);
            this.bindingSettingsControl.TargetEditButtonClick += new System.EventHandler(this.bindingSettingsControl_TargetEditButtonClick);
            this.bindingSettingsControl.ConverterEditButtonClick += new System.EventHandler(this.bindingSettingsControl_ConverterEditButtonClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configMnuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(526, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configMnuItem
            // 
            this.configMnuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.midiConfigurationToolStripMenuItem,
            this.layerConfigurationToolStripMenuItem});
            this.configMnuItem.Name = "configMnuItem";
            this.configMnuItem.Size = new System.Drawing.Size(72, 20);
            this.configMnuItem.Text = "Configure";
            // 
            // midiConfigurationToolStripMenuItem
            // 
            this.midiConfigurationToolStripMenuItem.Name = "midiConfigurationToolStripMenuItem";
            this.midiConfigurationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.midiConfigurationToolStripMenuItem.Text = "Midi Configuration";
            this.midiConfigurationToolStripMenuItem.Click += new System.EventHandler(this.midiConfigurationToolStripMenuItem_Click);
            // 
            // layerConfigurationToolStripMenuItem
            // 
            this.layerConfigurationToolStripMenuItem.Name = "layerConfigurationToolStripMenuItem";
            this.layerConfigurationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.layerConfigurationToolStripMenuItem.Text = "Layer Configuration";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 493);
            this.Controls.Add(this.bindingSettingsControl);
            this.Controls.Add(this.menuStrip1);
            this.Name = "BindingForm";
            this.Text = "WinForms Graphics Device";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configMnuItem;
        private System.Windows.Forms.ToolStripMenuItem midiConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerConfigurationToolStripMenuItem;
        private BindingLibrary.BindingSettingsControl bindingSettingsControl;
    }
}

