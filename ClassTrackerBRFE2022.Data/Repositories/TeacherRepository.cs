﻿using ClassTrackerBRFE2022.Data.Models.TeacherModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Data.Repositories
{
    public  class TeacherRepository : ApiRequest<Teacher>, ITeacherRepository
    {
        public TeacherRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {

        }
    }
}
