using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.Models;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private User _iUser = new User();
        public ActionResult Index()
        {
            return View(_iUser.GetListUser());
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
            var result = _iUser.DetailUser(id);
            if (result != null)
                return View(result);
            return HttpNotFound();
        }
        public bool Delete(string id)
        {
            if (_iUser.DeleteUser(id))
                return true;
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