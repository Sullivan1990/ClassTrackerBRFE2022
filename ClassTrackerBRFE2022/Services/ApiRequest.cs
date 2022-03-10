using Microsoft.AspNetCore.Http;
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
        private readonly HttpClient _client;
        private readonly HttpContext _httpContext;

        public ApiRequest(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
            if(_client == null)
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:44379/api/");
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            // If we have a token stored
            if(_httpContext.Session.GetString("Token") != null && _httpContext.Session.GetString("TokenExpiry") != null)
            {
                // If the token is still valid
                if(DateTime.Parse(_httpContext.Session.GetString("TokenExpiry")) > DateTime.Now)
                {
                    // Set the Auth header to the token value
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContext.Session.GetString("Token"));
                }
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
            HttpResponseMessage response = _client.DeleteAsync($"{controllerName}/{id}").Result;

        }

        public T Edit(string controllerName, T entity, int id)
        {
            HttpResponseMessage response = _client.PutAsJsonAsync($"{controllerName}/{id}", entity).Result;

            var responseEntity = response.Content.ReadAsAsync<T>().Result;

            return responseEntity;

        }

        /// <summary>
        /// Retrieves a list of items where the foreign key matches the provided Id
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="endpointName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<T> GetAllForParentId(string controllerName, string endpointName, int id)
        {
            var response = _client.GetAsync($"{controllerName}/{endpointName}/{id}").Result;

            var responseEntities = response.Content.ReadAsAsync<List<T>>().Result;

            return responseEntities;
        }
    }
}
