using BuildingHealth.Mobile.Services.Interfaces;

namespace BuildingHealth.Mobile.Pages;

public partial class Login : ContentPage
{
    private readonly IAuthService _authService;

    public Login(IAuthService authService)
    {
        InitializeComponent();

        _authService = authService;
    }


    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            await _authService.LogInAsync(Email.Text, Password.Text);
            App.Current.MainPage = Handler.MauiContext.Services.GetService<AppShell>();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Login failed", "Email or password if invalid", "Try again");
        }
    }


}