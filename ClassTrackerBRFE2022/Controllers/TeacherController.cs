using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassTrackerBRFE2022.Services;
using ClassTrackerBRFE2022.Models.TeacherModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClassTrackerBRFE2022.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ClassTrackerBRFE2022.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IApiRequest<Teacher> _apiRequest;

        private readonly string teacherController = "Teacher";

        private IWebHostEnvironment _environment;


        public TeacherController(IApiRequest<Teacher> apiRequest, IWebHostEnvironment environment)
        {
            _apiRequest = apiRequest;
            _environment = environment;
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
            if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

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
                if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }

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
            if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

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

                if (!AuthenticationHelper.isAuthenticated(this.HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }

                _apiRequest.Delete(teacherController, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult FilterTeacher(IFormCollection collection)
        {
            // Retrieve filter text
            string filterText = collection["emailProvider"];

            //var teacherList = _apiRequest.GetAll(teacherController).Where(c => c.Email.Contains(filterText)).ToList();

            // retrieve a list of all teachers
            var teacherList = _apiRequest.GetAll(teacherController);

            // filter that list, return the results to a new list
            var filteredList = teacherList.Where(c => c.Email.ToLower().Contains(filterText.ToLower())).ToList();

            // return this list to the index page
            return View("Index", filteredList);

            // Very Bad
            //return View("Index", _apiRequest.GetAll(teacherController).Where(c => c.Email.Contains(collection["emailProvider"])).ToList());
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            // retrieve folder path
            string folderRoot = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads");

            // combine filename and folder path
            string filePath = Path.Combine(folderRoot, file.FileName);

            try
            {
                // save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { success = true, message = "File Uploaded" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = e.Message });
            }
        }
    }
}
