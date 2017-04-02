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


        //Chi tiết phiếu nhập xuất
        public ActionResult CreateCTPNX(int id)
        {
            //TODO     
            ViewBag.NhapXuatKhoID = id;
            ViewBag.SanPhamID = new SelectList(NhapXuatKhoService.GetSanPham(), "ID", "TenSP");
            return PartialView("_CreateEditCTPNX");
        }

        [HttpGet]
        public ActionResult EditCTPNX(int idSP,int idPNXK)
        {
            
            var model = NhapXuatKhoService.GetByIdCT(idSP,idSP);
            //TODO        
            return PartialView("_CreateEditCTPNX");
        }
        [HttpPost]
        //[AuthorizeAdmin(Permissions = new Permission[] { Permission.Floor_Create, Permission.Floor_Edit })]
        public ActionResult CreateEditCTPNX(NHAPXUATKHO model)
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
        //public JsonResult GetSanPham(string name)
        //{
        //    var data = NhapXuatKhoService.GetSanPham(name);
        //    return Json(data.Select(x => new { Id = x.ID, Name = x.TenSP}).ToList(), JsonRequestBehavior.AllowGet);
        //}

    }
}