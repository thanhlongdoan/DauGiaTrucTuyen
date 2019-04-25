using DauGiaTrucTuyen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Hosting;

namespace DauGiaTrucTuyen.DataBinding
{
    public class ChaterService
    {
        public static List<Users_Chat> listUser = new List<Users_Chat>();
        public Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        public void AddUser(string userId, string id)
        {
            Users_Chat user = new Users_Chat
            {
                UserChat_Id = Guid.NewGuid().ToString(),
                ConnectionId = id,
                User_Id = userId,
                IsOnline = true,
                DateOnline = DateTime.Now
            };
            listUser.Add(user);
            if (listUser.Count() == 1)
                HostingEnvironment.QueueBackgroundWorkItem(ct => AddListUserIntoDb());

        }

        public void AddListUserIntoDb()
        {
            Thread.Sleep(10000);
            List<Users_Chat> list = listUser;
            listUser = new List<Users_Chat>();

            foreach (var item in list)
            {
                db.Users_Chat.Add(item);
                db.SaveChanges();
            }
        }

        public List<Users_Chat> GetAllUser()
        {
            List<Users_Chat> users = new List<Users_Chat>();
            var listFromList = listUser.OrderByDescending(x => x.DateOnline).ToList();
            if (listFromList.Count() > 0)
                foreach (var item in listFromList)
                {
                    users.Add(item);
                }
            var lista = db.Users_Chat.ToList();
            var listFromDb = db.Users_Chat.ToList().OrderByDescending(x => x.IsOnline).ThenByDescending(x => x.DateOnline).ToList();
            if (listFromDb.Count() > 0)
                foreach (var item in listFromDb)
                {
                    users.Add(item);
                }
            return users;
        }

        public Users_Chat GetUser(string userId)
        {
            Users_Chat user;
            user = listUser.FirstOrDefault(x => x.User_Id == userId);
            if (user == null)
                user = db.Users_Chat.FirstOrDefault(x => x.User_Id == userId);
            return user;
        }

        public void UpdateIsOnlineOfUser(string userId, bool isOnline)
        {
            Users_Chat user;
            user = listUser.FirstOrDefault(x => x.User_Id == userId);
            if (user != null)
            {
                user.IsOnline = isOnline;
                user.DateOnline = isOnline == true ? DateTime.Now : user.DateOnline;
            }
            else
            {
                user = db.Users_Chat.FirstOrDefault(x => x.User_Id == userId);
                if (user != null)
                {
                    user.IsOnline = isOnline;
                    user.DateOnline = isOnline == true ? DateTime.Now : user.DateOnline;
                    db.SaveChanges();
                }
            }

        }

        public void UpdateConnectionId(string userId, string id)
        {
            Users_Chat user;
            user = listUser.FirstOrDefault(x => x.User_Id == userId);
            if (user != null)
            {
                user.ConnectionId = id;
            }
            else
            {
                user = db.Users_Chat.FirstOrDefault(x => x.User_Id == userId);
                if (user != null)
                {
                    user.ConnectionId = id;
                    db.SaveChanges();
                }
            }
        }

        public Users_Chat GetUserByConnectionId(string id)
        {
            Users_Chat user;
            user = listUser.FirstOrDefault(x => x.ConnectionId == id);
            if (user == null)
                user = db.Users_Chat.FirstOrDefault(x => x.ConnectionId == id);
            return user;
        }
    }
}