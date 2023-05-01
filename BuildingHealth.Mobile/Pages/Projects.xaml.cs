using BuildingHealth.Mobile.ViewModels;

namespace BuildingHealth.Mobile.Pages;

public partial class Projects : ContentPage
{
	public ProjectsViewModel ViewModel { get; set; }

	public Projects(ProjectsViewModel viewModel)
	{
		InitializeComponent();

		ViewModel = viewModel;

		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

		ViewModel.OnLoadUserProducts();
    }
}