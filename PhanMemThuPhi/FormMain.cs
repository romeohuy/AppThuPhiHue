using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThuVien.Core.Models;
using ThuVien.Core.Services;

namespace PhanMemThuPhi
{
    public partial class FormMain : Form
    {
        private long _soHoaDon = 0;
        private PhienNhapLieu _phienNhapLieu;
        private PhienNhapLieuService _phienNhapLieuService;
        private DonViService _donViService;
        private DanhMucSanPhamService _danhMucSanPhamService;
        private SoLieuNhapLieuService _soLieuNhapLieuService;
        public FormMain()
        {
            _donViService = new DonViService();
            _danhMucSanPhamService = new DanhMucSanPhamService();
            _soLieuNhapLieuService = new SoLieuNhapLieuService();
            _phienNhapLieuService = new PhienNhapLieuService();

            _phienNhapLieu = new PhienNhapLieu()
            {
                Id = Guid.NewGuid().ToString(),
                NgayNhapLieu = DateTime.Now,
                TenGoiNho = $"PhienNhapLieu_{DateTime.Now.ToString("yyyy MMMM dd HH:mm")}"
            };
            _phienNhapLieuService.Insert(_phienNhapLieu);
            _soHoaDon = _soLieuNhapLieuService.GetCurrentMaHD();

            InitializeComponent();

            comboBoxLoaiPhi.SelectedIndex = 0;

            if (_soHoaDon > 0)
            {
                textBoxSHD.Text = (_soHoaDon + 1).ToString();
            }

            labelQuanLyMaDonVi.Text = string.Empty;
            LoadDonVis();


            labelQuanLyMaSPEdit.Text = string.Empty;
            LoadSanPhams();

            comboBoxLoaiPhi.SelectedIndex = 0;

            LoadAllMainData();

            LoadSanPhams();
        }

        private void LoadAllMainData()
        {
            //Load all data for main
            LoadMainDonVi();
            LoadMainSP();
        }

        private void LoadMainDonVi()
        {
            var data = _donViService.GetDonViDoanhNghieps();
            comboBoxMainTenDv.DataSource = data;
            comboBoxMainTenDv.ValueMember = "MaDonVi";
            comboBoxMainTenDv.DisplayMember = "TenTimKiem";
            comboBoxMainTenDv.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxMainTenDv.AutoCompleteSource = AutoCompleteSource.ListItems;
            if (data.Any())
            {
                comboBoxMainTenDv.SelectedIndex = 0;
            }
        }

        private void LoadMainSP()
        {
            var data = _danhMucSanPhamService.GetDanhMucSanPhams();
            comboBoxMainSP.DataSource = data;
            comboBoxMainSP.ValueMember = "MaSanPham";
            comboBoxMainSP.DisplayMember = "TenTimKiem";
            comboBoxMainSP.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxMainSP.AutoCompleteSource = AutoCompleteSource.ListItems;
            if (data.Any())
            {
                comboBoxMainSP.SelectedIndex = 0;
            }
        }

        private void LoadDonVis()
        {
            dataGridViewDonVi.DataSource = _donViService.GetDonViDoanhNghieps();
            dataGridViewDonVi.Columns[0].Visible = false;
        }

        private void dataGridViewDonVi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dataGridViewDonVi.Rows[e.RowIndex].Cells[0].Value;
            LoadDonViEdit(id.ToString());
        }

        private void LoadDonViEdit(string id)
        {
            var donVi = _donViService.GetDonViDoanhNghiep(id);
            if (donVi != null)
            {
                labelQuanLyMaDonVi.Text = id;
                textBoxQuanLyMaDonVi.Text = donVi.MaDonVi;
                textBoxQuanLyTenDonVi.Text = donVi.TenDonVi;
            }
        }

        private void buttonLuuDonVi_Click(object sender, EventArgs e)
        {
            var donVi = new DonViDoanhNghiep()
            {
                Id = Guid.NewGuid().ToString(),
                MaDonVi = textBoxQuanLyMaDonVi.Text.Trim(),
                TenDonVi = textBoxQuanLyTenDonVi.Text.Trim()
            };
            donVi.TenTimKiem = donVi.MaDonVi + " - " + donVi.TenDonVi;

            _donViService.Insert(donVi);

            LoadDonVis();
        }

        private void buttonQuanLyDonViReset_Click(object sender, EventArgs e)
        {
            labelQuanLyMaDonVi.Text = string.Empty;
            textBoxQuanLyMaDonVi.Text = string.Empty;
            textBoxQuanLyTenDonVi.Text = string.Empty;
        }

        private void buttonQuanLyDonViLuuChinhSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(labelQuanLyMaDonVi.Text))
            {
                var dialogResult = MessageBox.Show("Bạn chưa chọn dòng chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK)
                {
                    return;
                }
            }

            var donViDoanhNghiep = _donViService.GetDonViDoanhNghiep(labelQuanLyMaDonVi.Text);
            donViDoanhNghiep.MaDonVi = textBoxQuanLyMaDonVi.Text.Trim();
            donViDoanhNghiep.TenDonVi = textBoxQuanLyTenDonVi.Text.Trim();
            donViDoanhNghiep.TenTimKiem = donViDoanhNghiep.MaDonVi + " - " + donViDoanhNghiep.TenDonVi;
            _donViService.Update(donViDoanhNghiep);

            LoadDonVis();
        }

        #region Danhmucsanpham

        private void dataGridViewQuanLySP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dataGridViewQuanLySP.Rows[e.RowIndex].Cells[0].Value;
            LoadDanhMucSPEdit(id.ToString());
        }
        private void LoadDanhMucSPEdit(string id)
        {
            var danhMucSanPham = _danhMucSanPhamService.GetDanhMucSanPham(id);
            if (danhMucSanPham != null)
            {
                labelQuanLyMaSPEdit.Text = id;
                textBoxQuanLyMaSP.Text = danhMucSanPham.MaSanPham;
                textBoxQuanLyTenSP.Text = danhMucSanPham.TenSanPham;
            }
        }

        private void LoadSanPhams()
        {
            dataGridViewQuanLySP.DataSource = _danhMucSanPhamService.GetDanhMucSanPhams();
            dataGridViewQuanLySP.Columns[0].Visible = false;
        }

        private void buttonLuuChinhSuaSP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(labelQuanLyMaSPEdit.Text))
            {
                var dialogResult = MessageBox.Show("Bạn chưa chọn dòng chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK)
                {
                    return;
                }
            }

            var danhMucSanPham = _danhMucSanPhamService.GetDanhMucSanPham(labelQuanLyMaSPEdit.Text);
            danhMucSanPham.MaSanPham = textBoxQuanLyMaSP.Text.Trim();
            danhMucSanPham.TenSanPham = textBoxQuanLyTenSP.Text.Trim();
            danhMucSanPham.TenTimKiem = danhMucSanPham.MaSanPham + " - " + danhMucSanPham.TenSanPham;
            _danhMucSanPhamService.Update(danhMucSanPham);

            LoadSanPhams();
        }

        private void buttonQuanLySanPhamReset_Click(object sender, EventArgs e)
        {
            labelQuanLyMaSPEdit.Text = string.Empty;
            textBoxQuanLyMaSP.Text = string.Empty;
            textBoxQuanLyTenSP.Text = string.Empty;
        }

        private void buttonQuanLyLuuSanPham_Click(object sender, EventArgs e)
        {
            var danhMucSanPham = new DanhMucSanPham()
            {
                Id = Guid.NewGuid().ToString(),
                MaSanPham = textBoxQuanLyMaSP.Text.Trim(),
                TenSanPham = textBoxQuanLyTenSP.Text.Trim()
            };
            danhMucSanPham.TenTimKiem = danhMucSanPham.MaSanPham + " - " + danhMucSanPham.TenSanPham;

            _danhMucSanPhamService.Insert(danhMucSanPham);

            LoadSanPhams();
        }
        #endregion

        private void buttonReloadData_Click(object sender, EventArgs e)
        {
            LoadAllMainData();
        }

        private void buttonThemSoLieu_Click(object sender, EventArgs e)
        {
            var solieu = new SoLieuNhapLieu()
            {
                Id = Guid.NewGuid().ToString(),
                MaHD = _soHoaDon,
                MaPhien = _phienNhapLieu.Id,
                TenDonVi = comboBoxMainTenDv.Text,
                TenSanPham = comboBoxMainSP.Text,
                GhiChu = textBoxGhiChu.Text,
                LoaiPhi = comboBoxLoaiPhi.Text,
                NgayNhap = dateTimePickerNgayNop.Value,
                SoTien = long.Parse(textBoxSoTien.Text.Trim())
            };
            _soLieuNhapLieuService.Insert(solieu);
            _soHoaDon += 1;
            textBoxSHD.Text = _soHoaDon.ToString();

            LoadSoLieuGrid();
        }

        private void LoadSoLieuGrid()
        {
            dataGridViewMainNhapLieu.DataSource = _soLieuNhapLieuService.GetSoLieuNhapLieus(_phienNhapLieu.Id);
            dataGridViewMainNhapLieu.Columns[0].Visible = false;
        }

        private void textBoxSHD_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxSHD.Text.Trim(), out int sohd))
            {
                _soHoaDon = sohd;
            }
            else
            {
                MessageBox.Show("Số HD sai.");
                textBoxSHD.Focus();
            }
        }

        private void textBoxSoTien_TextChanged(object sender, EventArgs e)
        {
            if (long.TryParse(textBoxSoTien.Text.Trim(), out long st) == false)
            {
                MessageBox.Show("Số tiền sai.");
                textBoxSoTien.Focus();
            }
        }

        private void buttonXuatFile_Click(object sender, EventArgs e)
        {
            var tenFile = textBoxTenFile.Text + DateTime.Now.ToString("yyyyMMdd HH-mm") + ".csv";
            var data = _soLieuNhapLieuService.GetSoLieuNhapLieusXuatFile(dateTimePickerFromDate.Value,
                dateTimePickerToDate.Value, null, null);

            //before your loop
            var csv = new StringBuilder();
            var header = "SoHD,Ten don vi,Ten Hang, So Tien, Ghi Chu";
            csv.AppendLine(header);
            foreach (var itemLieu in data)
            {
                var newLine = $"{itemLieu.MaHD},{itemLieu.TenDonVi},{itemLieu.TenSanPham},{itemLieu.SoTien},{itemLieu.GhiChu}";
                csv.AppendLine(newLine);

            }

            //after your loop
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\XuatFiles\\" + tenFile, csv.ToString());
        }
    }
}
