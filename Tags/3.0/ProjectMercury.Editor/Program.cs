namespace ProjectMercury.Editor
{
    using System;
    using ProjectMercury.Editor.UI;

    public static class Program
    {
        [STAThread]
        static public void Main(string[] args)
        {
            SplashForm.Show(2000);

            using (Editor editor = new Editor())
            {
                editor.Run();
            }
        }
    }
}