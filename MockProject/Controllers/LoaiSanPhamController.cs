using MockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.DataBase;
using MockProject.Services;
using CommonData;
using MockProject.Common;

namespace MockProject.Controllers
{
    [AuthorizeAdmin(Permission = Permission.NhanVien_Xem)]
    public class LoaiSanPhamController : Controller
    {
        // GET: LoaiSanPham
        [AuthorizeAdmin(Permission = Permission.LoaiSanPham_Xem)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(LoaiSanPhamSearchModel searchModel)
        {
            var pagedList = LoaiSanPham.Search(searchModel.Tendanhmuc, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            //TODO     
            ViewBag.TrangThai = WebUtil.GetEnumSelectList<TrangThaiLoaiSanPham>();
            return View("CreateEdit");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.TrangThai = WebUtil.GetEnumSelectList<TrangThaiLoaiSanPham>();
            var model = LoaiSanPham.GetById(id);
            //TODO        
            return View("CreateEdit", model);
        }
        [HttpPost]
        [AuthorizeAdmin(Permission = Permission.LoaiSanPham_ThemSua)]
        public ActionResult CreateEdit(LOAISANPHAM model)
        {
            if (model.ID == 0)
            {

                var result = LoaiSanPham.Create(model);
                return

                Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
            else
            {

                var result = LoaiSanPham.Edit(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
        }

    }
}

