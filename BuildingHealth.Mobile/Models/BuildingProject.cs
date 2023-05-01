using System;
using System.Collections.Generic;

namespace BuildingHealth.Mobile.Models
{
    public partial class BuildingProject
    {
        public BuildingProject()
        {

        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public DateTime? WorkStartedDate { get; set; }
        public DateTime? HandoverDate { get; set; }
        public string? Description { get; set; }
        public int? ArchitectId { get; set; }

    }
}
