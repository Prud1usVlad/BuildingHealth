using BuildingHealth.Mobile.ViewModels;

namespace BuildingHealth.Mobile.Pages;

public partial class Statistics : ContentPage
{
	public StatisticsViewModel ViewModel { get; set; }

    public Statistics(StatisticsViewModel viewModel)
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