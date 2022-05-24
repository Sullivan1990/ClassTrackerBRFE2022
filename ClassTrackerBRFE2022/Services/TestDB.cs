using ClassTrackerBRFE2022.Models.TafeClassModels;
using ClassTrackerBRFE2022.Models.TeacherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Services
{
    // Added as a container for the two related Tables
    // This is an in memory representation of the database
    // and tables or seed data could be here.
    public class TestDB
    {
        public List<Teacher> Teachers { get; set; }
        public List<TafeClass> TafeClasses { get; set; }

        public TestDB()
        {
            Teachers = new List<Teacher>();
            TafeClasses = new List<TafeClass>();
        }
    }
}
