using BuildingHealth.Mobile.ViewModels;

namespace BuildingHealth.Mobile.Pages;

public partial class ProjectDetails : ContentPage
{
	public ProjectDetailsViewModel ViewModel { get; set; }

    public ProjectDetails(ProjectDetailsViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        BindingContext = ViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ViewModel.OnLoadData();
    }
}