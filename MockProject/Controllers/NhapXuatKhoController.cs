using CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MockProject.DataBase;
using MockProject.Models;

namespace MockProject.Controllers
{
    public class NhapXuatKhoController : Controller
    {
        // GET: NhapKho
        public ActionResult Index()
        {
            ViewBag.KhoID = new SelectList(NhapXuatKhoService.GetAllKho(), "ID", "TenKho");
            ViewBag.LoaiPhieu = WebUtil.GetEnumSelectList<LoaiPhieuNhapXuatKho1>();
            return View();
        }
        public ActionResult List(NhapXuatKhoSearchModel searchModel)
        {
            var pagedList = NhapXuatKhoService.Search(searchModel.ID, searchModel.KhoID, searchModel.LoaiPhieu, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            //TODO     
            ViewBag.KhoID = new SelectList(NhapXuatKhoService.GetAllKho(), "ID", "TenKho");
            ViewBag.LoaiPhieu = WebUtil.GetEnumSelectList<LoaiPhieuNhapXuatKho1>();
            return View("CreateEdit");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.KhoID = new SelectList(NhapXuatKhoService.GetAllKho(), "ID", "TenKho");
            ViewBag.LoaiPhieu = WebUtil.GetEnumSelectList<LoaiPhieuNhapXuatKho1>();
            var model = NhapXuatKhoService.GetById(id);
            //TODO        
            return View("CreateEdit", model);
        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
        public ActionResult CreateEdit(NHAPXUATKHO model)
        {
            if (model.ID == 0)
            {

                var result = NhapXuatKhoService.Create(model);
                return

                Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
            else
            {

                var result = NhapXuatKhoService.Edit(model);
                return
                 Json(
                new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Index", new { id = model.ID }) },
                JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        //tạo phiếu nhập xuất kho
        public ActionResult CreateCTPNX(int id)
        {
           
          
            ViewBag.NhapXuatKhoID = id;
            //ViewData["ID"]=id;
            ViewBag.SanPhamID = new SelectList(NhapXuatKhoService.GetSanPham(), "ID", "TenSP");
     
            return PartialView("_CreateCTPNX");
        }
        [HttpPost]
        public ActionResult CreateCTPNX(CTPHIEUNHAPXUATKHO model)
        {
            var result = NhapXuatKhoService.CreateCTPNX(model);
            return
                Json(
               new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Edit", new { id = model.NhapXuatKhoID }) },
               JsonRequestBehavior.AllowGet);


        }
        public ActionResult UpdateSoLuongSanPham(int KhoID, int SanPhamID,decimal SoLuong)
        {
            var result = NhapXuatKhoService.UpdateSoLuongSanPham(KhoID, SanPhamID, SoLuong);
            return
                 Json(
                 new RedirectCommand() { Code = result.Code, Message = result.Message },
                 JsonRequestBehavior.AllowGet);
        }

        //chưa xử lý được vì không biết truyền 2id từ view vào contrler

        //[HttpGet]
        //public ActionResult EditCTPNX(int idSP,int idPNXK)
        //{

        //    var model = NhapXuatKhoService.GetByIdCT(idSP,idSP);
        //    return PartialView("_CreateCTPNX");
        //}
        //[HttpPost]

        //public ActionResult CreateEditCTPNX(CTPHIEUNHAPXUATKHO model)
        //{
        //    if (model.NhapXuatKhoID == 0 && model.SanPhamID == 0)
        //    {

        //        var result = NhapXuatKhoService.CreateCTPNX(model);
        //        return
        //            Json(
        //           new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Edit", new { id = model.NhapXuatKhoID }) },
        //           JsonRequestBehavior.AllowGet);

        //    }
        //    else
        //    {

        //        var result = NhapXuatKhoService.EditCTPNX(model);
        //        return
        //         Json(
        //        new RedirectCommand() { Code = result.Code, Message = result.Message, Url = Url.Action("Edit", new { id = model.NhapXuatKhoID }) },
        //        JsonRequestBehavior.AllowGet);
        //    }
        //}


        public ActionResult ListCTPNX(CTPNXSearchModel searchModel)
        {
            var pagedList = NhapXuatKhoService.SearchCTPNX(searchModel.ID, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_ListCTPNX", pagedList);
        }

    }
}