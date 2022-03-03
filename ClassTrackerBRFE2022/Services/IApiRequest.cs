using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Services
{
    public interface IApiRequest<T>
    {
        public List<T> GetAll(string controllerName);
        public T GetSingle(string controllerName, int id);
        public T Create(string controllerName, T entity);
        public T Edit(string controllerName, T entity, int id);
        public void Delete(string controllerName, int id);

    }
}
