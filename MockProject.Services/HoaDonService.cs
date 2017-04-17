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
        public static PagedSearchList<BILL> Search(int? Id, int? NhanvienId, DateTime? Tungay, DateTime? Denngay, int pageIndex)
        {
            using (var context = new GST_MockProjectEntities())

            {
                var query = context.BILLs
                    .Include(x => x.BILL_DETAIL)
                   
                    .AsNoTracking().AsQueryable();

                if (Id.HasValue)
                {
                    query = query.Where(x => x.ID == Id);
                }
                if (NhanvienId.HasValue)
                {
                    query = query.Where(x => x.CreateBy == NhanvienId);
                }
                if (Tungay.HasValue)
                {
                    query = query.Where(x => x.CreateDate >= Tungay);

                }
                if (Denngay.HasValue)
                {
                    query = query.Where(x => x.CreateDate <= Denngay);

                }
                query = query.OrderByDescending(x => x.ID);
                int pageSize = 10;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<BILL>(query, pageIndex, pageSize);
            }
        }
        public static BILL GetById(long id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var query = context.BILLs.AsNoTracking().AsQueryable();
                return query.FirstOrDefault(x => x.ID == id);
            }
        }
        public static CommandResult Create(BILL c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                c.CreateBy = UserService.GetUserInfo().ID; ;
                c.CreateDate = DateTime.Now;
                context.BILLs.Add(c);
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
        public static CommandResult Edit(WAREHOUSE_TRANSACTION c)
        {
            using (var context = new GST_MockProjectEntities())
            {
                var NhapXuatKho = context.WAREHOUSE_TRANSACTION.First(x => x.ID == c.ID);

                NhapXuatKho.CreateDate = DateTime.Now;
                NhapXuatKho.Warehouse_ID = c.Warehouse_ID;
                NhapXuatKho.Type = c.Type.Value;
                context.SaveChanges();
                //TODO
                // context.Log(c, LogType.Customer_Edit, userId, "", HttpContext.Current.Request.Form);
                return new CommandResult();
            }
        }
        public static List<USER> GetNhanvien()
        {

            using (var context = new GST_MockProjectEntities())
            {
                return context.USERs.AsNoTracking().ToList();
            }
        }



        //phần mới
        public static List<PRODUCT> GetSanPham(string name)
        {
            using (var context = new GST_MockProjectEntities())
            {
                return context.PRODUCTs.Where(x => x.ProductName.Contains(name)).AsNoTracking().ToList();
            }
        }

        //hàm tạo mới 1 hóa đơn sẽ tự tạo các chi tiết hóa đơn
        public static CommandResult Create1(BILL c, int userId, string[] listSanphamID, string[] listSoluong, string[] listGia)

        {
            using (var context = new GST_MockProjectEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = context.Database.BeginTransaction())
                {

                    decimal? Tongtien = 0;

                    c.CreateBy = userId;
                    c.Amount = 0;
                    c.CreateDate = DateTime.Now;
                   
                    context.BILLs.Add(c);
                    context.SaveChanges();



                    //luu chi tiet san pham
                    var listwtd = new List<BILL_DETAIL>();
                    for (var i = 0; i < listSanphamID.Length; i++)
                    {

                        decimal? price = null;
                        if (listGia[i] != "")
                        {
                            price = decimal.Parse(listGia[i]);
                            Tongtien = Tongtien + price;
                        }

                        var wtd = new BILL_DETAIL()
                        {
                            Bill_ID = c.ID,
                            Product_ID = int.Parse(listSanphamID[i]),
                            Quantity = int.Parse(listSoluong[i]),
                            Amount = price
                        };
                        listwtd.Add(wtd);


                    }

                    context.BILL_DETAIL.AddRange(listwtd);
                    c.Amount = Tongtien;
                    context.SaveChanges();
                    dbTran.Commit();
                    return new CommandResult();
                }
            }
        }


        //hàm lấy các CTHD có mã hóa đơn

        public static List<BILL_DETAIL> GetListCTHD(int id)
        {
            using (var context = new GST_MockProjectEntities())
            {
                return
                    context.BILL_DETAIL.Include(x => x.PRODUCT).AsNoTracking()
                        .Where(x => x.Bill_ID == id)
                        .ToList();
            }
        }


        //chỉnh sửa hóa đơn + ch tiết hóa đơn
        //public static CommandResult Edit1(HOADON c, int userId, int flag, long? warehousePlace, List<string> fileList, string[] listProductId, string[] listQuantity, string[] listQuantityApprove, string[] listPrice)
       public static CommandResult Edit1(BILL c, int userId, string[] listSanphamID, string[] listSoluong, string[] listGia)
        {
            using (var context = new GST_MockProjectEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = context.Database.BeginTransaction())
                {
                    decimal? Tongtien = 0;

                    var HoaDon =
                        context.BILLs.Include(x => x.BILL_DETAIL).First(x => x.ID == c.ID);

         

                    //xóa danh sách sản phẩm cũ
                    var listwtdOld = HoaDon.BILL_DETAIL;
                    context.BILL_DETAIL.RemoveRange(listwtdOld);

                    //thêm danh sách sản phẩm mới
                    var listwtd = new List<BILL_DETAIL>();
                    for (int i = 0; i < listSanphamID.Length; i++)
                    {
                        var SanphamID = int.Parse(listSanphamID[i]);
                        var Soluong = int.Parse(listSoluong[i]);
                        decimal? Giaban = null;
                        if (listGia[i] != "")
                        {
                            Giaban = decimal.Parse(listGia[i]);
                            Tongtien = Tongtien + Giaban;
                        }
                        var cthd = new BILL_DETAIL()
                        {
                            Product_ID = SanphamID,
                            Quantity = Soluong,
                            Bill_ID = c.ID,
                            Amount = Giaban
                        };
                        listwtd.Add(cthd);                  
                        
                    }
                    context.BILL_DETAIL.AddRange(listwtd);
                    //Chỉnh sửa lại thông tin hóa đơn khi sửa
                    HoaDon.UpdateBy = userId;
                    HoaDon.UpdateDate = DateTime.Now;
                    HoaDon.Amount = Tongtien;

                    context.SaveChanges();
                    dbTran.Commit();
                  
                    return new CommandResult(ResultCode.Success, "Sửa thành công");
                }
            }
        }


    }
}
