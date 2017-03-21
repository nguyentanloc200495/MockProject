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
    
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.HOADONs = new HashSet<HOADON>();
            this.NHANVIEN_QUYEN = new HashSet<NHANVIEN_QUYEN>();
            this.PHIEUNHAPs = new HashSet<PHIEUNHAP>();
        }
    
        public int ID { get; set; }
        public string TenNV { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public Nullable<LoaiGioiTinh> GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string SoDT { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string ChucVu { get; set; }
        public Nullable<TrangThaiNhanVien> TrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHANVIEN_QUYEN> NHANVIEN_QUYEN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }
    }
}
