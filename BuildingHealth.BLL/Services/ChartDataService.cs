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
    public class ChartDataService : IChartDataService
    {
        private readonly BuildingHealthDBContext _dbContext;

        public ChartDataService(BuildingHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ChartEntries>> GetBuildingStateDynamic(int buildingId)
        {
            var result = new List<ChartEntries>();
            var building = await _dbContext.BuildingProjects
                .Include(p => p.SensorsResponses)
                .ThenInclude(r => r.MainCostructionStates)
                .FirstAsync(b => b.Id == buildingId);

            var responses = building.SensorsResponses;

            foreach (var response in responses)
            {
                result.Add(await GetEntries(response));
            }

            return result;
        }

        public async Task<List<ChartEntries>> GetBuildingState(int buildingId)
        {
            var building = await _dbContext.BuildingProjects
                .Include(p => p.SensorsResponses)
                .ThenInclude(r => r.MainCostructionStates)
                .FirstAsync(b => b.Id == buildingId);

            var response = building.SensorsResponses.MaxBy(r => r.Date);

            if (response == null)
            {
                return new List<ChartEntries>();
            }

            return await GetSensorsResultData(response.Id);
        }

        public async Task<List<ChartEntries>> GetSensorsResultData(int responseId)
        {
            var result = new List<ChartEntries>()
            {
                new ChartEntries() { Label = "Good State, %" },
                new ChartEntries() { Label = "Bad State, %" },
            };

            var sensorsResponse = await _dbContext.SensorsResponses
                .Include(r => r.MainCostructionStates)
                .OrderByDescending(r => r.Date)
                .FirstAsync(r => r.Id == responseId);
            double chartValue = 0;

            chartValue += 25 - (int)sensorsResponse.GroundWaterLevel * 5;
            chartValue += 25 - (int)sensorsResponse.GroundAcidityLevel * 5;
            chartValue += GetConstructionsState(sensorsResponse.MainCostructionStates) / 2;

            result[0].FirstValue = Math.Round(chartValue, 2);
            result[1].FirstValue = Math.Round(100D - chartValue, 2);
            return result;
        }

        public double GetConstructionsState(
            IEnumerable<MainCostructionState> states)
        {
            double coef = 100d / (states.Count() * 2 * 5);
            double agregatedValue = states.Aggregate(0D, (a, i) =>
                a += (double)i.DeformationLevel + (double)i.CompressionLevel);

            return 100 - (coef * agregatedValue);
        }

        private async Task<ChartEntries> GetEntries(SensorsResponse response)
        {
            var entry = (await GetSensorsResultData(response.Id)).First();

            entry.Label = response?.Date?.ToString() ?? DateTime.Now.ToString();
            entry.SecondValue = GetConstructionsState(response.MainCostructionStates);
            entry.ThirdValue = 0;

            entry.ThirdValue += 50 - (int)response.GroundWaterLevel * 10;
            entry.ThirdValue += 50 - (int)response.GroundAcidityLevel * 10;

            return entry;
        }
    }
}
