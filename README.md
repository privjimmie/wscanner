
# Bitcoin Wallet Finder

A simple .NET console application that scans your computer's drive to find potential old Bitcoin wallets.

## Overview

This application searches through the specified drive on your computer to locate files that might be Bitcoin wallets or contain wallet-related information. It looks for specific file types, keywords, and high-entropy files that could indicate encrypted data.

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation and Usage](#installation-and-usage)
  - [1. Install .NET SDK](#1-install-net-sdk)
  - [2. Download the Source Code](#2-download-the-source-code)
  - [3. Build the Application](#3-build-the-application)
  - [4. Run the Application](#4-run-the-application)
  - [5. Review the Results](#5-review-the-results)
- [Notes](#notes)
- [Troubleshooting](#troubleshooting)
- [Disclaimer](#disclaimer)
- [License](#license)

## Features

- **File Scanning**: Searches for files with extensions commonly associated with Bitcoin wallets (e.g., `.dat`, `.wallet`, `.key`, etc.).
- **Keyword Detection**: Scans file contents for wallet-related keywords like "bitcoin", "wallet", "privatekey", etc.
- **Entropy Analysis**: Identifies files with high entropy, which may indicate encrypted wallet files.
- **Compressed Files Support**: Scans inside `.zip` and `.rar` archives for potential wallet files.
- **CSV Reporting**: Generates a CSV report of all findings for easy review in Excel or other spreadsheet applications.
- **Progress Updates**: Displays the number of files processed during the scan.

## Prerequisites

- **Windows Operating System**
- **.NET SDK (version 6.0 or later)**
  - Download from [Microsoft .NET Downloads](https://dotnet.microsoft.com/download)

## Installation and Usage

Follow these steps to install and run the Bitcoin Wallet Finder:

### 1. Install .NET SDK

If you don't have the .NET SDK installed:

- Visit [Microsoft .NET Downloads](https://dotnet.microsoft.com/download)
- Download and install the appropriate version for your system (usually the latest **.NET 8.0 SDK**).

### 2. Download the Source Code

Download the source code of the application:

- **Option 1: Clone the repository using Git**:
  ```bash
  git clone https://github.com/yourusername/bitcoin-wallet-finder.git
  ```
- **Option 2: Download the ZIP file from the repository and extract it to a folder**.

### 3. Build the Application

#### Option 1: Using Command Prompt (Recommended for Non-Tech Users)

1. **Open Command Prompt as Administrator**:
   - Click on the **Start** menu.
   - Type `cmd`.
   - Right-click on **Command Prompt** and select **Run as administrator**.

2. **Navigate to the Source Code Directory**:
   - Use the `cd` command to navigate to the folder where you extracted the source code.
     ```bash
     cd path	oitcoin-wallet-finder
     ```

3. **Restore Dependencies**:
   - Run the following command to restore any required packages:
     ```bash
     dotnet restore
     ```

4. **Build the Application**:
   - Build the application in Release mode:
     ```bash
     dotnet build -c Release
     ```
   - The executable will be located in the `bin\Release
et6.0\` directory.

#### Option 2: Using Visual Studio (For Users with Visual Studio Installed)

1. **Open the Solution File**:
   - Double-click the `.sln` file in the source code directory to open the project in Visual Studio.

2. **Restore NuGet Packages**:
   - Visual Studio should automatically restore any required NuGet packages.

3. **Build the Solution**:
   - Click on **Build** > **Build Solution** or press `Ctrl+Shift+B`.

4. **Locate the Executable**:
   - After a successful build, the executable will be in the `bin\Release
et6.0\` folder.

### 4. Run the Application

#### Important: Run as Administrator

To ensure the application can access all files and folders, **you must run it as an Administrator**.

1. **Open Command Prompt as Administrator**:
   - Click on the **Start** menu.
   - Type `cmd`.
   - Right-click on **Command Prompt** and select **Run as administrator**.

2. **Navigate to the Executable Directory**:
   - Use the `cd` command to navigate to the directory containing the built executable:
     ```bash
     cd path	oitcoin-wallet-finderin\Release
et6.0
     ```

3. **Run the Application**:
   - Execute the application by typing:
     ```bash
     WalletScanner.exe
     ```
     *(Replace `WalletScanner.exe` with the actual name of your executable if different.)*

4. **Follow On-Screen Prompts**:
   - **Enter the drive letter to scan (e.g., C):**
     - Type the drive letter you wish to scan (e.g., `C`) and press **Enter**.
   - **Enter the maximum file size to scan (in KB):**
     - Press **Enter** to accept the default of `5120` KB (5 MB), or type a different value and press **Enter**.

5. **Monitor Progress**:
   - The application will display progress updates every 100 files processed.
   - It may take some time to scan large drives.

6. **Completion**:
   - Once the scan is complete, the application will display the location of the CSV results file.

### 5. Review the Results

- **Locate the CSV File**:
  - The results file will be saved in the root of the drive you scanned, with a name like `wallet_scan_results.csv`.
- **Open the CSV File**:
  - Use Microsoft Excel or another spreadsheet program to open the CSV file.
- **Review Findings**:
  - Examine the entries to identify any files that may be old Bitcoin wallets or related data.

## Notes

- **Administrative Privileges**: Running the application as an Administrator is necessary to access all files and folders, including those that are protected or hidden.
- **Privacy**: The application processes files on your computer locally and does not transmit any data externally.
- **Disk Space**: Ensure you have enough disk space for temporary files, especially when scanning compressed files.
- **Performance**: Scanning a large drive can take significant time and system resources. It's recommended to close other applications during the scan.

## Troubleshooting

- **Access Denied Errors**:
  - If you see messages about access being denied to certain paths, it's normal for system-protected areas.
  - The application will skip these areas and continue scanning.
- **Application Closes Immediately**:
  - If the console window closes right after opening, run the application from the Command Prompt to see any error messages.
- **.NET SDK Not Recognized**:
  - Ensure that the .NET SDK is properly installed and that the installation path is added to your system's environment variables.
- **Cannot Find Executable**:
  - Make sure you are in the correct directory where the `WalletScanner.exe` file is located.
  - Use the `dir` command to list files in the directory.

## Disclaimer

- **Use at Your Own Risk**: This tool is provided "as is" without any warranties. Use it responsibly and ensure compliance with all relevant laws and regulations.
- **Data Sensitivity**: Be cautious when handling files that may contain sensitive information, such as private keys or seed phrases.
- **No Liability**: The developers are not responsible for any loss or damage resulting from the use of this application.

## License

This project is licensed under the MIT License. 
