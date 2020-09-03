using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationThuPhi.Models
{
    public class ExportModel
    {

        [Display(Name = "Tên file")]
        public string TenFile { get; set; }
        [Display(Name = "Từ ngày")]
        public DateTime? fromDate { get; set; }
        [Display(Name = "Đến ngày")]
        public DateTime? toDate { get; set; }
        [Display(Name = "Tên người thu")]
        public string TenNguoiThu { get; set; }
        [Display(Name = "Từ số HĐ")]
        public long? fromNumber { get; set; }
        [Display(Name = "Đến số HĐ")]
        public long? toNumber { get; set; }
    }
}