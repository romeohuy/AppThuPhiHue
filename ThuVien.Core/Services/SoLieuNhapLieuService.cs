using JsonFlatFileDataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using ThuVien.Core.Models;

namespace ThuVien.Core.Services
{
    public class SoLieuNhapLieuService
    {
        private DataStore _dataStore;

        public SoLieuNhapLieuService()
        {
            _dataStore = new DataStore(AppDomain.CurrentDomain.BaseDirectory + "\\DuLieuHeThong\\SoLieuNhapLieus.json");
        }

        private IDocumentCollection<SoLieuNhapLieu> LoadData()
        {
            _dataStore = new DataStore(AppDomain.CurrentDomain.BaseDirectory + "\\DuLieuHeThong\\SoLieuNhapLieus.json");
            return _dataStore.GetCollection<SoLieuNhapLieu>();
        }
        public List<SoLieuNhapLieu> GetSoLieuNhapLieus(string maPhien = null)
        {
            if (_dataStore == null)
            {
                LoadData();
            }

            if (string.IsNullOrEmpty(maPhien) == false)
            {
                return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().Where(_ => _.MaPhien == maPhien).ToList();
            }
            return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().OrderByDescending(_ => _.MaHD).ToList();
        }
        public List<SoLieuNhapLieu> GetSoLieuNhapLieuHienTai()
        {
            if (_dataStore == null)
            {
                LoadData();
            }
            return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().Where(_ => _.NgayNhap.Date == DateTime.Now.Date).OrderByDescending(_ => _.MaHD).ToList();
        }

        public List<SoLieuNhapLieu> GetSoLieuNhapLieusXuatFile(DateTime? fromDate, DateTime? toDate, long? fromNumber, long? toNumber)
        {
            if (_dataStore == null)
            {
                LoadData();
            }

            if (fromNumber.HasValue && toNumber.HasValue)
            {
                return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().ToList().Where(_ => _.MaHD >= fromNumber.Value && _.MaHD <= toNumber.Value).OrderBy(_ => _.MaHD).ToList();
            }

            if (fromDate.HasValue && toDate.HasValue)
            {
                return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().ToList().Where(_ => _.NgayNhap.Date >= fromDate.Value.Date && _.NgayNhap.Date <= toDate.Value.Date).OrderBy(_ => _.MaHD).ToList();
            }
            else
            {
                return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().ToList();
            }
        }

        public long GetCurrentMaHD()
        {
            var data = _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().OrderByDescending(_ => _.NgayNhap).FirstOrDefault();
            if (data == null)
            {
                return 0;
            }

            return data.MaHD;
        }

        public SoLieuNhapLieu GetSoLieuNhapLieu(long soHD)
        {
            var collection = LoadData();
            return collection.Find(e => e.MaHD == soHD).FirstOrDefault();
        }

        public SoLieuNhapLieu GetSoLieuNhapLieuById(string id)
        {
            var collection = LoadData();
            return collection.Find(e => e.Id == id).FirstOrDefault();
        }

        public void Insert(SoLieuNhapLieu SoLieuNhapLieu)
        {
            var collection = LoadData();
            collection.InsertOne(SoLieuNhapLieu);
        }
        public void Update(SoLieuNhapLieu SoLieuNhapLieu)
        {
            var collection = LoadData();
            collection.UpdateOne(e => e.MaHD == SoLieuNhapLieu.MaHD, SoLieuNhapLieu);
        }

        public void Delete(string id)
        {
            var collection = LoadData();
            collection.DeleteOne(e => e.Id == id);
        }
    }
}
