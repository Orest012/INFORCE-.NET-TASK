using INFORCE_.NET_TASK.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using INFORCE_.NET_TASK.Models;
using System.Linq;
using INFORCE_.NET_TASK.Controllers;

namespace INFORCE_.NET_TASK.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AccountController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            ViewBag.Error = "";
            return PartialView("~/Views/Account/Login.cshtml");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(User user)
        {
            if (user != null)
            {
                var existingUser = _appDbContext.dataUser.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                if (existingUser != null)
                {
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    // HttpContext.Session.SetString("UserPassword", existingUser.Password);

                    return RedirectToAction("Index", "URL");
                }
                else
                {
                    ViewBag.Error = "Invalid email or password";
                    return View("Login");
                }
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
