using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingHealth.Mobile.Models
{
    public class StatisticPageItem
    {
        public BuildingProject BuildingProject { get; set; }
        public List<Recomendation> Recomendations { get; set; }
        public List<ISeries> Series { get; set; }
        public List<Axis> XAxes { get; set; }
    }
}
