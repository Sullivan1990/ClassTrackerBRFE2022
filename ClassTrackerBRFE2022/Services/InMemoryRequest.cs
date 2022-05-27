using ClassTrackerBRFE2022.Models.TafeClassModels;
using ClassTrackerBRFE2022.Models.TeacherModels;
using ClassTrackerBRFE2022.Models.Testing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Services
{
    public class InMemoryRequest<T> : IApiRequest<T> where T : class
    {
        TestDatabase _db;
        public InMemoryRequest(TestDatabase db, IHttpContextAccessor accessor)
        {
            _db = db;
            accessor.HttpContext.Session.SetString("Token", "TestingToken");
        }

        public T Create(string controllerName, T entity)
        {
            switch (typeof(T).Name)
            {
                case nameof(Teacher):
                    var teacher = entity as Teacher;
                    teacher.TeacherId = _db.Teachers.Count == 0 ? 1 : _db.Teachers.OrderByDescending(c => c.TeacherId)
                                                                                  .FirstOrDefault().TeacherId + 1;
                    _db.Teachers.Add(teacher);
                    return teacher as T;
                case nameof(TafeClass):
                    var tafeClass = entity as TafeClass;
                    tafeClass.TafeClassId = _db.TafeClasses.Count == 0 ? 1 : _db.TafeClasses.OrderByDescending(c => c.TafeClassId)
                                                                                            .FirstOrDefault().TafeClassId + 1;
                    _db.TafeClasses.Add(tafeClass);
                    return tafeClass as T;
                default:
                    return null;
            }
        }

        public void Delete(string controllerName, int id)
        {
            switch (typeof(T).Name)
            {
                case nameof(Teacher):
                    var teacherEntity = _db.Teachers.Where(c => c.TeacherId == id).FirstOrDefault();
                    _db.Teachers.Remove(teacherEntity);
                    break;
                case nameof(TafeClass):
                    var tafeclassEntity = _db.TafeClasses.Where(c => c.TafeClassId == id).FirstOrDefault();
                    _db.TafeClasses.Remove(tafeclassEntity);
                    break;           
            }
        }

        public T Edit(string controllerName, T entity, int id)
        {
            switch (typeof(T).Name)
            {
                case nameof(Teacher):
                    var newTeacher = entity as Teacher;
                    var existingTeacher = _db.Teachers.Where(c => c.TeacherId == id).FirstOrDefault();

                    // mapping
                    existingTeacher.Name = newTeacher.Name;
                    existingTeacher.Phone = newTeacher.Phone;
                    existingTeacher.Email = newTeacher.Email;

                    return existingTeacher as T;

                case nameof(TafeClass):
                    var newTafeclass = entity as TafeClass;
                    var existingTafeclass = _db.TafeClasses.Where(c => c.TafeClassId == id).FirstOrDefault();

                    // mapping
                    existingTafeclass.Name = newTafeclass.Name;
                    existingTafeclass.StartTime = newTafeclass.StartTime;
                    existingTafeclass.Description = newTafeclass.Description;
                    existingTafeclass.DurationMinutes = newTafeclass.DurationMinutes;
                    existingTafeclass.Location = newTafeclass.Location;

                    return existingTafeclass as T;

                default:
                    return null;
            }
        }

        public List<T> GetAll(string controllerName)
        {
            switch(typeof(T).Name)
            {
                case nameof(Teacher):
                    return _db.Teachers as List<T>;
                case nameof(TafeClass):
                    foreach(var tafeClass in _db.TafeClasses)
                    {
                        tafeClass.Teacher = _db.Teachers.Where(c => c.TeacherId == tafeClass.TeacherId).FirstOrDefault();
                    }
                    return _db.TafeClasses as List<T>;
                default:
                    return null;
            }
        }

        public List<T> GetAllForEndpoint(string endpoint)
        {
            if (endpoint.Contains("TafeClassesForTeacherId"))
            {
                //"TafeClass/TafeClassesForTeacherId/3"
                //[0]/[1]/[2]
                int teacherId = int.Parse(endpoint.Split('/').LastOrDefault());
                return _db.TafeClasses.Where(c => c.TeacherId == teacherId).ToList() as List<T>;
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
                default:
                    return null;
            }
        }

        public T GetSingleForEndpoint(string endpoint)
        {
            throw new NotImplementedException();
        }
    }
}
