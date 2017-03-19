using MockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.DataBase;
using MockProject.Services;

namespace MockProject.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: LoaiSanPham
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

        public ActionResult Create()
        {
            //TODO        
            return View("CreateEdit");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = LoaiSanPham.GetById(id);
            //TODO        
            return View("CreateEdit", model);
        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
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

