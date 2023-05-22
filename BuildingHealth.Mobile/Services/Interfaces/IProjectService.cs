using BuildingHealth.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingHealth.Mobile.Services.Interfaces
{
    public interface IProjectService
    {
        public Task<List<BuildingProject>> GetUserProjectsAsync(int userId);
        public Task<List<ChartPiece>> GetProjectLastStateAsync(int projectId);
        public Task<List<ChartPiece>> GetProjectStatisticAsync(int projectId);
        public Task<List<Recomendation>> GetProjectRecomendationAsync(int projectId);
        public Task<BuildingProject> GetBuildingProjectAsync(int projectId);
    }
}
