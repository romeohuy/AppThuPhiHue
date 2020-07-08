using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationThuPhi.Models
{
    public class NhapLieuModel
    {
        public string MaPhien { get; set; }
        public long MaHD { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public long SoTien { get; set; }
        public string GhiChu { get; set; }
        public string LoaiPhi { get; set; }
        public DateTime NgayNhap { get; set; }

        public List<SelectListItem> LoaiPhis { get; set; }
    }
}