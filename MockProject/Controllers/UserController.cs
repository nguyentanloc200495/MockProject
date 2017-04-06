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
            ViewBag.GioiTinh = WebUtil.GetEnumSelectList<LoaiGioiTinh>();
            ViewBag.TrangThai = WebUtil.GetEnumSelectList<TrangThaiNhanVien>();
            
              
            return View("CreateEdit");
        }
        [HttpGet]
       
        public ActionResult Edit(int id)
        {
            var model = UserSevice.GetById(id);
            ViewBag.GioiTinh = WebUtil.GetEnumSelectList<LoaiGioiTinh>();
            ViewBag.TrangThai = WebUtil.GetEnumSelectList<TrangThaiNhanVien>();
                   
            return View("CreateEdit", model);
        }
        [HttpPost]
        [AuthorizeAdmin(Permission = Permission.NhanVien_ThemSua)]
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



        //phân quyền nhân viên
        //trang hiển thị quyền
        [AuthorizeAdmin(Permission = Permission.NhanVien_CapQuyen)]
        public ActionResult ListPermission(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN user = db.NHANVIENs.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [AuthorizeAdmin(Permission = Permission.NhanVien_CapQuyen)]
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult ListPermission2(NHANVIEN user, AddUserPermission model, string selectedPermissions)
        {
            //lấy ra các quyền nhân viên
                int[] SelectedGroupPermission = string.IsNullOrWhiteSpace(selectedPermissions) ? new int[0]
                    : selectedPermissions.Split(',').Select(x => int.Parse(x)).ToArray();
          
                    user = db.NHANVIENs.Include(x => x.NHANVIEN_QUYEN).FirstOrDefault(x => x.ID == model.UserId);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    if (SelectedGroupPermission == null)
                    {
                        SelectedGroupPermission = new int[0];
                    }
                    //ktra selectedpermiss có quyền hay chưa
                    var deleted = user.NHANVIEN_QUYEN.Where(x => SelectedGroupPermission.Contains(x.QuyenID) == false);
                    foreach (var userPermission in deleted.ToList())
                    {
                        db.NHANVIEN_QUYEN.Remove(userPermission);
                    }
                    var addNew = SelectedGroupPermission.Where(x => user.NHANVIEN_QUYEN.All(y => y.QuyenID != x));
                    foreach (var permissionId in addNew)
                    {
                        var permission = new NHANVIEN_QUYEN();
                        permission.NhanVienID = model.UserId;
                        permission.QuyenID = permissionId;
                        db.NHANVIEN_QUYEN.Add(permission);
                    }
                var log = new StringBuilder();
                log.Append(db.GetAddDeleteLog((NHANVIEN_QUYEN permission) => permission.QuyenID));
                db.SaveChanges();
                return Json(new RedirectCommand() { Code = ResultCode.Success, Message = "Cập nhật thành công", Url = Url.Action("ListPermission", new { id = user.ID }) },
                      JsonRequestBehavior.AllowGet);
            }


    }
}