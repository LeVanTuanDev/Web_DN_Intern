using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Source_Demo.Models;
using Source_Demo.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Source_Demo.Lib;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace Source_Demo.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        private readonly IS_Account _s_Account;

        public AccountController(IS_Account account)
        {
            _s_Account = account;
        }

        public IActionResult P_Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> P_Login(EM_LoginAccount model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = 0, error = new { message = "Thông tin không hợp lệ" } });
            }

            var res = await _s_Account.Login(model);

            if (res.result == 0 && res.data == null)
            {
                return Json(new { result = 0, error = new { message = "Tài khoản hoặc mật khẩu không đúng" } });
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, res.data.userName),
        new Claim(ClaimTypes.Email, res.data.email),
        new Claim("AccessToken", res.data.accessToken)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Json(new { result = 1, data = res.data });
        }

        public IActionResult P_Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> P_Register(EM_Account model)
        {
            var res = await _s_Account.Register(model);
            var jResult = new M_JResult();
            if (res.data == null && res.result == 0)
            {
                return Json(jResult.MapData(res));
            }
            return Json(jResult.MapData(res));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult P_Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            HttpContext.Session.Clear();
            Response.Cookies.Delete("AccessToken");

            return RedirectToAction("P_Login", "A_Account");
        }

    }
}