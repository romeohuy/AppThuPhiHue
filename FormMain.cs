using AppThuPhiHue.Models;
using AppThuPhiHue.Services;
using System.Windows.Forms;

namespace AppThuPhiHue
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            IDoanhNghiepService service = new DoanhNghiepService();
            service.AddNew(new DoanhNghiep()
            {
                Id = service.GetMaxId() + 1,
                MaDN = "test",
                MaSoThue = "a",
                Ten = "a"
            });
            dataGridView1.DataSource = service.GetAll();

        }

    }
}
