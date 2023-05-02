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
            Routing.RegisterRoute("details", typeof(ProjectDetails));


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var id = Preferences.Default.Get("UserId", -1);

            if (id < 0)
            {
                App.Current.MainPage = App.Current.Handler.MauiContext.Services.GetService<Login>();
            }
        }
    }
}