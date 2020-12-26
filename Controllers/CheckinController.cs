using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeKeepingYaz.Infrastructure;
using TimeKeepingYaz.Models;
using TimeKeepingYaz.StaticData;

namespace TimeKeepingYaz.Controllers
{
    public class CheckinController : CustomController
    {

        private TimeKeepingContext _db = new TimeKeepingContext();

        // GET: Checkin
        public ActionResult Index(int? month, int? year)
        {
            if (month == null)
            {
                month = DateTime.Now.Month;
                year = DateTime.Now.Year;
            }

            var checkinsViewModel = new CheckinsViewModel();
            checkinsViewModel.UserName = Session["UserName"].ToString();
            checkinsViewModel.Month = month.Value;
            checkinsViewModel.Year = year.Value;
            
            var employeeId = (int) Session["UserId"];

            var employee = _db.Employees.Find(employeeId);

            var records = _db
                .DailyCheckins
                .Where(x => x.Employee.Id == employeeId && 
                    x.Date.Value.Year == year && x.Date.Value.Month == month)
                .ToList();

            if (records.Count == 0)
            {
                var daysInMonth = DateTime.DaysInMonth(year.Value, month.Value);

                var checkins = new List<DailyCheckin>();
                for (int day = 1; day <= daysInMonth; day++)
                {
                    var checkin = new DailyCheckin();
                    checkin.Date = new DateTime(year.Value, month.Value, day);
                    checkin.EmployeeId = employee.Id;
                    checkin.StatusId = (int) StatusEnum.Init;
                    checkin.WorkingHours = 0;
                    checkin.Note = "";
                    checkin.ModifiedDate = DateTime.Now;

                    checkins.Add(checkin);
                }
                checkinsViewModel.Checkins.AddRange(checkins);
                _db.DailyCheckins.AddRange(checkins);
                _db.SaveChanges();
                return View(checkinsViewModel);
            }
            checkinsViewModel.Checkins.AddRange(records);
            return View(checkinsViewModel);
        }

        [HttpPost]
        public ActionResult UpdateWorkingHours(int pk, int value)
        {            
            var checkinRecord = _db.DailyCheckins.Find(pk);
            if (checkinRecord == null)
            {
                return Content("No record found");
            }

            checkinRecord.WorkingHours = value;
            checkinRecord.StatusId = (int) StatusEnum.CreatedByEmployee;
            checkinRecord.ModifiedDate = DateTime.Now;
            _db.SaveChanges();

            return Content("OK");
        }

        [HttpPost]
        public ActionResult UpdateNote(int pk, string value)
        {
            var checkinRecord = _db.DailyCheckins.Find(pk);
            if (checkinRecord == null)
            {
                return Content("No record found");
            }

            checkinRecord.Note = value;
            checkinRecord.StatusId = (int)StatusEnum.CreatedByEmployee;
            checkinRecord.ModifiedDate = DateTime.Now;
            _db.SaveChanges();

            return Content("OK");
        }
    }
}