using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ClassTrackerBRFE2022.Models.Teacher;
using System.Text.Json;
using System.Dynamic;

namespace ClassTrackerBRFE2022.Services
{
    public static class TeacherService
    {
        private static HttpClient _client;

        private static void ConfigureClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44379/api/");
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static List<Teacher> GetAllTeachers()
        {
            if(_client == null)
            {
                ConfigureClient();
            }

            HttpResponseMessage response = _client.GetAsync("Teacher").Result;
            List<Teacher> teachers = response.Content.ReadAsAsync<List<Teacher>>().Result;

            return teachers;
            //var result = response.Content.ReadAsStringAsync().Result;
            //List<Teacher> teacherList = JsonSerializer.Deserialize<List<Teacher>>(result);
        }


    }
}
