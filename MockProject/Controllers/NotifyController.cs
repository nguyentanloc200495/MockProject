using CommonData;
using MockProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.DataBase;
using MockProject.Services;
using Microsoft.AspNet.Identity;

namespace MockProject.Controllers
{
    [Authorize]
    public class NotifyController : Controller
    {
        // GET: Notify
        public ActionResult Index(NotifySearchModel searchModel)
        {
            ViewBag.Status = WebUtil.GetEnumSelectList<Notifi_Status>();
            ViewBag.Type = WebUtil.GetEnumSelectList<Notifi_Type>();
            return View(searchModel);
        }
        public ActionResult List(NotifySearchModel searchModel)
        {
         
            var pagedList = NotifyService.Search(searchModel.Title, searchModel.FromDateTimeCreated, searchModel.ToDateTimeCreated, searchModel.Type, searchModel.Status, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            ViewBag.Type = WebUtil.GetEnumSelectList<Notifi_Type>();
            ViewBag.UserCreate = new SelectList(HoaDonService.GetNhanvien(), "ID", "FullName");
            var model = NotifyService.GetById(id);
            //TODO        
            return View("Detail", model);
        }
        public ActionResult IndexUser(NotifySearchModel searchModel)
        {
           
            ViewBag.Type = WebUtil.GetEnumSelectList<Notifi_Type>();
            return View(searchModel);
        }
        public ActionResult ListUser(NotifySearchModel searchModel)
        {

            var pagedList = NotifyService.Search2(searchModel.Title, searchModel.FromDateTimeCreated, searchModel.ToDateTimeCreated, searchModel.Type, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_ListUser", pagedList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Type = WebUtil.GetEnumSelectList<Notifi_Type>();
            return View("Create");
        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
        public ActionResult Create(NOTIFY model)
        {
         
                var result = NotifyService.Create(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("IndexUser", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            
          
        }

        public ActionResult Notify(int? flag, int? ma)
        {
            if (flag.HasValue)
            {
                NotifyService.ChangeStatus();
            }
            if (ma.HasValue)
            {
                NotifyService.ChangeStatusNotifyId(ma);
            }
            var model = NotifyService.GetNotify();
            return PartialView("_Notify", model);
        }

        public ActionResult Theme()

        {
            return PartialView("_Theme");
        }
    }
}