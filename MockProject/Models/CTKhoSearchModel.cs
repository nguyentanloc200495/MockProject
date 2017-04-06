using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MockProject.Services;

namespace MockProject.Models
{
    public class CTKhoSearchModel : SearchModel
    {
        public int? KhoID { get; set; }
        public int? SanPhamID { get; set; }

    }
}