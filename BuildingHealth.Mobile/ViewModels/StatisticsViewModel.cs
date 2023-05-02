using BuildingHealth.Mobile.Models;
using BuildingHealth.Mobile.Services.Interfaces;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
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
    public class StatisticsViewModel : InpcBase
    {
        private readonly IProjectService _projectService;

        private bool isLoading;
        private ObservableCollection<StatisticPageItem> pageItems;
        private int userId;

        public ObservableCollection<StatisticPageItem> PageItems
        {
            get { return pageItems; }
            set 
            { 
                pageItems = value;
                OnPropertyChanged("PageItems");
            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        
        

        public ICommand LoadData { get; private set; }

        public StatisticsViewModel(IProjectService projectService)
        {
            _projectService = projectService;

            LoadData = new Command(async () => await OnLoadData());

            PageItems = new ObservableCollection<StatisticPageItem>();
            userId = Preferences.Default.Get("UserId", -1);
        }

        public async Task OnLoadData()
        {
            IsLoading = true;

            try
            {
                var items = await GetItems();
                PageItems.Clear();

                foreach (var item in items)
                    PageItems.Add(item);

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Can't load data. See inner error: " + ex.Message, "OK");
            }

            IsLoading = false;
        }

        private async Task<List<StatisticPageItem>> GetItems()
        {
            var res = new List<StatisticPageItem>();

            var projects = await _projectService.GetUserProjectsAsync(userId);

            foreach ( var project in projects )
            {
                var chartData = await _projectService.GetProjectStatisticAsync(project.Id);

                res.Add(new StatisticPageItem
                {
                    BuildingProject = project,
                    Recomendations = await _projectService.GetProjectRecomendationAsync(project.Id),
                    Series = new List<ISeries>
                    {
                        new LineSeries<double>
                        {
                            Values = chartData.Select(i => i.FirstValue),
                            Name = "General state",
                            LegendShapeSize = 40,
                            DataLabelsSize = 40,
                            GeometrySize = 40,
                        },
                        new LineSeries<double>
                        {
                            Values = chartData.Select(i => i.SecondValue),
                            Name = "Constructions state",
                            LegendShapeSize = 40,
                            DataLabelsSize = 40,
                            GeometrySize = 40,
                        },
                        new LineSeries<double>
                        {
                            Values = chartData.Select(i => i.ThirdValue),
                            Name = "Ground state",
                            LegendShapeSize = 40,
                            DataLabelsSize = 40,
                            GeometrySize = 40,
                        }
                    },
                    XAxes = new List<Axis> 
                    { 
                        new Axis 
                        { 
                            Labels = chartData.Select(i => i.Label).ToList(), 
                            TextSize = 40 
                        } 
                    },
                    YAxes = new List<Axis> 
                    { 
                        new Axis 
                        {
                            TextSize = 60
                        } 
                    }
                    
                });
            }

            return res;
        }
    }
}
