using ChartJSCore.Models;
using ClassTrackerBRFE2022.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Controllers
{
    public class ReportController : Controller
    {
        HttpClient _client;
        public ReportController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("ApiClient");
        }

        public IActionResult TeacherClassCount()
        {
            var response = _client.GetAsync("Report/TeacherClassCountReport").Result;

            List<TeacherClassCount> teacherClassCount = response.Content.ReadAsAsync<List<TeacherClassCount>>().Result;

            // define the chart object itself
            Chart chart = new Chart();

            // define the type of chart
            chart.Type = Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = teacherClassCount.Select(c => c.TeacherName).ToList();

            BarDataset barData = new BarDataset()
            {
                Label = "Tafe Classes per teacher",
                Data = teacherClassCount.Select(c => (double?)c.ClassCount).ToList()
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(barData);

            chart.Data = data;

            ViewData["chart"] = chart;
 
            return View();
        }


    }
}
