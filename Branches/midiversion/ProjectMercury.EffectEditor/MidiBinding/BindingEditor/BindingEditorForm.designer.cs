using System.Windows.Forms;
using BindingLibrary;

namespace BindingApplication
{
    partial class BindingEditorForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bindingEditorControl1 = new BindingApplication.BindingEditorControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.midiSettingsControl = new BindingLibrary.MidiSettingsControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataControl = new BindingLibrary.DataTreeControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.applyBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.bindingEditorControl1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(704, 627);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // bindingEditorControl1
            // 
            this.bindingEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bindingEditorControl1.Location = new System.Drawing.Point(359, 3);
            this.bindingEditorControl1.Name = "bindingEditorControl1";
            this.bindingEditorControl1.Size = new System.Drawing.Size(342, 591);
            this.bindingEditorControl1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(342, 591);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.midiSettingsControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(334, 565);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // midiSettingsControl
            // 
            this.midiSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midiSettingsControl.Location = new System.Drawing.Point(3, 3);
            this.midiSettingsControl.MidiEventList = null;
            this.midiSettingsControl.Name = "midiSettingsControl";
            this.midiSettingsControl.Size = new System.Drawing.Size(328, 559);
            this.midiSettingsControl.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(334, 565);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataControl
            // 
            this.dataControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataControl.Location = new System.Drawing.Point(3, 3);
            this.dataControl.Name = "dataControl";
            this.dataControl.Size = new System.Drawing.Size(328, 559);
            this.dataControl.TabIndex = 0;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.applyBtn);
            this.panel1.Controls.Add(this.CancelBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 597);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 30);
            this.panel1.TabIndex = 2;
            // 
            // applyBtn
            // 
            this.applyBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.applyBtn.Location = new System.Drawing.Point(516, 0);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(94, 30);
            this.applyBtn.TabIndex = 0;
            this.applyBtn.Text = "Ok";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.CancelBtn.Location = new System.Drawing.Point(610, 0);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(94, 30);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(221, 204);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(27, 28);
            this.addBtn.TabIndex = 0;
            this.addBtn.Text = "=>";
            this.addBtn.UseVisualStyleBackColor = true;
            // 
            // BindingEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 627);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BindingEditorForm";
            this.Text = "PatchItemPropertyForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MidiSettingsControl midiSettingsControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private BindingEditorControl bindingEditorControl1;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private DataTreeControl dataControl;
        private System.Windows.Forms.Button addBtn;
    }
}