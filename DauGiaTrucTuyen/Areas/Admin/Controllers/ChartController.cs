using DauGiaTrucTuyen.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chart()
        {
            decimal Jan = 0;
            decimal Feb = 4;
            decimal Mar = 3;
            decimal Apr = 8;
            decimal May = 10;
            decimal Jun = 5;
            decimal Jul = 3;
            decimal Aug = 7;
            decimal Sept = 7;
            decimal Oct = 11;
            decimal Nov = 9;
            decimal Dec = 1;
            new Chart(width: 1300, height: 800, theme: ChartTheme.Blue)
                .AddSeries(
                chartType: "column",
                xValue: new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" },
                yValues: new[] { Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sept, Oct, Nov, Dec })
                .AddTitle("Số lượng sản phẩm trong năm")
                //.AddSeries("Defaulf", chartType: "Column", xValue: xValue, yValues: yValue)
                .Write("bmp");
            return null;
        }
    }
}