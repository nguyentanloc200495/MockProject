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
        public static PagedSearchList<KHO> Search(string TenKho,TinhTrangKho? Tinhtrang, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.KHOes.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(TenKho))
                {
                    query = query.Where(x => x.TenKho.Contains(TenKho));
                }
                if (Tinhtrang.HasValue)
                {
                    query = query.Where(x => x.TinhTrang == Tinhtrang);
                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<KHO>(query, pageIndex, pageSize);
            }
        }
        public static KHO GetById(long id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.KHOes.AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.ID == id);
            }
        }

        public static PagedSearchList<CTKHO> SearchCTKho(int? Khoid, int? Sanphamid, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.CTKHOes
                    .Include(x=>x.SANPHAM)
                    .Include(x=>x.KHO)
                    .AsNoTracking().AsQueryable();

                if (Khoid.HasValue)
                {
                    query = query.Where(x => x.KhoID == Khoid);
                }
                if (Sanphamid.HasValue)
                {
                    query = query.Where(x => x.SanPhamID == Sanphamid);
                }
                query = query.OrderBy(x => x.SoLuong);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<CTKHO>(query, pageIndex, pageSize);
            }
        }
        public static CommandResult Create(KHO c)
        {
            using (var context = new GST_MockProjectEntities())
            {

                context.KHOes.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static CommandResult Edit(KHO c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var Kho = context.KHOes.First(x => x.ID == c.ID);

                Kho.TenKho = c.TenKho;
                Kho.ViTriKho = c.ViTriKho;
                Kho.TinhTrang = c.TinhTrang;
                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
    }
}
