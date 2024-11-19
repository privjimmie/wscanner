namespace WalletScanner;

public class WalletScanner
{
    public static void Scan(string driveLetter)
    {
        string directory = driveLetter + @":\";

        string outputFilePath = Path.Combine(directory, $"{DateTime.UtcNow.Ticks}-wallet_scan_results.csv");
        
        Console.WriteLine($"Output file: {outputFilePath}");
        
        int filesProcessed = 0;

        using (var outputFile = new StreamWriter(outputFilePath, false))
        {
            // Write CSV header
            outputFile.WriteLine("FindingType,FilePath,FileSize,Entropy,KeywordMatched");

            Console.WriteLine($"Scanning drive: {directory}");

            try
            {
                foreach (var file in SafeEnumerateFiles(directory, "*.*"))
                {
                    filesProcessed++;

                    // Display progress every 100 files
                    if (filesProcessed % 100 == 0)
                    {
                        Console.WriteLine($"Files processed: {filesProcessed}");
                    }

                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);

                        // Skip files larger than the threshold
                        if (fileInfo.Length > Settings.MaxFileSizeKb * 1024)
                            continue;

                        if (fileInfo.Extension.Equals(".zip", StringComparison.OrdinalIgnoreCase))
                        {
                            Helpers.ProcessZipFile(fileInfo, Settings.TargetExtensions, outputFile);
                        }
                        else if (fileInfo.Extension.Equals(".rar", StringComparison.OrdinalIgnoreCase))
                        {
                            Helpers.ProcessRarFile(fileInfo, Settings.TargetExtensions, outputFile);
                        }
                        else if (Settings.TargetExtensions.Contains(fileInfo.Extension, StringComparer.OrdinalIgnoreCase))
                        {
                            var fileAnalysis = Helpers.AnalyzeFile(fileInfo);
                            if (fileAnalysis != null)
                            {
                                Console.WriteLine($"[FOUND] {fileAnalysis.FindingType} - {fileAnalysis.FilePath}");
                                outputFile.WriteLine(fileAnalysis.ToCsv());
                                outputFile.Flush();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file: {file} - {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during scanning: {ex.Message}");
            }

            Console.WriteLine($"Scan complete. Results saved to {outputFilePath}");
        }
    }

    public static IEnumerable<string> SafeEnumerateFiles(string root, string searchPattern)
    {
        var pending = new Stack<string>();
        pending.Push(root);
        while (pending.Count != 0)
        {
            var path = pending.Pop();
            string[] files = null;
            try
            {
                files = Directory.GetFiles(path, searchPattern);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied to path: {path} - {ex.Message}");
                continue;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing path: {path} - {ex.Message}");
                continue;
            }

            if (files != null)
            {
                foreach (var file in files)
                {
                    yield return file;
                }
            }

            string[] subDirs = null;
            try
            {
                subDirs = Directory.GetDirectories(path);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied to directory: {path} - {ex.Message}");
                continue;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing directories in: {path} - {ex.Message}");
                continue;
            }

            if (subDirs != null)
            {
                foreach (var dir in subDirs)
                {
                    pending.Push(dir);
                }
            }
        }
    }
}