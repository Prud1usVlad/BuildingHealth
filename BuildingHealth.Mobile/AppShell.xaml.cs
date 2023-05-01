using BuildingHealth.Mobile.Pages;

namespace BuildingHealth.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login", typeof(Login));
            Routing.RegisterRoute("main", typeof(MainPage));

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!Preferences.ContainsKey("UserId"))
            {
                App.Current.MainPage = App.Current.Handler.MauiContext.Services.GetService<Login>();
            }
        }
    }
}