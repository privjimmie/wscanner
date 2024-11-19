using System.IO.Compression;
using SharpCompress.Archives.Rar;
using WalletScanner.Models;

namespace WalletScanner;

public class Helpers
{
    public static WalletFinding AnalyzeFile(FileInfo fileInfo)
    {
        try
        {
            var content = File.ReadAllBytes(fileInfo.FullName);

            // Calculate entropy
            double entropy = CalculateEntropy(content);

            // Check for keywords
            string contentString = System.Text.Encoding.UTF8.GetString(content);

            foreach (var keyword in Settings.Keywords)
            {
                if (contentString.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return new WalletFinding
                    {
                        FindingType = "Keyword Match",
                        FilePath = fileInfo.FullName,
                        FileSize = fileInfo.Length,
                        Entropy = entropy,
                        KeywordMatched = keyword
                    };
                }
            }

            // Check for high entropy (encrypted files)
            if (entropy > 7.5)
            {
                return new WalletFinding
                {
                    FindingType = "High Entropy",
                    FilePath = fileInfo.FullName,
                    FileSize = fileInfo.Length,
                    Entropy = entropy
                };
            }
        }
        catch(Exception exc)
        {
            // Handle unreadable files or other exceptions silently
            Console.WriteLine($"Error reading file: {fileInfo.FullName} {exc.Message}");
        }

        return null;
    }

    public static void ProcessZipFile(FileInfo zipFile, string[] targetExtensions, StreamWriter outputFile)
    {
        try
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipFile.FullName))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.FullName.EndsWith("/")) // Skip directories
                    {
                        var tempPath = Path.GetTempFileName();
                        entry.ExtractToFile(tempPath, true);
                        var tempFileInfo = new FileInfo(tempPath);

                        if (targetExtensions.Contains(Path.GetExtension(entry.FullName), StringComparer.OrdinalIgnoreCase))
                        {
                            var fileAnalysis = AnalyzeFile(tempFileInfo);
                            if (fileAnalysis != null)
                            {
                                Console.WriteLine($"[FOUND in ZIP] {fileAnalysis.FindingType} - {zipFile.FullName}\\{entry.FullName}");
                                outputFile.WriteLine(fileAnalysis.ToCsv());
                                outputFile.Flush();
                            }
                        }

                        File.Delete(tempPath);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing ZIP file: {zipFile.FullName} - {ex.Message}");
        }
    }

    public static void ProcessRarFile(FileInfo rarFile, string[] targetExtensions, StreamWriter outputFile)
    {
        try
        {
            using (var archive = RarArchive.Open(rarFile.FullName))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        var tempPath = Path.GetTempFileName();
                        using (var entryStream = entry.OpenEntryStream())
                        using (var tempFileStream = File.Create(tempPath))
                        {
                            entryStream.CopyTo(tempFileStream);
                        }
                        var tempFileInfo = new FileInfo(tempPath);

                        if (targetExtensions.Contains(Path.GetExtension(entry.Key), StringComparer.OrdinalIgnoreCase))
                        {
                            var fileAnalysis = AnalyzeFile(tempFileInfo);
                            if (fileAnalysis != null)
                            {
                                Console.WriteLine($"[FOUND in RAR] {fileAnalysis.FindingType} - {rarFile.FullName}\\{entry.Key}");
                                outputFile.WriteLine(fileAnalysis.ToCsv());
                                outputFile.Flush();
                            }
                        }

                        File.Delete(tempPath);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing RAR file: {rarFile.FullName} - {ex.Message}");
        }
    }

    public static double CalculateEntropy(byte[] data)
    {
        int[] counts = new int[256];
        foreach (var b in data)
            counts[b]++;

        double entropy = 0.0;
        double dataLength = data.Length;

        foreach (var count in counts)
        {
            if (count == 0) continue;
            double frequency = count / dataLength;
            entropy -= frequency * Math.Log(frequency, 2);
        }

        return entropy;
    }
    
    public static string EscapeCsv(string field)
    {
        if (field == null)
            return "";

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        else
        {
            return field;
        }
    }
}