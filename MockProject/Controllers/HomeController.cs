using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["SubTitle"] = "Welcome to Hutech_Net1_SIMS Website";
            ViewData["Message"] = "Trang web quản lý cửa hàng bách hóa";

            return View();
        }


    }
}