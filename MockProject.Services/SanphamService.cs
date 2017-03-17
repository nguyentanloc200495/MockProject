using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockProject.DataBase;
using System.Data;
using  System.Data.Entity;

namespace MockProject.Services
{
   public class SanphamService
    {

        public static PagedSearchList<SANPHAM> Search(int? id, string tensanpham, decimal? Giaban,int? loaisanphamID, int pageIndex)
        {
               
            using (var context = new MOCKPROJECT_SIMSEntities1())

            {
                var query = context.SANPHAMs.Include(x=>x.LOAISANPHAM ).AsNoTracking().AsQueryable();
                if (id.HasValue)
                {
                    query = query.Where(x => x.Id==id);
                }
                if (!string.IsNullOrEmpty(tensanpham))
                {
                    query = query.Where(x => x.TenSanpham.Contains(tensanpham));
                }
                if (Giaban.HasValue)
                {
                    query = query.Where(x => x.Giaban == Giaban.Value);
                }
                if (loaisanphamID.HasValue)
                {
                    query = query.Where(x => x.LoaiSanphamID==loaisanphamID);
                }
                query = query.OrderByDescending(x => x.Id);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<SANPHAM>(query, pageIndex, pageSize);
            }
        }
        public static List<LOAISANPHAM> GetAllLoaisanpham()
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())

            {
                return context.LOAISANPHAMs.AsNoTracking().ToList();
            }
        }
        public static SANPHAM GetById(long id)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())
            {
                var query = context.SANPHAMs.AsNoTracking().AsQueryable();


                return query.FirstOrDefault(x => x.Id == id);
            }
        }
        public static CommandResult Create(SANPHAM c)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())
            {

                context.SANPHAMs.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static CommandResult Edit(SANPHAM c)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())
            {
                var Sanpham = context.SANPHAMs.First(x => x.Id == c.Id);
                Sanpham.TenSanpham = c.TenSanpham;
                Sanpham.Giaban = c.Giaban;
                Sanpham.Soluongton = c.Soluongton;
                Sanpham.Donvitinh = c.Donvitinh;
                Sanpham.Image = c.Image;
               Sanpham.LoaiSanphamID = c.LoaiSanphamID;

                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }

        public static CommandResult Delete(SANPHAM c)
        {
            using (var context = new MOCKPROJECT_SIMSEntities1())
            {
                var Sanpham = context.SANPHAMs.First(x => x.Id == c.Id);
                context.SANPHAMs.Remove(Sanpham);
                context.SaveChanges();
                return new CommandResult();
            }
        }
    }
}
