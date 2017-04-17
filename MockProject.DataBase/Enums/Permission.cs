using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockProject.DataBase
{
    public static class Manage_Group
    {
        public static string[] Names = new string[] { "", "Nhân viên","Sản phẩm","Kho"};
    }
    public enum Permission
    {
        //cấp quyền cho nhân viên
        [Description("Xem danh sách nhân viên")]
        NhanVien_Xem = 1001,
        [Description("Thêm sửa nhân viên")]
        NhanVien_ThemSua = 1002,
        [Description("Cấp quyền nhân viên")]
        NhanVien_CapQuyen = 1003,


        //cấp quyền cho loại sản phẩm
        [Description("Xem loại sản phẩm")]
        LoaiSanPham_Xem = 2001,
        [Description("Thêm, sửa loại sản phẩm")]
        LoaiSanPham_ThemSua = 2002,


        //cấp quyền cho xem thông báo
        [Description("Xem loại sản phẩm")]
        Notify_View = 3001,

    }
}
