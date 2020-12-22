using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeKeepingYaz.Models;
namespace TimeKeepingYaz.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            var model = new Employee();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(Employee model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new TimeKeepingContext())
                {
                    var user = context.Employees
                                       .Where(u => u.PhoneNo == model.PhoneNo && u.Password == model.Password)
                                       .FirstOrDefault();

                    if (user != null)
                    {
                        Session["UserName"] = user.Name;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu");
                        return View(model);
                    }
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["UserName"] = string.Empty;
            return RedirectToAction("Login", "Account");
        }
    }
}