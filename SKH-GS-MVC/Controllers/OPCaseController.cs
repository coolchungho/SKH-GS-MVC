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
    public class OPCaseController : Controller
    {
        private OPEntities db = new OPEntities();
        private GSEntities GSdb = new GSEntities();

        // GET: OPCase
        public ActionResult Index(FormCollection form)
        {
            Int32 begin_date =0;
            Int32 end_date = 0;
            DateTime begin_date_search = DateTime.Today;
            DateTime end_date_search = DateTime.Today;

            if (form["begin_date"] ==null)
            {
                DateTime Date_Today = DateTime.Today;
                DateTime Start_Date = Date_Today.AddDays(1 - Convert.ToInt32(Date_Today.DayOfWeek.ToString("d")) - 7);
                DateTime End_Date = Start_Date.AddDays(6);
                var taiwanCalendar = new System.Globalization.TaiwanCalendar();
                var Start_Date_Taiwan = string.Format("{0:D3}{1:D2}{2:D2}", taiwanCalendar.GetYear(Start_Date), Start_Date.Month, Start_Date.Day);
                var End_Date_Taiwan = string.Format("{0:D3}{1:D2}{2:D2}", taiwanCalendar.GetYear(End_Date), End_Date.Month, End_Date.Day);
                begin_date = Convert.ToInt32(Start_Date_Taiwan);               
                end_date = Convert.ToInt32(End_Date_Taiwan);
                begin_date_search = Start_Date;
                end_date_search = End_Date;
            }
            else
            {
                DateTime Start_Date = Convert.ToDateTime(form["begin_date"]);
                DateTime End_Date = Convert.ToDateTime(form["end_date"]);
                var taiwanCalendar = new System.Globalization.TaiwanCalendar();
                var Start_Date_Taiwan = string.Format("{0:D3}{1:D2}{2:D2}", taiwanCalendar.GetYear(Start_Date), Start_Date.Month, Start_Date.Day);
                var End_Date_Taiwan = string.Format("{0:D3}{1:D2}{2:D2}", taiwanCalendar.GetYear(End_Date), End_Date.Month, End_Date.Day);
                begin_date = Convert.ToInt32(Start_Date_Taiwan);
                end_date =Convert.ToInt32(End_Date_Taiwan);
                begin_date_search = Start_Date;
                end_date_search = End_Date;
            }

            ViewBag.begin_date = begin_date_search;
            ViewBag.end_date = end_date_search;

            var result = db.OPRTODRFs
                .AsQueryable()
                .Where(c => c.ODR_DEPT.Trim() == "17")
                .AsEnumerable()
                .Where(c => Convert.ToInt32(c.ODR_TXDT) >= begin_date && Convert.ToInt32(c.ODR_TXDT) <= end_date)
                .OrderBy(c => c.ODR_PSRC)
                .ThenByDescending(c => c.ODR_TXDT)
                .ToList();

            foreach(var resultItem in result)
            {
                try
                {
                resultItem.ODR_M_DR = GSdb.members.Where(c => c.brief_id==resultItem.ODR_M_DR).First().brief;
                }
                catch (Exception ex)
                {
                    resultItem.ODR_M_DR = "";
                }
            }
            return View(result);
        }

        // GET: OPCase/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPRTODRF oPRTODRF = db.OPRTODRFs.Find(id);
            if (oPRTODRF == null)
            {
                return HttpNotFound();
            }
            return View(oPRTODRF);
        }

        // GET: OPCase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OPCase/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ODR_LOGN,ODR_CHRT,ODR_TXDT,ODR_OPRM,ODR_DEPT,ODR_PSRC,ODR_BDNO,ODR_EFLG,ODR_IPNO,ODR_AS_D,ODR_AS_T,ODR_AE_D,ODR_AE_T,ODR_IN_D,ODR_IN_T,ODR_OS_D,ODR_OS_T,ODR_OE_D,ODR_OE_T,ODR_OT_D,ODR_OT_T,ODR_OP_1,ODR_OP_2,ODR_KF_2,ODR_SK_2,ODR_OP_3,ODR_KF_3,ODR_SK_3,ODR_OP_4,ODR_KF_4,ODR_SK_4,ODR_M_DR,ODR_DN_1,ODR_DN_2,ODR_WN_1,ODR_WN_2,ODR_AD_1,ODR_AD_2,ODR_AD_3,ODR_AD_4,ODR_PAYK,ODR_OPID,ODR_ANAM,ODR_AN_D,ODR_INDR,ODR_ASA,ODR_IRFG,ODR_IDFC,ODR_OPAG,ODR_FAID,ODR_DEAD,ODR_SAT1,ODR_SAT2,ODR_SAT3,ODR_SAT4,ODR_SAT5,ODR_ANS1,ODR_ANS2,ODR_ANS3,ODR_ANS4,ODR_ANS5,ODR_M_D2,ODR_M_D3,ODR_M_D4,ODR_PKN1,ODR_PKN2,ODR_PKN3,ODR_PKN4,ODR_TIM1,ODR_TIM2,ODR_TIM3,ODR_TIM4,ODR_CHRF,ODR_SPKD,ODR_ORMT,ODR_ANMT,ODR_ANT1,ODR_ANT2,ODR_ANT3,ODR_WOUD,ODR_ITE1,ODR_ITE2,ODR_ITE3,ODR_ITE4,ODR_NPRO,ODR_PC01,ODR_PC02,ODR_PC03,ODR_PC04,ODR_PC05,ODR_PC06,ODR_PC07,ODR_PC08,ODR_PC09,ODR_PC10,ODR_PC11,ODR_PC12,ODR_PC13,ODR_PC14,ODR_PC15,ODR_PC16,ODR_PC17,ODR_PC18,ODR_PC19,ODR_PC20,ODR_PKN5,ODR_TIM5,ODR_PRDG,ODR_FIND,ODR_OPF,ODR_OPP,ODR_PODG")] OPRTODRF oPRTODRF)
        {
            if (ModelState.IsValid)
            {
                db.OPRTODRFs.Add(oPRTODRF);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oPRTODRF);
        }

        // GET: OPCase/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPRTODRF oPRTODRF = db.OPRTODRFs.Find(id);
            if (oPRTODRF == null)
            {
                return HttpNotFound();
            }
            return View(oPRTODRF);
        }

        // POST: OPCase/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ODR_LOGN,ODR_CHRT,ODR_TXDT,ODR_OPRM,ODR_DEPT,ODR_PSRC,ODR_BDNO,ODR_EFLG,ODR_IPNO,ODR_AS_D,ODR_AS_T,ODR_AE_D,ODR_AE_T,ODR_IN_D,ODR_IN_T,ODR_OS_D,ODR_OS_T,ODR_OE_D,ODR_OE_T,ODR_OT_D,ODR_OT_T,ODR_OP_1,ODR_OP_2,ODR_KF_2,ODR_SK_2,ODR_OP_3,ODR_KF_3,ODR_SK_3,ODR_OP_4,ODR_KF_4,ODR_SK_4,ODR_M_DR,ODR_DN_1,ODR_DN_2,ODR_WN_1,ODR_WN_2,ODR_AD_1,ODR_AD_2,ODR_AD_3,ODR_AD_4,ODR_PAYK,ODR_OPID,ODR_ANAM,ODR_AN_D,ODR_INDR,ODR_ASA,ODR_IRFG,ODR_IDFC,ODR_OPAG,ODR_FAID,ODR_DEAD,ODR_SAT1,ODR_SAT2,ODR_SAT3,ODR_SAT4,ODR_SAT5,ODR_ANS1,ODR_ANS2,ODR_ANS3,ODR_ANS4,ODR_ANS5,ODR_M_D2,ODR_M_D3,ODR_M_D4,ODR_PKN1,ODR_PKN2,ODR_PKN3,ODR_PKN4,ODR_TIM1,ODR_TIM2,ODR_TIM3,ODR_TIM4,ODR_CHRF,ODR_SPKD,ODR_ORMT,ODR_ANMT,ODR_ANT1,ODR_ANT2,ODR_ANT3,ODR_WOUD,ODR_ITE1,ODR_ITE2,ODR_ITE3,ODR_ITE4,ODR_NPRO,ODR_PC01,ODR_PC02,ODR_PC03,ODR_PC04,ODR_PC05,ODR_PC06,ODR_PC07,ODR_PC08,ODR_PC09,ODR_PC10,ODR_PC11,ODR_PC12,ODR_PC13,ODR_PC14,ODR_PC15,ODR_PC16,ODR_PC17,ODR_PC18,ODR_PC19,ODR_PC20,ODR_PKN5,ODR_TIM5,ODR_PRDG,ODR_FIND,ODR_OPF,ODR_OPP,ODR_PODG")] OPRTODRF oPRTODRF)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oPRTODRF).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oPRTODRF);
        }

        // GET: OPCase/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPRTODRF oPRTODRF = db.OPRTODRFs.Find(id);
            if (oPRTODRF == null)
            {
                return HttpNotFound();
            }
            return View(oPRTODRF);
        }

        // POST: OPCase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            OPRTODRF oPRTODRF = db.OPRTODRFs.Find(id);
            db.OPRTODRFs.Remove(oPRTODRF);
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
