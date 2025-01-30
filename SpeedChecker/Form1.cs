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
        if (string.IsNullOrWhiteSpace(userNameTextBox.Text)) return;

        var processStartInfo = new ProcessStartInfo
        {
            FileName = "speedtest.exe",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using var process = new Process { StartInfo = processStartInfo };
        process.Start();
        string output = await process.StandardOutput.ReadToEndAsync();
        string error = await process.StandardError.ReadToEndAsync();
        process.WaitForExit();
        if (process.ExitCode != 0)
        {
            throw new Exception($"Command failed with exit code {process.ExitCode}: {error}");
        }
        using var connection = new SqlConnection(_configuration["connectionString"]);
        connection.Open();
        connection.Insert(SpeedtestResult.Parse(userNameTextBox.Text, output));
    }


    private async void button1_Click(object sender, EventArgs e)
    {
        button1.Enabled = false;
        await Speedtest();
        button1.Enabled = true;
    }

    private void userNameTextBox_TextChanged(object sender, EventArgs e)
    {
        button1.Enabled = !string.IsNullOrWhiteSpace(userNameTextBox.Text);
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;
        SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
        base.OnFormClosing(e);
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        Hide();
        notifyIcon1.Visible = true;
    }

    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        Show();
        WindowState = FormWindowState.Normal;
        notifyIcon1.Visible = false;
    }

    private async void timer1_Tick(object sender, EventArgs e)
    {
        await Speedtest();
    }
}
