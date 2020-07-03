using AppThuPhiHue.Models;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppThuPhiHue.Services
{
    public class DoanhNghiepService : IDoanhNghiepService
    {
        public readonly QueryFactoryCustom QueryFactoryCustom;

        public DoanhNghiepService()
        {
            QueryFactoryCustom = new QueryFactoryCustom();
        }
        public void AddNew(DoanhNghiep model)
        {
            QueryFactoryCustom.DbFactory.Query("DoanhNghiep").Insert(model);
        }

        public void Update(DoanhNghiep model)
        {
            throw new NotImplementedException();
        }

        public void Delete(DoanhNghiep model)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            var maxId = QueryFactoryCustom.DbFactory.Query("DoanhNghiep").Max<int>("Id");
            return maxId;
        }

        public List<DoanhNghiep> GetAll()
        {
            var doanhNghieps = QueryFactoryCustom.DbFactory.Query("DoanhNghiep").Get<DoanhNghiep>();
            return doanhNghieps.ToList();
        }
    }
}
