using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper.Contrib.Extensions;
using Microsoft.Win32;
using System.Text;

namespace SpeedChecker;

public partial class Form1 : Form
{
    private readonly IConfiguration _configuration;
    private System.Windows.Forms.Timer timer;

    public Form1()
    {
        InitializeComponent();

#if DEBUG
        var builder = new ConfigurationBuilder().AddUserSecrets<Form1>();
#else
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
#endif
        _configuration = builder.Build();

        userNameTextBox.Text = Properties.Settings.Default.UserName;
        AutoRestartCheckBox.Checked = Properties.Settings.Default.AutoRestart;
        DbWritableCheckBox.Checked = Properties.Settings.Default.DbWritable;
        HourCheckCheckBox.Checked = Properties.Settings.Default.HourCheck;

        SystemEvents.PowerModeChanged += async (sender, e) =>
        {
            if (e.Mode == PowerModes.Resume)
            {
                await Speedtest();
            }
        };
        SystemEvents.SessionSwitch += async (sender, e) =>
        {
            if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                await Speedtest();
            }
        };
        timer = new System.Windows.Forms.Timer
        {
            Interval = 1000
        };
        timer.Tick += async (sender, e) =>
        {
            TimerTextBox.Text = DateTime.Now.ToString("HH:mm:ss");
            if (DateTime.Now.ToString("mm:ss") == "00:00")
            {
                if (HourCheckCheckBox.Checked)
                {
                    await Speedtest();
                }
            }
        };
        timer.Start();
        TimerTextBox.Text = DateTime.Now.ToString("HH:mm:ss");
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
        Properties.Settings.Default.DbWritable = DbWritableCheckBox.Checked;
        Properties.Settings.Default.HourCheck = HourCheckCheckBox.Checked;
        Properties.Settings.Default.Save();
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        string appName = "SpeedChecker";
        string exePath = Application.ExecutablePath;
        using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
        if (key == null) return;

        if (AutoRestartCheckBox.Checked)
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

    private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Show();
        WindowState = FormWindowState.Normal;
        notifyIcon1.Visible = false;
        checkBox2.Checked = false;
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
        Hide();
        notifyIcon1.Visible = true;
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Minimized)
        {
            Hide();
            notifyIcon1.Visible = true;
        }
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private async Task Speedtest()
    {
        if (button1.Enabled == false) return;
        if (string.IsNullOrWhiteSpace(userNameTextBox.Text))
        {
            DownloadTextBox.Text = $"計測失敗{Environment.NewLine}名前を入力しないと計測出来ません";
            return;
        }

        button1.Enabled = false;
        Cursor = Cursors.WaitCursor;
        DownloadTextBox.Text = "";

        var output = "";
        try
        {
            await Task.Run(async () =>
            {
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
                    Invoke(() =>
                    {
                        var value = e.Data.Trim() + Environment.NewLine;
                        if (value == Environment.NewLine) return;

                        DownloadTextBox.Text += value;
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
                    throw new Exception($"speedtest実行例外{error}");
                }
            });

            SpeedtestResult result = new();
            try
            {
                result = SpeedtestResult.Parse(userNameTextBox.Text, output);
                DownloadTextBox.Text = $"計測成功{Environment.NewLine}ダウンロード : {result.DownloadSpeed} {result.DownloadSpeedUnit}{Environment.NewLine}アップロード : {result.UploadSpeed} {result.UploadSpeedUnit}";
            }
            catch (FormatException ex)
            {
                throw new Exception($"パースフォーマット例外{ex.Message}");
            }
            if (DbWritableCheckBox.Checked)
            {
                try
                {
                    using var connection = new SqlConnection(_configuration["ConnectionString"]);
                    connection.Open();
                    connection.Insert(result);
                }
                catch (SqlException ex)
                {
                    throw new Exception($"SqlServer実行例外{ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            DownloadTextBox.Text = $"計測失敗{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}{output}";
            using var client = new HttpClient();
            var json = $"{{\"content\": \"❌ **エラー発生**: {ex.Message}\\n{output}\\n```{ex.StackTrace}```\"}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(_configuration["DiscordWebhookUrl"], content);
        }
        finally
        {
            Cursor = Cursors.Default;
            button1.Enabled = true;
        }
    }
}
