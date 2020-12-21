using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeKeepingYaz.Models;

namespace TimeKeepingYaz.Controllers
{
    public class EmployeeController : Controller
    {
        private TimeKeepingContext _timeKeepingContext = new TimeKeepingContext();
        // GET: Employee
        public ActionResult Index()
        {
            var emp = _timeKeepingContext.Statuses.ToList();
            return View();
        }
    }
}