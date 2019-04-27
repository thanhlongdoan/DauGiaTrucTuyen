using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập !")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Tên đăng nhập không hợp lệ")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu !")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập !")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Tên đăng nhập không hợp lệ")]
        [Display(Name = "Tên đăng nhập")]
        [Remote("CheckUserNameExist", "Account", ErrorMessage = "Tên đăng nhập đã tồn tại !")]
        [MaxLength(15, ErrorMessage = "Tên đăng nhập không quá 15 ký tự !")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email !")]
        [Display(Name = "Email")]
        [Remote("CheckEmailExist", "Account", ErrorMessage = "Email này đã được đăng ký !")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại !")]
        [Display(Name = "Số điện thoại")]
        [Remote("CheckNumberPhoneExist", "Account", ErrorMessage = "Số điện thoại này đã được đăng ký !")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu !")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6-23 kí tự !")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu nhập lại không đúng !")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hình thức xác thực !")]
        public string Select { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập !")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Tên đăng nhập không hợp lệ")]
        [Display(Name = "Tên đăng nhập")]

        public string UserName { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6-23 kí tự !")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu nhập lại")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Mật khẩu mà mật khẩu nhập lại không đúng")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập !")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Tên đăng nhập không hợp lệ")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
    }
    public class UpdateUserViewModel
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên !")]
        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ !")]
        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ !")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
    }

}
