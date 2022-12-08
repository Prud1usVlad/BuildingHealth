using System;
using System.Collections.Generic;

namespace BuildingHealth.Core.Models
{
    public partial class Builder
    {
        public int Id { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public int? ArchitectId { get; set; }

        public virtual Architect? Architect { get; set; }
        public virtual User IdNavigation { get; set; } = null!;
    }
}
