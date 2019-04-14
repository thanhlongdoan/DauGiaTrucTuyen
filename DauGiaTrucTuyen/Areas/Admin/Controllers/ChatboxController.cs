using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChatboxController : Controller
    {
        public Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        // GET: Admin/Chatbox
        public ActionResult Index()
        {
            ChaterService chater = new ChaterService();
            ApplicationDbContext db = new ApplicationDbContext();
            List<UserChat> list = chater.GetAllUser();
            List<ChatboxViewModel> listUser = new List<ChatboxViewModel>();
            MessageService messageDb = new MessageService();
            foreach (var item in list)
            {
                ChatboxViewModel userView = new ChatboxViewModel();
                userView.ConnectionId = item.ConnectionId;
                userView.User_Id = item.User_Id;
                userView.UserName = db.Users.Find(item.User_Id).UserName;
                userView.IsOnline = (bool)item.IsOnline;
                userView.LastMsg = messageDb.GetLastMessageByUserId(item.User_Id).Msg;
                listUser.Add(userView);
            }
            return View(listUser);
        }
    }
}