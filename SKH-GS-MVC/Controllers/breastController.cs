using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SKH_GS_MVC.Models;

namespace SKH_GS_MVC.Controllers
{
    public class breastController : Controller
    {
        private breast_cancerEntities db = new breast_cancerEntities();

        // GET: breast
        public ActionResult Index(string meet_date)
        {
            var meet_dates = db.meeting_case.Select(c => c.meet_date).Distinct().OrderByDescending(c => c.Value).ToList();
            List<string> dates_list = new List<string>();
            foreach (var dateItem in meet_dates)
            {
                if (dateItem != null) { dates_list.Add(dateItem.Value.ToShortDateString()); }
            }           
            SelectList meet_dateList = new SelectList(dates_list);
            ViewBag.meet_date = meet_dateList;
            string currentDate = dates_list.First();

           
            if (meet_date != null)
            {
                currentDate = meet_date;
            }
 
                var  meet_list = db.meeting_case
                .AsEnumerable()
                .SkipWhile(c=> c.meet_date == null)
                .Where(c => c.meet_date.Value.ToShortDateString() == currentDate)
                .OrderByDescending(c => c.meet_date).ToList();
            foreach(var meet_list_item in meet_list)
            {
                meet_list_item.name = (meet_list_item.name.Substring(0, 1) + " O " + meet_list_item.name.Substring(2, 1));
            }
                return View(meet_list);      
        }

        // GET: breast/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            meeting_case meeting_case = db.meeting_case.Find(id);
            if (meeting_case == null)
            {
                return HttpNotFound();
            }
            return View(meeting_case);
        }

        // GET: breast/Create
        public ActionResult Create()
        {
            var GSVS = db.members.Where(c => c.division.Trim() == "一般外科").ToList();
            var PathoVS = db.members.Where(c => c.division.Trim() == "病理檢驗科").ToList();
            SelectList GSVSList = new SelectList(GSVS,"name","name");
            SelectList PathoVSList = new SelectList(PathoVS, "name", "name");
            ViewBag.GSVS = GSVSList;
            ViewBag.PathoVS = PathoVSList;
            return View();
        }

        // POST: breast/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,age,sex,chartNo,history,GSR,GSVS,PathoVS,meet_date")] meeting_case meeting_case)
        {
            if (ModelState.IsValid)
            {
                db.meeting_case.Add(meeting_case);  
                db.SaveChanges();
  
 
               
                return RedirectToAction("Index");
            }

            return View(meeting_case);
        }

        // GET: breast/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            meeting_case meeting_case = db.meeting_case.Find(id);
            if (meeting_case == null)
            {
                return HttpNotFound();
            }
            return View(meeting_case);
        }

        // POST: breast/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,age,sex,chartNo,history,GSR,GSVS,PathoVS,meet_date")] meeting_case meeting_case)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meeting_case).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meeting_case);
        }

        // GET: breast/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            meeting_case meeting_case = db.meeting_case.Find(id);
            if (meeting_case == null)
            {
                return HttpNotFound();
            }
            return View(meeting_case);
        }

        // POST: breast/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            meeting_case meeting_case = db.meeting_case.Find(id);
            db.meeting_case.Remove(meeting_case);
            db.SaveChanges();
            return RedirectToAction("Index");
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
