namespace ProjectMercury.Editor.UI
{
    partial class ControlPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            this.uxCreationGroup = new System.Windows.Forms.GroupBox();
            this.uxTabs = new System.Windows.Forms.TabControl();
            this.exEmitterTab = new System.Windows.Forms.TabPage();
            this.uxEmitterTerm = new System.Windows.Forms.NumericUpDown();
            this.uxEmitterCreate = new System.Windows.Forms.Button();
            this.uxEmitterBudget = new System.Windows.Forms.NumericUpDown();
            this.uxEmitterTypes = new System.Windows.Forms.ComboBox();
            this.uxTriggerTab = new System.Windows.Forms.TabPage();
            this.uxTriggerFrequency = new System.Windows.Forms.NumericUpDown();
            this.uxEnableAutoTrigger = new System.Windows.Forms.CheckBox();
            this.uxRendererTab = new System.Windows.Forms.TabPage();
            this.uxRendererProperties = new System.Windows.Forms.PropertyGrid();
            this.uxRendererType = new System.Windows.Forms.ComboBox();
            this.uxControlGroup = new System.Windows.Forms.GroupBox();
            this.uxLoadTexture = new System.Windows.Forms.Button();
            this.uxSaveEmitter = new System.Windows.Forms.Button();
            this.uxOpenEmitter = new System.Windows.Forms.Button();
            this.uxPropertiesGroup = new System.Windows.Forms.GroupBox();
            this.uxEmitterProperties = new System.Windows.Forms.PropertyGrid();
            this.uxTriggerTimer = new System.Windows.Forms.Timer(this.components);
            this.uxLoadTextureDialog = new System.Windows.Forms.OpenFileDialog();
            this.uxOpenEmitterDialog = new System.Windows.Forms.OpenFileDialog();
            this.uxSaveEmitterDialog = new System.Windows.Forms.SaveFileDialog();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            this.uxCreationGroup.SuspendLayout();
            this.uxTabs.SuspendLayout();
            this.exEmitterTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxEmitterTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxEmitterBudget)).BeginInit();
            this.uxTriggerTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxTriggerFrequency)).BeginInit();
            this.uxRendererTab.SuspendLayout();
            this.uxControlGroup.SuspendLayout();
            this.uxPropertiesGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 35);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 13);
            label1.TabIndex = 4;
            label1.Text = "Budget";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 62);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(31, 13);
            label2.TabIndex = 5;
            label2.Text = "Term";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 31);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(93, 13);
            label3.TabIndex = 2;
            label3.Text = "Trigger Frequency";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 9);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(66, 13);
            label4.TabIndex = 7;
            label4.Text = "Emitter Type";
            // 
            // uxCreationGroup
            // 
            this.uxCreationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxCreationGroup.Controls.Add(this.uxTabs);
            this.uxCreationGroup.Location = new System.Drawing.Point(3, 3);
            this.uxCreationGroup.Name = "uxCreationGroup";
            this.uxCreationGroup.Size = new System.Drawing.Size(288, 166);
            this.uxCreationGroup.TabIndex = 0;
            this.uxCreationGroup.TabStop = false;
            this.uxCreationGroup.Text = "Emitter && Renderer";
            // 
            // uxTabs
            // 
            this.uxTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxTabs.Controls.Add(this.exEmitterTab);
            this.uxTabs.Controls.Add(this.uxTriggerTab);
            this.uxTabs.Controls.Add(this.uxRendererTab);
            this.uxTabs.Location = new System.Drawing.Point(6, 19);
            this.uxTabs.Name = "uxTabs";
            this.uxTabs.SelectedIndex = 0;
            this.uxTabs.Size = new System.Drawing.Size(276, 141);
            this.uxTabs.TabIndex = 0;
            // 
            // exEmitterTab
            // 
            this.exEmitterTab.Controls.Add(label4);
            this.exEmitterTab.Controls.Add(this.uxEmitterTerm);
            this.exEmitterTab.Controls.Add(label2);
            this.exEmitterTab.Controls.Add(label1);
            this.exEmitterTab.Controls.Add(this.uxEmitterCreate);
            this.exEmitterTab.Controls.Add(this.uxEmitterBudget);
            this.exEmitterTab.Controls.Add(this.uxEmitterTypes);
            this.exEmitterTab.Location = new System.Drawing.Point(4, 22);
            this.exEmitterTab.Name = "exEmitterTab";
            this.exEmitterTab.Padding = new System.Windows.Forms.Padding(3);
            this.exEmitterTab.Size = new System.Drawing.Size(268, 115);
            this.exEmitterTab.TabIndex = 0;
            this.exEmitterTab.Text = "Emitter";
            this.exEmitterTab.UseVisualStyleBackColor = true;
            // 
            // uxEmitterTerm
            // 
            this.uxEmitterTerm.DecimalPlaces = 1;
            this.uxEmitterTerm.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.uxEmitterTerm.Location = new System.Drawing.Point(118, 59);
            this.uxEmitterTerm.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.uxEmitterTerm.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.uxEmitterTerm.Name = "uxEmitterTerm";
            this.uxEmitterTerm.Size = new System.Drawing.Size(144, 20);
            this.uxEmitterTerm.TabIndex = 6;
            this.uxEmitterTerm.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // uxEmitterCreate
            // 
            this.uxEmitterCreate.Location = new System.Drawing.Point(118, 86);
            this.uxEmitterCreate.Name = "uxEmitterCreate";
            this.uxEmitterCreate.Size = new System.Drawing.Size(144, 23);
            this.uxEmitterCreate.TabIndex = 3;
            this.uxEmitterCreate.Text = "Apply";
            this.uxEmitterCreate.UseVisualStyleBackColor = true;
            this.uxEmitterCreate.Click += new System.EventHandler(this.uxEmitterCreate_Click);
            // 
            // uxEmitterBudget
            // 
            this.uxEmitterBudget.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.uxEmitterBudget.Location = new System.Drawing.Point(118, 33);
            this.uxEmitterBudget.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.uxEmitterBudget.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxEmitterBudget.Name = "uxEmitterBudget";
            this.uxEmitterBudget.Size = new System.Drawing.Size(144, 20);
            this.uxEmitterBudget.TabIndex = 1;
            this.uxEmitterBudget.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // uxEmitterTypes
            // 
            this.uxEmitterTypes.FormattingEnabled = true;
            this.uxEmitterTypes.Items.AddRange(new object[] {
            "Emitter",
            "CircleEmitter",
            "ConeEmitter",
            "LineEmitter",
            "PolygonEmitter",
            "RectEmitter"});
            this.uxEmitterTypes.Location = new System.Drawing.Point(118, 6);
            this.uxEmitterTypes.Name = "uxEmitterTypes";
            this.uxEmitterTypes.Size = new System.Drawing.Size(144, 21);
            this.uxEmitterTypes.TabIndex = 0;
            this.uxEmitterTypes.Text = "Emitter";
            // 
            // uxTriggerTab
            // 
            this.uxTriggerTab.Controls.Add(label3);
            this.uxTriggerTab.Controls.Add(this.uxTriggerFrequency);
            this.uxTriggerTab.Controls.Add(this.uxEnableAutoTrigger);
            this.uxTriggerTab.Location = new System.Drawing.Point(4, 22);
            this.uxTriggerTab.Name = "uxTriggerTab";
            this.uxTriggerTab.Padding = new System.Windows.Forms.Padding(3);
            this.uxTriggerTab.Size = new System.Drawing.Size(268, 115);
            this.uxTriggerTab.TabIndex = 2;
            this.uxTriggerTab.Text = "Trigger";
            this.uxTriggerTab.UseVisualStyleBackColor = true;
            // 
            // uxTriggerFrequency
            // 
            this.uxTriggerFrequency.DecimalPlaces = 2;
            this.uxTriggerFrequency.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.uxTriggerFrequency.Location = new System.Drawing.Point(118, 29);
            this.uxTriggerFrequency.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.uxTriggerFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.uxTriggerFrequency.Name = "uxTriggerFrequency";
            this.uxTriggerFrequency.Size = new System.Drawing.Size(150, 20);
            this.uxTriggerFrequency.TabIndex = 1;
            this.uxTriggerFrequency.Value = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.uxTriggerFrequency.ValueChanged += new System.EventHandler(this.uxTriggerFrequency_ValueChanged);
            // 
            // uxEnableAutoTrigger
            // 
            this.uxEnableAutoTrigger.AutoSize = true;
            this.uxEnableAutoTrigger.Location = new System.Drawing.Point(118, 6);
            this.uxEnableAutoTrigger.Name = "uxEnableAutoTrigger";
            this.uxEnableAutoTrigger.Size = new System.Drawing.Size(120, 17);
            this.uxEnableAutoTrigger.TabIndex = 0;
            this.uxEnableAutoTrigger.Text = "Enable Auto Trigger";
            this.uxEnableAutoTrigger.UseVisualStyleBackColor = true;
            this.uxEnableAutoTrigger.CheckedChanged += new System.EventHandler(this.uxEnableAutoTrigger_CheckedChanged);
            // 
            // uxRendererTab
            // 
            this.uxRendererTab.Controls.Add(this.uxRendererProperties);
            this.uxRendererTab.Controls.Add(this.uxRendererType);
            this.uxRendererTab.Location = new System.Drawing.Point(4, 22);
            this.uxRendererTab.Name = "uxRendererTab";
            this.uxRendererTab.Padding = new System.Windows.Forms.Padding(3);
            this.uxRendererTab.Size = new System.Drawing.Size(268, 115);
            this.uxRendererTab.TabIndex = 1;
            this.uxRendererTab.Text = "Renderer";
            this.uxRendererTab.UseVisualStyleBackColor = true;
            // 
            // uxRendererProperties
            // 
            this.uxRendererProperties.HelpVisible = false;
            this.uxRendererProperties.Location = new System.Drawing.Point(6, 33);
            this.uxRendererProperties.Name = "uxRendererProperties";
            this.uxRendererProperties.Size = new System.Drawing.Size(262, 76);
            this.uxRendererProperties.TabIndex = 1;
            this.uxRendererProperties.ToolbarVisible = false;
            // 
            // uxRendererType
            // 
            this.uxRendererType.FormattingEnabled = true;
            this.uxRendererType.Items.AddRange(new object[] {
            "PointSpriteRenderer",
            "SpriteBatchRenderer"});
            this.uxRendererType.Location = new System.Drawing.Point(6, 6);
            this.uxRendererType.Name = "uxRendererType";
            this.uxRendererType.Size = new System.Drawing.Size(262, 21);
            this.uxRendererType.TabIndex = 0;
            this.uxRendererType.Text = "PointSpriteRenderer";
            this.uxRendererType.SelectedIndexChanged += new System.EventHandler(this.uxRendererType_SelectionChangeCommitted);
            // 
            // uxControlGroup
            // 
            this.uxControlGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxControlGroup.Controls.Add(this.uxLoadTexture);
            this.uxControlGroup.Controls.Add(this.uxSaveEmitter);
            this.uxControlGroup.Controls.Add(this.uxOpenEmitter);
            this.uxControlGroup.Location = new System.Drawing.Point(3, 489);
            this.uxControlGroup.Name = "uxControlGroup";
            this.uxControlGroup.Size = new System.Drawing.Size(288, 106);
            this.uxControlGroup.TabIndex = 1;
            this.uxControlGroup.TabStop = false;
            this.uxControlGroup.Text = "Load && Save";
            // 
            // uxLoadTexture
            // 
            this.uxLoadTexture.Location = new System.Drawing.Point(6, 19);
            this.uxLoadTexture.Name = "uxLoadTexture";
            this.uxLoadTexture.Size = new System.Drawing.Size(276, 23);
            this.uxLoadTexture.TabIndex = 2;
            this.uxLoadTexture.Text = "Load Texture";
            this.uxLoadTexture.UseVisualStyleBackColor = true;
            this.uxLoadTexture.Click += new System.EventHandler(this.uxLoadTexture_Click);
            // 
            // uxSaveEmitter
            // 
            this.uxSaveEmitter.Location = new System.Drawing.Point(6, 77);
            this.uxSaveEmitter.Name = "uxSaveEmitter";
            this.uxSaveEmitter.Size = new System.Drawing.Size(276, 23);
            this.uxSaveEmitter.TabIndex = 1;
            this.uxSaveEmitter.Text = "Save Emitter";
            this.uxSaveEmitter.UseVisualStyleBackColor = true;
            this.uxSaveEmitter.Click += new System.EventHandler(this.uxSaveEmitter_Click);
            // 
            // uxOpenEmitter
            // 
            this.uxOpenEmitter.Location = new System.Drawing.Point(6, 48);
            this.uxOpenEmitter.Name = "uxOpenEmitter";
            this.uxOpenEmitter.Size = new System.Drawing.Size(276, 23);
            this.uxOpenEmitter.TabIndex = 0;
            this.uxOpenEmitter.Text = "Open Emitter";
            this.uxOpenEmitter.UseVisualStyleBackColor = true;
            this.uxOpenEmitter.Click += new System.EventHandler(this.uxOpenEmitter_Click);
            // 
            // uxPropertiesGroup
            // 
            this.uxPropertiesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxPropertiesGroup.Controls.Add(this.uxEmitterProperties);
            this.uxPropertiesGroup.Location = new System.Drawing.Point(3, 175);
            this.uxPropertiesGroup.Name = "uxPropertiesGroup";
            this.uxPropertiesGroup.Size = new System.Drawing.Size(288, 308);
            this.uxPropertiesGroup.TabIndex = 2;
            this.uxPropertiesGroup.TabStop = false;
            this.uxPropertiesGroup.Text = "Properties";
            // 
            // uxEmitterProperties
            // 
            this.uxEmitterProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxEmitterProperties.Location = new System.Drawing.Point(6, 19);
            this.uxEmitterProperties.Name = "uxEmitterProperties";
            this.uxEmitterProperties.Size = new System.Drawing.Size(276, 283);
            this.uxEmitterProperties.TabIndex = 0;
            // 
            // uxTriggerTimer
            // 
            this.uxTriggerTimer.Interval = 500;
            this.uxTriggerTimer.Tick += new System.EventHandler(this.uxTriggerTimer_Tick);
            // 
            // uxLoadTextureDialog
            // 
            this.uxLoadTextureDialog.Filter = "All Files|*.*";
            // 
            // uxOpenEmitterDialog
            // 
            this.uxOpenEmitterDialog.Filter = "Emitter files|*.em";
            // 
            // uxSaveEmitterDialog
            // 
            this.uxSaveEmitterDialog.Filter = "Emitter files|*.em";
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(295, 598);
            this.ControlBox = false;
            this.Controls.Add(this.uxPropertiesGroup);
            this.Controls.Add(this.uxControlGroup);
            this.Controls.Add(this.uxCreationGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ControlPanel";
            this.ShowInTaskbar = false;
            this.uxCreationGroup.ResumeLayout(false);
            this.uxTabs.ResumeLayout(false);
            this.exEmitterTab.ResumeLayout(false);
            this.exEmitterTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxEmitterTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxEmitterBudget)).EndInit();
            this.uxTriggerTab.ResumeLayout(false);
            this.uxTriggerTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxTriggerFrequency)).EndInit();
            this.uxRendererTab.ResumeLayout(false);
            this.uxControlGroup.ResumeLayout(false);
            this.uxPropertiesGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox uxCreationGroup;
        private System.Windows.Forms.GroupBox uxControlGroup;
        private System.Windows.Forms.GroupBox uxPropertiesGroup;
        private System.Windows.Forms.TabControl uxTabs;
        private System.Windows.Forms.TabPage exEmitterTab;
        private System.Windows.Forms.TabPage uxRendererTab;
        private System.Windows.Forms.Button uxSaveEmitter;
        private System.Windows.Forms.Button uxOpenEmitter;
        private System.Windows.Forms.PropertyGrid uxEmitterProperties;
        private System.Windows.Forms.NumericUpDown uxEmitterTerm;
        private System.Windows.Forms.Button uxEmitterCreate;
        private System.Windows.Forms.NumericUpDown uxEmitterBudget;
        private System.Windows.Forms.ComboBox uxEmitterTypes;
        private System.Windows.Forms.PropertyGrid uxRendererProperties;
        private System.Windows.Forms.ComboBox uxRendererType;
        private System.Windows.Forms.Button uxLoadTexture;
        private System.Windows.Forms.TabPage uxTriggerTab;
        private System.Windows.Forms.NumericUpDown uxTriggerFrequency;
        private System.Windows.Forms.CheckBox uxEnableAutoTrigger;
        private System.Windows.Forms.Timer uxTriggerTimer;
        private System.Windows.Forms.OpenFileDialog uxLoadTextureDialog;
        private System.Windows.Forms.OpenFileDialog uxOpenEmitterDialog;
        private System.Windows.Forms.SaveFileDialog uxSaveEmitterDialog;

    }
}
