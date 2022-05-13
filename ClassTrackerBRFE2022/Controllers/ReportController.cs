using ChartJSCore.Models;
using ClassTrackerBRFE2022.Models.ViewModels;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ClassTrackerBRFE2022.Controllers
{
    public class ReportController : Controller
    {
        HttpClient _client;
        public ReportController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("ApiClient");
        }

        /// <summary>
        /// Returns a rendered chart and export button
        /// </summary>
        /// <returns></returns>
        public IActionResult TeacherClassCount()
        {
            var response = _client.GetAsync("Report/TeacherClassCountReport").Result;

            List<TeacherClassCount> teacherClassCount = response.Content.ReadAsAsync<List<TeacherClassCount>>().Result;

            // Save the reportData to a temporary store
            //TempData["ReportData"] = teacherClassCount;
            
            // serialise the report data and save in the session.
            var jsonData = JsonSerializer.Serialize(teacherClassCount);
            HttpContext.Session.SetString("ReportData", jsonData);

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

        public IActionResult ExportData()
        {
            // serialise the report data and save in the session.
            var jsonData = HttpContext.Session.GetString("ReportData");
            var reportData = JsonSerializer.Deserialize<List<TeacherClassCount>>(jsonData);

            // Create an empty memory stream
            var stream = new MemoryStream();

            // generate the CSV data
            using (var writeFile = new StreamWriter(stream, leaveOpen: true))
            {
                // Configuration of the CSV Writer
                var csv = new CsvWriter(writeFile, CultureInfo.CurrentCulture, leaveOpen: true);

                // Write the csv data to the memory stream
                csv.WriteRecords(reportData);
            }

            // reset stream position
            stream.Position = 0;

            // return the memory stream as a file
            return File(stream, "application/octet-stream", $"ReportData_{DateTime.Now.ToString("ddMMMyy_HHmmss")}.csv");
        }
    }
}
