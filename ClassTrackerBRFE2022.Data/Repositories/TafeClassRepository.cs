using ClassTrackerBRFE2022.Data.Models.TafeClassModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Data.Repositories
{
    public class TafeClassRepository : ApiRequest<TafeClass>
    {
        public TafeClassRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }

        public List<TafeClass> GetTafeClassesForTeacherId(int teacherId)
        {
            var response = _client.GetAsync($"TafeClass/GetTafeClassesForTeacherId/{teacherId}").Result;

            var responseEntities = response.Content.ReadAsAsync<List<TafeClass>>().Result;

            return responseEntities;
        }
    }
}
