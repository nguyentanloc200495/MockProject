using System.Linq;
using MockProject.DataBase;
using System.Web;

using System.Web.Security;
using CommonData.Common;

namespace MockProject.Services
{
    public class UserService
    {
        public static NHANVIEN GetUserInfo()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return null;
            var username = HttpContext.Current.User.Identity.Name;
            string key = $"UserService-Info-{username}";
            var user = new MemoryCacheManager().Get(key, () =>
            {
                using (var context = new GST_MockProjectEntities())
                {

                    return context.NHANVIENs.AsNoTracking().FirstOrDefault(x => x.TaiKhoan == username);
                }
            });
            if (user == null)
            {
                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
            }
            return user;

        }
        //public static NHANVIEN GetUserInfo()
        //{
        //    var username = System.Web.HttpContext.Current.User.Identity.Name;
        //    using (var context = new GST_MockProjectEntities())
        //    {

               
        //        if (!string.IsNullOrEmpty(username))
        //        {

        //           var user = context.NHANVIENs.AsNoTracking().FirstOrDefault(x => x.TaiKhoan == username);

        //            return user;


        //        }
        //        else
        //        {

        //            HttpContext.Current.Session.Abandon();
        //            FormsAuthentication.SignOut();
        //            return null;

        //        }
              
        //    }
       // }
    }
}
