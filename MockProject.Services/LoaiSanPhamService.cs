using MockProject.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MockProject.Services
{
    public class LoaiSanPham
    {
        public static PagedSearchList<LOAISANPHAM> Search(string Tenloaisanpham, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.LOAISANPHAMs.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(Tenloaisanpham))
                {
                    query = query.Where(x => x.TenLoaiSanPham.Contains(Tenloaisanpham));
                }
               
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<LOAISANPHAM>(query, pageIndex, pageSize);
            }
        }
        public static LOAISANPHAM GetById(long id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.LOAISANPHAMs.Include(x=>x.SANPHAMs).AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.ID == id);
            }
        }
        public static CommandResult Create(LOAISANPHAM c)
        {
            using (var context = new GST_MockProjectEntities())
            {
              
                context.LOAISANPHAMs.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static CommandResult Edit(LOAISANPHAM c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var Loaisanpham = context.LOAISANPHAMs.First(x => x.ID == c.ID);

                Loaisanpham.TenLoaiSanPham = c.TenLoaiSanPham;
                Loaisanpham.TrangThai = c.TrangThai;
                
                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
    }
}
