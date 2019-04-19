using System;
using System.ComponentModel.DataAnnotations;

namespace DauGiaTrucTuyen.Areas.Admin.Models
{
    public class ManagerUserViewModel
    {
        public class DetailUserViewModel
        {
            public string Id { get; set; }

            [Display(Name = "Tên đăng nhập")]
            public string UserName { get; set; }

            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Trạng thái Email")]
            public bool EmailConfirmed { get; set; }

            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Trạng thái SDT")]
            public bool PhoneNumberConfirmed { get; set; }

            [Display(Name = "Ngày tạo")]
            public DateTime CreateDate { get; set; }

            [Display(Name = "Tên")]
            public string FirstName { get; set; }

            [Display(Name = "Họ")]
            public string LastName { get; set; }

            [Display(Name = "Địa chỉ")]
            public string Address { get; set; }

            [Display(Name = "Trạng thái khóa đấu giá")]
            public string BlockAuctionStatus { get; set; }

            [Display(Name = "Thời gian khóa đấu giá")]
            public DateTime? BlockAuctionDate { get; set; }

            [Display(Name = "Trạng thái khóa tài khoản")]
            public string BlockUserStatus { get; set; }

            [Display(Name = "Thời gian khóa tài khoản")]
            public DateTime? BlockUserDate { get; set; }
        }
        public class ListUserViewModel
        {
            public string Id { get; set; }

            [Display(Name = "Tên hiển thị")]
            public string UserName { get; set; }

            [Display(Name = "SDT")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Ngày tạo")]
            public DateTime CreateDate { get; set; }
        }

        public class HandleUserViewModel
        {
            public string StatusUsers_Id { get; set; }

            [Display(Name = "Trạng thái khóa đấu giá")]
            public string BlockAuctionStatus { get; set; }

            [Display(Name = "Trạng thái khóa tài khoản")]
            public string BlockUserStatus { get; set; }
        }
    }
}