using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MockProject.Services;

namespace MockProject.Models
{
    public class UserSearchModel : SearchModel
    {
        public String TenNV { get; set; }
        public String TaiKhoan { get; set; }
        public String ChucVu { get; set; }
        public String SoDT { get; set; }
      
    }
}