using System.Linq;
using MockProject.DataBase;
using System.Web;
using System.Data.Entity;
using System.Web.Security;
using CommonData.Common;

namespace MockProject.Services
{
    public class UserService
    {
        public static USER GetUserInfo()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return null;
            var username = HttpContext.Current.User.Identity.Name;
            string key = $"UserService-Info-{username}";
            var user = new MemoryCacheManager().Get(key, () =>
            {
                using (var context = new GST_MockProjectEntities())
                {

                    return context.USERs.AsNoTracking().FirstOrDefault(x => x.UserName == username);
                }
            });
            if (user == null)
            {
                HttpContext.Current.Session.Abandon();
                FormsAuthentication.SignOut();
            }
            return user;

        }
        public static USER GetUserByUsername(string username)
        {
            using (var context = new GST_MockProjectEntities())
            {
                return
                    context.USERs.Include(x => x.USER_PERMISSION).AsNoTracking()
                        .FirstOrDefault(x => x.UserName == username);
            }
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
