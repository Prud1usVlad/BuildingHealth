using BuildingHealth.Mobile.Models;
using BuildingHealth.Mobile.Pages;
using BuildingHealth.Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuildingHealth.Mobile.ViewModels
{
    public class ProjectsViewModel : InpcBase
    {
        private readonly IProjectService _projectService;
        private ObservableCollection<BuildingProject> _buildingProjects;
        private bool _isListRefreshing;

        public ObservableCollection<BuildingProject> BuildingProjects
        {
            get => _buildingProjects;
            set
            {
                _buildingProjects = value;
                OnPropertyChanged("BuildingProjects");
            }
        }

        public bool IsListRefreshing
        {
            get => _isListRefreshing;
            set
            {
                _isListRefreshing = value;
                OnPropertyChanged("IsListRefreshing");
            }
        }

        public ICommand LoadUserProjects { get; protected set; }
        public ICommand ShowDetails { get; protected set; }

        public ProjectsViewModel(IProjectService projectService)
        {
            _projectService = projectService;

            LoadUserProjects = new Command(async () => await OnLoadUserProducts());
            ShowDetails = new Command(async (projectId) => await OnShowDetails((int)projectId));

            _buildingProjects = new ObservableCollection<BuildingProject>(); 
        }

        public async Task OnLoadUserProducts()
        {
            IsListRefreshing = true;

            try
            {
                int userId = Preferences.Default.Get("UserId", -1);

                if (userId < 0)
                    throw new Exception("No user id detected");

                var projects = await _projectService.GetUserProjectsAsync(userId);
                BuildingProjects.Clear();

                foreach ( var project in projects ) 
                {
                    BuildingProjects.Add(project);
                }
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Can't load data. See inner error: " + ex.Message, "OK");
            }

            IsListRefreshing = false;
        }

        public async Task OnShowDetails(int projectId)
        {
            Preferences.Default.Set("SelectedProjectId", projectId);
            await Shell.Current.GoToAsync("details");
        }


    }
}
