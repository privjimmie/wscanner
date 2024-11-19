namespace WalletScanner;

public class Settings
{
    public static string[] TargetExtensions = { ".dat", ".wallet", ".db", ".key", ".txt", ".log", ".backup", ".bak" };
    
    public static string[] Keywords = { "bitcoin", "electrum", "wallet", "encrypted", "keystore", "blockchain", "satoshi", "privatekey", "seedphrase" };
    
    public static int MaxFileSizeKb = 5120;
}