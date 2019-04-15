using AutoMapper;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.DataBinding
{
    public class ReportService : IReport
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        public bool AddReport(AddReportViewModel model, string userId)
        {
            var report = new Report
            {
                ReportUser = model.ReportUser,
                Content = model.Content,
                Title = model.Title,
                Transaction_Id = model.Transaction_Id,
                Reports_Id = Guid.NewGuid().ToString(),
                CreateDate = DateTime.Now,
                CreateBy = HttpContext.Current.User.Identity.Name,
                Status = StatusReport.NotResponed,
                User_Id = userId,
            };
            db.Reports.Add(report);
            db.SaveChanges();
            return true;
        }

        public List<ListReportViewModel> GetListReport()
        {
            var list = from report in db.Reports
                       select new ListReportViewModel
                       {
                           Reports_Id = report.Reports_Id,
                           Title = report.Title,
                           Transaction_Id = report.Transaction_Id
                       };
            return list.ToList();
        }

        //Xóa báo cáo
        public bool DeleteReport(string id)
        {
            var report = db.Reports.Find(id);
            if (report != null)
            {
                db.Reports.Remove(report);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public DetailReportViewModel DetailReport(string reports_id)
        {
            var report = db.Reports.Find(reports_id);
            if (report != null)
            {
                var model = Mapper.Map<DetailReportViewModel>(report);
                return model;
            }
            return null;
        }

        public bool Responed(string id)
        {
            var report = db.Reports.Find(id);
            if (report != null)
            {
                report.Status = StatusReport.Responed;
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}