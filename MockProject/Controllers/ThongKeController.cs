using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.Models;
using MockProject.Services;

namespace MockProject.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDataChartRevenue(ThongKeModel data) //flag  1: ngay, 2: thang, 3:nam
        {
            List<ChartThongKe> model = new List<ChartThongKe>();

            //ngay
            if (data.flag == 1)
            {
                model = ThongKeService.GetDataChartForDay(data.TuNgay, data.DenNgay);
            }

            //thang
            if (data.flag == 2)
            {
                model = ThongKeService.GetDataChartForMonth(data.TuNgay, data.DenNgay);
            }

            ////nam
            if (data.flag == 3)
            {
                model = ThongKeService.GetDataChartForYear(data.TuNgay, data.DenNgay);
            }

            ViewBag.TotalRevenue = ThongKeService.GetDataChartTotalThongKe(data.TuNgay, data.DenNgay);

            ViewBag.Flag = data.flag;
            return PartialView("_ChartThongKe", model);
        }
    }
}