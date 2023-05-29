using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.BLL.Interfaces;
using BuildingHealth.Core.Models;
using BuildingHealth.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

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
                var result = await _userManagerService.Login(body);
                await Authenticate(result.User);

                return Ok(result);
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
        [Authorize(Policy = "IsAdmin")]
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
                var model = await _userManagerService.Register(body);
                if (model?.Error != null)
                {
                    return BadRequest(model.Error);
                }

                await Authenticate(model.User);

                return Ok(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, user.Email),
                new ("Admin", (user.Admin != null).ToString()),
                new (ClaimTypes.MobilePhone, user.Phone)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
