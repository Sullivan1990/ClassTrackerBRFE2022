using ClassTrackerBRFE2022.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;

namespace ClassTrackerBRFE2022.Controllers
{
    public class TokenController : Controller
    {
        private readonly HttpClient _httpClient;

        public TokenController()
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new System.Uri("https://localhost:44379/api/");
            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(UserInfo userInfo)
        {
            HttpResponseMessage message = _httpClient
                .PostAsJsonAsync("Auth/GenerateToken", userInfo)
                .Result;

            if (message.IsSuccessStatusCode)
            {
                // Retrieve the token from the HttpResponse
                var token = message.Content.ReadAsStringAsync().Result;

                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetString("TokenExpiry", DateTime.Now.AddDays(2).ToString());

                // Return the user to the home page
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
