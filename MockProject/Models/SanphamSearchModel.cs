using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MockProject.Services;

namespace MockProject.Models
{
    public class SanphamSearchModel : SearchModel
    {
        public int? ID { get; set; }
        public String TenSanpham { get; set; }
        public decimal? Giaban { get; set; }
        public int? LoaiSanphamID { get; set; }

    }
}