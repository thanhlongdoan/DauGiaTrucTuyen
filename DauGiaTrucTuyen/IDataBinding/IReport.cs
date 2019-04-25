using DauGiaTrucTuyen.Areas.Admin.Models;
using System.Collections.Generic;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface IReport
    {
        bool AddReport(AddReportViewModel model, string userId);

        bool DeleteReport(string id);

        List<ListReportViewModel> GetListReport();

        DetailReportViewModel DetailReport(string id);

        bool Responed(string id);
    }
}