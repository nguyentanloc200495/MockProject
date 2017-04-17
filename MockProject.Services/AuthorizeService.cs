using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security;
using System.Web.Security;

using MockProject.DataBase;
using CommonData;
using CommonData.Common;

namespace MockProject.Services
{
    public class AuthorizeService
    {
        public static bool HasPermission(int permission)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return false;
            var username = HttpContext.Current.User.Identity.Name;
            string key = $"AuthorizeService-Permission-{username}";
            var list = new MemoryCacheManager().Get(key, GetPermissionByUser);
            return list.Contains(permission);
        }

        public static bool HasPermission(Permission permission)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return false;
            var username = HttpContext.Current.User.Identity.Name;
            string key = $"AuthorizeService-Permission-{username}";
            var list = new MemoryCacheManager().Get(key, GetPermissionByUser);
            return list.Contains((int)permission);
        }

        public static bool HasPermission(int[] permissions)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return false;
            var username = HttpContext.Current.User.Identity.Name;
            string key = $"AuthorizeService-Permission-{username}";
            var list = new MemoryCacheManager().Get(key, GetPermissionByUser);
            return list.Any(permissions.Contains);
        }

        public static void ClearCache(string username)
        {
            string key = $"AuthorizeService-Permission-{username}";
            new MemoryCacheManager().Remove(key);

            string key2 = $"UserService-Info-{username}";
            new MemoryCacheManager().Remove(key2);
        }

        public static List<int> GetPermissionByUser()
        {
            using (var context = new GST_MockProjectEntities())
            {
                var username = HttpContext.Current.User.Identity.Name;
                var user =
                    context.USERs.Include(x => x.USER_PERMISSION)
                        .FirstOrDefault(x => x.UserName == username);
                if (user == null)
                {
                    return new List<int>();
                }
                else
                {
                    var result = user.USER_PERMISSION.Select(y => y.Permisstion_ID).ToList();
                    //result.AddRange(user.NHANVIEN_QUYEN.Select(x => x.QuyenID));
                    return result.Distinct().ToList();
                }
            }
        }

        public static CommandResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            using (var context = new GST_MockProjectEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = context.Database.BeginTransaction())
                {

                    var user = context.USERs.FirstOrDefault(x => x.UserName == userName);
                         if (user == null)
                        {
                            return new CommandResult(ResultCode.Fail, "User không tồn tại");
                        }
                         else
                         {
                        var passMD5 = Encryptor.MD5Hash(oldPassword);
                        if (user.Password != passMD5)
                        {
                            return new CommandResult(ResultCode.Fail, "Mật khẩu cũ không đúng");
                        }
                        user.Password = Encryptor.MD5Hash(newPassword);
                        context.SaveChanges();
                    }
                        
                    

                    dbTran.Commit();

                    return new CommandResult(ResultCode.Success, "Đổi mật khẩu thành công.");
                }
            }
        }

        //public static bool SetPassLDAP(string userName, string newPassword)
        //{
        //    var sPath = ConfigurationManager.ConnectionStrings["ADService"].ConnectionString; //This is if your domain was my.domain.com
        //    DirectoryEntry de = new DirectoryEntry(sPath, ConfigurationManager.AppSettings["ADUsername"], ConfigurationManager.AppSettings["ADPassword"], AuthenticationTypes.Secure);
        //    DirectorySearcher ds = new DirectorySearcher(de);
        //    string qry = string.Format("(&(objectCategory=person)(objectClass=user)(sAMAccountName={0}))", userName);
        //    ds.Filter = qry;
        //    try
        //    {
        //        var sr = ds.FindOne();
        //        DirectoryEntry user = sr.GetDirectoryEntry();
        //        user.Invoke("SetPassword", new object[] { newPassword });
        //        user.CommitChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}
