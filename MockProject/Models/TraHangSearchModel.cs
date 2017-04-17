﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MockProject.Services;

namespace MockProject.Models
{
    public class TraHangSearchModel:SearchModel
    {
        public int? ID { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }
}