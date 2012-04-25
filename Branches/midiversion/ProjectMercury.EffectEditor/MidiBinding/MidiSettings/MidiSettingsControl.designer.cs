using System.Windows.Forms;


namespace BindingLibrary
{
    partial class MidiSettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

    
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.midiEventsGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.midiEventsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clearListBtn = new System.Windows.Forms.Button();
            this.midiDevicesGroupBox = new System.Windows.Forms.GroupBox();
            this.Enabled = new System.Windows.Forms.CheckBox();
            this.midiDeviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.midiEventsGroupBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.midiEventsBindingSource)).BeginInit();
            this.midiDevicesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.midiDeviceBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // midiEventsGroupBox
            // 
            this.midiEventsGroupBox.Controls.Add(this.tableLayoutPanel2);
            this.midiEventsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midiEventsGroupBox.Location = new System.Drawing.Point(3, 102);
            this.midiEventsGroupBox.Name = "midiEventsGroupBox";
            this.midiEventsGroupBox.Size = new System.Drawing.Size(338, 369);
            this.midiEventsGroupBox.TabIndex = 3;
            this.midiEventsGroupBox.TabStop = false;
            this.midiEventsGroupBox.Text = "Midi Events";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.62738F));
            this.tableLayoutPanel2.Controls.Add(this.listBox2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.clearListBtn, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(332, 350);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.DataSource = this.midiEventsBindingSource;
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(3, 3);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(326, 305);
            this.listBox2.TabIndex = 4;
            // 
            // clearListBtn
            // 
            this.clearListBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearListBtn.Location = new System.Drawing.Point(3, 314);
            this.clearListBtn.Name = "clearListBtn";
            this.clearListBtn.Size = new System.Drawing.Size(326, 33);
            this.clearListBtn.TabIndex = 3;
            this.clearListBtn.Text = "Clear List";
            this.clearListBtn.UseVisualStyleBackColor = true;
            this.clearListBtn.Click += new System.EventHandler(this.clearListButton_Click);
            // 
            // midiDevicesGroupBox
            // 
            this.midiDevicesGroupBox.Controls.Add(this.Enabled);
            this.midiDevicesGroupBox.Controls.Add(this.listBox1);
            this.midiDevicesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midiDevicesGroupBox.Location = new System.Drawing.Point(3, 3);
            this.midiDevicesGroupBox.Name = "midiDevicesGroupBox";
            this.midiDevicesGroupBox.Size = new System.Drawing.Size(338, 93);
            this.midiDevicesGroupBox.TabIndex = 2;
            this.midiDevicesGroupBox.TabStop = false;
            this.midiDevicesGroupBox.Text = "Midi Devices";
            // 
            // Enabled
            // 
            this.Enabled.AutoSize = true;
            this.Enabled.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.midiDeviceBindingSource, "Enabled", true));
            this.Enabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.Enabled.Location = new System.Drawing.Point(3, 72);
            this.Enabled.Name = "Enabled";
            this.Enabled.Size = new System.Drawing.Size(332, 17);
            this.Enabled.TabIndex = 2;
            this.Enabled.Text = "Enabled";
            this.Enabled.UseVisualStyleBackColor = true;
            this.Enabled.CheckedChanged += new System.EventHandler(this.Enabled_CheckedChanged);
            // 
            // midiDeviceBindingSource
            // 
            this.midiDeviceBindingSource.DataMember = "MidiDeviceList";
            this.midiDeviceBindingSource.DataSource = typeof(BindingLibrary.MidiDeviceManager);
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.midiDeviceBindingSource;
            this.listBox1.DisplayMember = "Name";
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 16);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(332, 56);
            this.listBox1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.midiDevicesGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.midiEventsGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.propertyGrid1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(344, 542);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 477);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(338, 62);
            this.propertyGrid1.TabIndex = 4;
            this.propertyGrid1.ToolbarVisible = false;
            this.propertyGrid1.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGrid1_SelectedGridItemChanged);
            // 
            // MidiSettingsControl
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MidiSettingsControl";
            this.Size = new System.Drawing.Size(344, 542);
            this.midiEventsGroupBox.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.midiEventsBindingSource)).EndInit();
            this.midiDevicesGroupBox.ResumeLayout(false);
            this.midiDevicesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.midiDeviceBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox midiEventsGroupBox;
        private System.Windows.Forms.GroupBox midiDevicesGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button clearListBtn;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.CheckBox Enabled;
        private BindingSource midiDeviceBindingSource;
        private BindingSource midiEventsBindingSource;
        private PropertyGrid propertyGrid1;


    }
}
