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
        public String ProductName { get; set; }
        public decimal? Amount { get; set; }
        public int? ProductType_ID { get; set; }

    }
}