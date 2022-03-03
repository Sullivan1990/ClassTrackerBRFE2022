using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ClassTrackerBRFE2022.Models.TeacherModels;
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
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            List<Teacher> teachers = response.Content.ReadAsAsync<List<Teacher>>().Result;

            return teachers;
            //var result = response.Content.ReadAsStringAsync().Result;
            //List<Teacher> teacherList = JsonSerializer.Deserialize<List<Teacher>>(result);
        }

        public static void CreateNewTeacher(TeacherCreate teacher)
        {
            if (_client == null)
            {
                ConfigureClient();
            }

            HttpResponseMessage response = _client.PostAsJsonAsync("Teacher", teacher).Result;

        }

        // Get a single Teacher

        public static Teacher GetSingleTeacher(int id)
        {
            // Requires an ID
            if (_client == null)
            {
                ConfigureClient();
            }

            // Send a Get request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.GetAsync($"Teacher/{id}").Result;
            // handle the response
            var teacher = response.Content.ReadAsAsync<Teacher>().Result;
            // return a teacher
            return teacher;
        }

        // Update a Teacher

        public static void UpdateTeacher(int id, Teacher updatedTeacher)
        {
            // requires an ID and a Teacher object
            if (_client == null)
            {
                ConfigureClient();
            }
            // Send a Put request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.PutAsJsonAsync($"Teacher/{id}", updatedTeacher).Result;
            // Handle the response (check if success) 

        }

        // Delete a Teacher
        public static void DeleteTeacher(int id)
        {
            if (_client == null)
            {
                ConfigureClient();
            }
            // Send a Put request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.DeleteAsync($"Teacher/{id}").Result;
        }

    }
}
