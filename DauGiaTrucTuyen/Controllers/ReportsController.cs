using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;

namespace DauGiaTrucTuyen.Controllers
{
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
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                _iReport.AddReport(model);
                return RedirectToAction("Index");
            }
            return View(model);
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
