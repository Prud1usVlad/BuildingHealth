using BuildingHealth.Core.Models;

namespace BuildingHealth.Mqtt.Models
{
    public class ResponseModel
    {
        public int BuildingProjectId { get; set; }
        public int GroundWaterLevel { get; set; }
        public int GroundAcidityLevel { get; set; }

        public List<ConstructionsStateModel> MainCostructionStates { get; set; }

        public SensorsResponse GetDbModel()
        {
            return new SensorsResponse
            {
                BuildingProjectId = BuildingProjectId,
                GroundAcidityLevel = GroundAcidityLevel,
                GroundWaterLevel = GroundWaterLevel,
                Date = DateTime.Now,
                MainCostructionStates = MainCostructionStates
                    .Select(i => new MainCostructionState
                    {
                        DeformationLevel = i.DeformationLevel,
                        CompressionLevel = i.CompressionLevel,
                    })
                    .ToList(),
            };
        }
    }
}
