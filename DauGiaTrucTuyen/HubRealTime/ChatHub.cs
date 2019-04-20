using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using Microsoft.AspNet.SignalR;

namespace DauGiaTrucTuyen.HubRealTime
{
    public class ChatHub : Hub
    {
        public Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        public static List<UserChat> listUser = new List<UserChat>();
        MessageService messageDb = new MessageService();
        ChaterService chater = new ChaterService();
        UserService userService = new UserService();
        string emailAdmin = WebConfigurationManager.AppSettings["EmailAdmin"];

        public void Connect(string userId)
        {
            if (userId != null)
            {
                var username = HttpContext.Current.User.Identity.Name;
                bool checkExist;
                /// id = lấy ra chuỗi kết nối hiện tại của trình duyệt
                var id = Context.ConnectionId;
                /// lấy ra tài khoản 
                var item = chater.GetUser(userId);
                /// chưa có tài khoản , tạo mới
                if (item == null)
                {
                    listUser.Add(new UserChat
                    {
                        UserChat_Id = Guid.NewGuid().ToString(),
                        ConnectionId = id,
                        User_Id = userId,
                        IsOnline = true
                    });
                    chater.AddUser(userId, id);
                    checkExist = false;
                    Clients.User(emailAdmin).onConnected(id, username.ToLower(), checkExist);
                    Clients.Client(Context.ConnectionId).SendConnection(id, userId.ToLower());

                }
                /// đã có tài khoản
                else
                {
                    /////nếu đang đăng nhập vào trình duyệt cũ , bật một tab mới
                    //if (item.ConnectionId != id && item.IsOnline == true)
                    //{
                    //    Clients.Caller.CheckIsOnline();
                    //}
                    //else
                    /////có tài khoản email rồi và đang offline
                    //{
                    chater.UpdateConnectionId(userId, id);
                    chater.UpdateIsOnlineOfUser(userId, true);
                    //user.ConnectionId = id;
                    //user.IsOnline = true;
                    //var ModelMsg = db.messages.ToList().Where(x => x.FromEmail == email.ToLower());
                    //foreach (var itemMsg in ModelMsg)
                    //itemMsg.FromConnectionId = id;
                    messageDb.UpdateFromConnectionId(userId, id);
                    checkExist = true;
                    //db.SaveChanges();
                    Clients.User(emailAdmin).onConnected(id, userId, checkExist);
                    Clients.Client(Context.ConnectionId).SendConnection(id, userId);
                    //}
                }
            }
        }

        public void ChangeTab(string userId)
        {
            var id = Context.ConnectionId;
            var item = chater.GetUser(userId);
            //var item = db.account.FirstOrDefault(x => x.Email == email.ToLower());
            //item.ConnectionId = id;
            chater.UpdateConnectionId(userId, id);
            //db.SaveChanges();
            Clients.User(emailAdmin).onConnected(item.ConnectionId, userId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromEmail">tu 1 email nguoi dung nhap vao</param>
        /// <param name="toEmail">gui den email cua admin</param>
        /// <param name="msg">tin nhan nguoi dung nhap vao</param>
        public void SendMsg(string fromUserId, string toUserId, string msg)
        {
            var id = Context.ConnectionId;
            var item = chater.GetUser(fromUserId);
            //var item = listUser.FirstOrDefault(x => x.Email == fromEmail);
            //var item = db.account.FirstOrDefault(x => x.Email == fromEmail);
            //kiem tra id ket noi hien tai co dung voi ConnectionId cua 1 email nhap vao khong
            if (id == item.ConnectionId)
            {
                MessageService messageDb = new MessageService();
                var createDate = DateTime.Now;
                messageDb.AddMessage(fromUserId, toUserId, msg, id, createDate);
                var user = userService.DetailUser(fromUserId);
                var connectionId = Context.ConnectionId;
                Clients.User("admin@gmail.com").SendMsgForAdmin(msg, createDate, connectionId, user.UserName);
            }
            //truong hoi Id khong dung voi ConnectionId thi tra ve result va 'thong bao ket noi bi ngat'
            else
            {
                Clients.Client(Context.ConnectionId).SendError();
            }
        }

        /// <summary>
        /// Admin gửi mail cho client
        /// lưu vào cơ sở dữ liệu và gửi đến cho client
        /// </summary>
        /// <param name="toEmail">email nhận tin nhắn</param>
        /// <param name="msg">nội dung tin nhắn</param>
        /// <param name="connectionId">connectionId nhận tin nhắn</param>
        public void SendPrivateMessage(string toUserId, string msg, string connectionId)
        {
            var createDate = DateTime.Now;
            var fromUserId = "admin@gmail.com";
            messageDb.AddMessage(fromUserId.ToLower(), toUserId, msg, connectionId, createDate);
            Clients.Client(connectionId).AdminSendMsg(msg);
        }
        /// <summary>
        /// 
        /// </summary>          
        /// <param name="email">email nguoi dung truyen vao</param>
        public void LoadMsgOfClient(string userId)
        {
            if (userId != null)
            {
                string listMsg = messageDb.GetMessagesByUserId(userId.ToLower());
                Clients.Caller.LoadAllMsgOfClient(listMsg);
            }
        }
        /// <summary>
        /// Load tất cả các danh sách tin nhắn của email truyền vào và gửi về cho admin
        /// </summary>
        /// <param name="email"></param>
        public void LoadMsgByEmailOfAdmin(string userId)
        {
            string listMsg = messageDb.GetMessagesByUserId(userId);
            Clients.User(emailAdmin).loadAllMsgByEmailOfAdmin(listMsg);
        }

        public void UpdateIsReadMessage(string connectionId, string email, bool adRead)
        {
            var date = DateTime.Now;
            messageDb.UpdateIsReadMessage(email, adRead, date);
            if (adRead == true)
                Clients.Client(connectionId).AdminReaded(date);
            else
                Clients.User(emailAdmin).ClientReaded(date);
        }
        /// <summary>
        /// khi co su thay doi ConnectionID cua trinh duyet thi kiem tra
        /// Neu dung thi gan IsOnline == false de xu ly ben giao dien
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    //var item = db.account.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
        //    //var item = listUser.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
        //    var item = chater.GetUserByConnectionId(Context.ConnectionId);
        //    if (item != null)
        //    {
        //        Clients.User(emailAdmin).OnUserDisconnected(item.Email.ToLower());
        //        //item.IsOnline = false;
        //        chater.UpdateIsOnlineOfUser(item.Email, false);
        //    }
        //    return base.OnDisconnected(stopCalled);
        //}
    }
}