using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.Areas.Admin.Models
{
    public class AddReportViewModel
    {
        public string Reports_Id { get; set; }

        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        public string Title { get; set; }

        public string Content { get; set; }

        public string ReportUser { get; set; }

        public string Transaction_Id { get; set; }

    }
}