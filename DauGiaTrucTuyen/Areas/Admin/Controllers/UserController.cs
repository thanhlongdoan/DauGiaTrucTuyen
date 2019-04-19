using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;
using DauGiaTrucTuyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static DauGiaTrucTuyen.Areas.Admin.Models.ManagerUserViewModel;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        Db_DauGiaTrucTuyen context = new Db_DauGiaTrucTuyen();
        private readonly IUser _iUser;
        public UserController() : this(new UserService())
        {
        }
        public UserController(IUser iUser)
        {
            _iUser = iUser;
        }

        //Danh sách người dùng
        public ActionResult Index()
        {
            return View(_iUser.GetListUser());
        }


        //Chi tiết người dùng
        public ActionResult Detail(string id)
        {
            var result = _iUser.DetailUser(id);
            if (result != null)
                return View(result);
            return HttpNotFound();
        }

        //Xóa người dùng
        public bool Delete(string id)
        {
            if (_iUser.DeleteUser(id))
                return true;
            return false;
        }

        public ActionResult Handle(string id)
        {
            var statusUser = context.StatusUsers.FirstOrDefault(x => x.User_Id == id);
            HandleUserViewModel viewModel = new HandleUserViewModel();
            if (statusUser == null)
            {
                var model = new StatusUser
                {
                    StatusUsers_Id = Guid.NewGuid().ToString(),
                    BlockAuctionStatus = StatusBlockAuction.Open,
                    BlockAuctionDate = null,
                    BlockUserStatus = StatusBlockUser.Open,
                    BlockUserDate = null,
                    User_Id = id
                };
                context.StatusUsers.Add(model);
                context.SaveChanges();
                viewModel = new HandleUserViewModel
                {
                    StatusUsers_Id = model.StatusUsers_Id,
                    BlockAuctionStatus = model.BlockAuctionStatus,
                    BlockUserStatus = model.BlockUserStatus,
                };
            }
            else
            {
                viewModel = new HandleUserViewModel
                {
                    StatusUsers_Id = statusUser.StatusUsers_Id,
                    BlockAuctionStatus = statusUser.BlockAuctionStatus,
                    BlockUserStatus = statusUser.BlockUserStatus
                };
            }

            List<SelectListItem> itemsAuction = new List<SelectListItem>();
            itemsAuction.Add(new SelectListItem { Text = "Không khóa", Value = "Open" });
            itemsAuction.Add(new SelectListItem { Text = "Khóa", Value = "Close" });
            ViewBag.SelectedItemsAuction = itemsAuction;
            ViewBag.SelectedDefaultAuction = "Khóa";/* viewModel.BlockAuctionStatus;*/

            List<SelectListItem> itemsUser = new List<SelectListItem>();
            itemsUser.Add(new SelectListItem { Text = "Không khóa", Value = "Open" });
            itemsUser.Add(new SelectListItem { Text = "Khóa", Value = "Close" });
            ViewBag.SelectedItemsUser = itemsUser;
            ViewBag.SelectedDefaultUser = viewModel.BlockUserStatus;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Handle(HandleUserViewModel model)
        {
            if (_iUser.HandleUser(model))
                return RedirectToAction("Index");
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}