using CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.DataBase;
using MockProject.Models;
using MockProject.Services;

namespace MockProject.Controllers
{
    public class NhapXuatKhoController : Controller
    {
        // GET: NhapKho
        public ActionResult Index()
        {
            ViewBag.Warehouse_ID = new SelectList(NhapXuatKhoService.GetAllKho(), "ID", "WarehouseName");
            ViewBag.Type = WebUtil.GetEnumSelectList<WarehouseTransaction_Type>();
            return View();
        }
        public ActionResult List(NhapXuatKhoSearchModel searchModel)
        {
            var pagedList = NhapXuatKhoService.Search(searchModel.ID, searchModel.Warehouse_ID, searchModel.Type, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
       [HttpGet]
        public ActionResult Create1()
        {
            //TODO     
            ViewBag.Warehouse_ID = new SelectList(NhapXuatKhoService.GetAllKho(), "ID", "WarehouseName");
            ViewBag.Type = WebUtil.GetEnumSelectList<WarehouseTransaction_Type>();
            return View("CreateEdit1");
        }

        [HttpGet]
        public ActionResult Edit1(int id)
        {
            ViewBag.Warehouse_ID = new SelectList(NhapXuatKhoService.GetAllKho(), "ID", "WarehouseName");
            ViewBag.Type = WebUtil.GetEnumSelectList<WarehouseTransaction_Type>();
            var model = NhapXuatKhoService.GetById(id);
            //TODO        
            return View("CreateEdit1", model);
        }
        [HttpPost]
        public ActionResult CreateEdit1(WAREHOUSE_TRANSACTION model)
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
                var result = NhapXuatKhoService.Create1(model, user.ID, listSanPhamID, listSoLuong, listGia);
                return
                Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
            else
            {
                var user = UserService.GetUserInfo();
                var result = NhapXuatKhoService.Edit1(model, user.ID, listSanPhamID, listSoLuong, listGia);
                return
                Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);

            }

        }

        [ChildActionOnly]
        public ActionResult GetViewListProduct(int id)
        {
            var model = NhapXuatKhoService.GetListCTNXK(id);
            return PartialView("_ListCTNXK", model);
        }


    }
}