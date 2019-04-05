using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Controllers
{
    [Authorize]
    public class MyAuctionController : Controller
    {
        // GET: MyAuction
        public ActionResult Index()
        {
            return View();
        }
    }
}