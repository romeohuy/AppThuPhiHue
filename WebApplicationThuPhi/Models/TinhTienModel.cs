using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationThuPhi.Models
{
    public class TinhTienModel
    {
        [DisplayName(("Số tờ 500k"))]
        public int Soto500k { get; set; }
        [DisplayName(("Số tờ 200k"))]
        public int Soto200k { get; set; }
        [DisplayName(("Số tờ 100k"))]
        public int Soto100k { get; set; }
        [DisplayName(("Số tờ 50k"))]
        public int Soto50k { get; set; }
        [DisplayName(("Số tờ 20k"))]
        public int Soto20k { get; set; }
        [DisplayName(("Số tờ 10k"))]
        public int Soto10k { get; set; }
        [DisplayName(("Số tờ 5k"))]
        public int Soto5k { get; set; }
        [DisplayName(("Số tờ 2k"))]
        public int Soto2k { get; set; }
        [DisplayName(("Số tờ 1k"))]
        public int Soto1k { get; set; }
    }
}