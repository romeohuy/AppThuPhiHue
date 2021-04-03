using System;
using System.Linq;
using System.Web.Mvc;
using ThuVien.Core.Models;
using ThuVien.Core.Services;
using WebApplicationThuPhi.Models;

namespace WebApplicationThuPhi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NhapLieuController : Controller
    {
        private SoLieuNhapLieuService _soLieuNhapLieuService = new SoLieuNhapLieuService();
        private DanhMucSanPhamService _danhMucSanPhamService = new DanhMucSanPhamService();
        private DonViService _donViService = new DonViService();
        // GET: NhapLieu
        public ActionResult Index()
        {
            ViewBag.SoHD = _soLieuNhapLieuService.GetCurrentMaHD() + 1;
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

        public ActionResult Edit(string id)
        {
            return View(_soLieuNhapLieuService.GetSoLieuNhapLieuById(id));
        }
        [HttpPost]
        public ActionResult Edit(SoLieuNhapLieu model)
        {
            _soLieuNhapLieuService.Update(model);
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
            var donViDoanhNghieps = _donViService.GetDonViDoanhNghieps().Where(_ => _.TenTimKiem.ToLower().Contains(term.ToLower())).OrderBy(_ => _.TenTimKiem);
            return Json(donViDoanhNghieps.Select(_ => new { TenTimKiem = _.TenDonVi, MaDonVi = _.Id }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSanPhams(string term)
        {
            var sanPhams = _danhMucSanPhamService.GetDanhMucSanPhams().Where(_ => _.TenTimKiem.ToLower().Contains(term.ToLower())).OrderBy(_ => _.TenTimKiem);
            return Json(sanPhams.Select(_ => new { TenTimKiem = _.TenSanPham, MaSanPham = _.Id }), JsonRequestBehavior.AllowGet);
        }
    }
}