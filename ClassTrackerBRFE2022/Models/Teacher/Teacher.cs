using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Models.Teacher
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Navigation Properties
        //public ICollection<TafeClass> TafeClasses { get; set; }
    }
}
