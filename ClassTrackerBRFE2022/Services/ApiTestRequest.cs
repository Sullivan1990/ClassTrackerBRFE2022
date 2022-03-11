using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Services
{
    public class ApiTestRequest<T> : IApiRequest<T>
    {
        private static List<T> dataStore;

        public ApiTestRequest()
        {
            if(dataStore == null)
            {
                dataStore = new List<T>();
            }
        }

        public T Create(string controllerName, T entity)
        {
            dataStore.Add(entity);
            return entity;
            //row new NotImplementedException();
        }

        public void Delete(string controllerName, int id)
        {
            throw new NotImplementedException();
        }

        public T Edit(string controllerName, T entity, int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(string controllerName)
        {
            return dataStore;
        }

        public List<T> GetAllForParentId(string controllerName, string endpointName, int id)
        {
            throw new NotImplementedException();
        }

        public T GetSingle(string controllerName, int id)
        {
            throw new NotImplementedException();
        }
    }
}
