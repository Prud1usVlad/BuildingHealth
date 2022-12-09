using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.Core.Models;

namespace BuildingHealth.BLL.Interfaces
{
    public interface IChartDataService
    {
        Task<List<ChartEntries>> GetSensorsResultData(int responseId);
        Task<List<ChartEntries>> GetBuildingStateDynamic(int buildingId);
        public double GetConstructionsState(
            IEnumerable<MainCostructionState> states);
    }
}
