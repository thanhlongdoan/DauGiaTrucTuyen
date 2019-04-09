using DauGiaTrucTuyen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace DauGiaTrucTuyen.DataBinding
{
    public class MessageService
    {
        public Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        public static List<MessageChat> listMessages = new List<MessageChat>();
        public ChaterService chater = new ChaterService();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromEmail"></param>
        /// <param name="toEmail"></param>
        /// <param name="msg"></param>
        /// <param name="id"></param>
        /// <param name="createDate"></param>
        public void AddMessage(string fromEmail, string toEmail, string msg, string id, DateTime createDate)
        {
            var item = new MessageChat
            {
                MessageChat_Id = Guid.NewGuid().ToString(),
                FromConnectionId = id,
                FromEmail = fromEmail.ToLower(),
                ToEmail = toEmail.ToLower(),
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
        public string GetMessagesByEmail(string email)
        {
            List<MessageChat> listMsg = new List<MessageChat>();
            var user = chater.GetUser(email);
            if (user != null)
            {
                var Msgs = db.MessageChats.ToList().Where(x => x.FromEmail == email || x.ToEmail == email).OrderBy(x => x.DateSend);
                if (Msgs.Count() > 0)
                {
                    foreach (var message in Msgs)
                    {
                        listMsg.Add(message);
                    }
                }

                var messages = listMessages.ToList().Where(x => x.FromEmail == email || x.ToEmail == email).OrderBy(x => x.DateSend);
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
        public MessageChat GetLastMessageByEmail(string email)
        {
            MessageChat message = new MessageChat();
            var messages = listMessages.ToList().Where(x => x.FromEmail == email || x.ToEmail == email).OrderBy(x => x.DateSend);
            if (messages.Count() > 0)
            {
                message = messages.LastOrDefault();
            }
            else
            {
                var msgs = db.MessageChats.ToList().Where(x => x.FromEmail == email || x.ToEmail == email).OrderBy(x => x.DateSend);
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
            Thread.Sleep(30000);
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
            var messages = db.MessageChats.ToList().Where(x => x.FromEmail == email || x.ToEmail == email);
            foreach (var item in messages)
            {
                item.FromConnectionId = id;
            }
            db.SaveChanges();
        }

        public void UpdateIsReadMessage(string email, bool adRead, DateTime date)
        {
            //update in database
            IEnumerable<MessageChat> messages;
            if (adRead == true)
                messages = db.MessageChats.ToList().Where(x => x.FromEmail == email && x.IsRead == false);
            else
                messages = db.MessageChats.ToList().Where(x => x.ToEmail == email && x.IsRead == false);
            foreach (var item in messages)
            {
                item.IsRead = true;
                item.DateRead = date;
            }
            db.SaveChanges();
            //update in list messages
            if (adRead == true)
                messages = listMessages.ToList().Where(x => x.FromEmail == email && x.IsRead == false);
            else
                messages = listMessages.ToList().Where(x => x.ToEmail == email && x.IsRead == false);
            foreach (var item in messages)
            {
                item.IsRead = true;
                item.DateRead = date;
            }

        }
    }
}