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
               
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.SANPHAMs.Include(x=>x.LOAISANPHAM ).AsNoTracking().AsQueryable();
                if (id.HasValue)
                {
                    query = query.Where(x => x.ID==id);
                }
                if (!string.IsNullOrEmpty(tensanpham))
                {
                    query = query.Where(x => x.TenSP.Contains(tensanpham));
                }
                if (Giaban.HasValue)
                {
                    query = query.Where(x => x.GiaBan == Giaban.Value);
                }
                if (loaisanphamID.HasValue)
                {
                    query = query.Where(x => x.LoaiSanPhamID==loaisanphamID);
                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<SANPHAM>(query, pageIndex, pageSize);
            }
        }
        public static List<LOAISANPHAM> GetAllLoaisanpham()
        {
            using (var context = new GST_MockProjectEntities())

            {
                return context.LOAISANPHAMs.AsNoTracking().ToList();
            }
        }
        public static SANPHAM GetById(long id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.SANPHAMs.AsNoTracking().AsQueryable();


                return query.FirstOrDefault(x => x.ID == id);
            }
        }
        public static CommandResult Create(SANPHAM c)
        {
            using (var context = new GST_MockProjectEntities())
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
            using (var context = new GST_MockProjectEntities())
            {
                var Sanpham = context.SANPHAMs.First(x => x.ID == c.ID);
                Sanpham.TenSP = c.TenSP;
                Sanpham.GiaBan = c.GiaBan;
                Sanpham.SoLuongTon = c.SoLuongTon;
                Sanpham.DonViTinh = c.DonViTinh;
                Sanpham.HinhSP = c.HinhSP;
               Sanpham.LoaiSanPhamID = c.LoaiSanPhamID;

                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }

        public static CommandResult Delete(SANPHAM c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var Sanpham = context.SANPHAMs.First(x => x.ID == c.ID);
                context.SANPHAMs.Remove(Sanpham);
                context.SaveChanges();
                return new CommandResult();
            }
        }
    }
}
