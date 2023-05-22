using BuildingHealth.Mobile.Models;
using BuildingHealth.Mobile.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace BuildingHealth.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public AuthService(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public async Task<bool> LogInAsync(string email, string password)
        {
            HttpResponseMessage response = await _httpClient.PostAsync("UserManager/Login",
                new StringContent(JsonSerializer.Serialize(new { email = email, password = password }),
                    Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            string stringResult = await response.Content.ReadAsStringAsync();

            var responseResult = JsonSerializer.Deserialize<AuthResponse>(stringResult, _jsonSerializerOptions);

            if (responseResult.Role.ToLower() != "architect")
            {
                return false;
            }

            Preferences.Default.Set("UserId", responseResult.Id);
            Preferences.Default.Set("Token", responseResult.Token);

            var id = Preferences.Default.Get("UserID", -1);
            var token = Preferences.Default.Get("Token", -1);

            return true;
        }

        public async Task<bool> LogOutAsync()
        {
            SecureStorage.Remove("UserId");
            SecureStorage.Remove("Token");

            return true;
        }
    }
}
