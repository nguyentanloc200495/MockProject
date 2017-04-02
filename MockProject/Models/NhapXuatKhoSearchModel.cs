using MockProject.DataBase;
using MockProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockProject.Models
{
    public class NhapXuatKhoSearchModel : SearchModel
    {
        public int? ID { get; set; }
        public DateTime? NgayLapPhieu { get; set; }
        public int? KhoID { get; set; }
        public LoaiPhieuNhapXuatKho1? LoaiPhieu { get; set; }
    }
}