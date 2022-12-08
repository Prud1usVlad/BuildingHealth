using System;
using System.Collections.Generic;

namespace BuildingHealth.Core.Models
{
    public partial class BuildingProject
    {
        public BuildingProject()
        {
            Comments = new HashSet<Comment>();
            SensorsResponses = new HashSet<SensorsResponse>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public DateTime? WorkStartedDate { get; set; }
        public DateTime? HandoverDate { get; set; }
        public string? Description { get; set; }
        public int? ArchitectId { get; set; }

        public virtual Architect? Architect { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<SensorsResponse> SensorsResponses { get; set; }
    }
}
