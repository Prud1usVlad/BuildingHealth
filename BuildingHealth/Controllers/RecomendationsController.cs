using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingHealth.Core.Models;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.BLL.Interfaces;

namespace BuildingHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendationsController : ControllerBase
    {
        private readonly IRecomendationService _recomendationService;

        public RecomendationsController(IRecomendationService recomendationService)
        {
            _recomendationService = recomendationService;
        }

        [HttpGet("Building/{buildingId}")]
        public async Task<ActionResult<List<RecomendationViewModel>>> GetBuildingChart(int buildingId)
        {
            try
            {
                return await _recomendationService.GetRecomendations(buildingId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Sensors/{responseId}")]
        public async Task<ActionResult<List<RecomendationViewModel>>> GetResponseChart(int responseId)
        {
            try
            {
                return await _recomendationService.GetSensorsRecomendations(responseId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
