using DauGiaTrucTuyen.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    public class ChartController : Controller
    {
        // GET: Admin/Chart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ChartViewModel model)
        {
            return View();
        }
    }
}