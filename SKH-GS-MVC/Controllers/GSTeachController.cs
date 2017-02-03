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
    public class GSTeachController : Controller
    {
        private GSEntities db = new GSEntities();

        // GET: GSTeach 教學
        public ActionResult Index()
        {
            var teachList = db.teaches
                .Where(c => c.teach_date.Value.Month == DateTime.Today.Month)
                .OrderBy(c=> c.teach_date).ToList();
            return View(teachList);
        }

        // GET: GSTeach/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teach teach = db.teaches.Find(id);
            if (teach == null)
            {
                return HttpNotFound();
            }
            return View(teach);
        }

        // GET: GSTeach/Create

       
        public ActionResult Create()
        {
            var teachList = db.teaches
                    .Where(c => (c.teach_date.Value.Month >= DateTime.Today.Month))
                    .OrderByDescending(c => c.teach_date).ToList();
            return View(teachList);
        }

        [HttpPost]
        public ActionResult Create(FormCollection scope )
        {
            if (scope.Count != 0)
            {

                DateTime beginDate = Convert.ToDateTime(scope["beginDate"]) ;
                DateTime endDate = Convert.ToDateTime(scope["endDate"]);
                TimeSpan span = endDate.Subtract(beginDate);
                int dayDiff = span.Days + 1;


                teach teachItem = new teach();
                teachItem.teach_date =Convert.ToDateTime( scope["teachDate"]);
                teachItem.weekDay = scope["teachWeekDay"];
                teachItem.teach_time = scope["teach_time"];
                teachItem.activity_name = scope["activity_name"];
                teachItem.learner = scope["learner"];
                teachItem.supervisor = scope["teacher"];
                teachItem.location = scope["location"];
                db.teaches.Add(teachItem);
                db.SaveChanges();

                var teachList = db.teaches
                       .Where(c => (c.teach_date.Value.Month >= DateTime.Today.Month))
                       .OrderByDescending(c => c.teach_date).ToList();
            return View(teachList);

            }
            return View();
        }
        
        // GET: GSTeach/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teach teach = db.teaches.Find(id);
            if (teach == null)
            {
                return HttpNotFound();
            }
            return View(teach);
        }

        // POST: GSTeach/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,teach_date,weekDay,teach_time,activity_name,learner,supervisor,location")] teach teach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teach);
        }

        // GET: GSTeach/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            teach teach = db.teaches.Find(id);
            if (teach == null)
            {
                return HttpNotFound();
            }
            return View(teach);
        }

        // POST: GSTeach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            teach teach = db.teaches.Find(id);
            db.teaches.Remove(teach);
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
