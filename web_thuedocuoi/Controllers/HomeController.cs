using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using web_thuedocuoi.Models;

namespace web_thuedocuoi.Controllers
{
    public class HomeController : Controller
    {
        QL_THUE_DO_CUOIEntities1 data = new QL_THUE_DO_CUOIEntities1();

        public ActionResult Index()
        {
            var cate = data.DANHMUCSPs.ToList();
            ViewBag.Categories = cate;
            return View();
        }

        public ActionResult Service()
        {

            return View();
        }

        public ActionResult Products(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return View();
            }
            var products = data.SANPHAMs.Where(p => p.MA_DANHMUC == Id).ToList();
            ViewBag.CategoryName = data.DANHMUCSPs.FirstOrDefault(c => c.MA_DANHMUC == Id)?.TENDANHMUC;
            return View(products);
        }
        public PartialViewResult LoadCategoriesPartial()
        {
            var cate = data.DANHMUCSPs.ToList();
            return PartialView(cate);
        }

        public ActionResult Details(string id)
        {
            SANPHAM product = data.SANPHAMs.FirstOrDefault(p => p.MASP == id);
            return View(product);
        }
        public ActionResult Login()
        { return View(); }

        public ActionResult Contact()
        { return View(); }

        public ActionResult About()
        { return View(); }
    }
}