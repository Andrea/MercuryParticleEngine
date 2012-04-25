using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingApplication.BindingEditor;

namespace BindingApplication
{
    public partial class ConverterEditorControl : UserControl
    {
        private bool blockEvents;
        private ConverterEditorForm form;

        public ConverterEditorControl()
        {
            InitializeComponent();

        }


        public void Initialize(ConverterEditorForm form)
        {
            this.form = form;

            form.BindingChanged += form_BindingChanged;
                
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

                form.BindingChanged -= form_BindingChanged;

                form = null;
            }
            base.Dispose(disposing);
        }

        void form_BindingChanged(object sender, EventArgs e)
        {
            UpdateGui();

            this.propertyGrid1.SelectedObject = form.Binding.BindingConverter;
        }


        private void UpdateGui()
        {

            try
            {
                blockEvents = true;

                 // update
                if (form.Binding == null || form.Binding.SourceProperty == null)
                {
                    sourcePropertyTypeLbl.Text = "<empty>";
                }
                else
                {
                    sourcePropertyTypeLbl.Text = form.Binding.SourceProperty;
                } 
                
                
                // update
                if (form.Binding == null || form.Binding.TargetProperty == null)
                {
                    targetPropertyTypeLbl.Text = "<empty>";
                }
                else
                {
                    targetPropertyTypeLbl.Text = form.Binding.TargetProperty;
                }
            }
            finally
            {
                blockEvents = false;
            }            

        }

    }
}
