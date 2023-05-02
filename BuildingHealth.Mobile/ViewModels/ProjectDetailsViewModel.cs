using BuildingHealth.Mobile.Models;
using BuildingHealth.Mobile.Services.Interfaces;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuildingHealth.Mobile.ViewModels
{
    public class ProjectDetailsViewModel : InpcBase
    {
        private readonly IProjectService _projectService;
        private readonly ICommentService _commentService;

        private BuildingProject buildingProject;
        private ObservableCollection<Recomendation> recomendations;
        private ObservableCollection<Comment> comments;
        private List<ISeries> series;
        private string projectIdInput;
        private int selectedProjectId;


        public BuildingProject BuildingProject 
        { 
            get { return buildingProject; } 
            set
            {
                buildingProject = value;
                OnPropertyChanged("BuildingProject");
            }
        }

        public ObservableCollection<Recomendation> Recomendations
        {
            get { return recomendations; }
            set
            {
                recomendations = value;
                OnPropertyChanged("Recomendations");
            }
        }

        public ObservableCollection<Comment> Comments
        {
            get { return comments; }
            set
            {
                comments = value;
                OnPropertyChanged("Comments");
            }
        }

        public List<ISeries> Series
        {
            get { return series; }
            set
            {
                series = value;
                OnPropertyChanged("Series");
            }
        }

        public string ProjectIdInput
        {
            get { return projectIdInput; }
            set
            {
                projectIdInput = value;
                selectedProjectId = int.Parse(value);
                OnPropertyChanged("ProjectIdInput");
            }
        }

        public ICommand LoadData { get; protected set; }
        public ICommand LeaveComment { get; protected set; }

        public ProjectDetailsViewModel(IProjectService projectService, ICommentService commentService)
        {
            _projectService = projectService;
            _commentService = commentService;

            buildingProject = new BuildingProject();
            recomendations = new ObservableCollection<Recomendation>();
            comments = new ObservableCollection<Comment>();

            series = new List<ISeries>
            {
                new PieSeries<double> { Values = new List<double> { 2 }, InnerRadius = 50, Fill = new SolidColorPaint(SKColors.ForestGreen), Name = "Good" },
                new PieSeries<double> { Values = new List<double> { 4 }, InnerRadius = 50, Fill = new SolidColorPaint(SKColors.IndianRed), Name = "Bad" },
            };

            selectedProjectId = Preferences.Default.Get("SelectedProjectId", -1);
        }

        public async Task OnLoadData()
        {
            try
            {
                BuildingProject = await _projectService.GetBuildingProjectAsync(selectedProjectId);

                Series = await GetSeries();

                var rec = await _projectService.GetProjectRecomendationAsync(selectedProjectId);
                Recomendations.Clear();

                foreach (var r in rec)
                    Recomendations.Add(r);

                var com = await _commentService.GetProjectComments(selectedProjectId);
                Comments.Clear();

                foreach (var c in com)
                    Comments.Add(c);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Can't load data. See inner error: " + ex.Message, "OK");
            }
        }

        public async Task<List<ISeries>> GetSeries()
        {
            var res = new List<ISeries>();
            var series = await _projectService.GetProjectLastStateAsync(selectedProjectId); 

            foreach (var r in series)
            {
                res.Add(new PieSeries<double>
                {
                    Values = new List<double> { r.FirstValue },
                    InnerRadius = 50,
                    Name = r.Label
                });
            }

            return res;
        }
    }
}
