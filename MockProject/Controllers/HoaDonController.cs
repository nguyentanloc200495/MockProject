﻿using MockProject.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.Models;
using MockProject.Services;

namespace MockProject.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: HoaDon
        public ActionResult Index()
        {
            ViewBag.CreateBy = new SelectList(HoaDonService.GetNhanvien(),"ID", "FullName");
            return View();
        }
        public ActionResult List(HoaDonSearchModel searchModel)
        {
            var pagedList = HoaDonService.Search(searchModel.ID, searchModel.CreateBy, searchModel.TuNgay,searchModel.DenNgay, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        [HttpGet]
        public ActionResult Create1()
        {
            //ViewBag.CreateBy = new SelectList(HoaDonService.GetNhanvien(), "ID", "FullName");
            return View("CreateEdit1");
        }

        [HttpGet]
        public ActionResult Edit1(int id)
        {
           
            var model = HoaDonService.GetById(id);
            ViewBag.CreateBy = new SelectList(HoaDonService.GetNhanvien(), "ID", "FullName",model.CreateBy);
            //TODO        
            return View("CreateEdit1", model);
        }


        [ChildActionOnly]
        public ActionResult GetViewListProduct(int id)
        {
            var model = HoaDonService.GetListCTHD(id);
            return PartialView("_ListCTHD", model);
        }


        public JsonResult GetSanPham(string name)
        {
            var data = HoaDonService.GetSanPham(name);
            return Json(data.Select(x => new { Id = x.ID, Name = x.ProductName }).ToList(), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult CreateEdit1(BILL model)
        {
            if (Request["txtProductIdRow"] == null)
            {
                return Json(new CommandResult() { Code = ResultCode.Fail, Message = "Xin vui lòng thêm sản phẩm " }, JsonRequestBehavior.AllowGet);
            }
            var listSanPhamID = Request.Form.GetValues("txtProductIdRow");
            var listSoLuong = Request.Form.GetValues("txtQuantityRow");
            var listGia = Request.Form.GetValues("txtPriceRow");
            if (model.ID == 0)
            {
                var user = UserService.GetUserInfo();
                var result = HoaDonService.Create1(model, user.ID, listSanPhamID, listSoLuong, listGia);
                return
                Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
            else
            {
                var user = UserService.GetUserInfo();
                var result = HoaDonService.Edit1(model, user.ID, listSanPhamID, listSoLuong, listGia);
                return
                Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);

            }
           
        }
    }
}