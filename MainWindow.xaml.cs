using System;
using System.Threading.Tasks;
using System.Windows;

namespace PenSettingsManager
{
    public partial class MainWindow : Window
    {
        private readonly PenSettings _settings;

        public MainWindow()
        {
            InitializeComponent();

            // Create settings model and set as DataContext
            _settings = new PenSettings();
            DataContext = _settings;

            // Load settings from registry
            LoadSettings();
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