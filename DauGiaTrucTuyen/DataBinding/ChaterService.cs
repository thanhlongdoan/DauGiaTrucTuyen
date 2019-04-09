using DauGiaTrucTuyen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace DauGiaTrucTuyen.DataBinding
{
    public class ChaterService
    {
        public static List<UserChat> listUser = new List<UserChat>();
        public Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        public void AddUser(string email, string id)
        {
            UserChat user = new UserChat
            {
                UserChat_Id = Guid.NewGuid().ToString(),
                ConnectionId = id,
                Email = email.ToLower(),
                IsOnline = true,
                DateOnline = DateTime.Now
            };
            listUser.Add(user);
            if (listUser.Count() == 1)
                HostingEnvironment.QueueBackgroundWorkItem(ct => AddListUserIntoDb());

        }

        public void AddListUserIntoDb()
        {
            Thread.Sleep(30000);
            List<UserChat> list = listUser;
            listUser = new List<UserChat>();

            foreach (var item in list)
            {
                db.UserChats.Add(item);
                db.SaveChanges();
            }
        }

        public List<UserChat> GetAllUser()
        {
            List<UserChat> users = new List<UserChat>();
            var listFromList = listUser.OrderByDescending(x => x.DateOnline).ToList();
            if (listFromList.Count() > 0)
                foreach (var item in listFromList)
                {
                    users.Add(item);
                }
            var lista = db.UserChats.ToList();
            var listFromDb = db.UserChats.ToList().OrderByDescending(x => x.IsOnline).ThenByDescending(x => x.DateOnline).ToList();
            if (listFromDb.Count() > 0)
                foreach (var item in listFromDb)
                {
                    users.Add(item);
                }
            return users;
        }

        public UserChat GetUser(string email)
        {
            UserChat user;
            user = listUser.FirstOrDefault(x => x.Email == email.ToLower());
            if (user == null)
                user = db.UserChats.FirstOrDefault(x => x.Email == email.ToLower());
            return user;
        }

        public void UpdateIsOnlineOfUser(string email, bool isOnline)
        {
            UserChat user;
            user = listUser.FirstOrDefault(x => x.Email == email.ToLower());
            if (user != null)
            {
                user.IsOnline = isOnline;
                user.DateOnline = isOnline == true ? DateTime.Now : user.DateOnline;
            }
            else
            {
                user = db.UserChats.FirstOrDefault(x => x.Email == email.ToLower());
                if (user != null)
                {
                    user.IsOnline = isOnline;
                    user.DateOnline = isOnline == true ? DateTime.Now : user.DateOnline;
                    db.SaveChanges();
                }
            }

        }

        public void UpdateConnectionId(string email, string id)
        {
            UserChat user;
            user = listUser.FirstOrDefault(x => x.Email == email.ToLower());
            if (user != null)
            {
                user.ConnectionId = id;
            }
            else
            {
                user = db.UserChats.FirstOrDefault(x => x.Email == email.ToLower());
                if (user != null)
                {
                    user.ConnectionId = id;
                    db.SaveChanges();
                }
            }
        }

        public UserChat GetUserByConnectionId(string id)
        {
            UserChat user;
            user = listUser.FirstOrDefault(x => x.ConnectionId == id);
            if (user == null)
                user = db.UserChats.FirstOrDefault(x => x.ConnectionId == id);
            return user;
        }
    }
}