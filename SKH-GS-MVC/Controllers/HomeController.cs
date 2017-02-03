using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SKH_GS_MVC.Models;

namespace SKH_GS_MVC.Controllers
{
    public class HomeController : Controller
    {
        private GSEntities GSDb = new GSEntities();
        private GSdutyEntities GSDuty = new GSdutyEntities();

        public ActionResult Index()
        {
            Dictionary<string, string> dutyToday = new Dictionary<string, string>();

            ViewBag.duty = GSDuty.duty_View  // 今日值班
                .Where(c => c.date == DateTime.Today)
                .Where(c => c.duty_type_id.Substring(0, 4) == "0117")
                .OrderBy(c=> c.duty_type_id)
                .ToList();

            ViewBag.teach = GSDb.teaches.AsEnumerable() // 教學活動
                    .Where(c =>c.teach_date >=DateTime.Today )
                    .Where(c=> c.teach_date <= DateTime.Today.AddDays(7))
                    .OrderBy(c=> c.teach_date)
                    .ToList();

            List<string> teamListToday = new List<string>();

            var teamList = GSDb.teams
                .Where(c => c.team_date == DateTime.Today)
                .OrderBy(c=> c.team_no)
                .ToList();

            int teamCount = teamList.Select(c => c.team_no).Distinct().Count();

            foreach(var teamItem in teamList)
            {
                string teamString = "";
                if(teamItem.vs.Trim() !="") { teamString = "VS " + teamItem.vs.Trim(); }
                if (teamItem.fellow.Trim() !="") { teamString = "Fellow " + teamItem.fellow.Trim(); }
                if (teamItem.cr.Trim() !="") { teamString = teamString + "/CR " + teamItem.cr.Trim(); }
                if (teamItem.r.Trim() != "") { teamString = teamString + "/R " + teamItem.r.Trim(); }
                if (teamItem.pgy.Trim() != "") { teamString = teamString + "/PGY " + teamItem.pgy.Trim(); }
                if (teamItem.intern.Trim() != "") { teamString = teamString + "/Intern " + teamItem.intern.Trim(); }
                if (teamItem.clerk.Trim() != "") { teamString = teamString + "/Clerk " + teamItem.clerk.Trim(); }
                teamListToday.Add(teamString);
            }
            ViewBag.team = teamListToday;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "建置中...";

            return View();
        }

        public ActionResult Contact()
        {
            var contactList = GSDb.members.OrderBy(c=> c.sequece).ToList();

            ViewBag.Message = "Your contact page.";

            return View(contactList);
        }
    }
}