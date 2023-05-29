using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingHealth.BLL.Interfaces;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.Core.Models;
using BuildingHealth.DAL;
using Microsoft.EntityFrameworkCore;

namespace BuildingHealth.BLL.Services
{
    public class RecomendationService : IRecomendationService 
    {
        private readonly BuildingHealthDBContext _dbContext;
        private readonly IChartDataService _chartDataService;

        public RecomendationService(BuildingHealthDBContext dbContext,
            IChartDataService chartDataService)
        {
            _dbContext = dbContext;
            _chartDataService = chartDataService;
        }

        public async Task<List<RecomendationViewModel>> GetRecommendations(int buildingId)
        {
            var building = await _dbContext.BuildingProjects
                .Include(p => p.SensorsResponses)
                .ThenInclude(r => r.MainCostructionStates)
                .FirstAsync(b => b.Id == buildingId);

            var response = building.SensorsResponses.MaxBy(r => r.Date);

            if (response is null)
            {
                return new List<RecomendationViewModel>
                {
                    new RecomendationViewModel
                    {
                        Date = DateTime.Now,
                        Heading = "No recomendations",
                        Body = "There is no required data for recomendations",
                    }
                };
            }
            else
            {
                return GenerateRecomendations(response);
            }
        }

        public async Task<List<RecomendationViewModel>> GetSensorsRecomendations(int sensorsResponceId)
        {
            var response = await _dbContext.SensorsResponses
                .Include(r => r.MainCostructionStates)
                .FirstAsync(r => r.Id == sensorsResponceId);

            return GenerateRecomendations(response);
        }

        private List<RecomendationViewModel> GenerateRecomendations(SensorsResponse response)
        {
            var res = new List<RecomendationViewModel>();

            if (response.GroundAcidityLevel == 3
                || response.GroundAcidityLevel == 4)
            {
                res.Add(new RecomendationViewModel
                {
                    Heading = "High ground acidity",
                    Body = "You should decrease acidity level. You can use dolomite flour, lime tuff and stuff like that.",
                    Date = response.Date ?? DateTime.Now
                });
            }
            else if (response.GroundAcidityLevel == 5) 
            {
                res.Add(new RecomendationViewModel
                {
                    Heading = "Dangerous ground acidity",
                    Body = "You should decrease acidity level as fast as you can!. You can use dolomite flour, lime tuff and stuff like that.",
                    Date = response.Date ?? DateTime.Now
                });
            }
            else if (response.GroundWaterLevel == 3
                || response.GroundWaterLevel == 4)
            {
                res.Add(new RecomendationViewModel
                {
                    Heading = "High ground water level",
                    Body = "You should decrease water level. You can build drainages, or try installing light or ejector wellpoints.",
                    Date = response.Date ?? DateTime.Now
                });
            }
            else if (response.GroundAcidityLevel == 5)
            {
                res.Add(new RecomendationViewModel
                {
                    Heading = "Dangerous ground water level",
                    Body = "You should decrease water level as fast as you can!. You can build drainages, or try installing light or ejector wellpoints.",
                    Date = response.Date ?? DateTime.Now
                });
            }

            var constructionsState = _chartDataService
                .GetConstructionsState(response.MainCostructionStates);

            if (constructionsState < 25)
            {
                res.Add(new RecomendationViewModel
                {
                    Heading = "Dangerous level of deformation on main costructions",
                    Body = "Something went wrong with your constructions. A Dangerous level of" +
                    " compression and deformation detected. You should check it, but never forget about your safety",
                    Date = response.Date ?? DateTime.Now
                });
            }
            else if (constructionsState < 50)
            {
                res.Add(new RecomendationViewModel
                {
                    Heading = "High level of deformation on main costructions",
                    Body = "A High level of compression and deformation detected." +
                    " You should check it, but never forget about your safety",
                    Date = response.Date ?? DateTime.Now
                });
            }

            return res;
        }
    }
}
