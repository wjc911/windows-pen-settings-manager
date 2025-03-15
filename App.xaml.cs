using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PenSettingsManager
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Set the application icon for all windows in the application
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/pen.png");
            
            // Set the resource icon for the application
            if (File.Exists(iconPath))
            {
                try
                {
                    // Create BitmapImage from the file
                    var iconBitmap = new BitmapImage(new Uri(iconPath, UriKind.Absolute));
                    
                    // Set the icon for the main window
                    if (MainWindow is Window mainWindow)
                    {
                        mainWindow.Icon = iconBitmap;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load application icon: {ex.Message}", "Icon Loading Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
} 