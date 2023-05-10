using Microsoft.AspNetCore.Mvc;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.BLL.Interfaces;
using BuildingHealth.Security;
using Microsoft.AspNetCore.Authorization;

namespace BuildingHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserManagerService _userManagerService;

        public UserManagerController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<RegistrationViewModel>> Login([FromBody] LoginViewModel body)
        {
            try
            {
                return Ok(await _userManagerService.Login(body));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult<RegistrationViewModel>> Edit([FromBody] EditUserModel body)
        {
            try
            {
                await _userManagerService.UpdateData(body);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<RegistrationViewModel>> GetAllUsers()
        {
            try
            {
                return Ok(await _userManagerService.GetAllUsers());
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

                return Ok(await _userManagerService.Register(body));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
