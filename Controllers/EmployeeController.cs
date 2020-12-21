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

        [HttpGet]
        public ActionResult CreateEmployee(int? employeeId)
        {
            var emp = _timeKeepingContext.Employees.FirstOrDefault(x => x.Id == employeeId);

            if (emp == null)
                emp = new Employee();

            return View("EmployeeDetail", emp);
        }
    }
}