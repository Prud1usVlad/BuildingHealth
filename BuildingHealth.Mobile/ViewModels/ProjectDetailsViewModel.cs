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

namespace BuildingHealth.Mobile.ViewModels
{
    public class ProjectDetailsViewModel : InpcBase
    {
        private readonly IProjectService _projectService;

        private BuildingProject buildingProject;
        private ObservableCollection<Recomendation> recomendations;
        private ObservableCollection<Comment> comments;
        private ISeries[] series;


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

        public ISeries[] Series
        {
            get { return series; }
            set
            {
                series = value;
                OnPropertyChanged("Series");
            }
        }

        public ProjectDetailsViewModel(IProjectService projectService)
        {
            _projectService = projectService;

            buildingProject = new BuildingProject();
            recomendations = new ObservableCollection<Recomendation>();
            comments = new ObservableCollection<Comment>();

            series = new ISeries[]
            {
                new PieSeries<double> { Values = new List<double> { 2 }, InnerRadius = 50, Fill = new SolidColorPaint(SKColors.ForestGreen) },
                new PieSeries<double> { Values = new List<double> { 4 }, InnerRadius = 50, Fill = new SolidColorPaint(SKColors.IndianRed) },
            };

        }
    }
}
