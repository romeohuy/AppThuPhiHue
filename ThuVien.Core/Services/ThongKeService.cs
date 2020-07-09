using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ThuVien.Core.Models;

namespace ThuVien.Core.Services
{
    public static class ThongKeService
    {
        public static string ExportFileThongKe(string tenFile, string tenNguoiThu, DateTime fromDate, DateTime toDate, List<SoLieuNhapLieu> dataSoLieuNhapLieus)
        {
            var fileBytes = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateFiles\\TemplateFile.xlsx");
            var pathFileTemp = $"{Path.GetTempFileName()}{DateTime.Now.Ticks}.xlsx";

            File.WriteAllBytes(pathFileTemp, fileBytes);

            FileInfo fileInfo = new FileInfo(pathFileTemp);
            ExcelPackage excelPackage = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.First();
            var rowIndex = 6;
            worksheet.Cells[4, 1].Value = $"(Từ ngày {fromDate.ToString("dd-MM-yyyy")} - {toDate.ToString("dd-MM-yyyy")})";
            worksheet.Cells[4, 1].Style.Font.Italic = true;

            foreach (var soLieu in dataSoLieuNhapLieus)
            {
                worksheet.Cells[rowIndex, 1].Value = soLieu.MaHD;
                worksheet.Cells[rowIndex, 1].Style.Font.Bold = true;

                worksheet.Cells[rowIndex, 2].Value = soLieu.TenDonVi;
                worksheet.Cells[rowIndex, 3].Value = soLieu.TenSanPham;
                worksheet.Cells[rowIndex, 4].Value = soLieu.SoTien;
                worksheet.Cells[rowIndex, 5].Value = soLieu.GhiChu;
                rowIndex++;
            }

            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


            worksheet.Cells[rowIndex + 1, 3].Value = "Tổng tiền:";
            worksheet.Cells[rowIndex + 1, 3].Style.Font.Bold = true;
            worksheet.Cells[rowIndex + 1, 4].Value = dataSoLieuNhapLieus.Sum(_ => _.SoTien).ToString("##,###");
            worksheet.Cells[rowIndex + 1, 4].Style.Font.Bold = true;

            worksheet.Cells[rowIndex + 3, 3 ].Value = $"Quy Nhơn, ngày  tháng năm {DateTime.Now.Year}";
            worksheet.Cells[rowIndex + 5, 3 ].Value = "Người thu tiền";
            worksheet.Cells[rowIndex + 10, 3 ].Value = tenNguoiThu;
            worksheet.Cells[rowIndex + 10, 3].Style.Font.Bold = true;



            excelPackage.Save();

            var filePath = AppDomain.CurrentDomain.BaseDirectory + "\\XuatFiles\\" + tenFile;
            //after your loop
            System.IO.File.WriteAllBytes(filePath, File.ReadAllBytes(pathFileTemp));
            return filePath;
        }
    }
}
