using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeepingYaz.Models
{
    public class CheckinsViewModel
    {
        public string UserName {get;set;}
        public int Month { get; set; }
        public int Year { get; set; }
        public List<DailyCheckin> Checkins { get; set; }

        public CheckinsViewModel()
        {
            Checkins = new List<DailyCheckin>();
        }

    }
}