using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChartController : Controller
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult JSON()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint(1546300800000, 4.67));
            dataPoints.Add(new DataPoint(1548979200000, 4.7));
            dataPoints.Add(new DataPoint(1551398400000, 4.96));
            dataPoints.Add(new DataPoint(1554076800000, 5.12));
            dataPoints.Add(new DataPoint(1556668800000, 5.08));
            dataPoints.Add(new DataPoint(1559347200000, 5.11));
            dataPoints.Add(new DataPoint(1561939200000, 5));
            dataPoints.Add(new DataPoint(1564617600000, 5.2));
            dataPoints.Add(new DataPoint(1567296000000, 4.7));
            dataPoints.Add(new DataPoint(1569888000000, 4.74));
            dataPoints.Add(new DataPoint(1572566400000, 4.67));
            dataPoints.Add(new DataPoint(1575158400000, 4.67));


            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }

        public ActionResult Chart()
        {
            var product = db.Products.Where(x => x.CreateDate.Value.Year == 2019).ToList();
            foreach (var item in product)
            {

            }
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