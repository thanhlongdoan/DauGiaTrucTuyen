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
        [DisplayName("Nhập tiêu đề")]
        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        public string Title { get; set; }
        [DisplayName("Nhập nội dung")]
        [Required(ErrorMessage = " Nội dung là bắt buộc")]
        public string Content { get; set; }
        [DisplayName("Nhập tài khoản cần báo cáo")]
        [Required(ErrorMessage = "Tài khoản là bắt buộc")]
        public string ReportUser { get; set; }
        [DisplayName("Nhập mã phiên đấu giá")]
        [Required(ErrorMessage = "Mã phiên đấu giá là bắt buộc")]
        public string Transaction_Id { get; set; }

    }
}