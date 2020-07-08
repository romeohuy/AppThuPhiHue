using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationThuPhi.Models
{
    public class ExportModel
    {
        public string TenFile { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}