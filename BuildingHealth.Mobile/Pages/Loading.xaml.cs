using AlohaKit.Controls;

namespace BuildingHealth.Mobile.Pages;

public partial class Loading : ContentPage
{
	public Loading()
	{
		InitializeComponent();

	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (await isAuthenticated())
        {
            await Shell.Current.GoToAsync("main");
        }
        else
        {
            await Shell.Current.GoToAsync("login");
        }
        base.OnNavigatedTo(args);
    }

    async Task<bool> isAuthenticated()
    {
        await Task.Delay(2000);
        var hasAuth = await SecureStorage.GetAsync("hasAuth");
        return !(hasAuth == null);
    }

    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
}