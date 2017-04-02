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
        [Description("Xem danh sách nhân viên")]
        NhanVien_Xem = 1003,
        [Description("Sửa nhân viên")]
        NhanVien_Sua = 1004,
        [Description("Cấp quyền nhân viên")]
        NhanVien_CapQuyen = 1005,
    }
}
