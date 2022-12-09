using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingHealth.Core.Models;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.BLL.Interfaces;

namespace BuildingHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly IChartDataService _chartDataService;

        public ChartsController(IChartDataService chartDataService)
        {
            _chartDataService = chartDataService;
        }

        [HttpGet("SensorsResponse/{id}")]
        public async Task<ActionResult<List<ChartEntries>>> GetResponceChart(int id)
        {
            try
            {
                return await _chartDataService.GetSensorsResultData(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Building/{id}")]
        public async Task<ActionResult<List<ChartEntries>>> GetBuildingChart(int id)
        {
            try
            {
                return await _chartDataService.GetBuildingStateDynamic(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
