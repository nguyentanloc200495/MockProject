using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.DataBase;
using MockProject.Models;
using MockProject.Services;

namespace MockProject.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult Index(SanphamSearchModel searchModel)
        {
            ViewBag.LoaiSanphamID = new SelectList(SanphamService.GetAllLoaisanpham(), "Id", "TenDanhmuc");
            return View(searchModel);
        }
        public ActionResult List(SanphamSearchModel searchModel)
        {
            var pagedList = SanphamService.Search(searchModel.ID, searchModel.TenSanpham, searchModel.Giaban, searchModel.LoaiSanphamID, searchModel.PageIndex);
         
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
        public ActionResult Create()
        {
            ViewBag.LoaiSanphamID = new SelectList(SanphamService.GetAllLoaisanpham(), "Id", "TenDanhmuc");
            //TODO        
            return View("CreateEdit");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.LoaiSanphamID = new SelectList(SanphamService.GetAllLoaisanpham(), "Id", "TenDanhmuc");
            var model = SanphamService.GetById(id);
            //TODO        
            return View("CreateEdit", model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.LoaiSanphamID = new SelectList(SanphamService.GetAllLoaisanpham(), "Id", "TenDanhmuc");
            var model = SanphamService.GetById(id);
            //TODO        
            return PartialView("_Delete", model);
        }
        [HttpPost]
        public ActionResult Delete2(SANPHAM model)
        {
            ViewBag.LoaiSanphamID = new SelectList(SanphamService.GetAllLoaisanpham(), "Id", "TenDanhmuc");
            var result = SanphamService.Delete(model);
            return
                Json(
               new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index" ) },
               JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
        public ActionResult CreateEdit(SANPHAM model)
        {
            if (model.Id == 0)
            {

                var result = SanphamService.Create(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.Id }) },
                JsonRequestBehavior.AllowGet);
            }
            else
            {

                var result = SanphamService.Edit(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.Id }) },
                JsonRequestBehavior.AllowGet);
            }
        }

    }
}