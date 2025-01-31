namespace SpeedChecker;
internal static class Program
{
    static Mutex mutex = new(true, "SpeedChecker");

    [STAThread]
    static void Main()
    {
        if (!mutex.WaitOne(TimeSpan.Zero, true))
        {
            MessageBox.Show("すでにアプリが起動しています！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}