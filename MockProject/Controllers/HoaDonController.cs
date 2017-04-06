using MockProject.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.Models;
using MockProject.Services;

namespace MockProject.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: HoaDon
        public ActionResult Index()
        {
            ViewBag.NhanVienID = new SelectList(HoaDonService.GetNhanvien(),"ID", "TenNV");
            return View();
        }
        public ActionResult List(HoaDonSearchModel searchModel)
        {
            var pagedList = HoaDonService.Search(searchModel.ID, searchModel.NhanVienID, searchModel.TuNgay,searchModel.DenNgay, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.NhanVienID = new SelectList(HoaDonService.GetNhanvien(), "ID", "TenNV");
            return View("Create");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.NhanVienID = new SelectList(HoaDonService.GetNhanvien(), "ID", "TenNV");
            var model = HoaDonService.GetById(id);
            //TODO        
            return View("Edit", model);
        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
        public ActionResult CreateEdit(HOADON model)
        {
            int Nhanvienid= UserService.GetUserInfo().ID;
            if (model.ID != 0)
            {
                //var result = NhapXuatKhoService.Edit(model);
                //return
                // Json(
                //new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                //JsonRequestBehavior.AllowGet);
                return View();

                
            }
            else
            {
                var result = HoaDonService.Create(model);
                return
                Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult CreateCTHD()
        {
           
            return View("_EditCTHD");
        }
        public ActionResult Create1()
        {
            //ViewBag.NhanVienID = new SelectList(HoaDonService.GetNhanvien(), "ID", "TenNV");
            return View("CreateEdit1");
        }
        public JsonResult GetSanPham(string name)
        {
            var data = HoaDonService.GetSanPham(name);
            return Json(data.Select(x => new { Id = x.ID, Name = x.TenSP }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateEdit1(HOADON model)
        {
            if (Request["txtProductIdRow"] == null)
            {
                return Json(new CommandResult() { Code = ResultCode.Fail, Message = "Xin vui lòng thêm sản phẩm " }, JsonRequestBehavior.AllowGet);
            }
            var listSanPhamID = Request.Form.GetValues("txtProductIdRow");
            var listSoLuong = Request.Form.GetValues("txtQuantityRow");
            var listGia = Request.Form.GetValues("txtPriceRow");
            if (model.ID == 0)
            {
                var user = UserService.GetUserInfo();
                var result = HoaDonService.Create1(model, user.ID, listSanPhamID, listSoLuong, listGia);
                return
                Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var user = UserService.GetUserInfo();
                //var result = WarehouseImportTransactionService.Edit(model, user.Id, flag, warehousePlace, listFile, listProductId, listQuantity, listQuantityApprove, listPrice);
                //return
                //Json(
                //new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                //JsonRequestBehavior.AllowGet);
                return View();
            }
           
        }
    }
}