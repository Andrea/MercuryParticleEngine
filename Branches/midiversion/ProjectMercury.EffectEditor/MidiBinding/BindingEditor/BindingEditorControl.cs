using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BindingApplication
{
    public partial class BindingEditorControl : UserControl
    {
        private bool blockEvents;
        private BindingEditorForm form;

        public BindingEditorControl()
        {
            InitializeComponent();

        }


        public void Initialize(BindingEditorForm form)
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
        }


        private void UpdateGui()
        {

            try
            {
                blockEvents = true;

                 // update
                if (form.BindableObject == null)
                {
                    propertyTypeLbl.Text = "<empty>";
                    propertyValueLbl.Text = "<empty>";
                    objectLbl.Text = "<empty>";
                    objectToStringLbl.Text = "<empty>";
                }
                else
                {

                    // update
                    if (form.BindableProperty != null)
                    {
/*
                        propertyCBox.SelectedIndex =
                            TypeDescriptor.GetProperties(form.BindableObject).IndexOf(form.BindableProperty);
*/
                        propertyTypeLbl.Text = form.BindableProperty;
/*
                        propertyValueLbl.Text =
                            form.BindableProperty.GetValue(form.BindableObject.BindingObject).ToString();
*/

                        objectLbl.Text = form.BindableObject.BindingObject.GetType().Name;
                        objectToStringLbl.Text = form.BindableObject.BindingObject.ToString();

                    }
                }
            }
            finally
            {
                blockEvents = false;
            }            

        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            if (blockEvents) return;

            form.SetBinding(null, null);
        }

    }
}
