using System;
using System.Collections.Generic;

namespace BuildingHealth.Core.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string? Password { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
    }
}
