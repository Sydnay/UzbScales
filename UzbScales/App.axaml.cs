using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using UzbScales.ViewModels;
using Splat;
using UzbScales.Views;
using BL;
using Microsoft.EntityFrameworkCore;

namespace UzbScales
{
    public partial class App : Application
    {
        public static readonly IGoodsContext ScalesLocalContext;

        static App()
        {
            ScalesLocalContext = new GoodsContext();
            //необходимо совершать этот вызов до создания моделей/форм/и т.д.
            ((DbContext)ScalesLocalContext).Database.EnsureCreated();
            ScalesLocalContext.Goods.Load();
        }

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
