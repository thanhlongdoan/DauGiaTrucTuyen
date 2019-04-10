using DauGiaTrucTuyen.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface IReport
    {
        bool AddReport(AddReportViewModel model, string userId);
        List<ListReportViewModel> GetListReport();
    }
}