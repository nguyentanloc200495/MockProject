using CommonData;
using MockProject.DataBase;
using MockProject.Models;
using MockProject.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MockProject.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Home(string returnUrl)
        {
            ViewBag.message = "Welcome to C.T Group";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string username, string password, bool rememberMe, string returnUrl)
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {
                using (var context = new GST_MockProjectEntities())
                {
                    var passMD5 = Encryptor.MD5Hash(password);

                    var user = context.NHANVIENs.FirstOrDefault(x => x.TaiKhoan == username);
                    
                    if (user != null
                        && ((!string.IsNullOrWhiteSpace(user.MatKhau) && user.MatKhau == passMD5) ||
                            ( Membership.ValidateUser(username, password))))
                    {
                        if (user.TrangThai == TrangThaiNhanVien.NghiViec)
                        {
                            ViewBag.Message = "Tài khoản đã bị khóa !";
                            return View();
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(user.TaiKhoan, rememberMe);
                            if (string.IsNullOrEmpty(returnUrl))
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                return RedirectPermanent(returnUrl);
                            }
                        }
                    }
                    ViewBag.Message = "Tên đăng nhập hay mật khẩu không hợp lệ";
                    return View();
                }
            }
        }

        //[HttpPost]
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(string username, string password, bool rememberMe, string returnUrl)
        //{
        //    if (Request.HttpMethod == "GET")
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        using (var context = new GST_MockProjectEntities())
        //        {
        //            var passMD5 = Encryptor.MD5Hash(password);

        //            var user = context.NHANVIENs.FirstOrDefault(x => x.TaiKhoan == username);


        //            if (user != null
        //                && ((!string.IsNullOrWhiteSpace(user.MatKhau) && user.MatKhau == passMD5) ||
        //                    (Membership.ValidateUser(username, password))))
        //            {   

        //                    FormsAuthentication.SetAuthCookie(user.TaiKhoan, rememberMe);

        //                    if (string.IsNullOrEmpty(returnUrl))
        //                    {
        //                        return RedirectToAction("Index", "Home");
        //                    }
        //                    else
        //                    {
        //                        return RedirectPermanent(returnUrl);
        //                    }

        //            }
        //            ViewBag.Message = "Tên đăng nhập hay mật khẩu không hợp lệ";
        //            return View();
        //        }
        //    }
        //}
        public ActionResult Profile()
        {
            GST_MockProjectEntities db = new GST_MockProjectEntities();
            var id = UserService.GetUserInfo().ID;
            NHANVIEN user = db.NHANVIENs.FirstOrDefault(x => x.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Descripline = HrService.GetDiscipline(user.EmployeeId);
            //ViewBag.Award = HrService.GetAwards(user.EmployeeId);
            //ViewBag.Contract = HrService.GetContract(user.EmployeeId);


            //ViewBag.Device = DeviceAssignmentService.GetDeviceAssignmentByUser(UserService.GetUserInfo().Id);
            return View(user);
        }
        public ActionResult ShowUpdateImage()//hiện trang chỉnh sửa hình ảnh
        {
            GST_MockProjectEntities db = new GST_MockProjectEntities();
            var id = UserService.GetUserInfo().ID;
            NHANVIEN user = db.NHANVIENs.SingleOrDefault(s => s.ID == id);
            return View(user);
        }
        public ActionResult SaveChangeImage(int id, HttpPostedFileBase ImageUser)//hàm lưu lại hình ảnh đã đổi
        {
            GST_MockProjectEntities db = new GST_MockProjectEntities();
            var imageUpdate = db.NHANVIENs.Find(id);

            try
            {


                if (ImageUser != null)
                {

                    if (ImageUser.ContentLength > 0)
                    {

                        var pathFile = "~/Content/UserImage/";
                        var fileNameinput = id.ToString() + Path.GetFileName(ImageUser.FileName);

                        var ext = Path.GetExtension(fileNameinput);
                        if (ext != ".jpg" && ext != ".png" && ext != ".gif")
                        {
                            return RedirectToAction("ShowUpdateImage", "Account", new { id = id });
                        }
                        var fileName = id.ToString() + ".jpg";
                        bool exists = System.IO.Directory.Exists(Server.MapPath(pathFile));
                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(pathFile));
                        var path = Path.Combine(Server.MapPath(pathFile), fileName);
                        ImageUser.SaveAs(path);
                        imageUpdate.HinhAnh = fileName;

                    }
                    db.Entry(imageUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }

            return RedirectToAction("Profile", "Account", new { id = id });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthorizeService.ClearCache(HttpContext.User.Identity.Name);
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account", "");
        }


        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string name = HttpContext.User.Identity.Name;
                var result = AuthorizeService.ChangePassword(name, model.Password, model.NewPassword);

                ViewBag.Message = result.Message;
            }
            return View();
        }

        public ActionResult ChangePassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string name = HttpContext.User.Identity.Name;
                var result = AuthorizeService.ChangePassword(name, model.Password, model.NewPassword);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new CommandResult(ResultCode.Fail, "Thông tin chưa đúng"), JsonRequestBehavior.AllowGet);
        }


    }
}