namespace BuildingHealth.Mobile.Pages;

public partial class ProjectDetails : ContentPage
{
	public int Id { get; set; }

	public ProjectDetails()
	{
		InitializeComponent();
		BindingContext = this;

		Id = Preferences.Default.Get("SelectedProjectId", -1);

	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Id = Preferences.Default.Get("SelectedProjectId", -1);
    }
}