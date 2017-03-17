using MockProject.Models;
using MockProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.DataBase;

namespace MockProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        //[AuthorizeAdmin(Permission = Permission.Nhanvien_xem)]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(UserSearchModel searchModel)
        {
            var pagedList = UserSevice.Search(searchModel.Username, searchModel.Fullname, searchModel.PhoneNumber, searchModel.PageIndex);
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
            var model = UserSevice.GetById(id);
            //TODO        
            return View("CreateEdit", model);
        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
        public ActionResult CreateEdit(Account model)
        {
            if (model.Id == 0)
            {
               
                var result = UserSevice.Create(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.Id }) },
                JsonRequestBehavior.AllowGet); 
            }
            else
            {
          
                var result = UserSevice.Edit(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.Id }) },
                JsonRequestBehavior.AllowGet);
            }
        }

    }
}