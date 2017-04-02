using MockProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MockProject.DataBase
{
   public class NhapXuatKhoService
    {
        public static PagedSearchList<NHAPXUATKHO> Search(int? Id,int? Khoid, LoaiPhieuNhapXuatKho1? Loaiphieu, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.NHAPXUATKHOes
                    .Include(x=>x.NHANVIEN)
                    .Include(x=>x.KHO).AsNoTracking().AsQueryable();

                if (Id.HasValue)
                {
                    query = query.Where(x => x.ID == Id);
                }
                if (Khoid.HasValue)
                {
                    query = query.Where(x => x.KhoID == Khoid);
                }
                if (Loaiphieu.HasValue)
                {
                    query = query.Where(x => x.LoaiPhieu == Loaiphieu);
                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<NHAPXUATKHO>(query, pageIndex, pageSize);
            }
        }
        public static NHAPXUATKHO GetById(long id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.NHAPXUATKHOes.AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.ID == id);
            }
        }
        public static CTPHIEUNHAPXUATKHO GetByIdCT(long idSP,long idPNXK)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.CTPHIEUNHAPXUATKHOes.AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.SanPhamID == idSP && x.NhapXuatKhoID==idPNXK);
            }
        }
        public static List<KHO> GetAllKho()
        {
            using (var context = new GST_MockProjectEntities())
            {
                return context.KHOes.AsNoTracking().ToList();
            }
        }
        public static CommandResult Create(NHAPXUATKHO c)
        {
            using (var context = new GST_MockProjectEntities())
            {

                context.NHAPXUATKHOes.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static CommandResult Edit(NHAPXUATKHO c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var NhapXuatKho = context.NHAPXUATKHOes.First(x => x.ID == c.ID);

                NhapXuatKho.NgayLapPhieu = DateTime.Now;
                NhapXuatKho.KhoID = c.KhoID;
                NhapXuatKho.LoaiPhieu = c.LoaiPhieu.Value;
                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }

        //lấy tất cã các sản phẩm
        //public static List<SANPHAM> GetSanPham(string name)
        //{
        //    using (var context = new GST_MockProjectEntities())
        //    {
        //        return context.SANPHAMs
        //            .Where(x => x.TenSP.Contains(name)).ToList();

        //    }
        //}
        public static List<SANPHAM> GetSanPham()
        {

            using (var context = new GST_MockProjectEntities())
            {
                return context.SANPHAMs.AsNoTracking().ToList();
            }
        }
    }
}
