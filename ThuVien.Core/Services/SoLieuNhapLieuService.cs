﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonFlatFileDataStore;
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
                return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().Where(_=>_.MaPhien == maPhien).ToList();
            }
            return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().ToList();
        }
        public List<SoLieuNhapLieu> GetSoLieuNhapLieuHienTai()
        {
            if (_dataStore == null)
            {
                LoadData();
            }
            return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().Where(_=>_.NgayNhap.Date == DateTime.Now.Date).OrderByDescending(_=>_.MaHD).ToList();
        }

        public List<SoLieuNhapLieu> GetSoLieuNhapLieusXuatFile(DateTime fromDate, DateTime today)
        {
            if (_dataStore == null)
            {
                LoadData();
            }

            return _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().ToList().Where(_=>_.NgayNhap.Date >= fromDate.Date && _.NgayNhap.Date <= today.Date).ToList();
        }

        public long GetMaxMaHD()
        {
            var data = _dataStore.GetCollection<SoLieuNhapLieu>().AsQueryable().ToList();
            if (data.Any() == false)
            {
                return 0;
            }

            return data.Max(_ => _.MaHD);
        }

        public SoLieuNhapLieu GetSoLieuNhapLieu(long soHD)
        {
            var collection = LoadData();
            return collection.Find(e=>e.MaHD == soHD).FirstOrDefault();
        }

        public void Insert(SoLieuNhapLieu SoLieuNhapLieu)
        {
            var collection = LoadData();
            collection.InsertOne(SoLieuNhapLieu);
        }
        public  void Update(SoLieuNhapLieu SoLieuNhapLieu)
        {
            var collection = LoadData();
             collection.UpdateOne(e=>e.MaHD == SoLieuNhapLieu.MaHD,SoLieuNhapLieu);
        }

        public void Delete(string id)
        {
            var collection = LoadData();
             collection.DeleteOne(e => e.Id == id);
        }
    }
}