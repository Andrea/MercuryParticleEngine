namespace BindingApplication
{
    partial class ConverterEditorControl
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
            this.sourcePropertyTypeLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sourceGB = new System.Windows.Forms.GroupBox();
            this.targetGB = new System.Windows.Forms.GroupBox();
            this.targetPropertyTypeLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.sourceGB.SuspendLayout();
            this.targetGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // sourcePropertyTypeLbl
            // 
            this.sourcePropertyTypeLbl.Location = new System.Drawing.Point(75, 16);
            this.sourcePropertyTypeLbl.Name = "sourcePropertyTypeLbl";
            this.sourcePropertyTypeLbl.Size = new System.Drawing.Size(233, 25);
            this.sourcePropertyTypeLbl.TabIndex = 8;
            this.sourcePropertyTypeLbl.Text = "<Empty>";
            this.sourcePropertyTypeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Type:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sourceGB
            // 
            this.sourceGB.Controls.Add(this.sourcePropertyTypeLbl);
            this.sourceGB.Controls.Add(this.label4);
            this.sourceGB.Location = new System.Drawing.Point(21, 14);
            this.sourceGB.Name = "sourceGB";
            this.sourceGB.Size = new System.Drawing.Size(314, 58);
            this.sourceGB.TabIndex = 9;
            this.sourceGB.TabStop = false;
            this.sourceGB.Text = "Source";
            // 
            // targetGB
            // 
            this.targetGB.Controls.Add(this.targetPropertyTypeLbl);
            this.targetGB.Controls.Add(this.label2);
            this.targetGB.Location = new System.Drawing.Point(21, 78);
            this.targetGB.Name = "targetGB";
            this.targetGB.Size = new System.Drawing.Size(314, 58);
            this.targetGB.TabIndex = 10;
            this.targetGB.TabStop = false;
            this.targetGB.Text = "Target";
            // 
            // targetPropertyTypeLbl
            // 
            this.targetPropertyTypeLbl.Location = new System.Drawing.Point(68, 16);
            this.targetPropertyTypeLbl.Name = "targetPropertyTypeLbl";
            this.targetPropertyTypeLbl.Size = new System.Drawing.Size(246, 25);
            this.targetPropertyTypeLbl.TabIndex = 8;
            this.targetPropertyTypeLbl.Text = "<Empty>";
            this.targetPropertyTypeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Type:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(341, 14);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(197, 216);
            this.propertyGrid1.TabIndex = 11;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // ConverterEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.targetGB);
            this.Controls.Add(this.sourceGB);
            this.Name = "ConverterEditorControl";
            this.Size = new System.Drawing.Size(554, 256);
            this.sourceGB.ResumeLayout(false);
            this.targetGB.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label sourcePropertyTypeLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox sourceGB;
        private System.Windows.Forms.GroupBox targetGB;
        private System.Windows.Forms.Label targetPropertyTypeLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}
