using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Services
{
    public class ApiRequest<T> : IApiRequest<T>
    {
        private static HttpClient _client;

        public ApiRequest()
        {
            if(_client == null)
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:44379/api/");
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
        public List<T> GetAll(string controllerName)
        {
            HttpResponseMessage response = _client.GetAsync(controllerName).Result;

            var entityResult = response.Content.ReadAsAsync<List<T>>().Result;

            return entityResult;
        }

        public T GetSingle(string controllerName, int id)
        {
            HttpResponseMessage response = _client.GetAsync($"{controllerName}/{id}").Result;

            var entityResult = response.Content.ReadAsAsync<T>().Result;

            return entityResult;
        }

        public T Create(string controllerName, T entity)
        {
            HttpResponseMessage response = _client.PostAsJsonAsync(controllerName, entity).Result;

            var responseEntity = response.Content.ReadAsAsync<T>().Result;

            return responseEntity;
        }

        public void Delete(string controllerName, int id)
        {
            throw new NotImplementedException();
        }

        public T Edit(string controllerName, T entity, int id)
        {
            throw new NotImplementedException();
        }


    }
}
