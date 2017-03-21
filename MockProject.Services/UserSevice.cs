using System;
using System.Linq;
using MockProject;
using MockProject.DataBase;
using CommonData;

namespace MockProject.Services
{
    public class UserSevice
    {
   
        public static PagedSearchList<NHANVIEN> Search(string Tennv, string Taikhoan, string Sodt,string Chucvu, int pageIndex)
        {
            //dùng để search nhân viên
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.NHANVIENs.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(Tennv))
                {
                    query = query.Where(x => x.TenNV.Contains(Tennv));
                }
                if (!string.IsNullOrEmpty(Taikhoan))
                {
                    query = query.Where(x => x.TaiKhoan.Contains(Taikhoan));
                }
                if (!string.IsNullOrEmpty(Sodt))
                {
                    query = query.Where(x => x.SoDT.Contains(Sodt));
                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<NHANVIEN>(query, pageIndex, pageSize);
            }
        }
        public static NHANVIEN GetById(long id)
        {
            //hàm lấy nhân viên có ID = id
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.NHANVIENs.AsNoTracking().AsQueryable();
     

                return query.FirstOrDefault(x => x.ID == id);
            }
        }
        public static CommandResult Create(NHANVIEN c)
        {
            //Tạo 1 nhân viên mới
            using (var context = new GST_MockProjectEntities())
            {
                c.MatKhau = Encryptor.MD5Hash(c.MatKhau);
                context.NHANVIENs.Add(c);
                context.SaveChanges();
               
                return new CommandResult();
            }
        }
        public static CommandResult Edit(NHANVIEN c)
        {
            //chỉnh sửa 1 nhân viên (không cho chỉnh sửa mật khẩu)
            using (var context = new GST_MockProjectEntities())
            {
                var NhanVien = context.NHANVIENs.First(x => x.ID == c.ID);

                NhanVien.TenNV = c.TenNV;
                NhanVien.NgaySinh = c.NgaySinh;
                NhanVien.GioiTinh = c.GioiTinh;
                NhanVien.DiaChi = c.DiaChi;
                NhanVien.SoDT = c.DiaChi;
                NhanVien.TaiKhoan = c.TaiKhoan;
                NhanVien.Email = c.Email;
                NhanVien.ChucVu = c.ChucVu;
                NhanVien.TrangThai = c.TrangThai;
                context.SaveChanges();
                
                return new CommandResult();
            }
        }
    }
}
