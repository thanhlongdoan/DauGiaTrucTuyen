using AutoMapper;
using DauGiaTrucTuyen.Areas.Admin.Models;
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
    public class UserService : IUser
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //Danh sách người dùng
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
            return list.OrderByDescending(x => x.CreateDate).ToList();
        }

        //Chi tiết người dùng
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

        //Xóa người dùng
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
    }
}