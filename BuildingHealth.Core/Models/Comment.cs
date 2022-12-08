using System;
using System.Collections.Generic;

namespace BuildingHealth.Core.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? BuildingProjectId { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string? Text { get; set; }

        public virtual BuildingProject? BuildingProject { get; set; }
        public virtual User? User { get; set; }
    }
}
