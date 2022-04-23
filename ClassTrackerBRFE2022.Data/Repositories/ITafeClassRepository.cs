using ClassTrackerBRFE2022.Data.Models.TafeClassModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Data.Repositories
{ 
    public interface ITafeClassRepository : IApiRequest<TafeClass>
    {
        public List<TafeClass> GetTafeClassesForTeacherId(int teacherId);
    }
}
