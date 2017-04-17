using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockProject.DataBase;
using System.Data;
using System.Data.Entity;

namespace MockProject.Services
{
    public class SanphamService
    {

        public static PagedSearchList<PRODUCT> Search(int? id, string tensanpham, decimal? Giaban, int? loaisanphamID, int pageIndex)
        {

            using (var context = new GST_MockProjectEntities())

            {
                var query = context.PRODUCTs.Include(x => x.PRODUCT_TYPE).AsNoTracking().AsQueryable().Where(x => x.Status == true);

                if (id.HasValue)
                {
                    query = query.Where(x => x.ID == id);
                }
                if (!string.IsNullOrEmpty(tensanpham))
                {
                    query = query.Where(x => x.ProductName.Contains(tensanpham));
                }
                if (Giaban.HasValue)
                {
                    query = query.Where(x => x.Amount == Giaban.Value);
                }
                if (loaisanphamID.HasValue)
                {
                    query = query.Where(x => x.ProductType_ID == loaisanphamID);
                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<PRODUCT>(query, pageIndex, pageSize);
            }
        }
        public static List<PRODUCT_TYPE> GetAllLoaisanpham()
        {
            using (var context = new GST_MockProjectEntities())

            {
                return context.PRODUCT_TYPE.AsNoTracking().ToList();
            }
        }
        public static PRODUCT GetById(int id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.PRODUCTs.Include(x => x.PRODUCT_TYPE).AsNoTracking().AsQueryable();


                return query.FirstOrDefault(x => x.ID == id);
            }
        }
        public static CommandResult Create(PRODUCT c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                c.Status = true;
                context.PRODUCTs.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static CommandResult Edit(PRODUCT c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var Sanpham = context.PRODUCTs.First(x => x.ID == c.ID);
                Sanpham.ProductName = c.ProductName;
                Sanpham.Amount = c.Amount;
               
                Sanpham.Unit = c.Unit;
               
                Sanpham.ProductType_ID = c.ProductType_ID;

                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }

        public static CommandResult Delete(PRODUCT c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var Sanpham = context.PRODUCTs.First(x => x.ID == c.ID);
                Sanpham.Status = false;
                context.SaveChanges();
                return new CommandResult();
            }
        }
    }
}
