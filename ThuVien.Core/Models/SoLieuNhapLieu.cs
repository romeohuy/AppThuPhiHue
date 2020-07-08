using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien.Core.Models
{
    public class SoLieuNhapLieu
    {
        public string Id { get; set; }
        public string MaPhien { get; set; }
        public long MaHD { get; set; }
        public string TenDonVi { get; set; }
        public string TenSanPham { get; set; }
        public long SoTien { get; set; }
        public string GhiChu { get; set; }
        public string LoaiPhi { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
