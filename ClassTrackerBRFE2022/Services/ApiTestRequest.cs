using ClassTrackerBRFE2022.Models.TafeClassModels;
using ClassTrackerBRFE2022.Models.TeacherModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Services
{
    // added 'where T : class' to allow for casting to and from the T type
    public class ApiTestRequest<T> : IApiRequest<T> where T : class
    {
        private const string teacherController = "Teacher";
        private const string tafeclassController = "TafeClass";

        // reference to singleton db class
        TestDB _db;

        public ApiTestRequest(IHttpContextAccessor httpContextAccessor, TestDB db)
        {
            _db = db;
            // override the login checks
            var context = httpContextAccessor.HttpContext;
            context.Session.SetString("Token", "Testing");
            
        }

        public T Create(string controllerName, T entity)
        {
            switch (typeof(T).Name)
            {
                case nameof(Teacher):
                    var teacher = ParseTeacher(entity);
                    teacher.TeacherId = _db.Teachers.Count < 1 ? 1 : _db.Teachers.OrderByDescending(c => c.TeacherId).FirstOrDefault().TeacherId + 1;
                    _db.Teachers.Add(teacher);
                    break;
                case nameof(TafeClass):
                    var tafeClass = ParseTafeClass(entity);
                    tafeClass.TafeClassId = _db.TafeClasses.Count < 1 ? 1 : _db.TafeClasses.OrderByDescending(c => c.TafeClassId).FirstOrDefault().TeacherId + 1;

                    _db.TafeClasses.Add(tafeClass);
                    break;
            }
            return entity;
        }

        public void Delete(string controllerName, int id)
        {
            switch (controllerName)
            {
                case teacherController:
                    var teacherEntity = _db.Teachers.Where(c => c.TeacherId == id).FirstOrDefault();
                    _db.Teachers.Remove(teacherEntity);
                    break;
                case tafeclassController:
                    var tafeclassEntity = _db.TafeClasses.Where(c => c.TafeClassId == id).FirstOrDefault();
                    _db.TafeClasses.Remove(tafeclassEntity);
                    break;
            }
        }

        public T Edit(string controllerName, T entity, int id)
        {
            switch(typeof(T).Name)
            {
                case nameof(Teacher):
                    var newTeacher = ParseTeacher(entity);
                    var existingTeacher = _db.Teachers.Where(c => c.TeacherId == id).FirstOrDefault();
                    // mapping
                    existingTeacher.Name = newTeacher.Name;
                    existingTeacher.Phone = newTeacher.Phone;
                    existingTeacher.Email = newTeacher.Email;
                    break;

                case nameof(TafeClass):
                    var newTafeClass = ParseTafeClass(entity);
                    var existingTafeClass = _db.TafeClasses.Where(c => c.TafeClassId == id).FirstOrDefault();
                    existingTafeClass.Description = newTafeClass.Description;
                    existingTafeClass.DurationMinutes = newTafeClass.DurationMinutes;
                    existingTafeClass.Location = newTafeClass.Location;
                    existingTafeClass.Name = newTafeClass.Name;
                    existingTafeClass.StartTime = newTafeClass.StartTime;
                    break;
            }
            return entity;
        }

        public List<T> GetAll(string controllerName)
        {
            switch (controllerName)
            {
                case teacherController:
                    return _db.Teachers as List<T>;
                case tafeclassController:
                    foreach(var tafeClass in _db.TafeClasses)
                    {
                        // add the teachers to the TafeClasses
                        tafeClass.Teacher = _db.Teachers.Where(c => c.TeacherId == tafeClass.TeacherId).FirstOrDefault();
                    }
                    return _db.TafeClasses as List<T>;
            }
            return null;
        }



        public T GetSingle(string controllerName, int id)
        {
            switch (typeof(T).Name)
            {
                case nameof(Teacher):
                    return _db.Teachers.Where(c => c.TeacherId == id).FirstOrDefault() as T;
                case nameof(TafeClass):
                    return _db.TafeClasses.Where(c => c.TafeClassId == id).FirstOrDefault() as T;
            }
            return null;
        }

        public List<T> GetAllForEndpoint(string endpoint)
        {
            if (endpoint.Contains("TafeClassesForTeacherId"))
            {
                var teacherID = int.Parse(endpoint.Split('/').LastOrDefault());
                return _db.TafeClasses.Where(c => c.TeacherId == teacherID).ToList() as List<T>;
            }
            return null;
            
        }

        public T GetSingleForEndpoint(string endpoint)
        {
            throw new NotImplementedException();
        }

        private Teacher ParseTeacher(T entity)
        {
            return (Teacher)(object)entity;
        }

        private TafeClass ParseTafeClass(T entity)
        {
            return (TafeClass)(object)entity;
        }


    }
}
