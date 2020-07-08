using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonFlatFileDataStore;
using ThuVien.Core.Models;

namespace ThuVien.Core.Services
{
    public class PhienNhapLieuService
    {
        private DataStore _dataStore;

        public PhienNhapLieuService()
        {
            _dataStore = new DataStore(AppDomain.CurrentDomain.BaseDirectory + "\\DuLieuHeThong\\PhienNhapLieus.json");
        }

        private IDocumentCollection<PhienNhapLieu> LoadData()
        {
            _dataStore = new DataStore(AppDomain.CurrentDomain.BaseDirectory + "\\DuLieuHeThong\\PhienNhapLieus.json");
            return _dataStore.GetCollection<PhienNhapLieu>();
        }
        public List<PhienNhapLieu> GetPhienNhapLieus()
        {
            if (_dataStore == null)
            {
                LoadData();
            }
            return _dataStore.GetCollection<PhienNhapLieu>().AsQueryable().ToList();
        }

        public PhienNhapLieu GetPhienNhapLieu(string id)
        {
            var collection = LoadData();
            return collection.Find(e=>e.Id == id).FirstOrDefault();
        }

        public void Insert(PhienNhapLieu PhienNhapLieu)
        {
            var collection = LoadData();
            collection.InsertOne(PhienNhapLieu);
        }
        public  void Update(PhienNhapLieu PhienNhapLieu)
        {
            var collection = LoadData();
             collection.UpdateOne(e=>e.Id == PhienNhapLieu.Id,PhienNhapLieu);
        }

        public void Delete(string id)
        {
            var collection = LoadData();
             collection.DeleteOne(e => e.Id == id);
        }
    }
}
