using BuildingHealth.BLL.Interfaces;
using BuildingHealth.Core.Models;
using BuildingHealth.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildingHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost("health/{buildingId}")]
        public ActionResult<NotificationViewModel> StartNotifyingForBuildingHealth(int buildingId)
        {
            return Ok(_service.Notify(buildingId, NotificationType.Building));
        }

        [HttpPost("water/{buildingId}")]
        public ActionResult<NotificationViewModel> StartNotifyingForGroundWater(int buildingId)
        {
            return Ok(_service.Notify(buildingId, NotificationType.GroundWater));
        }

        [HttpGet("{buildingId}")]
        public ActionResult<NotificationViewModel> GetNotifications(int buildingId)
        {
            return Ok(_service.GetAllNotifications(buildingId));
        }
    }
}
