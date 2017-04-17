using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using MockProject.DataBase;
using MockProject.Services;

namespace MockProject
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }

        //public static UserSession Session()
        //{
        //    var obj = HttpContext.Current.Session[Constant.SessionUser];
        //    if (obj == null)
        //    {
        //        var user = UserService.GetUserByUsername(HttpContext.Current.User.Identity.GetUserName());
        //        if (user == null)
        //        {
        //            FormsAuthentication.SignOut();
        //            return new UserSession();
        //        }
        //        var session = new UserSession();
        //        session.Id = user.Id;
        //        session.Permission = user.Groups.SelectMany(x => x.Group_Permission.Select(y => y.PermissionId)).Distinct().ToList();
        //        session.Permission.AddRange(user.User_Permission.Select(x => x.PermissionId));

        //        session.PathImage = HttpUtility.UrlEncode(user.Image);
        //        HttpContext.Current.Session[Constant.SessionUser] = session;
        //        return session;
        //    }
        //    else
        //    {
        //        return obj as UserSession;
        //    }
        //}

        public static string Avatar()
        {
            var image = UserService.GetUserInfo()?.Image;
            if (string.IsNullOrEmpty(image) || !File.Exists(HttpContext.Current.Server.MapPath($"~/Content/UserImage/{HttpUtility.UrlEncode(image)}")))
            {
                return "/Content/Upload/noimage.png";
            }
            //return $"/Content/Upload/{HttpUtility.UrlEncode(image)}";
            return $"/Content/UserImage/{HttpUtility.UrlEncode(image)}";
        }
        //public static string DowloadFile()
        //{
        //    var image = UserService.GetUserInfo()?.HinhAnh;
        //    if (string.IsNullOrEmpty(image) || !File.Exists(HttpContext.Current.Server.MapPath($"~/Content/Upload/{HttpUtility.UrlEncode(image)}")))
        //    {
        //        return "/Content/Upload/noimage.png";
        //    }
        //    return $"/Content/Upload/{HttpUtility.UrlEncode(image)}";
        //}

    }

    public class GetUserName
    {
        public string GetUserNameFromId(int id)
        {
            string name = string.Empty;
            using (var context = new GST_MockProjectEntities())
            {
                if (context.USERs.Any(x => x.ID == id))
                {
                    var _users = from s in context.USERs
                        select s;
                    name = _users.Select(x => x.UserName).ToString();
                }
            }
            return name;
        }
    }
}