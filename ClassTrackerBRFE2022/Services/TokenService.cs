using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace ClassTrackerBRFE2022.Services
{
    public static class TokenService
    {
        public static bool isTokenValid(HttpContext context)
        {
            if (!context.Session.Keys.Any(c => c.Equals("Token")) || !context.Session.Keys.Any(c => c.Equals("TokenExpiry")))
            {
                return false;
            }

            DateTime tokenExpiry = DateTime.Parse(context.Session.GetString("TokenExpiry"));

            if(tokenExpiry < DateTime.Now)
            {
                return false;
            }

            return true;

        }
    }
}
