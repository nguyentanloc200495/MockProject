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



        public static CTPHIEUNHAPXUATKHO GetByIdCT(int idSP,int idPNXK)
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
    
        public static List<SANPHAM> GetSanPham()
        {

            using (var context = new GST_MockProjectEntities())
            {
                return context.SANPHAMs.AsNoTracking().ToList();
            }
        }
        // CreateCTPNX
        public static CommandResult CreateCTPNX(CTPHIEUNHAPXUATKHO c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                c.NhapXuatKhoID = 1;
                context.CTPHIEUNHAPXUATKHOes.Add(c);
                context.SaveChanges();
                return new CommandResult();
            }
        }
        public static CommandResult EditCTPNX(CTPHIEUNHAPXUATKHO c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var CTPHIEUNHAPXUATKHO = context.CTPHIEUNHAPXUATKHOes.First(x => x.NhapXuatKhoID == c.NhapXuatKhoID && x.SanPhamID==c.SanPhamID);

                CTPHIEUNHAPXUATKHO.SoLuong = c.SoLuong;
                CTPHIEUNHAPXUATKHO.DonGia = c.DonGia;
               
                context.SaveChanges();
                
                return new CommandResult();
            }
        }

        public static PagedSearchList<CTPHIEUNHAPXUATKHO> SearchCTPNX(int? Id, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.CTPHIEUNHAPXUATKHOes
                    .Include(x => x.SANPHAM)
                    .Include(x => x.NHAPXUATKHO).AsNoTracking().AsQueryable();

                if (Id.HasValue)
                {
                    query = query.Where(x => x.NhapXuatKhoID == Id);
                }
               
                query = query.OrderByDescending(x => x.NhapXuatKhoID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<CTPHIEUNHAPXUATKHO>(query, pageIndex, pageSize);
            }
        }

        public static CommandResult UpdateSoLuongSanPham(int KhoId,int SanphamId,decimal Soluong)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var CTKho = context.CTKHOes.First(x => x.KhoID == KhoId && x.SanPhamID == SanphamId);
                CTKho.SoLuong = CTKho.SoLuong + Soluong;
                context.SaveChanges();

                return new CommandResult();
            }
        }



    }
}
