using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    public class ChatboxController : Controller
    {
        public Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        // GET: Admin/Chatbox
        public ActionResult Index()
        {
            ChaterService chater = new ChaterService();
            List<UserChat> list = chater.GetAllUser();
            List<ChatboxViewModel> listUser = new List<ChatboxViewModel>();
            MessageService messageDb = new MessageService();
            foreach (var item in list)
            {
                ChatboxViewModel userView = new ChatboxViewModel();
                userView.ConnectionId = item.ConnectionId;
                userView.Email = item.Email;
                userView.IsOnline = (bool)item.IsOnline;
                userView.LastMsg = messageDb.GetLastMessageByEmail(item.Email).Msg;
                listUser.Add(userView);
            }
            return View(listUser);
        }
    }
}