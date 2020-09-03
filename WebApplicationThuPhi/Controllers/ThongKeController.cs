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
    [Authorize(Roles = "Admin")]
    public class ThongKeController : Controller
    {
        private SoLieuNhapLieuService _soLieuNhapLieuService = new SoLieuNhapLieuService();
        // GET: ThongKe
        public ActionResult Index()
        {
            return View(new List<SoLieuNhapLieu>());
        }
        [HttpPost]
        public ActionResult Index(ExportModel model)
        {
            var data = _soLieuNhapLieuService.GetSoLieuNhapLieusXuatFile(model.fromDate,
                model.toDate, model.fromNumber, model.toNumber);

            return View(data);
        }

        public ActionResult Export()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Export(ExportModel model)
        {
            var tenFile = model.TenFile + DateTime.Now.ToString("yyyyMMdd HH-mm") + ".xlsx";
            var data = _soLieuNhapLieuService.GetSoLieuNhapLieusXuatFile(model.fromDate,
                model.toDate, model.fromNumber, model.toNumber);

            if (model.fromDate.HasValue == false || model.toDate.HasValue == false)
            {
                model.fromDate = data.Min(_ => _.NgayNhap);
                model.toDate = data.Max(_ => _.NgayNhap);
            }

            var filePath =
                ThongKeService.ExportFileThongKe(tenFile, model.TenNguoiThu, model.fromDate.Value, model.toDate.Value, data);
            var mimeType = MimeMapping.GetMimeMapping(filePath);

            byte[] stream = System.IO.File.ReadAllBytes(filePath);
            return File("~/XuatFiles/" + tenFile, mimeType, tenFile);
        }
    }
}