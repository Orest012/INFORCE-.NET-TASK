using INFORCE_.NET_TASK.Data;
using INFORCE_.NET_TASK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace INFORCE_.NET_TASK.Controllers
{
    public class URLController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public string Errors;
        public URLController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        [HttpGet]
        [Route("/")]
        [Route("URL/Index")]
        public IActionResult Index()
        {

            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                ViewBag.UserEmail = "";
            }

            ViewBag.UserEmail = userEmail;
            List<URLModel> references = _appDbContext.dataUrl.ToList();
            return View(references);
        }

        [HttpPost]
        [Route("AddLink")]
        public IActionResult AddLink(URLModel url)
        {
            if (url != null && !string.IsNullOrEmpty(url.LongName))
            {
                Errors = null;
                var new_link = _appDbContext.dataUrl.FirstOrDefault(u => u.LongName == url.LongName);
                if (new_link == null)
                {
                    url.GenerateShortName();
                    url.Author = HttpContext.Session.GetString("UserEmail");
                    url.CreatedTime = DateTime.Now;
                    _appDbContext.dataUrl.Add(url);
                    _appDbContext.SaveChanges();
                }
                else {  
                    ViewBag.Error = "This link already exists";
                    var urls = _appDbContext.dataUrl.ToList();

                    var userEmail = HttpContext.Session.GetString("UserEmail");
                    ViewBag.UserEmail = userEmail;

                    return View("~/Views/URL/Index.cshtml", urls);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/{shortName}")]
        public IActionResult RedirectToLongUrl(string shortName)
        {
            var url = _appDbContext.dataUrl.FirstOrDefault(u => u.ShortName == shortName);
            if (url != null)
            {
                return Redirect(url.LongName);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var url = _appDbContext.dataUrl.FirstOrDefault(u => u.Id == id);
            return View(url);
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var url = _appDbContext.dataUrl.FirstOrDefault(u => u.Id == id);
            if (url != null)
            {
                _appDbContext.dataUrl.Remove(url);
                _appDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("AboutView")]
        public IActionResult AboutView()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                ViewBag.UserEmail = "";
            }

            ViewBag.UserEmail = userEmail;
            string fileName = "File.txt";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            string content;
            if (System.IO.File.Exists(filePath))
            {
                content = System.IO.File.ReadAllText(filePath);
            }
            else
            {
                content = "File not found.";
            }

            ViewBag.Content = content;
            

            return View();
        }

        [HttpPost]
        [Route("AboutView")]
        public IActionResult AboutView(string content)
        {
            string fileName = "File.txt";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            WriteToFileAsync(filePath, content);
            ViewData["Content"] = content;
            return View();
        }

        private void WriteToFileAsync(string path, string content)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteAsync(content);
            }
        }

        private  string ReadFromFileAsync(string path)
        {
            if (System.IO.File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    return reader.ReadToEnd();
                }
            }
            return string.Empty;
        }

    }
}
