using ClassTrackerBRFE2022.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022.Controllers
{
    public class AuthController : Controller
    {
        HttpClient _client;
        public AuthController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("ApiClient");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserInfo user)
        {
            string token = "";

            var response = _client.PostAsJsonAsync("Auth/GenerateToken", user).Result;

            if (response.IsSuccessStatusCode)
            {
                // logged in
                token = response.Content.ReadAsStringAsync().Result.Trim('"');

                // Store the token in the session
                HttpContext.Session.SetString("Token", token);
            }
            else
            {
                // there was an issue logging in
                ViewBag.Error = "The provided credentials were incorrect";
                // potentially save a message to ViewBag and render in the view
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
