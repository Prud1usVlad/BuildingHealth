using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingHealth.Core.ViewModels;

namespace BuildingHealth.BLL.Interfaces
{
    public interface IRecomendationService
    {
        Task<List<RecomendationViewModel>> GetRecommendations(int buildingId);

        Task<List<RecomendationViewModel>> GetSensorsRecomendations(int sensorsResponceId);

    }
}
