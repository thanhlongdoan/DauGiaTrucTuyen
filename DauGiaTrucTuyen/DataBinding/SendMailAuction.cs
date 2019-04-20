using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DauGiaTrucTuyen.DataBinding
{
    public class SendMailAuction
    {
        private ApplicationUserManager _userManager;

        public async Task<bool> SendMail()
        {
            return await UserManager.SendEmailAsync(user.Id, "Xác thực tài khoản 'Đấu giá trực tuyến'", "Vui lòng click vào  <a href=\"" + callbackUrl + "\">đây để xác thực tài khoản</a>");
        }
    }
}