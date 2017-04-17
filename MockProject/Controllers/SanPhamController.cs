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
            ViewBag.ProductType_ID = new SelectList(SanphamService.GetAllLoaisanpham(), "ID", "ProductTypeName");
            return View(searchModel);
        }
        public ActionResult List(SanphamSearchModel searchModel)
        {
            var pagedList = SanphamService.Search(searchModel.ID, searchModel.ProductName, searchModel.Amount, searchModel.ProductType_ID, searchModel.PageIndex);
         
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
        public ActionResult Create()
        {
            ViewBag.ProductType_ID = new SelectList(SanphamService.GetAllLoaisanpham(), "ID", "ProductTypeName");
            //TODO        
            return View("CreateEdit");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.ProductType_ID = new SelectList(SanphamService.GetAllLoaisanpham(), "ID", "ProductTypeName");
            var model = SanphamService.GetById(id);
            //TODO        
            return View("CreateEdit", model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.ProductType_ID = new SelectList(SanphamService.GetAllLoaisanpham(), "ID", "ProductTypeName");
            var model = SanphamService.GetById(id);
                  
            return PartialView("_Delete", model);
        }
        [HttpPost]
        public ActionResult Delete2(PRODUCT model)
        {
            //ViewBag.ProductType_ID = new SelectList(SanphamService.GetAllLoaisanpham(), "Id", "TenLoaiSanPham");
            var result = SanphamService.Delete(model);
            return
                Json(
               new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index" ) },
               JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
        public ActionResult CreateEdit(PRODUCT model)
        {
            if (model.ID == 0)
            {

                var result = SanphamService.Create(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
            else
            {

                var result = SanphamService.Edit(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
        }

    }
}