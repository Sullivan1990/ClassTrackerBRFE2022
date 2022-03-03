using ClassTrackerBRFE2022.Models.TafeClassModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Services
{
    public static class TafeClassService
    {
        private static HttpClient _client;

        private static void ConfigureClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44379/api/");
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static List<TafeClass> GetAllTafeClasses()
        {
            if (_client == null)
            {
                ConfigureClient();
            }

            HttpResponseMessage response = _client.GetAsync("TafeClass").Result;
            List<TafeClass> tafeClasses = response.Content.ReadAsAsync<List<TafeClass>>().Result;

            return tafeClasses;
            
            //var result = response.Content.ReadAsStringAsync().Result;
            //List<Teacher> teacherList = JsonSerializer.Deserialize<List<Teacher>>(result);
        }

        public static void CreateNewTafeClass(TafeClass tafeClass)
        {
            if (_client == null)
            {
                ConfigureClient();
            }

            HttpResponseMessage response = _client.PostAsJsonAsync("TafeClass", tafeClass).Result;

        }

        // Get a single TafeClass

        public static TafeClass GetSingleTafeClass(int id)
        {
            // Requires an ID
            if (_client == null)
            {
                ConfigureClient();
            }

            // Send a Get request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.GetAsync($"TafeClass/{id}").Result;
            // handle the response
            var tafeClass = response.Content.ReadAsAsync<TafeClass>().Result;
            // return a tafeClass
            return tafeClass;
        }

        // Update a Teacher

        public static void UpdateTafeClass(int id, TafeClass updatedTafeClass)
        {
            // requires an ID and a Teacher object
            if (_client == null)
            {
                ConfigureClient();
            }
            // Send a Put request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.PutAsJsonAsync($"TafeClass/{id}", updatedTafeClass).Result;
            // Handle the response (check if success) 

        }

        // Delete a Teacher
        public static void DeleteTafeClass(int id)
        {
            if (_client == null)
            {
                ConfigureClient();
            }
            // Send a Put request to the specified endpoint (+ the ID!)
            HttpResponseMessage response = _client.DeleteAsync($"TafeClass/{id}").Result;
        }

    }
}
