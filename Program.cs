namespace WalletScanner;

class Program
{
    
    
    static void Main(string[] args)
    {
        Console.WriteLine("MAKE SURE APP IS RUN AS ADMINISTRATOR");
        
        Console.WriteLine("Enter the drive letter to scan (e.g., C):");
        string driveLetter = Console.ReadLine();

        if (string.IsNullOrEmpty(driveLetter) || !DriveInfo.GetDrives().Any(d => d.Name.StartsWith(driveLetter, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Invalid drive letter. Exiting.");
            return;
        }
        
        WalletScanner.Scan(driveLetter);
    }
}