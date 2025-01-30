using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper.Contrib.Extensions;
using Microsoft.Win32;

namespace SpeedChecker;

public partial class Form1 : Form
{
    private readonly IConfiguration _configuration;

    public Form1()
    {
        InitializeComponent();

        var builder = new ConfigurationBuilder().AddUserSecrets<Form1>();
        _configuration = builder.Build();

        userNameTextBox.Text = Properties.Settings.Default.UserName;
        AutoRestartCheckBox.Checked = Properties.Settings.Default.AutoRestart;

        SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
    }

    private async void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
        if (e.Mode == PowerModes.Resume)
        {
            await Speedtest();
        }
    }

    private async void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
    {
        if (e.Reason == SessionSwitchReason.SessionUnlock)
        {
            await Speedtest();
        }
    }

    private async Task Speedtest()
    {
        if (button1.Enabled == false) return;
        if (string.IsNullOrWhiteSpace(userNameTextBox.Text))
        {
            DownloadTextBox.Text = $"名前を入力しないと計測出来ません";
            return;
        }

        button1.Enabled = false;
        Cursor = Cursors.WaitCursor;
        DownloadTextBox.Text = $"計測中";

        try
        {
            var output = "";
            await Task.Run(async ()  => { 
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "speedtest.exe",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using var process = new Process { StartInfo = processStartInfo };
                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data == null) return;
                    output += e.Data + Environment.NewLine;
                    // 改行のみは表示しない
                    if (e.Data == Environment.NewLine) return;
                    Invoke(() =>
                    {
                        DownloadTextBox.Text += e.Data.Trim() + Environment.NewLine;
                        DownloadTextBox.SelectionStart = DownloadTextBox.Text.Length;
                        DownloadTextBox.ScrollToCaret();
                    });
                };
                process.Start();
                process.BeginOutputReadLine();
                string error = await process.StandardError.ReadToEndAsync();
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    Invoke(() => { DownloadTextBox.Text = error; });
                }
            });

            var result = SpeedtestResult.Parse(userNameTextBox.Text, output);
            using var connection = new SqlConnection(_configuration["connectionString"]);
            connection.Open();
            connection.Insert(result);
            DownloadTextBox.Text = $"ダウンロード : {result.DownloadSpeed} {result.DownloadSpeedUnit}{Environment.NewLine}アップロード : {result.UploadSpeed} {result.UploadSpeedUnit}";
        }
        catch (Exception ex)
        {
            DownloadTextBox.Text = ex.Message;
        }
        finally
        {
            Cursor = Cursors.Default;
            button1.Enabled = true;
        }

    }


    private async void button1_Click(object sender, EventArgs e)
    {
        await Speedtest();
    }

    private void userNameTextBox_TextChanged(object sender, EventArgs e)
    {
        button1.Enabled = !string.IsNullOrWhiteSpace(userNameTextBox.Text);
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        Properties.Settings.Default.UserName = userNameTextBox.Text;
        Properties.Settings.Default.AutoRestart = AutoRestartCheckBox.Checked;
        Properties.Settings.Default.Save();

        SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;
        SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        RegisterStartup(AutoRestartCheckBox.Checked);
    }

    private static void RegisterStartup(bool on)
    {
        string appName = "SpeedChecker";
        string exePath = Application.ExecutablePath;

        using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
        if (key == null) return;

        if (on)
        {
            if (key.GetValue(appName) == null)
            {
                key.SetValue(appName, exePath);
            }
        }
        else
        {
            if (key.GetValue(appName) != null)
            {
                key.DeleteValue(appName, false);
            }
        }
    }

    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        Show();
        WindowState = FormWindowState.Normal;
        notifyIcon1.Visible = false;
        checkBox2.Checked = false;
    }

    private async void timer1_Tick(object sender, EventArgs e)
    {
        await Speedtest();
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
        Hide();
        notifyIcon1.Visible = true;
    }
}
