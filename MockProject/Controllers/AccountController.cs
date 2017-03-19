using CommonData;
using MockProject.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace MockProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, bool rememberMe)
        {
            using (var context =new GST_MockProjectEntities())
            {
                var passMD5 = Encryptor.MD5Hash(password);
                var user = context.NHANVIENs.FirstOrDefault(x => x.TaiKhoan == username);
                if (user!=null && user.MatKhau !=null)
                {
                    if (user.TaiKhoan == username && user.MatKhau == passMD5)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ViewBag.Message = "Tên đăng nhập hay mật khẩu không hợp lệ";
            }
            return View();
        }
    }
}