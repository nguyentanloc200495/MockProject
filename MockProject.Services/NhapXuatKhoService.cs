using MockProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;

namespace MockProject.DataBase
{
    public class NhapXuatKhoService
    {
        public static PagedSearchList<WAREHOUSE_TRANSACTION> Search(int? Id, int? Khoid, WarehouseTransaction_Type? Loaiphieu, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.WAREHOUSE_TRANSACTION
                    .Include(x => x.USER)
                    .Include(x => x.WAREHOUSE).AsNoTracking().AsQueryable();
               
                if (Id.HasValue)
                {
                    query = query.Where(x => x.ID == Id);
                }
                if (Khoid.HasValue)
                {
                    query = query.Where(x => x.Warehouse_ID == Khoid);
                }
                if (Loaiphieu.HasValue)
                {
                    query = query.Where(x => x.Type == Loaiphieu);

                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<WAREHOUSE_TRANSACTION>(query, pageIndex, pageSize);
            }
        }
        public static WAREHOUSE_TRANSACTION GetById(long id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.WAREHOUSE_TRANSACTION.AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.ID == id);
            }
        }



        public static WAREHOUSETRANSACTION_DETAIL GetByIdCT(int idSP, int idPNXK)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.WAREHOUSETRANSACTION_DETAIL.AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.Product_ID == idSP && x.WarehouseTransaction_ID == idPNXK);
            }
        }
        public static List<WAREHOUSE> GetAllKho()
        {
            using (var context = new GST_MockProjectEntities())
            {
                return context.WAREHOUSEs.AsNoTracking().ToList();
            }
        }
        public static CommandResult Create(WAREHOUSE_TRANSACTION c)
        {
            using (var context = new GST_MockProjectEntities())
            {

                c.CreateDate = DateTime.Now;
                c.CreateBy = UserService.GetUserInfo().ID;
                context.WAREHOUSE_TRANSACTION.Add(c);
                context.SaveChanges();
                //TODO
                //context.Log(c, LogType.BankBranch_Create, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static CommandResult Edit(WAREHOUSE_TRANSACTION c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var NhapXuatKho = context.WAREHOUSE_TRANSACTION.First(x => x.ID == c.ID);

                NhapXuatKho.UpdateDate = DateTime.Now;
                NhapXuatKho.UpdateBy = UserService.GetUserInfo().ID;
                NhapXuatKho.Warehouse_ID = c.Warehouse_ID;
                NhapXuatKho.Type = c.Type.Value;
                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }

        //lấy tất cã các sản phẩm

        public static List<PRODUCT> GetSanPham()
        {

            using (var context = new GST_MockProjectEntities())
            {
                return context.PRODUCTs.AsNoTracking().ToList();
            }
        }
        // CreateCTPNX
        public static CommandResult CreateCTPNX(WAREHOUSETRANSACTION_DETAIL c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                c.WarehouseTransaction_ID = 1;
                context.WAREHOUSETRANSACTION_DETAIL.Add(c);
                context.SaveChanges();
                return new CommandResult();
            }
        }
        public static CommandResult EditCTPNX(WAREHOUSETRANSACTION_DETAIL c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var CTPHIEUNHAPXUATKHO = context.WAREHOUSETRANSACTION_DETAIL.First(x => x.WarehouseTransaction_ID == c.WarehouseTransaction_ID && x.Product_ID == c.Product_ID);

                CTPHIEUNHAPXUATKHO.Quantity = c.Quantity;
                CTPHIEUNHAPXUATKHO.Amount = c.Amount;

                context.SaveChanges();

                return new CommandResult();
            }
        }

        public static PagedSearchList<WAREHOUSETRANSACTION_DETAIL> SearchCTPNX(int? Id, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.WAREHOUSETRANSACTION_DETAIL
                    .Include(x => x.PRODUCT)
                    .Include(x => x.WAREHOUSE_TRANSACTION).AsNoTracking().AsQueryable();

                if (Id.HasValue)
                {
                    query = query.Where(x => x.WarehouseTransaction_ID == Id);
                }

                query = query.OrderByDescending(x => x.WarehouseTransaction_ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<WAREHOUSETRANSACTION_DETAIL>(query, pageIndex, pageSize);
            }
        }

        public static CommandResult UpdateSoLuongSanPham(int KhoId, int SanphamId, decimal Soluong)
        {
            using (var context = new GST_MockProjectEntities())
            {
                try
                {

                    var CTKho = context.WAREHOUSE_DETAIL.First(x => x.Warehouse_ID == KhoId && x.Product_ID == SanphamId);

                    CTKho.Quantity = CTKho.Quantity + Soluong;

                    context.SaveChanges();

                }
                catch (Exception e)
                {
                    var c = new WAREHOUSE_DETAIL();

                    c.Warehouse_ID = KhoId;
                    c.Product_ID = SanphamId;
                    c.Quantity = Soluong;
                    context.WAREHOUSE_DETAIL.Add(c);
                    context.SaveChanges();


                }


                return new CommandResult();
            }
        }

        public static CommandResult UpdateSoLuongSanPham2(int KhoId, int SanphamId, decimal Soluong)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var CTKho = context.WAREHOUSE_DETAIL.First(x => x.Warehouse_ID == KhoId && x.Product_ID == SanphamId);
                CTKho.Quantity = CTKho.Quantity - Soluong;
                context.SaveChanges();

                return new CommandResult();
            }
        }


        //hàm tạo mới 1 hóa đơn sẽ tự tạo các chi tiết trong nhập xuất kho
        public static CommandResult Create1(WAREHOUSE_TRANSACTION c, int userId, string[] listSanphamID, string[] listSoluong, string[] listGia)

        {
            using (var context = new GST_MockProjectEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = context.Database.BeginTransaction())
                {

                    decimal? Tongtien = 0;
                    c.CreateBy = userId;
                    c.CreateDate = DateTime.Now;
                    context.WAREHOUSE_TRANSACTION.Add(c);
                    context.SaveChanges();



                    //luu chi tiet san pham
                    var listwtd = new List<WAREHOUSETRANSACTION_DETAIL>();
                    for (var i = 0; i < listSanphamID.Length; i++)
                    {
                        var SanphamID = int.Parse(listSanphamID[i]);
                        var Soluong = decimal.Parse(listSoluong[i]);
                        decimal? price = null;
                        if (listGia[i] != "")
                        {
                            price = decimal.Parse(listGia[i]);
                            Tongtien = Tongtien + price;
                        }
                        if (listSoluong[i] != "")
                        {

                            UpdateSoLuongSanPham(c.Warehouse_ID, SanphamID, Soluong);
                        }
                        var wtd = new WAREHOUSETRANSACTION_DETAIL()
                        {
                            WarehouseTransaction_ID = c.ID,
                            Product_ID = SanphamID,
                            Quantity = Soluong,
                            Amount = price
                        };
                        listwtd.Add(wtd);


                    }

                    context.WAREHOUSETRANSACTION_DETAIL.AddRange(listwtd);

                    context.SaveChanges();
                    dbTran.Commit();
                    return new CommandResult();
                }
            }
        }

        //chỉnh sửa hóa đơn + ch tiết hóa đơn
        public static CommandResult Edit1(WAREHOUSE_TRANSACTION c, int userId, string[] listSanphamID, string[] listSoluong, string[] listGia)
        {
            using (var context = new GST_MockProjectEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = context.Database.BeginTransaction())

{


                    var Nhapxuatkho =
                        context.WAREHOUSE_TRANSACTION.Include(x => x.WAREHOUSETRANSACTION_DETAIL).First(x => x.ID == c.ID);
                    //xóa số lượng sản phẩm đang được chỉnh sửa ở trong chi tiết kho
             
                    for (int i = 0; i < listSanphamID.Length; i++)
                    {
                        var SanphamID = int.Parse(listSanphamID[i]);
                        var Soluong =
                            context.WAREHOUSETRANSACTION_DETAIL.Where(y => y.WarehouseTransaction_ID == c.ID && y.Product_ID==SanphamID).First().Quantity;
                        
                        //var Soluong2 = int.Parse(Soluong);
                        UpdateSoLuongSanPham2(c.Warehouse_ID, SanphamID, Soluong.Value);

                    }
                    //xóa danh sách sản phẩm cũ
                    var listwtdOld = Nhapxuatkho.WAREHOUSETRANSACTION_DETAIL;
                    context.WAREHOUSETRANSACTION_DETAIL.RemoveRange(listwtdOld);
                    var listSoluongOld =
                        context.WAREHOUSETRANSACTION_DETAIL.Where(x => x.WarehouseTransaction_ID == c.ID).ToList();
                    
                    //thêm danh sách sản phẩm mới
                    var listwtd = new List<WAREHOUSETRANSACTION_DETAIL>();
                    for (int i = 0; i < listSanphamID.Length; i++)
                    {

                        var SanphamID = int.Parse(listSanphamID[i]);
                        var Soluong = int.Parse(listSoluong[i]);
                        decimal? Giaban = null;
                    
                        if (listGia[i] != "")
                        {
                            Giaban = decimal.Parse(listGia[i]);

                        }
                        var cthd = new WAREHOUSETRANSACTION_DETAIL()
                        {
                            Product_ID = SanphamID,
                            Quantity = Soluong,
                            WarehouseTransaction_ID = c.ID,
                            Amount = Giaban
                        };
                        listwtd.Add(cthd);

                    }




                    context.WAREHOUSETRANSACTION_DETAIL.AddRange(listwtd);
                    //Chỉnh sửa lại thông tin hóa đơn khi sửa
                    Nhapxuatkho.UpdateBy = userId;
                    Nhapxuatkho.UpdateDate = DateTime.Now;
                    Nhapxuatkho.Type = c.Type;
                    context.SaveChanges();
                    dbTran.Commit();

                    return new CommandResult(ResultCode.Success, "Sửa thành công");
                }
            }
        }

        //hàm lấy các CTPhieuNhapXuatKho có mã Nhapxuat

        public static List<WAREHOUSETRANSACTION_DETAIL> GetListCTNXK(int id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                return
                    context.WAREHOUSETRANSACTION_DETAIL.Include(x => x.PRODUCT).AsNoTracking()
                        .Where(x => x.WarehouseTransaction_ID == id)
                        .ToList();
            }
        }
    }
}
