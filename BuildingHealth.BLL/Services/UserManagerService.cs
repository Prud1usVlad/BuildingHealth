using BuildingHealth.BLL.Interfaces;
using BuildingHealth.Core.Models;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.DAL;
using BuildingHealth.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BuildingHealth.BLL.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly BuildingHealthDBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public UserManagerService(BuildingHealthDBContext context, TokenService tokenService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _context = context;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginResultViewModel> Login(LoginViewModel credentials)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(user => user.Email == credentials.Email);

            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, credentials.Password, false);
            if (result.Succeeded)
            {
                return GenerateResult(user);
            }

            return null;
        }

        public async Task<LoginResultViewModel> Register(RegistrationViewModel credentials)
        {
            if (await _userManager.Users.AnyAsync(user => user.Email == credentials.Email))
            {
                throw new ArgumentException("User exists");
            }

            if (credentials.Password != credentials.PasswordConfirm)
            {
                throw new ArgumentException("Password not confirmed");

            }

            var user = new User
            {
                UserName = credentials.UserName,
                FirstName = credentials.FirstName,
                Email = credentials.Email,
                SecondName = credentials.SecondName,
                Phone = credentials.Phone,
                Role = nameof(Admin),
                Admin = new Admin
                {
                    Password = credentials.Password,
                }
            };

            var result = await _userManager.CreateAsync(user, credentials.Password);

            if (result.Succeeded)
            {
                return GenerateResult(user);
            }

            return null;
        }

        public async Task UpdateData(EditUserModel user)
        {
            var userForUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (userForUpdate == null)
            {
                throw new Exception("User not found");
            }

            userForUpdate.FirstName = user.FirstName;
            userForUpdate.SecondName = user.SecondName;
            userForUpdate.Phone = user.Phone;

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        private LoginResultViewModel GenerateResult(User user)
        {
            var token = _tokenService.CreateToken(user);
            user.Token = token;

            return new LoginResultViewModel
            {
                Token = token,
                Role = user.Role,
                Id = user.Id
            };
        }
    }
}
