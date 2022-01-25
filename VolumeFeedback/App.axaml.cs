using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using VolumeFeedback.Models;
using VolumeFeedback.ViewModels;
using VolumeFeedback.Views;

namespace VolumeFeedback
{
    public class App : Application
    {
        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            // create our data dir
            if (!Directory.Exists(Files.ConfigFolderPath)) {
                Directory.CreateDirectory(Files.ConfigFolderPath);
            }

            if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    Content = new SettingsView
                    {
                        DataContext = new SettingsViewModel(Settings.Load())
                    }
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}