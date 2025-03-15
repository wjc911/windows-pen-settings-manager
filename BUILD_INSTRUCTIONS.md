# Build Instructions for Windows Pen Settings Manager

This document provides detailed instructions for building the Windows Pen Settings Manager application.

## Prerequisites

To build this application, you'll need:

1. **Visual Studio 2022** (Community Edition or higher)
   - Download from [Visual Studio website](https://visualstudio.microsoft.com/)
   - During installation, ensure you select:
     - ".NET Desktop Development" workload
     - ".NET 8.0 SDK" individual component

2. **Administrator privileges** on your Windows machine (required to test registry modifications)

## Building the Application

### Using Visual Studio

1. Open Visual Studio 2022
2. Select "Open a project or solution"
3. Navigate to the project folder and open `PenSettingsManager.csproj`
4. In the Solution Explorer, right-click on the project and select "Restore NuGet Packages"
5. Build the project by selecting "Build" > "Build Solution" from the menu (or press Ctrl+Shift+B)
6. If the build is successful, you can run the application by pressing F5 or clicking "Start" button

### Using Command Line

If you prefer using the command line:

1. Open a Developer Command Prompt for Visual Studio
2. Navigate to the project directory
3. Run the following commands:

```
dotnet restore
dotnet build
```

4. To create a release build:

```
dotnet publish -c Release -r win-x64 --self-contained false
```

## Running the Application

The application requires administrator privileges to modify registry settings. When running the application, you may see a User Account Control (UAC) prompt asking for permission.

### From Visual Studio

1. Right-click on the project in Solution Explorer
2. Select "Properties"
3. Go to "Debug" tab
4. Check "Run as administrator"
5. Press F5 to run with debugging

### From Explorer

1. Navigate to the build output directory (usually `bin\Debug\net8.0-windows` or `bin\Release\net8.0-windows`)
2. Right-click on `PenSettingsManager.exe`
3. Select "Run as administrator"

## Troubleshooting

If you encounter any issues:

1. **Missing .NET SDK**: Ensure you have .NET 8.0 SDK installed
2. **Build errors**: Make sure all files are properly included in the project
3. **Permission issues**: The application must run with administrator privileges
4. **Icon not displaying**: Ensure the icon file is properly included as a resource

## Distribution

To distribute the application:

1. Build a Release version
2. Package the output from the `bin\Release\net8.0-windows` directory
3. Include the README.md file with instructions for users 