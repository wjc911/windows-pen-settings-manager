using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

namespace PenSettingsManager
{
    [SupportedOSPlatform("windows")]
    public static class RegistryHelper
    {
        // Main registry path for Windows pen settings
        private const string PenRegistryPath = @"SOFTWARE\Microsoft\Wisp\Pen";
        
        // Registry path for Windows NT pen settings (for the "Ignore touch input when using pen" setting)
        private const string NtPenRegistryPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows\Pen";
        
        // Registry path for SysEventParameters
        private const string SysEventParametersPath = @"SOFTWARE\Microsoft\Wisp\Pen\SysEventParameters";
        
        // Registry path for pen visualization
        private const string PenVisualizationPath = @"Control Panel\Cursors";
        
        // Registry path for touch prediction
        private const string TouchPredictionPath = @"SOFTWARE\Microsoft\TouchPrediction";
        
        // Registry path for precision touchpad settings
        private const string TouchPadSettingsPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad";
        
        // Default values for pen settings
        private static readonly Dictionary<string, object> DefaultPenSettings = new()
        {
            // Pen pressure sensitivity (values are examples and may need adjustment)
            ["PressureSensitivity"] = 5, // Medium sensitivity (hypothetical range 1-10)
            
            // Pen latency settings
            ["LatencyMode"] = 1, // Low latency mode (0=standard, 1=low, 2=ultra-low) - hypothetical
            
            // Double-click settings
            ["DoubleTapWidth"] = 10, // Hypothetical double-tap width threshold in pixels
            ["DoubleTapHeight"] = 10, // Hypothetical double-tap height threshold in pixels
            ["DoubleTapTime"] = 500, // Hypothetical double-tap time threshold in milliseconds
        };
        
        // NT pen settings with default values
        private static readonly Dictionary<string, object> DefaultNtPenSettings = new()
        {
            // 0 = Do not ignore touch input, 1 = Ignore touch input when using pen
            ["PenArbitrationType"] = 0
        };

        // Default values for SysEventParameters
        private static readonly Dictionary<string, object> DefaultSysEventParameters = new()
        {
            ["DblDist"] = 20,         // Double-click distance
            ["DblTime"] = 300,        // Double-click time
            ["EraseEnable"] = 1,      // Enable eraser functionality
            ["FlickMode"] = 1,        // Enable flick gestures
            ["FlickTolerance"] = 50,  // Flick gesture tolerance
            ["HoldMode"] = 1,         // Enable hold gesture
            ["HoldTime"] = 2300,      // Hold time in ms
            ["RightMaskEnable"] = 1,  // Enable right mask
            ["Splash"] = 50,          // Splash threshold
            ["TapTime"] = 100,        // Single tap time in ms
        };
        
        // Default values for pen visualization settings
        private static readonly Dictionary<string, object> DefaultPenVisualizationSettings = new()
        {
            ["PenVisualization"] = 0x0023  // 0x0023=ON, 0=OFF
        };
        
        // Default values for touch prediction
        private static readonly Dictionary<string, object> DefaultTouchPredictionSettings = new()
        {
            ["Latency"] = 8,       // Default latency value
            ["SampleTime"] = 8     // Default sample time value
        };
        
        // Default values for precision touchpad
        private static readonly Dictionary<string, object> DefaultTouchPadSettings = new()
        {
            ["RightClickZoneWidth"] = 50,    // Default right click zone width
            ["RightClickZoneHeight"] = 25     // Default right click zone height
        };

        /// <summary>
        /// Gets a value from the registry
        /// </summary>
        public static object GetValue(string valueName, object defaultValue)
        {
            using var key = Registry.CurrentUser.OpenSubKey(PenRegistryPath, false);
            return key?.GetValue(valueName, defaultValue) ?? defaultValue;
        }

        /// <summary>
        /// Gets a value from the Windows NT pen registry
        /// </summary>
        public static object GetNtValue(string valueName, object defaultValue)
        {
            using var key = Registry.CurrentUser.OpenSubKey(NtPenRegistryPath, false);
            return key?.GetValue(valueName, defaultValue) ?? defaultValue;
        }

        /// <summary>
        /// Gets a value from the SysEventParameters registry
        /// </summary>
        public static object GetSysEventParameter(string valueName, object defaultValue)
        {
            using var key = Registry.CurrentUser.OpenSubKey(SysEventParametersPath, false);
            return key?.GetValue(valueName, defaultValue) ?? defaultValue;
        }
        
        /// <summary>
        /// Gets a value from the pen visualization registry
        /// </summary>
        public static object GetPenVisualizationSetting(string valueName, object defaultValue)
        {
            using var key = Registry.CurrentUser.OpenSubKey(PenVisualizationPath, false);
            return key?.GetValue(valueName, defaultValue) ?? defaultValue;
        }
        
        /// <summary>
        /// Gets a value from the touch prediction registry
        /// </summary>
        public static object GetTouchPredictionSetting(string valueName, object defaultValue)
        {
            using var key = Registry.LocalMachine.OpenSubKey(TouchPredictionPath, false);
            return key?.GetValue(valueName, defaultValue) ?? defaultValue;
        }
        
        /// <summary>
        /// Gets a value from the touchpad settings registry
        /// </summary>
        public static object GetTouchPadSetting(string valueName, object defaultValue)
        {
            using var key = Registry.CurrentUser.OpenSubKey(TouchPadSettingsPath, false);
            return key?.GetValue(valueName, defaultValue) ?? defaultValue;
        }

        /// <summary>
        /// Sets a value in the registry
        /// </summary>
        public static void SetValue(string valueName, object value, RegistryValueKind valueKind)
        {
            using var key = Registry.CurrentUser.CreateSubKey(PenRegistryPath, true);
            if (key != null)
            {
                key.SetValue(valueName, value, valueKind);
            }
        }

        /// <summary>
        /// Sets a value in the Windows NT pen registry
        /// </summary>
        public static void SetNtValue(string valueName, object value, RegistryValueKind valueKind)
        {
            using var key = Registry.CurrentUser.CreateSubKey(NtPenRegistryPath, true);
            if (key != null)
            {
                key.SetValue(valueName, value, valueKind);
            }
        }

        /// <summary>
        /// Sets a value in the SysEventParameters registry
        /// </summary>
        public static void SetSysEventParameter(string valueName, object value, RegistryValueKind valueKind)
        {
            using var key = Registry.CurrentUser.CreateSubKey(SysEventParametersPath, true);
            if (key != null)
            {
                key.SetValue(valueName, value, valueKind);
            }
        }
        
        /// <summary>
        /// Sets a value in the pen visualization registry
        /// </summary>
        public static void SetPenVisualizationSetting(string valueName, object value, RegistryValueKind valueKind)
        {
            using var key = Registry.CurrentUser.CreateSubKey(PenVisualizationPath, true);
            if (key != null)
            {
                key.SetValue(valueName, value, valueKind);
            }
        }
        
        /// <summary>
        /// Sets a value in the touch prediction registry (may require admin rights)
        /// </summary>
        public static void SetTouchPredictionSetting(string valueName, object value, RegistryValueKind valueKind)
        {
            try
            {
                using var key = Registry.LocalMachine.CreateSubKey(TouchPredictionPath, true);
                if (key != null)
                {
                    key.SetValue(valueName, value, valueKind);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Handle the case where admin rights are required
                throw new UnauthorizedAccessException(
                    "Administrator rights are required to modify touch prediction settings. " +
                    "Please run the application as administrator to change these settings.");
            }
        }
        
        /// <summary>
        /// Sets a value in the touchpad settings registry
        /// </summary>
        public static void SetTouchPadSetting(string valueName, object value, RegistryValueKind valueKind)
        {
            using var key = Registry.CurrentUser.CreateSubKey(TouchPadSettingsPath, true);
            if (key != null)
            {
                key.SetValue(valueName, value, valueKind);
            }
        }

        /// <summary>
        /// Gets all available pen settings from the registry
        /// </summary>
        public static Dictionary<string, object> GetAllPenSettings()
        {
            var settings = new Dictionary<string, object>();
            
            // Get Wisp pen settings
            using (var key = Registry.CurrentUser.OpenSubKey(PenRegistryPath, false))
            {
                if (key != null)
                {
                    foreach (var valueName in key.GetValueNames())
                    {
                        settings[valueName] = key.GetValue(valueName) ?? string.Empty;
                    }
                }
            }
            
            // Get NT pen settings
            using (var key = Registry.CurrentUser.OpenSubKey(NtPenRegistryPath, false))
            {
                if (key != null)
                {
                    foreach (var valueName in key.GetValueNames())
                    {
                        settings[$"NT_{valueName}"] = key.GetValue(valueName) ?? string.Empty;
                    }
                }
            }
            
            // Get SysEventParameters settings
            using (var key = Registry.CurrentUser.OpenSubKey(SysEventParametersPath, false))
            {
                if (key != null)
                {
                    foreach (var valueName in key.GetValueNames())
                    {
                        settings[$"SysEvent_{valueName}"] = key.GetValue(valueName) ?? string.Empty;
                    }
                }
            }
            
            // Get pen visualization settings
            using (var key = Registry.CurrentUser.OpenSubKey(PenVisualizationPath, false))
            {
                if (key != null)
                {
                    foreach (var valueName in key.GetValueNames())
                    {
                        if (valueName == "PenVisualization")
                        {
                            settings[$"Viz_{valueName}"] = key.GetValue(valueName) ?? string.Empty;
                        }
                    }
                }
            }
            
            // Get touch prediction settings
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(TouchPredictionPath, false))
                {
                    if (key != null)
                    {
                        foreach (var valueName in key.GetValueNames())
                        {
                            settings[$"TouchPred_{valueName}"] = key.GetValue(valueName) ?? string.Empty;
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Skip if we don't have access
            }
            
            // Get touchpad settings
            using (var key = Registry.CurrentUser.OpenSubKey(TouchPadSettingsPath, false))
            {
                if (key != null)
                {
                    foreach (var valueName in key.GetValueNames())
                    {
                        if (valueName.StartsWith("RightClickZone"))
                        {
                            settings[$"TouchPad_{valueName}"] = key.GetValue(valueName) ?? string.Empty;
                        }
                    }
                }
            }
            
            return settings;
        }

        /// <summary>
        /// Resets all pen settings to default values
        /// </summary>
        public static void ResetToDefaults()
        {
            // Reset Wisp pen settings
            using (var key = Registry.CurrentUser.CreateSubKey(PenRegistryPath, true))
            {
                if (key != null)
                {
                    foreach (var setting in DefaultPenSettings)
                    {
                        RegistryValueKind kind = setting.Value is int ? RegistryValueKind.DWord :
                                              setting.Value is string ? RegistryValueKind.String :
                                              RegistryValueKind.Unknown;
                        
                        key.SetValue(setting.Key, setting.Value, kind);
                    }
                }
            }
            
            // Reset NT pen settings
            using (var key = Registry.CurrentUser.CreateSubKey(NtPenRegistryPath, true))
            {
                if (key != null)
                {
                    foreach (var setting in DefaultNtPenSettings)
                    {
                        RegistryValueKind kind = setting.Value is int ? RegistryValueKind.DWord :
                                              setting.Value is string ? RegistryValueKind.String :
                                              RegistryValueKind.Unknown;
                        
                        key.SetValue(setting.Key, setting.Value, kind);
                    }
                }
            }
            
            // Reset SysEventParameters settings
            using (var key = Registry.CurrentUser.CreateSubKey(SysEventParametersPath, true))
            {
                if (key != null)
                {
                    foreach (var setting in DefaultSysEventParameters)
                    {
                        RegistryValueKind kind = setting.Value is int ? RegistryValueKind.DWord :
                                              setting.Value is string ? RegistryValueKind.String :
                                              RegistryValueKind.Unknown;
                        
                        key.SetValue(setting.Key, setting.Value, kind);
                    }
                }
            }
            
            // Reset pen visualization settings
            using (var key = Registry.CurrentUser.CreateSubKey(PenVisualizationPath, true))
            {
                if (key != null)
                {
                    foreach (var setting in DefaultPenVisualizationSettings)
                    {
                        RegistryValueKind kind = setting.Value is int ? RegistryValueKind.DWord :
                                              setting.Value is string ? RegistryValueKind.String :
                                              RegistryValueKind.Unknown;
                        
                        key.SetValue(setting.Key, setting.Value, kind);
                    }
                }
            }
            
            // Reset touch prediction settings if we have admin rights
            try
            {
                using (var key = Registry.LocalMachine.CreateSubKey(TouchPredictionPath, true))
                {
                    if (key != null)
                    {
                        foreach (var setting in DefaultTouchPredictionSettings)
                        {
                            RegistryValueKind kind = setting.Value is int ? RegistryValueKind.DWord :
                                                  setting.Value is string ? RegistryValueKind.String :
                                                  RegistryValueKind.Unknown;
                            
                            key.SetValue(setting.Key, setting.Value, kind);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Skip if we don't have admin rights
            }
            
            // Reset touchpad settings
            using (var key = Registry.CurrentUser.CreateSubKey(TouchPadSettingsPath, true))
            {
                if (key != null)
                {
                    foreach (var setting in DefaultTouchPadSettings)
                    {
                        RegistryValueKind kind = setting.Value is int ? RegistryValueKind.DWord :
                                              setting.Value is string ? RegistryValueKind.String :
                                              RegistryValueKind.Unknown;
                        
                        key.SetValue(setting.Key, setting.Value, kind);
                    }
                }
            }
        }
    }
} 