using MockProject.DataBase;
using MockProject.Models;
using MockProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockProject.Controllers
{
    public class CTKhoController : Controller
    {
        // GET: CTKho
        public ActionResult Index()
        {
            ViewBag.KhoID = new SelectList(NhapXuatKhoService.GetAllKho(), "ID", "WarehouseName");
            ViewBag.SanPhamID = new SelectList(NhapXuatKhoService.GetSanPham(), "ID", "ProductName");
            return View();
        }
        public ActionResult List(CTKhoSearchModel searchModel)
        {
            var pagedList = KhoService.SearchCTKho(searchModel.KhoID, searchModel.SanPhamID, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
    }
}