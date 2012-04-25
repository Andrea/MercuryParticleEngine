namespace BindingApplication.BindingEditor
{
    partial class ConverterEditorForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.applyBtn = new System.Windows.Forms.Button();
            this.converterEditorControl1 = new BindingApplication.ConverterEditorControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.applyBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 255);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 30);
            this.panel1.TabIndex = 3;
            // 
            // applyBtn
            // 
            this.applyBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.applyBtn.Location = new System.Drawing.Point(459, 0);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(94, 30);
            this.applyBtn.TabIndex = 0;
            this.applyBtn.Text = "Ok";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // converterEditorControl1
            // 
            this.converterEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.converterEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.converterEditorControl1.Name = "converterEditorControl1";
            this.converterEditorControl1.Size = new System.Drawing.Size(553, 255);
            this.converterEditorControl1.TabIndex = 4;
            // 
            // ConverterEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 285);
            this.Controls.Add(this.converterEditorControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ConverterEditorForm";
            this.Text = "ConverterEditorForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button applyBtn;
        private ConverterEditorControl converterEditorControl1;
    }
}