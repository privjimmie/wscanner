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
- **.NET SDK (version 8.0 or later)**
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
  git clone https://github.com/privjimmie/wscanner.git
