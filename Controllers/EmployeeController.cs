using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeKeepingYaz.Infrastructure;
using TimeKeepingYaz.Models;

namespace TimeKeepingYaz.Controllers
{
    public class EmployeeController : CustomController
    {
        private TimeKeepingContext _timeKeepingContext = new TimeKeepingContext();
        // GET: Employee
        public ActionResult Index()
        {
            var emp = _timeKeepingContext
                .Employees
                .Where(x => x.IsActive == true)
                .ToList();
            return View(emp);
        }

        [HttpGet]
        public ActionResult CreateOrUpdateEmployee(int? employeeId)
        {
            var emp = _timeKeepingContext.Employees.FirstOrDefault(x => x.Id == employeeId);

            if (emp == null)
                emp = new Employee();

            return View("EmployeeDetail", emp);
        }

        [HttpPost]
        public ActionResult CreateOrUpdateEmployee(Employee emp)
        {
            if (emp.Id == 0) // Create new
            {
                emp.IsActive = true;
                _timeKeepingContext.Employees.Add(emp);
            }
            else
            {
                var record = _timeKeepingContext.Employees.Find(emp.Id);
                record.Name = emp.Name;
                record.PhoneNo = emp.PhoneNo;
                record.Password = emp.Password;
                record.IsAdmin = emp.IsAdmin;
            }

            _timeKeepingContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int employeeId)
        {
            var emp = _timeKeepingContext.Employees.FirstOrDefault(x => x.Id == employeeId);
            emp.IsActive = false;
            _timeKeepingContext.SaveChanges();

            return Content("Done");
        }   
    }
}