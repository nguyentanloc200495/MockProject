using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MockProject.DataBase;

namespace MockProject.Services
{
    public class HoaDonService
    {
        public static PagedSearchList<HOADON> Search(int? Id, int? NhanvienId, DateTime? Tungay, DateTime? Denngay, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.HOADONs
                    .Include(x => x.CTHDs)
                    .Include(x => x.NHANVIEN)
                    .AsNoTracking().AsQueryable();

                if (Id.HasValue)
                {
                    query = query.Where(x => x.ID == Id);
                }
                if (NhanvienId.HasValue)
                {
                    query = query.Where(x => x.NhanVienID == NhanvienId);
                }
                if (Tungay.HasValue)
                {
                    query = query.Where(x => x.NgayLap >= Tungay);

                }
                if (Denngay.HasValue)
                {
                    query = query.Where(x => x.NgayLap <= Denngay);

                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<HOADON>(query, pageIndex, pageSize);
            }
        }
        public static HOADON GetById(long id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.HOADONs.AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.ID == id);
            }
        }
        public static CommandResult Create(HOADON c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                c.NhanVienID = UserService.GetUserInfo().ID; ;
                c.NgayLap = DateTime.Now;
                context.HOADONs.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
            //using (var context = new GST_MockProjectEntities())
            //{
            //    c.NhanVienID = 1;
            //    c.NgayLap = DateTime.Now;
            //    context.HOADONs.Add(c);
            //    context.SaveChanges();

            //    return new CommandResult();
            //}
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
        public static List<NHANVIEN> GetNhanvien()
        {

            using (var context = new GST_MockProjectEntities())
            {
                return context.NHANVIENs.AsNoTracking().ToList();
            }
        }



        //phần mới
        public static List<SANPHAM> GetSanPham(string name)
        {
            using (var context = new GST_MockProjectEntities())
            {
                return context.SANPHAMs.Where(x => x.TenSP.Contains(name)).AsNoTracking().ToList();
            }
        }
        public static CommandResult Create1(HOADON c, int userId, string[] listSanphamID, string[] listSoluong, string[] listGia)
        //flag = 0: luu
        {
            using (var context = new GST_MockProjectEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = context.Database.BeginTransaction())
                {

                    c.NhanVienID = userId;
                    c.TongTien = 0;
                    c.NgayLap = DateTime.Now;
                    context.HOADONs.Add(c);
                    context.SaveChanges();



                    //luu chi tiet san pham
                    var listwtd = new List<CTHD>();
                    for (var i = 0; i < listSanphamID.Length; i++)
                    {

                        decimal? price = null;
                        if (listGia[i] != "")
                            price = decimal.Parse(listGia[i]);

                        var wtd = new CTHD()
                        {
                            HoaDonID = c.ID,
                            SanPhamID = int.Parse(listSanphamID[i]),
                            SoLuong = int.Parse(listSoluong[i]),
                            GiaBan = price
                        };
                        listwtd.Add(wtd);


                    }

                    context.CTHDs.AddRange(listwtd);

                    context.SaveChanges();
                    dbTran.Commit();
                    return new CommandResult();
                }
            }
        }



    }
}
