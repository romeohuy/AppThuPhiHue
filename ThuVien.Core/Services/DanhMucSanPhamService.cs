using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonFlatFileDataStore;
using ThuVien.Core.Models;

namespace ThuVien.Core.Services
{
    public class DanhMucSanPhamService
    {
        private DataStore _dataStore;

        public DanhMucSanPhamService()
        {
            _dataStore = new DataStore(AppDomain.CurrentDomain.BaseDirectory + "\\DuLieuHeThong\\DanhMucSanPhams.json");
        }

        private IDocumentCollection<DanhMucSanPham> LoadData()
        {
            _dataStore = new DataStore(AppDomain.CurrentDomain.BaseDirectory + "\\DuLieuHeThong\\DanhMucSanPhams.json");
            return _dataStore.GetCollection<DanhMucSanPham>();
        }
        public List<DanhMucSanPham> GetDanhMucSanPhams()
        {
            if (_dataStore == null)
            {
                LoadData();
            }
            return _dataStore.GetCollection<DanhMucSanPham>().AsQueryable().ToList();
        }

        public DanhMucSanPham GetDanhMucSanPham(string idGuid)
        {
            var collection = LoadData();
            return collection.Find(e=>e.Id == idGuid).FirstOrDefault();
        }

        public void Insert(DanhMucSanPham DanhMucSanPham)
        {
            var collection = LoadData();
            collection.InsertOne(DanhMucSanPham);
        }
        public  void Update(DanhMucSanPham DanhMucSanPham)
        {
            var collection = LoadData();
             collection.UpdateOne(e=>e.Id == DanhMucSanPham.Id,DanhMucSanPham);
        }

        public void Delete(string idGuid)
        {
            var collection = LoadData();
             collection.DeleteOne(e => e.Id == idGuid);
        }
    }
}
