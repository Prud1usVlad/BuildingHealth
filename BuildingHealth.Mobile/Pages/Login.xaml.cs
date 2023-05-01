namespace BuildingHealth.Mobile.Pages;

public partial class Login : ContentPage
{
    public Login()
	{
		InitializeComponent();

        
    }


    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        if (await IsCredentialCorrect(Email.Text, Password.Text))
        {
            await SecureStorage.SetAsync("hasAuth", "true");
            await Shell.Current.GoToAsync("main");
        }
        else
        {
            await DisplayAlert("Login failed", "Email or password if invalid", "Try again");
        }
    }


    private async Task<bool> IsCredentialCorrect(string username, string password)
    {
        return false;
    }

}