using AutoMapper;
using BuildingHealth.BLL.Interfaces;
using BuildingHealth.Core.Models;
using BuildingHealth.Core.ViewModels;
using BuildingHealth.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace BuildingHealth.BLL.Services
{
    public class NotificationService : INotificationService, IDisposable
    {
        private readonly BuildingHealthDBContext _context;
        private readonly IMapper _mapper;
        private readonly ConcurrentBag<Timer> _timers = new();

        public NotificationService(BuildingHealthDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public NotificationViewModel Notify(int buildingId, NotificationType type)
        {
            var building = _context.BuildingProjects.FirstOrDefault(x => x.Id == buildingId);
            if (building == null)
            {
                throw new Exception("Building not found");
            }

            var maxDeformationLevel = _context.SensorsResponses
                .Include(r => r.MainCostructionStates)
                .Select(x => x.MainCostructionStates.Where(y => y.Id == buildingId)
                    .Select(s => s.DeformationLevel))
                .Max();

            var groundWaterLevel = _context.SensorsResponses
                .Include(r => r.MainCostructionStates)
                .Where(r => r.BuildingProjectId == buildingId)
                .Select(x => x.GroundWaterLevel)
                .Max();

            var notification = new Notification
            {
                Message = type == NotificationType.Building ? $"Be careful the DeformationLevel exceeded {maxDeformationLevel} " 
                    : $"Be careful the Ground water level exceeded {groundWaterLevel}",
                Type = type,
                BuildingProject = building
            };

            _timers.Add(new Timer(x =>
            {
                SaveNotification(notification);

            }, null, TimeSpan.Zero, TimeSpan.FromHours(24)));


            return _mapper.Map<NotificationViewModel>(notification);
        }

        public List<NotificationViewModel> GetAllNotifications(int buildingId)
        {
            return _mapper.Map<List<NotificationViewModel>>(_context.Notifications.Where(x => x.BuildingProject.Id == buildingId).ToList());
        }

        private void SaveNotification(object obj)
        {
            if (obj is Notification model)
            {
                _context.Notifications.Add(model);
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            foreach (var timer in _timers)
            {
                timer.Dispose();
            }
        }
    }
}
