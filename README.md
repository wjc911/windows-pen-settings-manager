# Windows Pen Settings Manager

A powerful registry customization tool that extends Windows Ink and pen capabilities beyond the standard Windows Settings interface. This application provides enhanced control over your stylus experience by accessing hidden registry settings that control Windows Ink, Surface Pen, and other Windows pen technologies.

![Windows Pen Settings Manager Screenshot](screenshot.png)

## 🔍 What Problems Does This Tool Solve?

- **Limited Windows Ink Settings**: Access advanced pen settings not available in Windows Ink Workspace or Settings
- **Surface Pen Lag/Latency Issues**: Reduce input lag for a more responsive drawing and writing experience
- **Inconsistent Windows Ink Pressure Sensitivity**: Fine-tune pressure sensitivity to match your drawing style and preferences
- **Unreliable Surface Pen Double-Tap Recognition**: Customize double-tap width, height, and timing parameters for more consistent shortcuts
- **Windows Ink Palm Rejection Problems**: Optimize "Ignore touch input when using pen" feature for better palm rejection
- **Poor Default Pen Right-Click Behavior**: Configure pen button actions and hold gestures for improved workflow

## ✨ Features

- **Advanced Windows Ink configuration** beyond what Microsoft's settings provide
- **Complete pen settings management** from multiple registry locations
- **User-friendly interface** with sliders and checkboxes for easy configuration
- **Pressure sensitivity adjustment** to customize how the pen responds to pressure
- **Latency optimization** to reduce input lag for a more responsive experience
- **Double-tap configuration** for precise control over double-tap behavior
- **Right-click settings** including pen button configuration and hold gestures
- **Immediate feedback** - changes are applied instantly without requiring restart
- **Registry-level settings** for full control of the Windows pen experience

## 🔄 Relationship to Windows Ink and Microsoft Pen Technologies

This tool works directly with the registry settings that control:
- **Windows Ink Workspace** features and sensitivity
- **Windows Pen and Touch** services (Wisp)
- **Surface Pen** functionality on Microsoft devices
- **PrecisionTouchPad** settings that affect pen and touch interaction
- **TouchPrediction** parameters that influence pen responsiveness

Rather than replacing these technologies, Windows Pen Settings Manager provides a deeper level of customization to optimize your existing Windows pen experience.

## 🎯 Who Needs This Tool?

- **Digital Artists & Illustrators**: Fine-tune Windows Ink pressure and responsiveness for better drawing control
- **Note-Takers & Students**: Optimize Surface Pen settings for smoother handwriting and annotation 
- **Surface, Wacom & Tablet PC Users**: Fix common pen behavior issues specific to your device
- **Designers & Creative Professionals**: Customize Windows Ink behavior for precise design work
- **Technical Users**: Safely modify pen-related registry settings without manually editing the registry

## 📋 Registry Settings Managed

The application provides access to the following registry keys:

- `HKEY_CURRENT_USER\Software\Microsoft\Wisp\Pen` - Core Windows pen settings
- `HKEY_CURRENT_USER\Software\Microsoft\Wisp\Pen\SysEventParameters` - System event parameters for pen input
- `HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\Windows\Pen` - Windows NT pen settings
- `HKEY_CURRENT_USER\Control Panel\Cursors` - Pen cursor behavior
- `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\TouchPrediction` - Touch and pen prediction algorithms
- `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad` - Precision TouchPad settings

## 💻 Requirements

- Windows 10 or Windows 11
- .NET 8.0 Runtime ([Download here](https://dotnet.microsoft.com/download/dotnet/8.0))
- Administrator privileges (required for registry modifications)
- Compatible with Surface Pen, Wacom stylus, and other Windows-compatible pen devices
- Works with any device that uses Windows Ink technology

## 💾 Installation

1. Download the appropriate version for your system architecture from the [Releases](https://github.com/wjc911/windows-pen-settings-manager/releases) page:
   - `PenSettingsManager-x64.zip` - For 64-bit Windows (most common)
   - `PenSettingsManager-x86.zip` - For 32-bit Windows
   - `PenSettingsManager-arm64.zip` - For ARM64-based Windows devices (like Surface Pro X)
2. Extract the ZIP file to any location on your computer
3. Run `PenSettingsManager.exe` as administrator
4. Adjust settings to match your preferences
5. Changes take effect immediately - no reboot required

## 🛠️ Troubleshooting Common Windows Ink and Pen Issues

- **Pen Not Detected**: Ensure Bluetooth is enabled and pen is paired correctly
- **Inconsistent Windows Ink Pressure**: Adjust pressure sensitivity settings in the app
- **Lag When Using Windows Ink**: Lower the latency settings for more responsive input
- **Surface Pen Double-Tap Not Working**: Increase the double-tap time and width/height values
- **Accidental Palm Input in Windows Ink**: Enable and adjust "Ignore touch input when using pen" setting
- **Windows Ink Not Responding Correctly**: Try adjusting the TouchPrediction parameters

## 🔨 Building from Source

1. Clone the repository
   ```
   git clone https://github.com/wjc911/windows-pen-settings-manager.git
   ```

2. Open the solution in Visual Studio 2022 or later

3. Build the solution using one of these methods:
   
   **Using Visual Studio:**
   - Right-click on the project and select "Build"
   - Or press Ctrl+Shift+B

   **Using Command Line:**
   ```
   dotnet restore
   dotnet build
   ```

4. To create a release build:
   ```
   dotnet publish -c Release -r win-x64 --self-contained false
   ```

5. The compiled application will be in the `bin\Debug\net8.0-windows` or `bin\Release\net8.0-windows` directory

For more detailed build instructions, see the [BUILD_INSTRUCTIONS.md](BUILD_INSTRUCTIONS.md) file.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 💬 Acknowledgments

- Special thanks to all contributors
- Inspired by the need for better Windows Ink and pen settings management in Windows

---

**Keywords**: Windows Ink settings, Windows Ink Workspace, Surface pen sensitivity, stylus latency fix, Windows pen optimization, pen pressure customization, tablet PC settings, pen registry tweaks, Windows 10 pen, Windows 11 stylus, pen double-tap settings, palm rejection, digital pen optimization, Wacom settings, Surface Pen customization 