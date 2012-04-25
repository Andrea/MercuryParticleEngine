namespace ProjectMercury.Editor.UI
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public partial class SplashForm : Form
    {
        private SplashForm()
        {
            InitializeComponent();

            this.uxVersionLabel.Text = string.Format("Version: {0}", base.ProductVersion);
        }

        static private SplashForm Instance { get; set; }

        static public void Show(int duration)
        {
            SplashForm.Instance = new SplashForm();

            SplashForm.Instance.uxCloseTimer.Interval = duration;

            SplashForm.Instance.uxCloseTimer.Tick += delegate
                { SplashForm.Instance.Dispose(); };

            SplashForm.Instance.uxCloseTimer.Start();

            SplashForm.Instance.ShowDialog();
        }
    }
}
