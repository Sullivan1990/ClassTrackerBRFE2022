using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassTrackerBRFE2022.Services;
using ClassTrackerBRFE2022.Models.TeacherModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClassTrackerBRFE2022.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IApiRequest<Teacher> _apiRequest;

        private readonly string teacherController = "Teacher";

        public TeacherController(IApiRequest<Teacher> apiRequest)
        {
            _apiRequest = apiRequest;
        }

        [HttpPost]
        public IActionResult Filter(IFormCollection collection)
        {
            var result = collection["teacherDDL"].ToString();
            return RedirectToAction("Index", new {filter = result});
        }

        // GET: TeacherController
        public ActionResult Index(string filter = "")
        {
            // If we do not have a token in the session 
            if(!HttpContext.Session.Keys.Any(c => c.Equals("Token")))
            {
                return RedirectToAction("Login", "Auth");
            }


            var teacherList = _apiRequest.GetAll(teacherController);

            

            var teacherDDL = teacherList.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name
            });

            ViewBag.TeacherDDL = teacherDDL;

            if (!String.IsNullOrEmpty(filter))
            {
                var teacherfilteredList = teacherList.Where(c => c.Name == filter);
                return View(teacherfilteredList);

            }

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
            Teacher teacher = _apiRequest.GetSingle(teacherController, id);

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
            Teacher teacher = _apiRequest.GetSingle(teacherController, id);

            return View(teacher);
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _apiRequest.Delete(teacherController, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
