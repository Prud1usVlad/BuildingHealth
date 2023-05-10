using BuildingHealth.Core.Models;
using BuildingHealth.Core.ViewModels;

namespace BuildingHealth.BLL.Interfaces
{
    public interface INotificationService
    {
        NotificationViewModel Notify(int buildingId, NotificationType type);

        List<NotificationViewModel> GetAllNotifications(int buildingId);
    }
}
