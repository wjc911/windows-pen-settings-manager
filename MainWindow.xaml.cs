using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace PenSettingsManager
{
    [SupportedOSPlatform("windows")]
    public partial class MainWindow : Window
    {
        private readonly PenSettings _settings;

        // P/Invoke constants and methods for setting the application icon
        private const int ICON_SMALL = 0;
        private const int ICON_BIG = 1;
        private const int WM_SETICON = 0x0080;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        public MainWindow()
        {
            InitializeComponent();

            // Create settings model and set as DataContext
            _settings = new PenSettings();
            DataContext = _settings;

            // Load settings from registry
            LoadSettings();
            
            // Set application icon in taskbar
            Loaded += MainWindow_Loaded;
        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "pen.png");
                if (File.Exists(iconPath))
                {
                    // Create bitmap from PNG (this is for the taskbar icon)
                    using (var bitmap = new Bitmap(iconPath))
                    {
                        // Set the Taskbar icon
                        var helper = new WindowInteropHelper(this);
                        IntPtr hWnd = helper.Handle;
                        
                        // Create icon from bitmap
                        IntPtr hIcon = bitmap.GetHicon();
                        
                        // Set icon for the window
                        SendMessage(hWnd, WM_SETICON, ICON_SMALL, hIcon);
                        SendMessage(hWnd, WM_SETICON, ICON_BIG, hIcon);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to set taskbar icon: {ex.Message}", "Icon Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadSettings()
        {
            try
            {
                _settings.LoadSettings();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(
                    $"Some settings require administrator rights to modify. These settings will be read-only.\n\n{ex.Message}",
                    "Admin Rights Required",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to reset all pen settings to their default values?",
                "Confirm Reset",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _settings.ResetToDefaults();
                    MessageBox.Show("Settings have been reset to default values.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(
                        $"Some settings could not be reset because they require administrator rights.\n\n{ex.Message}",
                        "Admin Rights Required",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error resetting settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // The settings are applied automatically when changed due to the binding setup
                // But we can force a refresh of all settings if needed
                _settings.LoadSettings();
                MessageBox.Show(
                    "Settings applied successfully.\n\n" +
                    "Note: Some settings may require you to log out and log back in or restart your computer to take effect.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(
                    $"Some settings could not be applied because they require administrator rights.\n\n{ex.Message}\n\n" +
                    "Try running the application as administrator to change these settings.",
                    "Admin Rights Required",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 