using Microsoft.AspNetCore.Identity;
using BuildingHealth.Core.Models;
using System.Xml.Linq;
using NuGet.Packaging;

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

            var buildings = GetBuildings(_context.Users.ToList());

            await _context.BuildingProjects.AddRangeAsync(buildings);

            var sensors = GetSensorsResponse(buildings);

            var construction = GetMainConstructionState(sensors);

            await _context.SensorsResponses.AddRangeAsync(sensors);
            await _context.MainCostructionStates.AddRangeAsync(construction);
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
                    },
                    new()
                    {
                        UserName = "Jane",
                        FirstName = "White",
                        SecondName = "White",
                        Email = "jane@nure.ua",
                        Phone = "+3809238443",
                        Role = nameof(Architect),
                        Architect = new Architect {Password = "Pa$$word2022"},
                    },
                    new()
                    {
                        UserName = "Walter",
                        FirstName = "White",
                        SecondName = "Black",
                        Email = "jane@nure.ua",
                        Phone = "+380923844332",
                        Role = nameof(Builder),
                        Builder = new Builder {Password = "Pa$$word2022"},
                    }
                };
            }

            foreach (var user in users)
            {
                _userManager.CreateAsync(user, "Pa$$w0rd");
            }

            return users;
        }

        private static List<BuildingProject> GetBuildings(List<User> users)
        {
            var projects = new List<BuildingProject>();
            if (!_context.BuildingProjects.Any())
            {
                return new List<BuildingProject>
                {
                    new()
                    {
                        Adress = "Road aenue 10-A",
                        Name = "Korlenila residence",
                        Architect = new Architect {Password = "Pa$$word2022", IdNavigation = users[0]}
                    },
                    new()
                    {
                        Adress = "Down town 15",
                        Name = "Barkle residence",
                        Architect = new Architect
                        {
                            Password = "Pa$$word2022", IdNavigation = users[2],
                        }
                    }
                };
            }


            return projects;
        }

        private static List<SensorsResponse> GetSensorsResponse(List<BuildingProject> buildings)
        {
            var projects = new List<SensorsResponse>();
            if (!_context.SensorsResponses.Any())
            {
                return new List<SensorsResponse>
                {
                    new()
                    {
                        BuildingProject = buildings[0],
                        MainCostructionStates = new List<MainCostructionState>(),
                        GroundAcidityLevel = 4,
                        GroundWaterLevel = 3
                    },
                    new()
                    {
                        BuildingProject = buildings[1],
                        MainCostructionStates = new List<MainCostructionState>(),
                        GroundAcidityLevel = 7,
                        GroundWaterLevel = 0
                    }
                };
            }


            return projects;
        }

        private static List<MainCostructionState> GetMainConstructionState(List<SensorsResponse> sensors)
        {
            var projects = new List<MainCostructionState>();
            if (!_context.MainCostructionStates.Any())
            {
                return new List<MainCostructionState>
                {
                    new()
                    {
                        SensorsResponse = sensors[0],
                        CompressionLevel = 6,
                        DeformationLevel = 8
                    },
                    new()
                    {
                        SensorsResponse = sensors[1],
                        CompressionLevel = 4,
                        DeformationLevel = 8
                    }
                };
            }


            return projects;
        }
    }
}
