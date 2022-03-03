using System;

namespace ClassTrackerBRFE2022.Models.TafeClassModels
{
    public class TafeClassCreateNoFK
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? StartTime { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
