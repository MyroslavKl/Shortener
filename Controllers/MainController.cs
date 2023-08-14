using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TaskPr.Data;
using TaskPr.Models;

namespace TaskPr.Controllers
{
    public class MainController:Controller
    {
        static bool status = false;
        private readonly ApplycationDbContext _db;

        public MainController(ApplycationDbContext db)
        {
            _db = db;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult LogUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(UserModel obj)
        {
            EntryChecking check = new(obj.Email, obj.Password);
            if (check.Veryfing())
            {
                status = true;
                FindObject(obj.Email);
                return RedirectToAction("ShortUrlsTable");
            }
            ModelState.AddModelError("SignInError", "Invalid password or email");
            return View();
        }
        [HttpPost]
        public IActionResult LogUp(UserModel obj)
        {
            Exist exist = new();
            if (exist.IsEmailExist(_db, obj.Email))
            {
                ModelState.AddModelError("ExistEmailError", "Email address already exist");
                return View();
            }
            else if (exist.IsPasswordExist(_db, obj.Password))
            {
                ModelState.AddModelError("ExistPasswordError", "Password already exist");
                return View();
            }
            _db.PesonalInformations.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("LogIn", "Main");
        }

        public IActionResult ShortUrlsTable()
        {
            IEnumerable<UserModel> Info = _db.PesonalInformations;
            return View(Info);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(UrlModel obj)
        {
            if (status == true) {
                Exist exist = new();
                if (exist.IsUrlExist(_db, obj.Url))
                {
                    ModelState.AddModelError("ExistUrlError", "Url already exist");
                    return View();
                }
                Shortener sh = new Shortener();
                obj.ShortUrl = sh.ShortenUrlInternal(obj.Url);
                var user = JsonConvert.DeserializeObject<UserModel>(TempData["key"].ToString());
                var person = _db.PesonalInformations.Include(p=>p.Urls).FirstOrDefault(p=>p.Id==user.Id);
                person.Urls.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("ShortUrlsTable");
            }
            return RedirectToAction("LogIn");
        }
        public IActionResult ShortUrlInfo(int?id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.PesonalInformations.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        public void FindObject(string email) {
            var user = _db.PesonalInformations.First(u => u.Email == email);
            if (user != null) {
                TempData["key"] = JsonConvert.SerializeObject(user);
            }
        }


    }
}
