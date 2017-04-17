using MockProject.Models;
using MockProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.DataBase;
using CommonData;
using MockProject.Common;
using System.Net;
using System.Data.Entity;
using System.Text;
using System.Data.Entity.Validation;

namespace MockProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private GST_MockProjectEntities db = new GST_MockProjectEntities();
        // GET: User
        [AuthorizeAdmin(Permission = Permission.NhanVien_Xem)]
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
        //tạo nhân viên  
        public ActionResult Create()
        {
            ViewBag.Sex = WebUtil.GetEnumSelectList<Sex_Type>();
            ViewBag.Status = WebUtil.GetEnumSelectList<User_Status>();
            
              
            return View("CreateEdit");
        }
        [HttpGet]
       
        public ActionResult Edit(int id)
        {
            var model = UserSevice.GetById(id);
            ViewBag.Sex = WebUtil.GetEnumSelectList<Sex_Type>();
            ViewBag.Status = WebUtil.GetEnumSelectList<User_Status>();

            return View("CreateEdit", model);
        }


        [HttpPost]
        [AuthorizeAdmin(Permission = Permission.NhanVien_ThemSua)]
        public ActionResult CreateEdit(USER model)
        {
            ViewBag.Sex = WebUtil.GetEnumSelectList<Sex_Type>();
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



        //phân quyền nhân viên
        //trang hiển thị quyền
        [AuthorizeAdmin(Permission = Permission.NhanVien_CapQuyen)]
        public ActionResult ListPermission(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER user = db.USERs.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [AuthorizeAdmin(Permission = Permission.NhanVien_CapQuyen)]
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult ListPermission2(USER user, AddUserPermission model, string selectedPermissions)
        {
            //lấy ra các quyền nhân viên
                int[] SelectedGroupPermission = string.IsNullOrWhiteSpace(selectedPermissions) ? new int[0]
                    : selectedPermissions.Split(',').Select(x => int.Parse(x)).ToArray();
          
                    user = db.USERs.Include(x => x.USER_PERMISSION).FirstOrDefault(x => x.ID == model.UserId);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    if (SelectedGroupPermission == null)
                    {
                        SelectedGroupPermission = new int[0];
                    }
                    //ktra selectedpermiss có quyền hay chưa
                    var deleted = user.USER_PERMISSION.Where(x => SelectedGroupPermission.Contains(x.Permisstion_ID) == false);
                    foreach (var userPermission in deleted.ToList())
                    {
                        db.USER_PERMISSION.Remove(userPermission);
                    }
                    var addNew = SelectedGroupPermission.Where(x => user.USER_PERMISSION.All(y => y.Permisstion_ID != x));
                    foreach (var permissionId in addNew)
                    {
                        var permission = new USER_PERMISSION();
                        permission.User_ID = model.UserId;
                        permission.Permisstion_ID = permissionId;
                        db.USER_PERMISSION.Add(permission);
                    }
                var log = new StringBuilder();
                log.Append(db.GetAddDeleteLog((USER_PERMISSION permission) => permission.Permisstion_ID));
                db.SaveChanges();
                return Json(new RedirectCommand() { Code = ResultCode.Success, Message = "Cập nhật thành công", Url = Url.Action("ListPermission", new { id = user.ID }) },
                      JsonRequestBehavior.AllowGet);
            }


    }
}