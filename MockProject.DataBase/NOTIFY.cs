//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MockProject.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class NOTIFY
    {
        public int ID { get; set; }
        public Nullable<int> UserCreate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime TimeCreate { get; set; }
        public Nullable<Notifi_Type> Type { get; set; }
        public Nullable<Notifi_Status> Status { get; set; }
    
        public virtual USER USER { get; set; }
    }
}
