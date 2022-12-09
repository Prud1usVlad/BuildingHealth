using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.Core.Models;

namespace BuildingHealth.BLL.Interfaces
{
    public interface IUserManagerServise
    {
        LoginResultViewModel Login(LoginViewModel credentials);
        LoginResultViewModel Register(RegistrationViewModel credentials);
        void UpdateData(User user);
    }
}
