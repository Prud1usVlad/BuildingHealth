using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.BLL.Interfaces;

namespace BuildingHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserManagerServise _userManagerServise;

        public UserManagerController(IUserManagerServise userManagerServise)
        {
            _userManagerServise = userManagerServise;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<RegistrationViewModel>> Login([FromBody] LoginViewModel body)
        {
            try
            {
                return Ok(_userManagerServise.Login(body));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<RegistrationViewModel>> Register([FromBody] RegistrationViewModel body)
        {
            try
            {
                return Ok(_userManagerServise.Register(body));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
