using DauGiaTrucTuyen.Areas.Admin.Models.ManagerUserViewModel;
using DauGiaTrucTuyen.Models;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/User
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        public ActionResult Update()
        {
            return View();
        }
        public ActionResult Detail(string id)
        {
            var item = db.Users.Find(id);

            if (item != null)
            {
                var model = Mapper.Map<DetailUserViewModel>(item);
                return View(model);
            }
            return View("Error");
        }
        public ActionResult Delete()
        {
            return View();
        }
    }
}