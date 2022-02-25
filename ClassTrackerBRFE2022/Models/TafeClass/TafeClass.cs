using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ClassTrackerBRFE2022.Models.TafeClass
{
    public class TafeClass
    {
        public int TafeClassId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? StartTime { get; set; }
        public int? DurationMinutes { get; set; }

        // Foreign Key

        public int? TeacherId { get; set; }

        // Navigation Properties

        public ClassTrackerBRFE2022.Models.Teacher.Teacher? Teacher { get; set; }
        //public ICollection<Unit> Units { get; set; }
    }
}
