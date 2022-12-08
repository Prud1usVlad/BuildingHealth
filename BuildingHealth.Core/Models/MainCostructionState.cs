using System;
using System.Collections.Generic;

namespace BuildingHealth.Core.Models
{
    public partial class MainCostructionState
    {
        public int Id { get; set; }
        public int? SensorsResponseId { get; set; }
        public int? CompressionLevel { get; set; }
        public int? DeformationLevel { get; set; }

        public virtual SensorsResponse? SensorsResponse { get; set; }
    }
}
