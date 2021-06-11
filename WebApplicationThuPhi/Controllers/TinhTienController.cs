using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationThuPhi.Models;

namespace WebApplicationThuPhi.Controllers
{
    public class TinhTienController : Controller
    {
        // GET: TinhTien
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index()
        {
            return View(new TinhTienModel());
        }
    }
}