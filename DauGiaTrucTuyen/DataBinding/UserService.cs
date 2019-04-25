using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
using DauGiaTrucTuyen.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using static DauGiaTrucTuyen.Areas.Admin.Models.ManagerUserViewModel;

namespace DauGiaTrucTuyen.DataBinding
{
    public class UserService : IUser
    {
        ApplicationDbContext db = new ApplicationDbContext();
        Db_DauGiaTrucTuyen context = new Db_DauGiaTrucTuyen();

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
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                var statusUser = context.StatusUsers.FirstOrDefault(x => x.User_Id == user.Id);
                return new DetailUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    CreateDate = user.CreateDate,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    BlockAuctionStatus = statusUser == null ? null : statusUser.BlockAuctionStatus,
                    BlockAuctionDate = statusUser == null ? null : statusUser.BlockAuctionDate,
                    BlockUserStatus = statusUser == null ? null : statusUser.BlockUserStatus,
                    BlockUserDate = statusUser == null ? null : statusUser.BlockUserDate
                };
            }
            return null;
        }

        //xữ lý khóa tài khoản
        public bool HandleUser(HandleUserViewModel model)
        {
            var statusUser = context.StatusUsers.FirstOrDefault(x => x.StatusUsers_Id == model.StatusUsers_Id);
            if (model.BlockAuctionStatus == "Close")
            {
                statusUser.BlockAuctionStatus = StatusBlockAuction.Close;
                statusUser.BlockAuctionDate = DateTime.Now;
            }
            else
            {
                statusUser.BlockAuctionDate = null;
                statusUser.BlockAuctionStatus = StatusBlockAuction.Open;
            }

            if (model.BlockUserStatus == "Close")
            {
                statusUser.BlockUserStatus = StatusBlockUser.Close;
                statusUser.BlockUserDate = DateTime.Now;
            }
            else
            {
                statusUser.BlockUserDate = null;
                statusUser.BlockUserStatus = StatusBlockUser.Open;
            }

            context.Entry(statusUser).State = EntityState.Modified;
            context.SaveChanges();
            return true;
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