using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace DauGiaTrucTuyen.DataBinding
{
    public class SendMail
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public bool SendMailNotication(NoticationWin model)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("long205888126@gmail.com", "pailaanh126");

            string content = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Views/ManagerAuction/MailContent.cshtml"));
            content = content.Replace("{{ProductName}}", model.ProductName);
            content = content.Replace("{{Transaction_Id}}", model.Transaction_Id);
            content = content.Replace("{{PriceAuction}}", model.PriceAuction.ToString());
            content = content.Replace("{{User_Id_Auction}}", model.User_Id_Auction);
            content = content.Replace("{{User_Id_Add}}", model.User_Id_Add);

            var fromEmail = new MailAddress("long205888126@gmail.com", "Đấu giá trực tuyến");
            var UserWin = db.Users.FirstOrDefault(x => x.Id == model.User_Id_Auction);
            var AddProduct = db.Users.FirstOrDefault(x => x.Id == model.User_Id_Add);
            var toEmailForUserWin = new MailAddress(UserWin.Email.ToString());
            var toEmailForUserAddProduct = new MailAddress(AddProduct.Email.ToString());
            var mailForUserWin = new MailMessage(fromEmail, toEmailForUserWin)
            {
                Subject = "Thông báo",
                Body = content,
                IsBodyHtml = true,
                BodyEncoding = UTF8Encoding.UTF8
            };

            var mailForUserAddProduct = new MailMessage(fromEmail, toEmailForUserAddProduct)
            {
                Subject = "Thông báo",
                Body = content,
                IsBodyHtml = true,
                BodyEncoding = UTF8Encoding.UTF8
            };

            smtp.Send(mailForUserWin);
            smtp.Send(mailForUserAddProduct);
            return true;
        }
    }
}