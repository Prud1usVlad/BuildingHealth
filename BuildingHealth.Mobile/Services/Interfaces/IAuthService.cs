using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingHealth.Mobile.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> LogInAsync(string email, string password);
        public Task<bool> LogOutAsync();
    }
}
