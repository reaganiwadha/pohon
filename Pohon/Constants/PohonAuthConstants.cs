using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Pohon.Models;

namespace Pohon.Constants
{
    public static class PohonAuthConstants
    {
        public static AuthenticationProperties DefaultAuthProperties()
        {
            return new()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30),
            };
        }

        public static ClaimsIdentity GenerateCookieClaimsFromUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }
    }
}