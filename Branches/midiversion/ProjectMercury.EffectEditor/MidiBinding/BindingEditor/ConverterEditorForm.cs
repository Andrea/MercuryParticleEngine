using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingLibrary;

namespace BindingApplication.BindingEditor
{
    public partial class ConverterEditorForm : Form
    {
        public event EventHandler BindingChanged;


        public ConverterEditorForm()
        {
            InitializeComponent();

            converterEditorControl1.Initialize(this);

        }

        public IObjectBinding Binding { get; set; }


        public void SetBinding(IObjectBinding bindableObject)
        {

            if (bindableObject == null)
            {
                Binding = null;
            }
            else
            {
                Binding = bindableObject;
            }

            OnBindingPropertyChanged();
        }

        
        

        private void OnBindingPropertyChanged()
        {
            if (BindingChanged != null) BindingChanged(this, EventArgs.Empty);
        }



        private void applyBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

        }
    }
}
