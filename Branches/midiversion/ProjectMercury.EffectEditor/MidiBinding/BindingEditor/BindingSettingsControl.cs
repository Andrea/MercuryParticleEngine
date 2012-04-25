using System;
using System.Windows.Forms;

namespace BindingLibrary
{
    public partial class BindingSettingsControl : UserControl
    {
        private ObjectBindingRepository repository;

        public event EventHandler SourceEditButtonClick;
        public event EventHandler TargetEditButtonClick;
        public event EventHandler ConverterEditButtonClick;

        public BindingSettingsControl()
        {
            InitializeComponent();

            
        }

        public void SetRepository(ObjectBindingRepository repository)
        {

            this.repository = repository;
            
            // databinding
            patchBindingSource.DataSource = repository.Repository;
        }


        public int SelectedBinding
        {
            get
            {
                return patchBindingSource.Position;
            }
        }


        private void addBtn_Click(object sender, System.EventArgs e)
        {
            if (repository != null)
            {
                listBox1.SelectedItem = repository.AddNewObjectBinding();
            }
        }

        private void deleteBtn_Click(object sender, System.EventArgs e)
        {
            if (repository != null) repository.RemoveObjectBinding(listBox1.SelectedItem as IObjectBinding);
        }

        private void clearBtn_Click(object sender, System.EventArgs e)
        {
            if (repository != null) repository.ClearRepository();            
        }

        private void sourceBtn_Click(object sender, System.EventArgs e)
        {
            if (SourceEditButtonClick != null) SourceEditButtonClick(this, e);
        }


        private void targetBtn_Click(object sender, System.EventArgs e)
        {
            if (TargetEditButtonClick != null) TargetEditButtonClick(this, e);

        }

       
      
        private void saveBtn_Click(object sender, System.EventArgs e)
        {
            if (repository != null)
            {
                repository.Save();
            }
        }

        private void converterBtn_Click(object sender, EventArgs e)
        {
            if (ConverterEditButtonClick != null) ConverterEditButtonClick(this, e);
        }

    }
}
