using BuildingHealth.Core.Models;
using Newtonsoft.Json;

namespace BuildingHealth.Core.ViewModels
{
    public class LoginResultViewModel
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public int Id { get; set; } 
        [JsonIgnore] 
        public User User { get; set; }
        public string Error { get; set; }
    }
}
