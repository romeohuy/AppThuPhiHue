using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.Core.Models;
using ThuVien.Core.Services;

namespace WebApplicationThuPhi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DonViController : Controller
    {
        private DonViService _donViService = new DonViService();
        // GET: DonVi
        public ActionResult Index()
        {
            return View(_donViService.GetDonViDoanhNghieps());
        }

        [HttpPost]
        public ActionResult Create(DonViDoanhNghiep model)
        {
            model.Id = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(model.TenTimKiem))
            {
                model.TenTimKiem = $"{model.MaDonVi} - {model.TenDonVi}";
            }
            _donViService.Insert(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            return View(_donViService.GetDonViDoanhNghiep(id));
        }

        [HttpPost]
        public ActionResult Edit(DonViDoanhNghiep model)
        {
            model.TenTimKiem = $"{model.MaDonVi} - {model.TenDonVi}";
            _donViService.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            _donViService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}