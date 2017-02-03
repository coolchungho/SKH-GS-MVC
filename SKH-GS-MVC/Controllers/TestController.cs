using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SKH_GS_MVC.SKH_Check;

namespace SKH_GS_MVC.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            SKH_Check.ServiceSoapClient check = new ServiceSoapClient();
            string name = "";
            string ACC = "";
           ViewBag.result = check.CHECK_AUTH("M012363", "Hsieh0919", ref name, ref ACC);
            ViewBag.name = name;
            ViewBag.ACC = ACC;
            return View();
        }
    }
}