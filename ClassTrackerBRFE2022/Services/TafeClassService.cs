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
    }
}
