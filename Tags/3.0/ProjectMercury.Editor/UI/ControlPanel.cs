namespace ProjectMercury.Editor.UI
{
    using System;
    using System.Windows.Forms;

    public delegate void CreateEmitterCallback(string type, int budget, float term);
    public delegate void SwitchRendererCallback(string type);
    public delegate void TriggerCallback();
    public delegate void FileOperation(string fileName);

    public partial class ControlPanel : Form
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        public CreateEmitterCallback CreateEmitterCallback;
        public TriggerCallback TriggerCallback;
        public SwitchRendererCallback SwitchRendererCallback;
        public FileOperation LoadTextureCallback;
        public FileOperation LoadEmitterCallback;
        public FileOperation SaveEmitterCallback;

        public object EmitterPropertyGridWrapper
        {
            get { return this.uxEmitterProperties.SelectedObject; }
            set { this.uxEmitterProperties.SelectedObject = value; }
        }

        public object RendererPropertyGridWrapper
        {
            get { return this.uxRendererProperties.SelectedObject; }
            set { this.uxRendererProperties.SelectedObject = value; }
        }

        private void uxEnableAutoTrigger_CheckedChanged(object sender, EventArgs e)
        {
            this.uxTriggerTimer.Enabled = this.uxEnableAutoTrigger.Checked;
        }

        private void uxTriggerFrequency_ValueChanged(object sender, EventArgs e)
        {
            this.uxTriggerTimer.Interval = (int)(this.uxTriggerFrequency.Value * 1000);
        }

        private void uxTriggerTimer_Tick(object sender, EventArgs e)
        {
            if (this.TriggerCallback != null)
                this.TriggerCallback();
        }

        private void uxEmitterCreate_Click(object sender, EventArgs e)
        {
            if (this.CreateEmitterCallback != null)
                this.CreateEmitterCallback(this.uxEmitterTypes.Text,
                                           (int)this.uxEmitterBudget.Value,
                                           (float)this.uxEmitterTerm.Value);
        }

        private void uxRendererType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.SwitchRendererCallback != null)
                this.SwitchRendererCallback(this.uxRendererType.Text);
        }

        private void uxLoadTexture_Click(object sender, EventArgs e)
        {
            if (this.uxLoadTextureDialog.ShowDialog() == DialogResult.OK)
                if (this.LoadTextureCallback != null)
                    this.LoadTextureCallback(this.uxLoadTextureDialog.FileName);
        }

        private void uxOpenEmitter_Click(object sender, EventArgs e)
        {
            if (this.uxOpenEmitterDialog.ShowDialog() == DialogResult.OK)
                if (this.LoadEmitterCallback != null)
                    this.LoadEmitterCallback(this.uxOpenEmitterDialog.FileName);
        }

        private void uxSaveEmitter_Click(object sender, EventArgs e)
        {
            if (this.uxSaveEmitterDialog.ShowDialog() == DialogResult.OK)
                if (this.SaveEmitterCallback != null)
                    this.SaveEmitterCallback(this.uxSaveEmitterDialog.FileName);
        }
    }
}
