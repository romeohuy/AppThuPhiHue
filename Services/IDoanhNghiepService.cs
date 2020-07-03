using AppThuPhiHue.Models;
using System.Collections.Generic;

namespace AppThuPhiHue.Services
{
    public interface IDoanhNghiepService
    {
        void AddNew(DoanhNghiep model);
        void Update(DoanhNghiep model);
        void Delete(DoanhNghiep model);
        int GetMaxId();
        List<DoanhNghiep> GetAll();
    }
}
