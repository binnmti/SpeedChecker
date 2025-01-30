using Microsoft.Win32;

namespace SpeedChecker
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            RegisterStartup();
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        private static void RegisterStartup()
        {
            string appName = "SpeedChecker";
            string exePath = Application.ExecutablePath;

            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (key.GetValue(appName) == null)
            {
                key.SetValue(appName, exePath);
            }
        }
    }
}