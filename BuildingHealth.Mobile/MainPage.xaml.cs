using BuildingHealth.Mobile.Pages;
using BuildingHealth.Mobile.Services.Interfaces;

namespace BuildingHealth.Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly IAuthService _authService;

        public MainPage(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            _authService.LogOutAsync();
            App.Current.MainPage = Handler.MauiContext.Services.GetService<Login>();
        }
    }
}