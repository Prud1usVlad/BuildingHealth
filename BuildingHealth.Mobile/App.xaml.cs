using BuildingHealth.Mobile.Pages;
using LiveChartsCore;
using Microsoft.Maui.Hosting;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace BuildingHealth.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            LiveCharts.Configure(config =>
                config
                    .AddSkiaSharp()
                    .AddDefaultMappers()
                    .AddDarkTheme()
                );
        }
    }
}