using AutoMapper;
using DauGiaTrucTuyen.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using static DauGiaTrucTuyen.Areas.Admin.Models.ManagerUserViewModel;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var user = db.Users.OrderByDescending(x => x.CreateDate).ToList();
            List<ListUserViewModel> list = new List<ListUserViewModel>();
            string emailAdmin = ConfigurationManager.AppSettings["EmailAdmin"];
            foreach (var item in user)
            {
                var model = Mapper.Map<ListUserViewModel>(item);
                //chỉ add vào model trừ user Admin
                if (item.Email != emailAdmin)
                    list.Add(model);
            }
            return View(list);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var user = db.Users.Find(id);
            if (user != null)
                return View();
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Detail(string id)
        {
            var user = db.Users.Find(id);

            if (user != null)
            {
                var model = Mapper.Map<DetailUserViewModel>(user);
                return View(model);
            }
            return HttpNotFound();
        }
        public bool Delete(string id)
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