using System;
using System.Linq;
using MockProject;
using MockProject.DataBase;
using CommonData;

namespace MockProject.Services
{
    public class UserSevice
    {
   
        public static PagedSearchList<USER> Search(string Tennv, string Taikhoan, string Sodt,string Chucvu, int pageIndex)
        {
            //dùng để search nhân viên
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.USERs.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(Tennv))
                {
                    query = query.Where(x => x.FullName.Contains(Tennv));
                }
                if (!string.IsNullOrEmpty(Taikhoan))
                {
                    query = query.Where(x => x.UserName.Contains(Taikhoan));
                }
                if (!string.IsNullOrEmpty(Sodt))
                {
                    query = query.Where(x => x.PhoneNumber.Contains(Sodt));
                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<USER>(query, pageIndex, pageSize);
            }
        }
        public static USER GetById(long id)
        {
            //hàm lấy nhân viên có ID = id
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.USERs.AsNoTracking().AsQueryable();
     

                return query.FirstOrDefault(x => x.ID == id);
            }
        }
        public static CommandResult Create(USER c)
        {
            //Tạo 1 nhân viên mới
            using (var context = new GST_MockProjectEntities())
            {
                c.Password = Encryptor.MD5Hash(c.Password);
                context.USERs.Add(c);
                context.SaveChanges();
               
                return new CommandResult();
            }
        }
        public static CommandResult Edit(USER c)
        {
            //chỉnh sửa 1 nhân viên (không cho chỉnh sửa mật khẩu)
            using (var context = new GST_MockProjectEntities())
            {
                var NhanVien = context.USERs.First(x => x.ID == c.ID);

                NhanVien.FullName = c.FullName;
                NhanVien.DateOfBirth = c.DateOfBirth;
                NhanVien.Sex = c.Sex;
                NhanVien.Address = c.Address;
                NhanVien.PhoneNumber = c.PhoneNumber;
                NhanVien.UserName = c.UserName;
                NhanVien.Email = c.Email;
                NhanVien.Office = c.Office;
                NhanVien.Status = c.Status;
                context.SaveChanges();
                
                return new CommandResult();
            }
        }
    }
}
