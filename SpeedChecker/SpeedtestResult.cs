using Dapper.Contrib.Extensions;
using System.Text.RegularExpressions;

namespace SpeedChecker;

[Table("SpeedtestResults")]
public class SpeedtestResult
{
    [Key]
    public long Id { get; set; }
    public string UserName { get; set; } = "";
    public string Server { get; set; } = "";
    public string ISP { get; set; } = "";
    public double IdleLatency { get; set; }
    public double IdleJitter { get; set; }
    public double IdleLow { get; set; }
    public double IdleHigh { get; set; }
    public double DownloadSpeed { get; set; }
    public string DownloadSpeedUnit { get; set; } = "";
    public double DownloadDataUsed { get; set; }
    public string DownloadDataUsedUnit { get; set; } = "";
    public double DownloadLatency { get; set; }
    public double DownloadJitter { get; set; }
    public double DownloadLow { get; set; }
    public double DownloadHigh { get; set; }
    public double UploadSpeed { get; set; }
    public string UploadSpeedUnit { get; set; } = "";
    public double UploadDataUsed { get; set; }
    public string UploadDataUsedUnit { get; set; } = "";
    public double UploadLatency { get; set; }
    public double UploadJitter { get; set; }
    public double UploadLow { get; set; }
    public double UploadHigh { get; set; }
    public double PacketLoss { get; set; }
    public string ResultUrl { get; set; } = "";
    public DateTime CreateDate { get; set; }

    public static SpeedtestResult Parse(string userName, string output)
    {
        // TODO:Successにならないケースがあるので対応が必要

        var result = new SpeedtestResult
        {
            UserName = userName
        };
        var serverMatch = Regex.Match(output, @"Server:\s*(.+)");
        if (serverMatch.Success)
        {
            result.Server = serverMatch.Groups[1].Value.Trim();
        }

        var ispMatch = Regex.Match(output, @"ISP:\s*(.+)");
        if (ispMatch.Success)
        {
            result.ISP = ispMatch.Groups[1].Value.Trim();
        }

        // Idle Latency
        var idleMatch = Regex.Match(output, @"Idle Latency:\s*([\d.]+) ms\s*\(jitter:\s*([\d.]+)ms, low:\s*([\d.]+)ms, high:\s*([\d.]+)ms");
        if (idleMatch.Success)
        {
            result.IdleLatency = double.Parse(idleMatch.Groups[1].Value);
            result.IdleJitter = double.Parse(idleMatch.Groups[2].Value);
            result.IdleLow = double.Parse(idleMatch.Groups[3].Value);
            result.IdleHigh = double.Parse(idleMatch.Groups[4].Value);
        }

        // Download Speed & Data Used
        var downloadMatch = Regex.Match(output, @"Download:\s*([\d.]+)\s*(\w+)\s*\(data used:\s*([\d.]+)\s*(\w+)\)");
        if (downloadMatch.Success)
        {
            result.DownloadSpeed = double.Parse(downloadMatch.Groups[1].Value);
            result.DownloadSpeedUnit = downloadMatch.Groups[2].Value;
            result.DownloadDataUsed = double.Parse(downloadMatch.Groups[3].Value);
            result.DownloadDataUsedUnit = downloadMatch.Groups[4].Value;
        }

        // Download Latency
        var downloadLatencyMatch = Regex.Match(output, @"Download:[^\n]+\n\s*([\d.]+) ms\s*\(jitter:\s*([\d.]+)ms, low:\s*([\d.]+)ms, high:\s*([\d.]+)ms");
        if (downloadLatencyMatch.Success)
        {
            result.DownloadLatency = double.Parse(downloadLatencyMatch.Groups[1].Value);
            result.DownloadJitter = double.Parse(downloadLatencyMatch.Groups[2].Value);
            result.DownloadLow = double.Parse(downloadLatencyMatch.Groups[3].Value);
            result.DownloadHigh = double.Parse(downloadLatencyMatch.Groups[4].Value);
        }

        // Upload Speed & Data Used
        var uploadMatch = Regex.Match(output, @"Upload:\s*([\d.]+)\s*(\w+)\s*\(data used:\s*([\d.]+)\s*(\w+)\)");
        if (uploadMatch.Success)
        {
            result.UploadSpeed = double.Parse(uploadMatch.Groups[1].Value);
            result.UploadSpeedUnit = uploadMatch.Groups[2].Value;
            result.UploadDataUsed = double.Parse(uploadMatch.Groups[3].Value);
            result.UploadDataUsedUnit = uploadMatch.Groups[4].Value;
        }

        // Upload Latency
        var uploadLatencyMatch = Regex.Match(output, @"Upload:[^\n]+\n\s*([\d.]+) ms\s*\(jitter:\s*([\d.]+)ms, low:\s*([\d.]+)ms, high:\s*([\d.]+)ms");
        if (uploadLatencyMatch.Success)
        {
            result.UploadLatency = double.Parse(uploadLatencyMatch.Groups[1].Value);

            result.UploadJitter = double.Parse(uploadLatencyMatch.Groups[2].Value);
            result.UploadLow = double.Parse(uploadLatencyMatch.Groups[3].Value);
            result.UploadHigh = double.Parse(uploadLatencyMatch.Groups[4].Value);
        }

        var packetLossMatch = Regex.Match(output, @"Packet Loss:\s*([\d.]+)%");
        if (packetLossMatch.Success)
        {
            result.PacketLoss = double.Parse(packetLossMatch.Groups[1].Value);
        }

        var resultUrlMatch = Regex.Match(output, @"Result URL:\s*(.+)");
        if (resultUrlMatch.Success)
        {
            result.ResultUrl = resultUrlMatch.Groups[1].Value;
        }
        result.CreateDate = DateTime.Now;
        return result;
    }
}