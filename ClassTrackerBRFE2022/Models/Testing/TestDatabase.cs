using ClassTrackerBRFE2022.Models.TafeClassModels;
using ClassTrackerBRFE2022.Models.TeacherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Models.Testing
{
    public class TestDatabase
    {
        public List<Teacher> Teachers { get; set; }
        public List<TafeClass> TafeClasses { get; set; }
        public List<UserInfo> Users { get; set; }

        public TestDatabase()
        {
            Teachers = new List<Teacher>
            {
                new Teacher { TeacherId = 1, Name = "Steve", Email = "Steve@email.com", Phone = "1234123412" },
                new Teacher { TeacherId = 2, Name = "Jennifer", Email = "Jennifer@email.com", Phone = "5432234523" }
            };
            TafeClasses = new List<TafeClass>();
            Users = new List<UserInfo>();
        }
    }
}
