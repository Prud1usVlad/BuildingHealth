using BuildingHealth.Mobile.Models;
using BuildingHealth.Mobile.Services.Interfaces;
using System.Text.Json;

namespace BuildingHealth.Mobile.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ProjectService(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public async Task<List<BuildingProject>> GetUserProjectsAsync(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("BuildingProjects");
            response.EnsureSuccessStatusCode();

            string stringResult = await response.Content.ReadAsStringAsync();

            var projects = JsonSerializer.Deserialize<List<BuildingProject>>(stringResult, _jsonSerializerOptions);

            return projects.Where(p => p.ArchitectId == userId).ToList();
        }
    }
}
