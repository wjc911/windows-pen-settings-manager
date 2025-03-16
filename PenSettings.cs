using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace PenSettingsManager
{
    [SupportedOSPlatform("windows")]
    public class PenSettings : INotifyPropertyChanged
    {
        private int _pressureSensitivity;
        private int _latencyMode;
        private int _doubleTapWidth;
        private int _doubleTapHeight;
        private int _doubleTapTime;
        private bool _ignoreTouchInputWhenUsingPen;
        
        // Additional properties from registry
        private int _dblDist;       // Double-tap distance
        private int _dblTime;       // Double-tap time
        private bool _eraseEnable;  // Enable eraser
        private bool _flickMode;    // Enable flick gestures
        private int _flickTolerance; // Flick tolerance
        private bool _holdMode;     // Enable hold gesture
        private int _holdTime;      // Hold time in ms
        private bool _rightMaskEnable; // Enable right mask
        private int _splash;        // Splash threshold
        private int _tapTime;       // Single tap time

        // New properties for pen visualization
        private int _penVisualization; // Pen visualization settings
        private bool _hidePenCursor;   // Hide pen cursor option
        
        // New properties for touch prediction
        private int _touchPredictionLatency;    // Touch prediction latency
        private int _touchPredictionSampleTime; // Touch prediction sample time
        
        // New properties for right-click zone
        private int _rightClickZoneWidth;  // Right click zone width
        private int _rightClickZoneHeight; // Right click zone height

        // Event for property change notification
        public event PropertyChangedEventHandler? PropertyChanged;

        // Pressure sensitivity (1-10)
        public int PressureSensitivity
        {
            get => _pressureSensitivity;
            set
            {
                if (_pressureSensitivity != value)
                {
                    _pressureSensitivity = value;
                    OnPropertyChanged();
                    SaveSetting("PressureSensitivity", value);
                }
            }
        }

        // Latency mode (0=standard, 1=low, 2=ultra-low)
        public int LatencyMode
        {
            get => _latencyMode;
            set
            {
                if (_latencyMode != value)
                {
                    _latencyMode = value;
                    OnPropertyChanged();
                    SaveSetting("LatencyMode", value);
                }
            }
        }

        // Double-tap width threshold (pixels)
        public int DoubleTapWidth
        {
            get => _doubleTapWidth;
            set
            {
                if (_doubleTapWidth != value)
                {
                    _doubleTapWidth = value;
                    OnPropertyChanged();
                    SaveSetting("DoubleTapWidth", value);
                }
            }
        }

        // Double-tap height threshold (pixels)
        public int DoubleTapHeight
        {
            get => _doubleTapHeight;
            set
            {
                if (_doubleTapHeight != value)
                {
                    _doubleTapHeight = value;
                    OnPropertyChanged();
                    SaveSetting("DoubleTapHeight", value);
                }
            }
        }

        // Double-tap time threshold (milliseconds)
        public int DoubleTapTime
        {
            get => _doubleTapTime;
            set
            {
                if (_doubleTapTime != value)
                {
                    _doubleTapTime = value;
                    OnPropertyChanged();
                    SaveSetting("DoubleTapTime", value);
                }
            }
        }

        // Ignore touch input when using pen
        public bool IgnoreTouchInputWhenUsingPen
        {
            get => _ignoreTouchInputWhenUsingPen;
            set
            {
                if (_ignoreTouchInputWhenUsingPen != value)
                {
                    _ignoreTouchInputWhenUsingPen = value;
                    OnPropertyChanged();
                    SaveNtSetting("PenArbitrationType", value ? 1 : 0);
                }
            }
        }

        // Double-tap distance threshold (HKCU\...\SysEventParameters)
        public int DblDist
        {
            get => _dblDist;
            set
            {
                if (_dblDist != value)
                {
                    _dblDist = value;
                    OnPropertyChanged();
                    SaveSysEventParameter("DblDist", value);
                }
            }
        }

        // Double-tap time threshold (HKCU\...\SysEventParameters)
        public int DblTime
        {
            get => _dblTime;
            set
            {
                if (_dblTime != value)
                {
                    _dblTime = value;
                    OnPropertyChanged();
                    SaveSysEventParameter("DblTime", value);
                }
            }
        }

        // Enable eraser (HKCU\...\SysEventParameters)
        public bool EraseEnable
        {
            get => _eraseEnable;
            set
            {
                if (_eraseEnable != value)
                {
                    _eraseEnable = value;
                    OnPropertyChanged();
                    SaveSysEventParameter("EraseEnable", value ? 1 : 0);
                }
            }
        }

        // Enable flick gestures (HKCU\...\SysEventParameters)
        public bool FlickMode
        {
            get => _flickMode;
            set
            {
                if (_flickMode != value)
                {
                    _flickMode = value;
                    OnPropertyChanged();
                    SaveSysEventParameter("FlickMode", value ? 1 : 0);
                }
            }
        }

        // Flick tolerance (HKCU\...\SysEventParameters)
        public int FlickTolerance
        {
            get => _flickTolerance;
            set
            {
                if (_flickTolerance != value)
                {
                    _flickTolerance = value;
                    OnPropertyChanged();
                    SaveSysEventParameter("FlickTolerance", value);
                }
            }
        }

        // Enable hold gesture (HKCU\...\SysEventParameters)
        public bool HoldMode
        {
            get => _holdMode;
            set
            {
                if (_holdMode != value)
                {
                    _holdMode = value;
                    OnPropertyChanged();
                    // HoldMode=1 enables press and hold, HoldMode=3 disables it
                    SaveSysEventParameter("HoldMode", value ? 1 : 3);
                }
            }
        }

        // Hold time in ms (HKCU\...\SysEventParameters)
        public int HoldTime
        {
            get => _holdTime;
            set
            {
                if (_holdTime != value)
                {
                    _holdTime = value;
                    OnPropertyChanged();
                    SaveSysEventParameter("HoldTime", value);
                }
            }
        }

        // Enable right mask (HKCU\...\SysEventParameters)
        public bool RightMaskEnable
        {
            get => _rightMaskEnable;
            set
            {
                if (_rightMaskEnable != value)
                {
                    _rightMaskEnable = value;
                    OnPropertyChanged();
                    SaveSysEventParameter("RightMaskEnable", value ? 1 : 0);
                }
            }
        }

        // Splash threshold (HKCU\...\SysEventParameters)
        public int Splash
        {
            get => _splash;
            set
            {
                if (_splash != value)
                {
                    _splash = value;
                    OnPropertyChanged();
                    SaveSysEventParameter("Splash", value);
                }
            }
        }

        // Single tap time in ms (HKCU\...\SysEventParameters)
        public int TapTime
        {
            get => _tapTime;
            set
            {
                if (_tapTime != value)
                {
                    _tapTime = value;
                    OnPropertyChanged();
                    SaveSysEventParameter("TapTime", value);
                }
            }
        }
        
        // Pen Visualization (HKCU\Control Panel\Cursors\PenVisualization)
        public int PenVisualization
        {
            get => _penVisualization;
            set
            {
                if (_penVisualization != value)
                {
                    _penVisualization = value;
                    OnPropertyChanged();
                    SavePenVisualizationSetting("PenVisualization", value);
                }
            }
        }
        
        // Hide Pen Cursor (HKCU\Control Panel\Cursors\PenVisualization = 0)
        public bool HidePenCursor
        {
            get => _hidePenCursor;
            set
            {
                if (_hidePenCursor != value)
                {
                    _hidePenCursor = value;
                    OnPropertyChanged();
                    SavePenVisualizationSetting("PenVisualization", value ? 0 : 0x0023); // 0x0023=ON, 0=OFF
                    _penVisualization = value ? 0 : 0x0023;
                    OnPropertyChanged(nameof(PenVisualization));
                }
            }
        }
        
        // Touch Prediction Latency (HKLM\SOFTWARE\Microsoft\TouchPrediction\Latency)
        public int TouchPredictionLatency
        {
            get => _touchPredictionLatency;
            set
            {
                if (_touchPredictionLatency != value)
                {
                    _touchPredictionLatency = value;
                    OnPropertyChanged();
                    SaveTouchPredictionSetting("Latency", value);
                }
            }
        }
        
        // Touch Prediction Sample Time (HKLM\SOFTWARE\Microsoft\TouchPrediction\SampleTime)
        public int TouchPredictionSampleTime
        {
            get => _touchPredictionSampleTime;
            set
            {
                if (_touchPredictionSampleTime != value)
                {
                    _touchPredictionSampleTime = value;
                    OnPropertyChanged();
                    SaveTouchPredictionSetting("SampleTime", value);
                }
            }
        }
        
        // Right Click Zone Width (HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad)
        public int RightClickZoneWidth
        {
            get => _rightClickZoneWidth;
            set
            {
                if (_rightClickZoneWidth != value)
                {
                    _rightClickZoneWidth = value;
                    OnPropertyChanged();
                    SaveTouchPadSetting("RightClickZoneWidth", value);
                }
            }
        }
        
        // Right Click Zone Height (HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad) 
        public int RightClickZoneHeight
        {
            get => _rightClickZoneHeight;
            set
            {
                if (_rightClickZoneHeight != value)
                {
                    _rightClickZoneHeight = value;
                    OnPropertyChanged();
                    SaveTouchPadSetting("RightClickZoneHeight", value);
                }
            }
        }

        // Constructor
        public PenSettings()
        {
            LoadSettings();
        }

        // Load settings from registry
        public void LoadSettings()
        {
            _pressureSensitivity = Convert.ToInt32(RegistryHelper.GetValue("PressureSensitivity", 5));
            _latencyMode = Convert.ToInt32(RegistryHelper.GetValue("LatencyMode", 1));
            _doubleTapWidth = Convert.ToInt32(RegistryHelper.GetValue("DoubleTapWidth", 10));
            _doubleTapHeight = Convert.ToInt32(RegistryHelper.GetValue("DoubleTapHeight", 10));
            _doubleTapTime = Convert.ToInt32(RegistryHelper.GetValue("DoubleTapTime", 500));
            
            // This is in a different registry path
            var arbitrationType = Convert.ToInt32(RegistryHelper.GetNtValue("PenArbitrationType", 0));
            _ignoreTouchInputWhenUsingPen = arbitrationType == 1;
            
            // Load SysEventParameters settings
            _dblDist = Convert.ToInt32(RegistryHelper.GetSysEventParameter("DblDist", 20));
            _dblTime = Convert.ToInt32(RegistryHelper.GetSysEventParameter("DblTime", 300));
            _eraseEnable = Convert.ToInt32(RegistryHelper.GetSysEventParameter("EraseEnable", 1)) == 1;
            _flickMode = Convert.ToInt32(RegistryHelper.GetSysEventParameter("FlickMode", 1)) == 1;
            _flickTolerance = Convert.ToInt32(RegistryHelper.GetSysEventParameter("FlickTolerance", 50));
            _holdMode = Convert.ToInt32(RegistryHelper.GetSysEventParameter("HoldMode", 1)) == 1;
            _holdTime = Convert.ToInt32(RegistryHelper.GetSysEventParameter("HoldTime", 2300));
            _rightMaskEnable = Convert.ToInt32(RegistryHelper.GetSysEventParameter("RightMaskEnable", 1)) == 1;
            _splash = Convert.ToInt32(RegistryHelper.GetSysEventParameter("Splash", 50));
            _tapTime = Convert.ToInt32(RegistryHelper.GetSysEventParameter("TapTime", 100));
            
            // Load new settings
            _penVisualization = Convert.ToInt32(RegistryHelper.GetPenVisualizationSetting("PenVisualization", 0x0023));
            _hidePenCursor = _penVisualization == 0;
            
            _touchPredictionLatency = Convert.ToInt32(RegistryHelper.GetTouchPredictionSetting("Latency", 8));
            _touchPredictionSampleTime = Convert.ToInt32(RegistryHelper.GetTouchPredictionSetting("SampleTime", 8));
            
            _rightClickZoneWidth = Convert.ToInt32(RegistryHelper.GetTouchPadSetting("RightClickZoneWidth", 50));
            _rightClickZoneHeight = Convert.ToInt32(RegistryHelper.GetTouchPadSetting("RightClickZoneHeight", 25));
            
            // Notify that all properties have changed
            NotifyAllPropertiesChanged();
        }

        // Save a setting to the registry
        private void SaveSetting(string name, object value)
        {
            RegistryHelper.SetValue(name, value, RegistryValueKind.DWord);
        }

        // Save a setting to the NT registry path
        private void SaveNtSetting(string name, object value)
        {
            RegistryHelper.SetNtValue(name, value, RegistryValueKind.DWord);
        }

        // Save a setting to the SysEventParameters registry path
        private void SaveSysEventParameter(string name, object value)
        {
            RegistryHelper.SetSysEventParameter(name, value, RegistryValueKind.DWord);
        }
        
        // Save a setting to the PenVisualization registry path
        private void SavePenVisualizationSetting(string name, object value)
        {
            RegistryHelper.SetPenVisualizationSetting(name, value, RegistryValueKind.DWord);
        }
        
        // Save a setting to the TouchPrediction registry path
        private void SaveTouchPredictionSetting(string name, object value)
        {
            RegistryHelper.SetTouchPredictionSetting(name, value, RegistryValueKind.DWord);
        }
        
        // Save a setting to the PrecisionTouchPad registry path
        private void SaveTouchPadSetting(string name, object value)
        {
            RegistryHelper.SetTouchPadSetting(name, value, RegistryValueKind.DWord);
        }

        // Reset all settings to default values
        public void ResetToDefaults()
        {
            RegistryHelper.ResetToDefaults();
            LoadSettings();
        }

        // Notify property changed
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Notify all properties changed
        private void NotifyAllPropertiesChanged()
        {
            OnPropertyChanged(nameof(PressureSensitivity));
            OnPropertyChanged(nameof(LatencyMode));
            OnPropertyChanged(nameof(DoubleTapWidth));
            OnPropertyChanged(nameof(DoubleTapHeight));
            OnPropertyChanged(nameof(DoubleTapTime));
            OnPropertyChanged(nameof(IgnoreTouchInputWhenUsingPen));
            
            // Notify for SysEventParameters properties
            OnPropertyChanged(nameof(DblDist));
            OnPropertyChanged(nameof(DblTime));
            OnPropertyChanged(nameof(EraseEnable));
            OnPropertyChanged(nameof(FlickMode));
            OnPropertyChanged(nameof(FlickTolerance));
            OnPropertyChanged(nameof(HoldMode));
            OnPropertyChanged(nameof(HoldTime));
            OnPropertyChanged(nameof(RightMaskEnable));
            OnPropertyChanged(nameof(Splash));
            OnPropertyChanged(nameof(TapTime));
            
            // Notify for new properties
            OnPropertyChanged(nameof(PenVisualization));
            OnPropertyChanged(nameof(HidePenCursor));
            OnPropertyChanged(nameof(TouchPredictionLatency));
            OnPropertyChanged(nameof(TouchPredictionSampleTime));
            OnPropertyChanged(nameof(RightClickZoneWidth));
            OnPropertyChanged(nameof(RightClickZoneHeight));
        }
    }
} 