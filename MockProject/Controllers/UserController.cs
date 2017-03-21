using MockProject.Models;
using MockProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.DataBase;
using CommonData;

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
        //show ra table nhân viên
        public ActionResult List(UserSearchModel searchModel)
        {
            var pagedList = UserSevice.Search(searchModel.TenNV, searchModel.TaiKhoan, searchModel.SoDT,searchModel.ChucVu, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
        public ActionResult Create()
        {
            ViewBag.GioiTinh = WebUtil.GetEnumSelectList<LoaiGioiTinh>();
            ViewBag.TrangThai = WebUtil.GetEnumSelectList<TrangThaiNhanVien>();
            
            //tạo nhân viên       
            return View("CreateEdit");
        }
        [HttpGet]
       
        public ActionResult Edit(int id)
        {
            var model = UserSevice.GetById(id);
            ViewBag.GioiTinh = WebUtil.GetEnumSelectList<LoaiGioiTinh>();
            ViewBag.TrangThai = WebUtil.GetEnumSelectList<TrangThaiNhanVien>();
            //chỉnh sửa nhân viên        
            return View("CreateEdit", model);
        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
        public ActionResult CreateEdit(NHANVIEN model)
        {
            ViewBag.GioiTinh = WebUtil.GetEnumSelectList<LoaiGioiTinh>();
            if (model.ID == 0)
            {
               
                var result = UserSevice.Create(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
            else
            {
          
                var result = UserSevice.Edit(model);
                return
                 Json(
               new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
        }

    }
}