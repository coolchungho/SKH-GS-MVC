using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SKH_GS_MVC.Models;

namespace SKH_GS_MVC.Controllers
{
    public class GSTeamController : Controller
    {
        GSEntities db = new GSEntities();
        // GET: GSTeam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var teamList = db.teams
                .Where(c => c.team_date.Value.Month >= DateTime.Today.Month)
                .OrderBy(c => c.team_no).ThenBy(c=> c.team_date).ToList();
            return View(teamList);
        }

        [HttpPost]
        public ActionResult Create(FormCollection scope)
        {
            DateTime beginDate = Convert.ToDateTime(scope["beginDate"]);
            DateTime endDate = Convert.ToDateTime(scope["endDate"]);
            TimeSpan span = endDate.Subtract(beginDate);
            int dayDiff = span.Days + 1;

            for (int i = 0; i <= dayDiff; i++)
            {
                team team = new team();
                team.team_date = beginDate.AddDays(i);
                team.vs = (scope["VS"]).Substring(7);
                team.r = (scope["R"]).Substring(7);
                team.pgy = scope["PGY"];            
                team.team_no = Convert.ToInt16( scope["teamId"]);
                db.teams.Add(team);

            }
            db.SaveChanges();

            var teamList = db.teams
                .Where(c => c.team_date.Value.Month >= DateTime.Today.Month)
                .OrderBy(c => c.team_date).ToList();
            return View(teamList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}