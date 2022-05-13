using ClassTrackerBRFE2022.Models.TeacherModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace ClassTrackerBRFE2022.Models.TafeClassModels
{
    public class TafeClass
    {
        public int TafeClassId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [Display(Name="Start Time")]
        public DateTime? StartTime { get; set; }
        [Display(Name = "Minutes")]
        public int? DurationMinutes { get; set; }


        public int TeacherId { get; set; }

        // Navigation Properties

        public Teacher Teacher { get; set; }
        //public ICollection<Unit> Units { get; set; }
    }
}
