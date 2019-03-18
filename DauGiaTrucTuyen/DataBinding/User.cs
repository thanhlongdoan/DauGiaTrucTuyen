using AutoMapper;
using DauGiaTrucTuyen.IDataBinding;
using DauGiaTrucTuyen.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static DauGiaTrucTuyen.Areas.Admin.Models.ManagerUserViewModel;

namespace DauGiaTrucTuyen.DataBinding
{
    public class User
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<ListUserViewModel> GetListUser()
        {
            string emailAdmin = ConfigurationManager.AppSettings["EmailAdmin"];
            var list = from user in db.Users
                       where user.Email != emailAdmin
                       select new ListUserViewModel
                       {
                           Id = user.Id,
                           UserName = user.UserName,
                           PhoneNumber = user.PhoneNumber,
                           CreateDate = user.CreateDate
                       };
            return list.OrderByDescending(x => x.CreateDate) .ToList();
        }
        public bool DeleteUser(string id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public DetailUserViewModel DetailUser(string id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                var model = Mapper.Map<DetailUserViewModel>(user);
                return model;
            }
            return null;
        }
    }
}