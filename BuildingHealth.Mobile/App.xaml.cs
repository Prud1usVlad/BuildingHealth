using BuildingHealth.Mobile.Pages;
using Microsoft.Maui.Hosting;

namespace BuildingHealth.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}