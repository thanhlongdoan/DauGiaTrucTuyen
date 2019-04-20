using DauGiaTrucTuyen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.IdentityModel;
using Microsoft.AspNet.Identity;

namespace DauGiaTrucTuyen.DataBinding
{
    public class MessageService
    {
        public Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        public static List<MessageChat> listMessages = new List<MessageChat>();
        public ChaterService chater = new ChaterService();

        public void AddMessage(string fromUserId, string toUserId, string msg, string id, DateTime createDate)
        {
            var item = new MessageChat
            {
                MessageChat_Id = Guid.NewGuid().ToString(),
                FromConnectionId = id,
                FromUser_Id = fromUserId.ToLower(),
                ToUser_Id = toUserId.ToLower(),
                Msg = msg,
                DateSend = createDate,
                IsRead = false
            };
            listMessages.Add(item);
            if (listMessages.Count() == 1)
                HostingEnvironment.QueueBackgroundWorkItem(ct => AddListMessageIntoDb());
        }
        /// <summary>
        /// tao 1 list de chua cac msg co FromEmail hay ToEmail == email truyen vao do
        /// </summary>
        /// <param name="email">email truyen vao</param>
        /// <returns>list tin nhan da tra ve kieu JSON</returns>
        public string GetMessagesByUserId(string userId)
        {
            List<MessageChat> listMsg = new List<MessageChat>();
            var user = chater.GetUser(userId);
            if (user != null)
            {
                var Msgs = db.MessageChats.Where(x => x.FromUser_Id.Equals(userId) || x.ToUser_Id.Equals(userId)).ToList().OrderBy(x => x.DateSend);
                if (Msgs.Count() > 0)
                {
                    foreach (var message in Msgs)
                    {
                        listMsg.Add(message);
                    }
                }

                var messages = listMessages.Where(x => x.FromUser_Id.Equals(userId) || x.ToUser_Id.Equals(userId)).ToList().OrderBy(x => x.DateSend);
                if (messages.Count() > 0)
                {
                    foreach (var message in messages)
                    {
                        listMsg.Add(message);
                    }
                }
            }

            return new JavaScriptSerializer().Serialize(listMsg);
        }

        /// <summary>
        /// lấy tin nhắn cuối cùng của email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public MessageChat GetLastMessageByUserId(string userId)
        {
            MessageChat message = new MessageChat();
            var messages = listMessages.Where(x => x.FromUser_Id.Equals(userId)  || x.ToUser_Id.Equals(userId)).ToList().OrderBy(x => x.DateSend);
            if (messages.Count() > 0)
            {
                message = messages.LastOrDefault();
            }
            else
            {
                var msgs = db.MessageChats.Where(x => x.FromUser_Id.Equals(userId) || x.ToUser_Id.Equals(userId)).ToList().OrderBy(x => x.DateSend);
                if (msgs.Count() > 0)
                    message = msgs.LastOrDefault();
            }
            return message;
        }

        public string GetStringDateOfLastMessage(MessageChat msg)
        {
            string strDate = string.Empty;
            if (msg.MessageChat_Id != null)
            {
                var now = DateTime.Now;
                var date = (DateTime)msg.DateSend;
                int result = (int)(now.Date - date.Date).TotalDays;
                if (result < 7)
                {
                    switch (result)
                    {
                        case 0: strDate = date.Hour > 12 ? (date.Hour - 12) + ":" + date.Minute + " PM" : date.Hour + ":" + date.Minute + " AM"; break;
                        case 1: strDate = "Hôm qua"; break;
                        default:
                            strDate = (int)date.DayOfWeek == 7 ? "Chủ nhật" : "Thứ " + (int)date.DayOfWeek;
                            break;
                    }
                }
                else
                {
                    strDate = date.Day + "-" + date.Month + "-" + date.Year;
                }
            }

            return strDate;
        }
        public void AddListMessageIntoDb()
        {
            Thread.Sleep(1000);
            List<MessageChat> list = listMessages;
            listMessages = new List<MessageChat>();

            foreach (var item in list)
            {
                db.MessageChats.Add(item);
                db.SaveChanges();
            }
        }
        public void UpdateFromConnectionId(string email, string id)
        {
            var messages = db.MessageChats.Where(x => x.FromUser_Id == email || x.ToUser_Id == email).ToList();
            foreach (var item in messages)
            {
                item.FromConnectionId = id;
            }
            db.SaveChanges();
        }

        public void UpdateIsReadMessage(string email, bool adRead, DateTime date)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            //update in database
            IEnumerable<MessageChat> messages;
            if (adRead == true)
                messages = db.MessageChats.Where(x => x.FromUser_Id == userId && x.IsRead == false).ToList();
            else
                messages = db.MessageChats.Where(x => x.ToUser_Id == userId && x.IsRead == false).ToList();
            foreach (var item in messages)
            {
                item.IsRead = true;
                item.DateRead = date;
            }
            db.SaveChanges();
            //update in list messages
            if (adRead == true)
                messages = listMessages.Where(x => x.FromUser_Id == userId && x.IsRead == false).ToList();
            else
                messages = listMessages.Where(x => x.ToUser_Id == userId && x.IsRead == false).ToList();
            foreach (var item in messages)
            {
                item.IsRead = true;
                item.DateRead = date;
            }

        }
    }
}