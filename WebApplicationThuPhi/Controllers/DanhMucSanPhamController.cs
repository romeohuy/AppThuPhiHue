using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.Core.Models;
using ThuVien.Core.Services;

namespace WebApplicationThuPhi.Controllers
{
    public class DanhMucSanPhamController : Controller
    {
        private DanhMucSanPhamService _danhMucSanPhamService = new DanhMucSanPhamService();
        // GET: DanhMucSanPham
        public ActionResult Index()
        {
            return View(_danhMucSanPhamService.GetDanhMucSanPhams());
        }

        [HttpPost]
        public ActionResult Create(DanhMucSanPham model)
        {
            model.Id = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(model.TenTimKiem))
            {
                model.TenTimKiem = $"{model.MaSanPham} - {model.TenSanPham}";
            }
            _danhMucSanPhamService.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            return View(_danhMucSanPhamService.GetDanhMucSanPham(id));
        }

        [HttpPost]
        public ActionResult Edit(DanhMucSanPham model)
        {
            model.TenTimKiem = $"{model.MaSanPham} - {model.TenSanPham}";
            _danhMucSanPhamService.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            _danhMucSanPhamService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}