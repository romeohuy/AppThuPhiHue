using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonFlatFileDataStore;
using ThuVien.Core.Models;

namespace ThuVien.Core.Services
{
    public class DonViService
    {
        private DataStore _dataStore;

        public DonViService()
        {
            _dataStore = new DataStore(AppDomain.CurrentDomain.BaseDirectory + "\\DuLieuHeThong\\DonViDoanhNghieps.json");
        }

        private IDocumentCollection<DonViDoanhNghiep> LoadData()
        {
            _dataStore = new DataStore(AppDomain.CurrentDomain.BaseDirectory + "\\DuLieuHeThong\\DonViDoanhNghieps.json");
            return _dataStore.GetCollection<DonViDoanhNghiep>();
        }
        public List<DonViDoanhNghiep> GetDonViDoanhNghieps()
        {
            if (_dataStore == null)
            {
                LoadData();
            }
            return _dataStore.GetCollection<DonViDoanhNghiep>().AsQueryable().ToList();
        }

        public DonViDoanhNghiep GetDonViDoanhNghiep(string idGuid)
        {
            var collection = LoadData();
            return collection.Find(e=>e.Id == idGuid).FirstOrDefault();
        }

        public void Insert(DonViDoanhNghiep donViDoanhNghiep)
        {
            var collection = LoadData();
            collection.InsertOne(donViDoanhNghiep);
        }
        public  void Update(DonViDoanhNghiep donViDoanhNghiep)
        {
            var collection = LoadData();
             collection.UpdateOne(e=>e.Id == donViDoanhNghiep.Id,donViDoanhNghiep);
        }

        public void Delete(string idGuid)
        {
            var collection = LoadData();
             collection.DeleteOne(e => e.Id == idGuid);
        }
    }
}
