namespace ProjectMercury.Design.UITypeEditors
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using Microsoft.Xna.Framework;

    //YES I'm British OK!
    using Colour = System.Drawing.Color;

    public class ColourEditor : ColorEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var xnaColour = (Vector3)value;

            if (provider != null)
            {
                var edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (edSvc == null)
                    return value;

                this.StartColourPicker(edSvc, Colour.FromArgb(1, (int)(xnaColour.X * 255f),
                                                                 (int)(xnaColour.Y * 255f),
                                                                 (int)(xnaColour.Z * 255f)));
                edSvc.DropDownControl(this.ColourPicker);

                if ((this.ColorPickerValue != null) && (((Colour)this.ColorPickerValue) != Colour.Empty))
                {
                    var chosenColour = (Colour)this.ColorPickerValue;

                    value = new Vector3
                    {
                        X = chosenColour.R / 255f,
                        Y = chosenColour.G / 255f,
                        Z = chosenColour.B / 255f
                    };
                }

                this.EndColourPicker();
            }

            return value;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value is Vector3)
            {
                var colour = (Vector3)e.Value;

                var displayColor = Colour.FromArgb((int)(colour.X * 255f),
                                                   (int)(colour.Y * 255f),
                                                   (int)(colour.Z * 255f));

                using (var brush = new SolidBrush(displayColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
        }

        private void EnsureColourPicker()
        {
            if (this.ColourPicker == null)
            {
                var uiType = typeof(ColorEditor).GetNestedType("ColorUI", BindingFlags.NonPublic);

                var ui = Activator.CreateInstance(uiType, new object[] { this });

                this.ColourPicker = (Control)ui;
            }
        }

        private Control ColourPicker
        {
            get
            {
                var field = typeof(ColorEditor).GetField("colorUI", BindingFlags.Instance |
                                                                    BindingFlags.NonPublic);
                return (Control)field.GetValue(this);
            }
            set
            {
                var field = typeof(ColorEditor).GetField("colorUI", BindingFlags.Instance |
                                                                    BindingFlags.NonPublic);
                field.SetValue(this, value);
            }
        }

        private object ColorPickerValue
        {
            get
            {
                var valueProperty = this.ColourPicker.GetType().GetProperty("Value");

                return valueProperty.GetValue(this.ColourPicker, null);
            }
        }

        private void StartColourPicker(IWindowsFormsEditorService edSvc, object value)
        {
            this.EnsureColourPicker();

            var startMethod = this.ColourPicker.GetType().GetMethod("Start");

            startMethod.Invoke(this.ColourPicker, new object[] { edSvc, value });
        }

        private void EndColourPicker()
        {
            var endMethod = this.ColourPicker.GetType().GetMethod("End");

            endMethod.Invoke(this.ColourPicker, null);
        }
    }
}