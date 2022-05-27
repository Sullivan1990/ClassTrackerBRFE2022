using ClassTrackerBRFE2022.Models.TafeClassModels;
using ClassTrackerBRFE2022.Models.TeacherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Helpers
{
    public static class SeedData
    {
        public static List<Teacher> GenerateTeachers()
        {
            return new List<Teacher>
            {
                new Teacher { TeacherId = 1, Name = "Steve", Email = "Steve@email.com", Phone = "1234123412" },
                new Teacher { TeacherId = 2, Name = "Jennifer", Email = "Jennifer@email.com", Phone = "5432234523" }
            };
        }

        public static List<TafeClass> GenerateTafeClasses()
        {
            return new List<TafeClass>
            {
                new TafeClass { TafeClassId = 1, Name = "OOP2", Location="Bracken Ridge", DurationMinutes=500, TeacherId = 1, StartTime = DateTime.Now },
            };
        }

    }
}
