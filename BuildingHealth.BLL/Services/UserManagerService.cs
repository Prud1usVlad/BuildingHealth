using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingHealth.BLL.Interfaces;
using BuildingHealth.Core.Models;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace BuildingHealth.BLL.Services
{
    public class UserManagerService : IUserManagerServise
    {
        private readonly BuildingHealthDBContext _context;
        private readonly IConfiguration _configuration;

        public UserManagerService(BuildingHealthDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public LoginResultViewModel Login(LoginViewModel credentials)
        {
            var user = GetUserIfValid(credentials);

            if (user is not null)
                return GenerateResult(user);
            else 
                throw new UnauthorizedAccessException("Authorization error");

        }

        public LoginResultViewModel Register(RegistrationViewModel credentials)
        {
            if (_context.Users.Any(u => u.Email == credentials.Email))
                throw new ArgumentException("User exists");
            else if (credentials.Password != credentials.PasswordConfirm)
                throw new ArgumentException("Password not confirmed");
            else
            {
                var newUser = new User
                {
                    Email = credentials.Email,
                    Role = nameof(Architect),
                    FirstName = credentials.FirstName,
                    SecondName = credentials.SecondName,
                    Architect = new Architect
                    {
                        Password = credentials.Password,
                    }
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return GenerateResult(newUser);
            }
        }

        public void UpdateData(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private User GetUserIfValid(LoginViewModel credentials)
        {
            var user = _context.Users
                .Include(u => u.Builder)
                .Include(u => u.Architect)
                .Include(u => u.Admin)
                .FirstOrDefault(u => u.Email == credentials.Email);

            if (user is null)
                return null;
            else
            {
                if (user.Admin is not null &&
                    user.Admin.Password == credentials.Password)
                    return user;
                else if (user.Builder is not null &&
                    user.Builder.Password == credentials.Password)
                    return user;
                else if (user.Architect is not null &&
                    user.Architect.Password == credentials.Password)
                    return user;
                else 
                    return null;

            }
        }

        private LoginResultViewModel GenerateResult(User user)
        {
            string issuer = _configuration.GetValue<string>("Jwt:Issuer");
            string audience = _configuration.GetValue<string>("Jwt:Audience");
            byte[] key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return new LoginResultViewModel
            {
                Token = jwtToken,
                Role = user.Role,
                Id = user.Id,
            };
        }
    }
}
