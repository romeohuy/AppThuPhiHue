using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.Core.Models;
using ThuVien.Core.Services;
using WebApplicationThuPhi.Models;

namespace WebApplicationThuPhi.Controllers
{
    public class NhapLieuController : Controller
    {
        private SoLieuNhapLieuService _soLieuNhapLieuService = new SoLieuNhapLieuService();

        private DonViService _donViService = new DonViService();
        // GET: NhapLieu
        public ActionResult Index()
        {
            ViewBag.SoHD = _soLieuNhapLieuService.GetMaxMaHD() + 1;
            return View(_soLieuNhapLieuService.GetSoLieuNhapLieuHienTai());
        }
        [HttpPost]
        public ActionResult Create(NhapLieuModel model)
        {
            var solieu = new SoLieuNhapLieu()
            {
                Id = Guid.NewGuid().ToString(),
                MaHD = model.MaHD,
                MaPhien = model.MaPhien,
                TenDonVi = model.TenDonVi,
                TenSanPham = model.TenSanPham,
                NgayNhap = DateTime.Now,
                SoTien = model.SoTien,
                GhiChu = model.GhiChu,
                LoaiPhi = model.LoaiPhi
            };
            _soLieuNhapLieuService.Insert(solieu);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _soLieuNhapLieuService.Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult GetDonVis(string term)
        {
            var donViDoanhNghieps = _donViService.GetDonViDoanhNghieps().Where(_=>_.TenTimKiem.ToLower().Contains(term.ToLower())).OrderBy(_=>_.TenTimKiem);
            return Json(donViDoanhNghieps.Select(_=> new { TenTimKiem = _.TenTimKiem, MaDonVi = _.Id }), JsonRequestBehavior.AllowGet);
        }
    }
}