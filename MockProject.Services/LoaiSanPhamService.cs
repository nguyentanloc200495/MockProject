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
        public static PagedSearchList<LOAISANPHAM> Search(string tendanhmuc, int pageIndex)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())

            {
                var query = context.LOAISANPHAMs.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(tendanhmuc))
                {
                    query = query.Where(x => x.TenDanhmuc.Contains(tendanhmuc));
                }
               
                query = query.OrderByDescending(x => x.Id);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<LOAISANPHAM>(query, pageIndex, pageSize);
            }
        }
        public static LOAISANPHAM GetById(long id)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())
            {
                var query = context.LOAISANPHAMs.Include(x=>x.SANPHAMs).AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.Id == id);
            }
        }
        public static CommandResult Create(LOAISANPHAM c)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())
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
            using (var context = new MOCKPROJECT_SIMSEntities1())
            {
                var Loaisanpham = context.LOAISANPHAMs.First(x => x.Id == c.Id);

                Loaisanpham.TenDanhmuc = c.TenDanhmuc;
                
                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
    }
}
