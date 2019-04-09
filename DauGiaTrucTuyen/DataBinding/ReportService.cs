using AutoMapper;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.DataBinding
{
    public class ReportService : IReport
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        public bool AddReport(AddReportViewModel model)
        {
            var report = Mapper.Map<Data.Report>(model);
            report.Reports_Id = Guid.NewGuid().ToString();
            report.CreateDate = DateTime.Now;
            report.CreateBy = "";
            report.Status = "";
            report.User_Id = "";
            db.Reports.Add(report);
            db.SaveChanges();
            return true;
        }
    }
}