using ClassTrackerBRFE2022.Models.TafeClassModels;
using ClassTrackerBRFE2022.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Controllers
{
    public class TafeClassController : Controller
    {
        // GET: TafeClassController
        public ActionResult Index()
        {
            List<TafeClass> tafeClasses = TafeClassService.GetAllTafeClasses();
            return View(tafeClasses);
        }

        // GET: TafeClassController/Details/5
        public ActionResult Details(int id)
        {
            TafeClass tafeClass = TafeClassService.GetSingleTafeClass(id);
            return View(tafeClass);
        }

        // GET: TafeClassController/Create
        public ActionResult Create()
        {
            // Get a list of teachers from the API
            var teachers = TeacherService.GetAllTeachers();

            //// Create a List of SelectListItems
            //List<SelectListItem> teacherDDL = new List<SelectListItem>(); 

            //// For each Teacher in the list of teachers
            //foreach(var teacher in teachers)
            //{
            //    SelectListItem item = new SelectListItem
            //    {
            //        Text = teacher.Name,
            //        Value = teacher.TeacherId.ToString()
            //    };

            //    teacherDDL.Add(item);
            //}
            

            var teacherDropDownListModel = teachers.Select(c => new SelectListItem{
                Text = c.Name,
                Value = c.TeacherId.ToString()
            }).ToList();

            ViewBag.TeacherDropDown = teacherDropDownListModel;

            ViewData.Add("teacherDDL", teacherDropDownListModel);

            TempData.Add("teacherDDL", teacherDropDownListModel);

            return View();
        }

        // POST: TafeClassController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TafeClass tafeClass)
        {
            try
            {
                tafeClass.TafeClassId = 0;

                TafeClassService.CreateNewTafeClass(tafeClass);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TafeClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TafeClassController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: TafeClassController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TafeClassController/Delete/5
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
    }
}
