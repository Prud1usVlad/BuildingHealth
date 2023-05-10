namespace BuildingHealth.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public NotificationType Type { get; set; }

        public BuildingProject BuildingProject { get; set; }
    }
}
