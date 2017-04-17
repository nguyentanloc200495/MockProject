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
        public DateTime? CreateDate { get; set; }
        public int? Warehouse_ID { get; set; }
        public WarehouseTransaction_Type? Type { get; set; }
    }

   
}