using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.Models;
using System.Collections.Generic;
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
            List<Users_Chat> list = chater.GetAllUser();
            List<ChatboxViewModel> listUser = new List<ChatboxViewModel>();
            MessageService messageDb = new MessageService();
            foreach (var item in list)
            {
                var test = db.Users.Find(item.User_Id);

                ChatboxViewModel userView = new ChatboxViewModel();
                userView.ConnectionId = item.ConnectionId;
                userView.User_Id = item.User_Id;
                userView.UserName = db.Users.Find(item.User_Id) == null ? "Không rõ" : db.Users.Find(item.User_Id).UserName;
                userView.IsOnline = (bool)item.IsOnline;
                userView.LastMsg = messageDb.GetLastMessageByUserId(item.User_Id).Message;
                listUser.Add(userView);
            }
            return View(listUser);
        }
    }
}