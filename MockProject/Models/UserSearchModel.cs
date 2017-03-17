using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MockProject.Services;

namespace MockProject.Models
{
    public class UserSearchModel : SearchModel
    {
        public String Fullname { get; set; }
        public String Username { get; set; }
        public String PhoneNumber { get; set; }
      
    }
}