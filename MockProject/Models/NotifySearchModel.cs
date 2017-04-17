using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MockProject.DataBase;
using MockProject.Services;

namespace MockProject.Models
{
    public class NotifySearchModel : SearchModel
    {
        public String Title { get; set; }
        public DateTime? FromDateTimeCreated { get; set; }
        public DateTime? ToDateTimeCreated { get; set; }
        public Notifi_Type? Type { get; set; }
        public Notifi_Status? Status { get; set; }
        
    }
}