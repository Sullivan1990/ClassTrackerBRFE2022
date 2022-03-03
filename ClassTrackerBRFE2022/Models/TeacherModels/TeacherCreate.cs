using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ClassTrackerBRFE2022.Models.TeacherModels
{
    public class TeacherCreate
    {
        [StringLength(10, ErrorMessage = "Name exceeds 100 characters")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
