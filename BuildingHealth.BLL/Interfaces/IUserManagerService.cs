using BuildingHealth.Core.Models;
using BuildingHealth.Core.ViewModels;

namespace BuildingHealth.BLL.Interfaces
{
    public interface IUserManagerService
    {
        Task<LoginResultViewModel> Login(LoginViewModel credentials);
        Task<LoginResultViewModel> Register(RegistrationViewModel credentials);
        Task UpdateData(EditUserModel user);
        Task<List<User>> GetAllUsers();
    }
}
