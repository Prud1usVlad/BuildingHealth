using System;
using System.Collections.Generic;

namespace BuildingHealth.Core.Models
{
    public partial class Architect
    {
        public Architect()
        {
            Builders = new HashSet<Builder>();
            BuildingProjects = new HashSet<BuildingProject>();
        }

        public int Id { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
        public virtual ICollection<Builder> Builders { get; set; }
        public virtual ICollection<BuildingProject> BuildingProjects { get; set; }
    }
}
