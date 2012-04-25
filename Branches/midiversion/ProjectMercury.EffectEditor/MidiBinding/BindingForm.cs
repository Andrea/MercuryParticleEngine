using System;
using System.Windows.Forms;
using BindingApplication;
using BindingApplication.BindingEditor;
using BindingApplication.MidiSettings;
using BindingLibrary;
using ProjectMercury;

namespace WinFormsGraphicsDevice
{
    /// <summary>
    /// Custom form provides the main user interface for the program.
    /// In this sample we used the designer to add a splitter pane to the form,
    /// which contains a SpriteFontControl and a SpinningTriangleControl.
    /// </summary>
    public partial class BindingForm : Form
    {

        private ParticleEffect particleEffect;


        public BindingForm()
        {
            InitializeComponent();

        

        }

        public ObjectBindingRepository Repository
        {
            get { return particleEffect.BindingRepository; }
           
        }

        public ParticleEffect ParticleEffect
        {
            get { return particleEffect; }
            set {
                particleEffect = value;

                bindingSettingsControl.SetRepository(particleEffect.BindingRepository);
            }
        }


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


        private void midiConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidiSettingsForm form = new MidiSettingsForm();
            form.Initialize();
            form.Show(this);
        }

   

      

        private void bindingSettingsControl_TargetEditButtonClick(object sender, EventArgs e)
        {

            if (particleEffect.BindingRepository == null) return;

            var propertyForm = new BindingEditorForm();

            // Edit target property
            var current = particleEffect.BindingRepository.GetObjectBinding(bindingSettingsControl.SelectedBinding);
            if (current != null)
            {

                propertyForm.Initialize(ParticleEffect);

                propertyForm.SetBinding(current.TargetObject, current.TargetProperty);

                DialogResult result = propertyForm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    particleEffect.BindingRepository.SetBindingTarget(bindingSettingsControl.SelectedBinding, propertyForm.BindableObject, propertyForm.BindableProperty);
                }

            }

            propertyForm.Dispose();
        }

        private void bindingSettingsControl_SourceEditButtonClick(object sender, EventArgs e)
        {
            if (particleEffect.BindingRepository == null) return;

            var propertyForm = new BindingEditorForm();

            // Edit soure property
            var current = particleEffect.BindingRepository.GetObjectBinding(bindingSettingsControl.SelectedBinding);
            if (current != null)
            {
                propertyForm.Initialize(ParticleEffect);

                propertyForm.SetBinding(current.SourceObject, current.SourceProperty);

                DialogResult result = propertyForm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    particleEffect.BindingRepository.SetBindingSource(bindingSettingsControl.SelectedBinding, propertyForm.BindableObject, propertyForm.BindableProperty);
                }
            }
            propertyForm.Dispose();

        }

        private void bindingSettingsControl_ConverterEditButtonClick(object sender, EventArgs e)
        {
            if (particleEffect.BindingRepository == null) return;

            var propertyForm = new ConverterEditorForm();

            // Edit soure property
            var current = particleEffect.BindingRepository.GetObjectBinding(bindingSettingsControl.SelectedBinding);
            if (current != null)
            {

                propertyForm.SetBinding(current);

                // the form edits the binding directly.
                propertyForm.ShowDialog(this);

/*
                if (result == DialogResult.OK)
                {
                    repository.SetBindingConverter(bindingSettingsControl.SelectedBinding, propertyForm.BindingConverter);
                }
*/
            }
            propertyForm.Dispose();
        }
    }
}
