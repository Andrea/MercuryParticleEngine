namespace ProjectMercury.Design.Emitters
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Globalization;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using ProjectMercury.Emitters;
    using Point = System.Drawing.Point;

    public class PolygonPointCollectionEditor : CollectionEditor
    {
        public PolygonPointCollectionEditor()
            : base(typeof(PolygonPointCollection)) { }

        protected override CollectionEditor.CollectionForm CreateCollectionForm()
        {
            return new PolygonPointCollectionForm(this);
        }

        private class PolygonPointCollectionForm : CollectionEditor.CollectionForm
        {
            private Button uxCancel { get; set; }
            private PolygonPointCollectionEditor Editor { get; set; }
            private Label uxInstruction { get; set; }
            private Button uxOK { get; set; }
            private TextBox uxTextEntry { get; set; }

            public PolygonPointCollectionForm(CollectionEditor editor)
                : base(editor)
            {
                this.uxInstruction = new Label();
                this.uxTextEntry = new TextBox();
                this.uxOK = new Button();
                this.uxCancel = new Button();
                this.Editor = (PolygonPointCollectionEditor)editor;
                this.InitializeComponent();
            }

            private void uxTextEntry_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.uxCancel.PerformClick();

                    e.Handled = true;
                }
            }

            private void InitializeComponent()
            {
                this.uxInstruction.Location = new Point(4, 7);
                this.uxInstruction.Size = new Size(0x1a6, 14);
                this.uxInstruction.TabIndex = 0;
                this.uxInstruction.TabStop = false;
                this.uxInstruction.Text = "Enter each Vector2 value on a seperate line.";
                this.uxTextEntry.Location = new Point(4, 0x16);
                this.uxTextEntry.Size = new Size(0x1a6, 0xf4);
                this.uxTextEntry.TabIndex = 0;
                this.uxTextEntry.Text = "";
                this.uxTextEntry.AcceptsTab = false;
                this.uxTextEntry.AcceptsReturn = true;
                this.uxTextEntry.AutoSize = false;
                this.uxTextEntry.Multiline = true;
                this.uxTextEntry.ScrollBars = ScrollBars.Both;
                this.uxTextEntry.WordWrap = false;
                this.uxTextEntry.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                this.uxTextEntry.KeyDown += new KeyEventHandler(this.uxTextEntry_KeyDown);
                this.uxOK.Location = new Point(0xb9, 0x112);
                this.uxOK.Size = new Size(0x4b, 0x17);
                this.uxOK.TabIndex = 1;
                this.uxOK.Text = "OK";
                this.uxOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                this.uxOK.DialogResult = DialogResult.OK;
                this.uxOK.Click += new EventHandler(this.uxOK_Click);
                this.uxCancel.Location = new Point(0x108, 0x112);
                this.uxCancel.Size = new Size(0x4b, 0x17);
                this.uxCancel.TabIndex = 2;
                this.uxCancel.Text = "Cancel";
                this.uxCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                this.uxCancel.DialogResult = DialogResult.Cancel;
                base.Location = new Point(7, 7);
                this.Text = "PolygonPointCollection Editor";
                base.AcceptButton = this.uxOK;
                base.AutoScaleMode = AutoScaleMode.Font;
                base.AutoScaleDimensions = new SizeF(6f, 13f);
                base.CancelButton = this.uxCancel;
                base.ClientSize = new Size(0x1ad, 0x133);
                base.MaximizeBox = false;
                base.MinimizeBox = false;
                base.ControlBox = false;
                base.ShowInTaskbar = false;
                base.StartPosition = FormStartPosition.CenterScreen;
                this.MinimumSize = new Size(300, 200);
                base.Controls.Clear();
                base.Controls.AddRange(new Control[] { this.uxInstruction, this.uxTextEntry, this.uxOK, this.uxCancel });
            }

            private void uxOK_Click(object sender, EventArgs e)
            {
                char[] separator = new char[] { '\n' };
                char[] trimChars = new char[] { '\r' };
                
                string[] strArray = this.uxTextEntry.Text.Split(separator);
                object[] items = base.Items;
                
                int length = strArray.Length;
                
                if ((strArray.Length > 0) && (strArray[strArray.Length - 1].Length == 0))
                {
                    length--;
                }

                Vector2[] numArray = new Vector2[length];

                for (int i = 0; i < length; i++)
                {
                    strArray[i] = strArray[i].Trim(trimChars);
                    
                    try
                    {
                        TypeConverter converter = TypeDescriptor.GetConverter(typeof(Vector2));

                        numArray[i] = (Vector2)converter.ConvertFromInvariantString(strArray[i]);
                    }
                    catch (Exception exception)
                    {
                        base.DisplayError(exception);
                    }
                }

                bool flag = true;
                
                if (length == items.Length)
                {
                    int index = 0;
                    while (index < length)
                    {
                        if (!numArray[index].Equals((Vector2)items[index]))
                        {
                            break;
                        }
                        
                        index++;
                    }
                    if (index == length)
                    {
                        flag = false;
                    }
                }

                if (!flag)
                {
                    base.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    object[] objArray2 = new object[length];
                    
                    for (int j = 0; j < length; j++)
                    {
                        objArray2[j] = numArray[j];
                    }
                    
                    base.Items = objArray2;
                }
            }

            protected override void OnEditValueChanged()
            {
                object[] items = base.Items;
                
                string str = string.Empty;

                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Vector2));
                
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] is Vector2)
                    {
                        str = str + converter.ConvertToString((Vector2)items[i]);
                        
                        if (i != (items.Length - 1))
                        {
                            str = str + "\r\n";
                        }
                    }
                }
                this.uxTextEntry.Text = str;
            }
        }
    }
}