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
        public static PagedSearchList<PRODUCT_TYPE> Search(string Tenloaisanpham, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.PRODUCT_TYPE.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(Tenloaisanpham))
                {
                    query = query.Where(x => x.ProductTypeName.Contains(Tenloaisanpham));
                }
               
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<PRODUCT_TYPE>(query, pageIndex, pageSize);
            }
        }
        public static PRODUCT_TYPE GetById(long id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.PRODUCT_TYPE.Include(x=>x.PRODUCTs).AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.ID == id);
            }
        }
      
        public static CommandResult Create(PRODUCT_TYPE c)
        {
            using (var context = new GST_MockProjectEntities())
            {
              
                context.PRODUCT_TYPE.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static CommandResult Edit(PRODUCT_TYPE c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var Loaisanpham = context.PRODUCT_TYPE.First(x => x.ID == c.ID);

                Loaisanpham.ProductTypeName = c.ProductTypeName;
                Loaisanpham.Status = c.Status;
                
                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
    }
}
