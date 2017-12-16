using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using belt_exam.Models;

namespace belt_exam.Controllers
{
    public class HomeController : Controller
    {
        private belt_examContext _context;

        public HomeController(belt_examContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpPost]
        [Route("ValidateRegistration")]
        public IActionResult RegisterUser(RegisterViewModels userAdd)
        {
            users user = _context.users.Where(u => u.email == userAdd.email).SingleOrDefault();
            if (ModelState.IsValid && user == null)
            {
                CreateUser(userAdd);
                int? userIdQ = HttpContext.Session.GetInt32(key: "userId");
                int userId = (int)userIdQ;
                return RedirectToAction("Dashboard", "BeltExam");
            }
            if(user != null && ModelState.IsValid){
                ViewBag.emailError = "That email is already in the database!";
            }
            return View("Index");
        }

        public void CreateUser(RegisterViewModels userAdd)
        {
            users user = new users
            {
                firstName = userAdd.firstName,
                lastName = userAdd.lastName,
                email = userAdd.email,
                psw = userAdd.psw,
            };
            _context.Add(user);
            _context.SaveChanges();
            SelectUser(userAdd);
        }

        public void SelectUser(RegisterViewModels useradd)
        {
            users user = _context.users.Where(u => u.email == useradd.email).Single();
            HttpContext.Session.SetInt32(key: "userId", value: user.id);
        }

        [HttpPost]
        [Route("login")]

        public IActionResult LoginUser(LoginViewModels loginView)
        {
            if (ModelState.IsValid)
            {
                users user = _context.users.Where(u => u.email == loginView.lEmail).SingleOrDefault();
                if (user != null && user.psw == loginView.lPassword)
                {
                    HttpContext.Session.SetInt32(key: "userId", value: user.id);
                    return RedirectToAction("Dashboard", "BeltExam");
                }
                ViewBag.error = "Information does not match any account in the database";
            }
            return View("Index");
        }
    }
}
