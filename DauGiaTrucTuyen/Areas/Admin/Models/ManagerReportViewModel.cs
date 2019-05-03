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

        [DisplayName("Nội dung")]
        [Required(ErrorMessage = " Nội dung là bắt buộc")]
        public string Content { get; set; }

        [DisplayName("Tài khoản cần báo cáo")]
        [Required(ErrorMessage = "Tài khoản là bắt buộc")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Tên đăng nhập không hợp lệ")]
        public string ReportUser { get; set; }

        [DisplayName("Mã phiên đấu giá")]
        [Required(ErrorMessage = "Mã phiên đấu giá là bắt buộc")]
        public string Transaction_Id { get; set; }

    }

    public class ListReportViewModel
    {
        public string Reports_Id { get; set; }

        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [DisplayName("Mã phiên đấu giá")]
        public string Transaction_Id { get; set; }

        public string Status { get; set; }

    }

    public class DetailReportViewModel
    {
        public string Reports_Id { get; set; }

        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [DisplayName("Nội dung")]
        public string Content { get; set; }

        [DisplayName("Tài khoản cần báo cáo")]
        public string ReportUser { get; set; }

        [DisplayName("Mã phiên đấu giá")]
        public string Transaction_Id { get; set; }

        [DisplayName("Người gửi báo cáo")]
        public string CreateBy { get; set; }

        [DisplayName("Ngày gửi báo cáo")]
        public DateTime CreateDate { get; set; }

        [DisplayName("Trạng thái")]
        public string Status { get; set; }
    }
}