using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.ViewModels;
using AvaloniaApplication2.Views;
using Splat;

namespace AvaloniaApplication2
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            string workingDirectory = Environment.CurrentDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", $"{workingDirectory}\\Base");

            if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime)
            {
                base.OnFrameworkInitializationCompleted();
                return;
            }

            var desktop = ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            desktop.MainWindow = new MainWindow();
            AppBootstrapper.Register(Locator.CurrentMutable, Locator.Current);
            desktop.MainWindow.DataContext = Locator.Current.GetRequiredService<IMainWindowViewModel>();
            desktop.MainWindow.Show();

            base.OnFrameworkInitializationCompleted();
        }
    }
}
