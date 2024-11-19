namespace WalletScanner.Models;

public class WalletFinding
{
    public string FindingType { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
    public double Entropy { get; set; }
    public string KeywordMatched { get; set; } = null;

    public string ToCsv()
    {
        return $"{Helpers.EscapeCsv(FindingType)},{Helpers.EscapeCsv(FilePath)},{FileSize},{Entropy},{Helpers.EscapeCsv(KeywordMatched)}";
    }

    
}