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
    
    public partial class NHANVIEN_QUYEN
    {
        public int NhanVienID { get; set; }
        public int QuyenID { get; set; }
    
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}