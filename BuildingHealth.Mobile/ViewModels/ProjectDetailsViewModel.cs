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
        private int selectedProjectId;
        private string commentInput;


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

        public string CommentInput
        {
            get { return commentInput; }
            set
            {
                commentInput = value;
                OnPropertyChanged("CommentInput");
            }
        }
        public SolidColorPaint LegendPaint { get; set; } = new SolidColorPaint(SKColors.White);

        public ICommand LoadData { get; protected set; }
        public ICommand LeaveComment { get; protected set; }

        public ProjectDetailsViewModel(IProjectService projectService, ICommentService commentService)
        {
            _projectService = projectService;
            _commentService = commentService;

            buildingProject = new BuildingProject();
            recomendations = new ObservableCollection<Recomendation>();
            comments = new ObservableCollection<Comment>();

            LoadData = new Command(async () => await OnLoadData());
            LeaveComment = new Command(async () => await OnLeaveComment());

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

        public async Task OnLeaveComment()
        {
            try
            {
                var comment = new Comment()
                {
                    BuildingProjectId = selectedProjectId,
                    UserId = Preferences.Default.Get("UserId", -1),
                    Text = CommentInput,
                    Date = DateTime.Now,
                };

                await _commentService.PostComment(comment);

                var com = await _commentService.GetProjectComments(selectedProjectId);
                Comments.Clear();

                foreach (var c in com)
                    Comments.Add(c);

                CommentInput = "";
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Can't Send comment. See inner error: " + ex.Message, "OK");
            }
        }

        private async Task<List<ISeries>> GetSeries()
        {
            var res = new List<ISeries>();
            var series = await _projectService.GetProjectLastStateAsync(selectedProjectId); 

            foreach (var r in series)
            {
                res.Add(new PieSeries<double>
                {
                    Values = new List<double> { r.FirstValue },
                    InnerRadius = 100,
                    Name = r.Label,
                    DataLabelsPaint = new SolidColorPaint(SKColors.White),
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer,
                    DataLabelsFormatter = p => $"{r.Label}:\n {p.StackedValue.Share:P2}",
                    DataLabelsSize = 30,
                });
            }

            return res;
        }
    }
}
