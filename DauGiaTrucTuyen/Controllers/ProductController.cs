using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;

namespace DauGiaTrucTuyen.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        DataBinding.Product _iProduct = new DataBinding.Product();
        public ActionResult GetListProductForPageClient()
        {
            return PartialView(_iProduct.GetListProductForPageClient());
        }
    }
}