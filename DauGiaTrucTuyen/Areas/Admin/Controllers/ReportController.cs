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
        //public ActionResult GetListReportForPageClient()
        //{
        //    return PartialView(_iReport.GetListReportForPageClient());
        //}
        //[AllowAnonymous]
        //Danh sách sản phẩm cho trang người dùng


        //public ActionResult Index()
        //{
        //    return View();
        //}
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