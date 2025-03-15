# Windows Pen Settings Manager

A lightweight GUI tool for managing Windows pen settings through the registry.

## Features

- Adjust pen pressure sensitivity
- Customize latency settings for improved responsiveness
- Configure double-tap settings
- Enable/disable touch rejection when using pen
- Hide/show pen cursor for cleaner drawing experience
- Configure touch prediction for optimal responsiveness
- Customize right-click zone dimensions
- Enable/disable pen gestures (flicks, hold, eraser)
- Reset all settings to default values
- Changes take effect immediately

## Requirements

- Windows 10 or later
- .NET 8.0 or later
- Administrator privileges (required to modify some registry settings)

## Installation

1. Download the latest release from the Releases section
2. Extract the zip file to a location of your choice
3. Run `PenSettingsManager.exe` as administrator

Note: Administrator privileges are required to modify certain system-level registry settings like touch prediction.

## Settings

This application manages settings from the following registry locations:

- `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Wisp\Pen` - Contains various pen settings
- `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Wisp\Pen\SysEventParameters` - Contains pen gesture settings
- `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows\Pen` - Contains settings for touch rejection
- `HKEY_CURRENT_USER\Control Panel\Cursors` - Contains pen visualization settings
- `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\TouchPrediction` - Contains touch prediction settings (requires admin rights)
- `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad` - Contains right-click zone settings

The following settings can be adjusted:

### Pressure Sensitivity
Adjust how the pen responds to pressure.

### Latency Settings
Choose between standard, low, and ultra-low latency modes. Lower latency provides more responsive drawing but may impact battery life.

#### Touch Prediction Settings
Configure touch prediction latency and sample time. Lower values (2 or less) improve touch responsiveness but may use more battery. Requires administrator rights.

### Double-Tap Settings
Customize the width, height, and time thresholds for double-tap detection.

### Pen Visualization
- Hide pen cursor: Allows hiding the pen cursor for a cleaner drawing experience

### Pen Behavior
- Ignore touch input when using pen: Provides better palm rejection
- Enable eraser: Toggle eraser functionality
- Enable flick gestures: Enable/disable flick gestures for navigation
- Enable hold gesture: Enable/disable hold gesture for context menus
- Enable right click zone: Toggle the right-click zone feature
- Right-click zone dimensions: Customize width and height percentages

### Advanced Pen Settings
- Double tap distance and time
- Flick tolerance
- Hold time
- Splash threshold
- Tap time

## Building from Source

1. Clone this repository
2. Open the solution in Visual Studio 2022 or later
3. Build the solution
4. Run the application with administrator privileges

## License

This software is released under the MIT License. 