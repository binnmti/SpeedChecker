using System.Diagnostics;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SpeedChecker;

public partial class Form1 : Form
{
    private readonly IConfiguration _configuration;

    public Form1()
    {
        InitializeComponent();

        var builder = new ConfigurationBuilder().AddUserSecrets<Form1>();
        _configuration = builder.Build(); // Configurationを作成
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        button1.Enabled = false;

        var processStartInfo = new ProcessStartInfo
        {
            FileName = "speedtest.exe",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = new Process { StartInfo = processStartInfo })
        {
            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception($"Command failed with exit code {process.ExitCode}: {error}");
            }

            SpeedtestResult result = SpeedtestResult.Parse(output);

            string connectionString = _configuration["connectionString"];
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string insertSql = @"
                INSERT INTO SpeedtestResults (
                    Server, ISP, IdleLatency, IdleJitter, IdleLow, IdleHigh, 
                    DownloadSpeed, DownloadSpeedUnit, DownloadDataUsed, 
                    DownloadDataUsedUnit, DownloadLatency, DownloadJitter, 
                    DownloadLow, DownloadHigh, UploadSpeed, UploadSpeedUnit, 
                    UploadDataUsed, UploadDataUsedUnit, UploadLatency, 
                    UploadJitter, UploadLow, UploadHigh, PacketLoss, ResultUrl
                )
                VALUES (
                    @Server, @ISP, @IdleLatency, @IdleJitter, @IdleLow, @IdleHigh, 
                    @DownloadSpeed, @DownloadSpeedUnit, @DownloadDataUsed, 
                    @DownloadDataUsedUnit, @DownloadLatency, @DownloadJitter, 
                    @DownloadLow, @DownloadHigh, @UploadSpeed, @UploadSpeedUnit, 
                    @UploadDataUsed, @UploadDataUsedUnit, @UploadLatency, 
                    @UploadJitter, @UploadLow, @UploadHigh, @PacketLoss, @ResultUrl
                )";

            connection.Execute(insertSql, result);
        }

        button1.Enabled = true;
    }
}
