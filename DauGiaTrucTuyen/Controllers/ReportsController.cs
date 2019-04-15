using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;

namespace DauGiaTrucTuyen.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        private readonly IReport _iReport;

        public ReportsController() : this(new ReportService())
        {

        }

        public ReportsController(IReport report)
        {
            _iReport = report;
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            ViewBag.Message = null;
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_iReport.AddReport(model, User.Identity.GetUserId()))
                    return RedirectToAction("ConfirmCreateReport");
            }
            return View(model);
        }

        public ActionResult ConfirmCreateReport()
        {
            return View();
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
