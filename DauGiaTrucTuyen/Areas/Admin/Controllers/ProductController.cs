using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using System.Web;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        private DataBinding.Product _iProduct = new DataBinding.Product();
        // GET: Admin/Product
        /// <summary>
        /// get danh sách sản phẩm đang đấu giá
        /// </summary>
        /// <param name="status">status ==2 </param>
        /// <returns></returns>
        public ActionResult Index(int status)
        {
            return View(_iProduct.GetListProduct(status));
        }
        /// <summary>
        /// get danh sách sản phẩm đang chờ phê duyệt
        /// </summary>
        /// <param name="status">status ==1 </param>
        /// <returns></returns>
        public ActionResult GetListProductStatus1(int status)
        {
            return View(_iProduct.GetListProduct(status));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.Categorys, "Categorys_Id", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(AddProductViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (_iProduct.Create(model, file))
                    return RedirectToAction("Index", new { status = 2 });
                return HttpNotFound();
            }
            else
            {
                ViewBag.Category_Id = new SelectList(db.Categorys, "Categorys_Id", "CategoryName");
                return View(model);
            }
        }
    }
}