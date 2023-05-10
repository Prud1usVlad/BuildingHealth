using Microsoft.AspNetCore.Identity;
using BuildingHealth.Core.Models;

namespace BuildingHealth.DAL
{
    public static class Seed
    {
        private static BuildingHealthDBContext _context;
        private static UserManager<User> _userManager;

        public static async Task SeedData(BuildingHealthDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            var users = GetUsers();

            await _context.AddRangeAsync(users);

            await _context.SaveChangesAsync();
        }

        private static List<User> GetUsers()
        {
            var users = new List<User>();
            if (!_context.Users.Any())
            {
                return new List<User>
                {
                    new()
                    {
                        UserName = "Dimas",
                        FirstName = "Dmytro",
                        SecondName = "Zinchenko",
                        Email = "dima@nure.ua",
                        Phone = "+3809238422354",
                        Role = nameof(Admin),
                        Admin = new Admin {Password = "Pa$$word2022"},
                    }
                };
            }

            foreach (var user in users)
            {
                _userManager.CreateAsync(user, "Pa$$w0rd");
            }

            return users;
        }
    }
}
