using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace DauGiaTrucTuyen.Controllers
{
    public class HomeController : Controller
    {
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        private readonly IProduct _iProduct;

        public HomeController() : this(new ProductService())
        {
        }

        public HomeController(IProduct iProduct)
        {
            _iProduct = iProduct;
        }

        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult Index(int? page)
        {
            int pageSize = 24;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            return View(_iProduct.GetListProductForPageClient().ToPagedList(pageIndex, pageSize));
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ListAuctioning(int? page)
        {
            int pageSize = 24;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            return View(_iProduct.GetListProductForPageClientAuctionning().ToPagedList(pageIndex, pageSize));
        }
        public ActionResult SecurityView()
        {
            return View();
        }
    }
}