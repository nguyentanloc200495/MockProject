using CommonData;
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
    [Authorize]
    public class KhoController : Controller
    {
        // GET: Kho
      
        public ActionResult Index()
        {
            ViewBag.TinhTrang = WebUtil.GetEnumSelectList<TinhTrangKho>();
            return View();
        }
        public ActionResult List(KhoSearchModel searchModel)
        {
            var pagedList = KhoService.Search(searchModel.TenKho,searchModel.TinhTrang, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            //TODO     
            ViewBag.TinhTrang = WebUtil.GetEnumSelectList<TinhTrangKho>();
            return View("CreateEdit");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.TinhTrang = WebUtil.GetEnumSelectList<TinhTrangKho>();
            var model = KhoService.GetById(id);
            //TODO        
            return View("CreateEdit", model);
        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
        public ActionResult CreateEdit(KHO model)
        {
            if (model.ID == 0)
            {

                var result = KhoService.Create(model);
                return

                Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
            else
            {

                var result = KhoService.Edit(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
        }

        
    }
}

