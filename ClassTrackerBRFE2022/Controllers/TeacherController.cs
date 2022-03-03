using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassTrackerBRFE2022.Services;
using ClassTrackerBRFE2022.Models.TeacherModels;
using ClassTrackerBRFE2022.Models.TafeClassModels;

namespace ClassTrackerBRFE2022.Controllers
{
    public class TeacherController : Controller
    {
        #region Setup

        private readonly IApiRequest<Teacher> _apiRequest;
        private readonly IApiRequest<TafeClass> _apiTafeClassRequest;

        private readonly string teacherController = "Teacher";

        public TeacherController(IApiRequest<Teacher> apiRequest, IApiRequest<TafeClass> apiTafeClassRequest)
        {
            _apiRequest = apiRequest;
            _apiTafeClassRequest = apiTafeClassRequest;
        }
        #endregion

        #region General CRUD
        // GET: TeacherController
        public ActionResult Index()
        {
            var teacherList = _apiRequest.GetAll(teacherController);

            return View(teacherList);
        }

        // GET: TeacherController/Details/5
        public ActionResult Details(int id)
        {
            Teacher teacher = _apiRequest.GetSingle(teacherController, id);

            return View(teacher);
        }

        // GET: TeacherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeacherCreate teacher)
        {
            try
            {

                Teacher createdTeacher = new Teacher()
                {
                    Email = teacher.Email,
                    Name = teacher.Name,
                    Phone = teacher.Phone
                };

                _apiRequest.Create(teacherController, createdTeacher);

                //TeacherService.CreateNewTeacher(teacher);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Edit/5
        public ActionResult Edit(int id)
        {

            var teacher = _apiRequest.GetSingle(teacherController, id);

            return View(teacher);
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Teacher teacher)
        {
            try
            {
                _apiRequest.Edit(teacherController, teacher, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {               
                return View();
            }
        }

        // GET: TeacherController/Delete/5
        public ActionResult Delete(int id)
        {

            // use the teacher service to get a teacher
            // return the teacher to the view
            // Create the view!

            return View();
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Custom Endpoints



        #endregion
    }
}
