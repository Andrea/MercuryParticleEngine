using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingLibrary;
using ProjectMercury;

namespace BindingApplication
{

    

    public partial class BindingEditorForm : Form
    {

        public event EventHandler BindingChanged;

        public BindingEditorForm()
        {
            InitializeComponent();
        }

        public void Initialize(ParticleEffect particleEffect)
        {

            midiSettingsControl.Initizalize();
            dataControl.Initialize(particleEffect);

            bindingEditorControl1.Initialize(this);

            midiSettingsControl.SelectionChanged += midiSettingsControl_MidiEventSelectionChanged;
            dataControl.SelectionChanged += new EventHandler(dataControl_SelectionChanged);

        }

        void dataControl_SelectionChanged(object sender, EventArgs e)
        {
            if (dataControl.SelectedObject != null && dataControl.SelectedTreeProperty != null)
            {
                SetBinding(dataControl.SelectedObject, dataControl.SelectedTreeProperty);
            }
        }

        void midiSettingsControl_MidiEventSelectionChanged(object sender, EventArgs e)
        {
            // current changed 
            if (midiSettingsControl.SelectedObject != null && midiSettingsControl.SelectedProperty != null)
            {
                SetBinding(midiSettingsControl.SelectedObject, midiSettingsControl.SelectedProperty);
            }
        }

        public IBindableObject BindableObject
        {
            get; private set;
        }

        public string BindableProperty
        {
            get; private set;
        }

        public void SetBinding(IBindableObject bindableObject, string objectProperty)
        {
            if (bindableObject == null)
            {
                BindableObject = null;
                BindableProperty = null;
            }
            else
            {
                BindableObject = bindableObject;
                BindableProperty = objectProperty;
            }

            OnBindingPropertyChanged();
        }

        private void OnBindingPropertyChanged()
        {
            if (BindingChanged != null) BindingChanged(this, EventArgs.Empty);
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
           DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
           DialogResult = DialogResult.Cancel;
        }


        
    }
}
