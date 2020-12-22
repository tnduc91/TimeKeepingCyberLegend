using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeKeepingYaz.Infrastructure;
using TimeKeepingYaz.Models;

namespace TimeKeepingYaz.Controllers
{
    public class CheckinController : CustomController
    {

        private TimeKeepingContext _timeKeepingContext = new TimeKeepingContext();

        // GET: Checkin
        public ActionResult Index()
        {
            return View();
        }
    }
}