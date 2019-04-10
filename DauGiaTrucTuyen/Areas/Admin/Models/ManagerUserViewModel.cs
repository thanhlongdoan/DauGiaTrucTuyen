using System;
using System.ComponentModel.DataAnnotations;

namespace DauGiaTrucTuyen.Areas.Admin.Models
{
    public class ManagerUserViewModel
    {
        public class DetailUserViewModel
        {

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
    }
}