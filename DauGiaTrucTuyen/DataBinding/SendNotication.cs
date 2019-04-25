using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Models;
using Nexmo.Api;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace DauGiaTrucTuyen.DataBinding
{
    public class SendNotication
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public bool SendMailNoticationUserAdd(NoticationWin model)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("long205888126@gmail.com", "gvelfqgnpenxscay");

            var fromEmail = new MailAddress("long205888126@gmail.com", "Đấu giá trực tuyến");
            var UserWin = db.Users.FirstOrDefault(x => x.Id == model.User_Id_Auction);
            var AddProduct = db.Users.FirstOrDefault(x => x.Id == model.User_Id_Add);
            var toEmailForUserWin = new MailAddress(UserWin.Email.ToString());
            var toEmailForUserAddProduct = new MailAddress(AddProduct.Email.ToString());

            //gửi cho người chủ sản phẩm
            string contentForBoss = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Views/ManagerAuction/MailContentForUserAdd.cshtml"));
            contentForBoss = contentForBoss.Replace("{{ProductName}}", model.ProductName);
            contentForBoss = contentForBoss.Replace("{{Transaction_Id}}", model.Transaction_Id);
            contentForBoss = contentForBoss.Replace("{{PriceAuction}}", model.PriceAuction.ToString());
            contentForBoss = contentForBoss.Replace("{{User_Id_Auction}}", model.User_Id_Auction);
            contentForBoss = contentForBoss.Replace("{{UserName}}", UserWin.UserName);
            contentForBoss = contentForBoss.Replace("{{Email}}", UserWin.Email);
            contentForBoss = contentForBoss.Replace("{{PhoneNumber}}", UserWin.PhoneNumber);
            contentForBoss = contentForBoss.Replace("{{LastName}}", UserWin.LastName);
            contentForBoss = contentForBoss.Replace("{{FirstName}}", UserWin.FirstName);

            var mailForUserAddProduct = new MailMessage(fromEmail, toEmailForUserAddProduct)
            {
                Subject = "Thông báo",
                Body = contentForBoss,
                IsBodyHtml = true,
                BodyEncoding = UTF8Encoding.UTF8
            };
            smtp.Send(mailForUserAddProduct);

            return true;
        }

        public bool SendMailNoticationForUserAuction(NoticationWin model)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("long205888126@gmail.com", "gvelfqgnpenxscay");

            var fromEmail = new MailAddress("long205888126@gmail.com", "Đấu giá trực tuyến");
            var UserWin = db.Users.FirstOrDefault(x => x.Id == model.User_Id_Auction);
            var AddProduct = db.Users.FirstOrDefault(x => x.Id == model.User_Id_Add);
            var toEmailForUserWin = new MailAddress(UserWin.Email.ToString());
            var toEmailForUserAddProduct = new MailAddress(AddProduct.Email.ToString());

            //gửi cho người thắng cuộc
            string content = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Views/ManagerAuction/MailContent.cshtml"));
            content = content.Replace("{{ProductName}}", model.ProductName);
            content = content.Replace("{{Transaction_Id}}", model.Transaction_Id);
            content = content.Replace("{{PriceAuction}}", model.PriceAuction.ToString());
            content = content.Replace("{{User_Id_Auction}}", model.User_Id_Auction);
            content = content.Replace("{{UserName}}", AddProduct.UserName);
            content = content.Replace("{{Email}}", AddProduct.Email);
            content = content.Replace("{{PhoneNumber}}", AddProduct.PhoneNumber);
            content = content.Replace("{{LastName}}", AddProduct.LastName);
            content = content.Replace("{{FirstName}}", AddProduct.FirstName);

            var mailForUserWin = new MailMessage(fromEmail, toEmailForUserWin)
            {
                Subject = "Thông báo",
                Body = content,
                IsBodyHtml = true,
                BodyEncoding = UTF8Encoding.UTF8
            };
            smtp.Send(mailForUserWin);

            return true;
        }

        public bool SendSMSNoticationForUserAdd(NoticationWin model)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == model.User_Id_Auction);
            var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "8e34d9a0",
                ApiSecret = "HaoyijDNILS5P1f8"
            });
            string productName = model.ProductName.ToLower();
            productName = Regex.Replace(productName, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            productName = Regex.Replace(productName, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            productName = Regex.Replace(productName, "ì|í|ị|ỉ|ĩ|/g", "i");
            productName = Regex.Replace(productName, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            productName = Regex.Replace(productName, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            productName = Regex.Replace(productName, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            productName = Regex.Replace(productName, "đ", "d");
            string text = "TTPDG: Ten SP: "
                        + productName
                        + ""
                        + ", ST cao nhat: "
                        + model.PriceAuction + "K"
                        + ", TTCT Chu SP: "
                        + ""
                        + ", Ten TK: "
                        + user.UserName
                        + ""
                        + ", Email: "
                        + user.Email
                         + ""
                        + ", SDT: "
                        + user.PhoneNumber
                         + ""
                        + ", HoTen: "
                        + user.LastName + user.FirstName;
            var results = client.SMS.Send(request: new SMS.SMSRequest
            {
                from = "DauGiaTrucTuyen",
                to = "84" + db.Users.FirstOrDefault(x => x.Id == model.User_Id_Add).PhoneNumber,
                text = text
            });
            return true;
        }

        public bool SendSMSNoticationForUserAuction(NoticationWin model)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == model.User_Id_Add);
            var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "8e34d9a0",
                ApiSecret = "HaoyijDNILS5P1f8"
            });
            string productName = model.ProductName.ToLower();
            productName = Regex.Replace(productName, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            productName = Regex.Replace(productName, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            productName = Regex.Replace(productName, "ì|í|ị|ỉ|ĩ|/g", "i");
            productName = Regex.Replace(productName, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            productName = Regex.Replace(productName, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            productName = Regex.Replace(productName, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            productName = Regex.Replace(productName, "đ", "d");

            string text = "TTPDG: Ten SP: "
                        + productName
                        + ""
                        + ", ST cao nhat: "
                        + model.PriceAuction + "K"
                        + ", TTCT SP: "
                        + ""
                        + ", Ten TK: "
                        + user.UserName
                        + ""
                        + ", Email: "
                        + user.Email
                         + ""
                        + ", SDT: "
                        + user.PhoneNumber
                         + ""
                        + ", HoTen: "
                        + user.LastName + user.FirstName;
            var results = client.SMS.Send(request: new SMS.SMSRequest
            {
                from = "DauGiaTrucTuyen",
                to = "84" + db.Users.FirstOrDefault(x => x.Id == model.User_Id_Auction).PhoneNumber,
                text = text
            });
            return true;
        }
    }
}