namespace BuildingHealth.Core.ViewModels
{
    public class NotificationViewModel
    {
        public string Message { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

    }
}
