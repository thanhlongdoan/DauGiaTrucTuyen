using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;
using DauGiaTrucTuyen.Models;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
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