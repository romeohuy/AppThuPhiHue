using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThuVien.Core.Services;
using WebApplicationThuPhi.Models;

namespace WebApplicationThuPhi.Controllers
{
    public class ThongKeController : Controller
    {
        private SoLieuNhapLieuService _soLieuNhapLieuService = new SoLieuNhapLieuService();
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Export(ExportModel model)
        {
            var tenFile = model.TenFile + DateTime.Now.ToString("yyyyMMdd HH-mm") + ".xlsx";
            var data = _soLieuNhapLieuService.GetSoLieuNhapLieusXuatFile(model.fromDate,
                model.toDate);

            ////before your loop
            //var csv = new StringBuilder();
            //var header = "SoHD,Ten don vi,Ten Hang, So Tien, Ghi Chu";
            //csv.AppendLine(header);
            //foreach (var itemLieu in data)
            //{
            //    var newLine = $"{itemLieu.MaHD},{itemLieu.TenDonVi},{itemLieu.TenSanPham},{itemLieu.SoTien},{itemLieu.GhiChu}";
            //    csv.AppendLine(newLine);

            //}

            //var filePath = AppDomain.CurrentDomain.BaseDirectory + "\\XuatFiles\\" + tenFile;
            ////after your loop
            //System.IO.File.WriteAllText(filePath, csv.ToString());
            //String file = Server.MapPath("~/XuatFiles/"+tenFile);
            var filePath =
                ThongKeService.ExportFileThongKe(tenFile, model.TenNguoiThu, model.fromDate, model.toDate, data);
            String mimeType = MimeMapping.GetMimeMapping(filePath);

            byte[] stream = System.IO.File.ReadAllBytes(filePath);
            return File("~/XuatFiles/" + tenFile, mimeType, tenFile);
        }
    }
}