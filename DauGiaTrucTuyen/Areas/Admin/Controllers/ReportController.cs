using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
using DauGiaTrucTuyen.DataBinding;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {

        // GET: Admin/Report
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        private readonly IReport _iReport;

        public ReportController() : this(new ReportService())
        {
        }
        public ReportController(IReport iReport)
        {
            _iReport = iReport;
        }
        // danh sách reports
        public ActionResult Index()
        {
            return View(_iReport.GetListReport());
        } 

        public ActionResult Details(string reports_id)
        {
            var result = _iReport.DetailReport(reports_id);
            if (result != null)
                return View(result);
            return HttpNotFound();
        }
        public bool Delete(string id)
        {
            if (_iReport.DeleteReport(id))
                return true;
            return false;
        }

        public bool Responed(string id)
        {
            if (_iReport.Responed(id))
                return true;
            return false;
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