using System;
using System.Collections.Generic;

namespace BuildingHealth.Core.Models
{
    public partial class SensorsResponse
    {
        public SensorsResponse()
        {
            MainCostructionStates = new HashSet<MainCostructionState>();
        }

        public int Id { get; set; }
        public int? BuildingProjectId { get; set; }
        public DateTime? Date { get; set; }
        public int? GroundWaterLevel { get; set; }
        public int? GroundAcidityLevel { get; set; }

        public virtual BuildingProject? BuildingProject { get; set; }
        public virtual ICollection<MainCostructionState> MainCostructionStates { get; set; }
    }
}
