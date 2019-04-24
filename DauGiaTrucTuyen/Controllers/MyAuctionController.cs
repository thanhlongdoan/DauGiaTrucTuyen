using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;
using Microsoft.AspNet.Identity;
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
        private readonly IMyAuction _iMyAuction;
        public MyAuctionController() : this(new MyAuctionService())
        {

        }

        public MyAuctionController(MyAuctionService myAuctionService)
        {
            _iMyAuction = myAuctionService;
        }

        public ActionResult Auctioning()
        {
            return View(_iMyAuction.ListAuctioning(User.Identity.GetUserId()));
        }

        public ActionResult AuctionWin()
        {
            return View(_iMyAuction.ListAuctionWin(User.Identity.GetUserId()));
        }

        public ActionResult AuctionLost()
        {
            return View(_iMyAuction.ListAuctionLost(User.Identity.GetUserId()));
        }

        public ActionResult FeedBack(string productId)
        {
            ViewBag.ProductId = productId;
            return View();
        }
    }
}