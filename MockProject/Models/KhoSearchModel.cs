using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MockProject.DataBase;
using MockProject.Services;

namespace MockProject.Models
{
    public class KhoSearchModel : SearchModel
    {
        public string TenKho { get; set; }
        public TinhTrangKho? TinhTrang { get; set; }
    }
}