using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MockProject.DataBase;

namespace MockProject.Services
{
    public class KhoService
    {
        public static PagedSearchList<WAREHOUSE> Search(string TenKho,Warehouse_Status? Tinhtrang, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.WAREHOUSEs.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(TenKho))
                {
                    query = query.Where(x => x.WarehouseName.Contains(TenKho));
                }
                if (Tinhtrang.HasValue)
                {
                    query = query.Where(x => x.Status == Tinhtrang);
                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<WAREHOUSE>(query, pageIndex, pageSize);
            }
        }
        public static WAREHOUSE GetById(long id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.WAREHOUSEs.AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.ID == id);
            }
        }

        public static PagedSearchList<WAREHOUSE_DETAIL> SearchCTKho(int? Khoid, int? Sanphamid, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.WAREHOUSE_DETAIL
                    .Include(x=>x.PRODUCT)
                    .Include(x=>x.WAREHOUSE)
                    .AsNoTracking().AsQueryable();

                if (Khoid.HasValue)
                {
                    query = query.Where(x => x.Warehouse_ID == Khoid);
                }
                if (Sanphamid.HasValue)
                {
                    query = query.Where(x => x.Product_ID == Sanphamid);
                }
                query = query.OrderBy(x => x.Quantity);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<WAREHOUSE_DETAIL>(query, pageIndex, pageSize);
            }
        }
        public static CommandResult Create(WAREHOUSE c)
        {
            using (var context = new GST_MockProjectEntities())
            {

                context.WAREHOUSEs.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static CommandResult Edit(WAREHOUSE c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var Kho = context.WAREHOUSEs.First(x => x.ID == c.ID);

                Kho.WarehouseName = c.WarehouseName;
                Kho.Address = c.Address;
                Kho.Status = c.Status;
                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
    }
}
