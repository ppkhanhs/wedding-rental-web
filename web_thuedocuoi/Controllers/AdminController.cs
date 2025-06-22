using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_thuedocuoi.Models;
using System.Data.Entity;


namespace web_thuedocuoi.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        QL_THUE_DO_CUOIEntities1 data = new QL_THUE_DO_CUOIEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(SANPHAM p)
        {
            data.SANPHAMs.Add(p);
            return View();
        }

        public ActionResult ManageUsers()
        {
            List<KHACHHANG> users = data.KHACHHANGs.ToList();
            ViewBag.UserCount = users.Count(u => u.TYPE == "user");
            ViewBag.AdminCount = users.Count(u => u.TYPE == "admin");
            return View(users);
        }

        public ActionResult ManageProducts(string searchTerm = "")
        {
            var products = data.SANPHAMs
                .Where(p => string.IsNullOrEmpty(searchTerm) || p.TENSP.Contains(searchTerm))
                .ToList();

            ViewBag.CountPro = products.Count;

            return View(products);
        }

        //=====================search=========================

        [HttpPost]
        public ActionResult SearchProducts(string searchTerm)
        {
            var products = data.SANPHAMs
                .Where(p => p.TENSP.Contains(searchTerm))
                .ToList();

            ViewBag.CountPro = products.Count;

            return View("ManageProducts", products);
        }

        public ActionResult UsersDetail(string id)
        {
            KHACHHANG users = data.KHACHHANGs.Where(row => row.MAKH==id).FirstOrDefault();
            return View(users);
        }

        public ActionResult ProductsDetail(string id)
        {
            SANPHAM products = data.SANPHAMs.Where(row => row.MASP == id).FirstOrDefault();
            return View(products);
        }

        public ActionResult DeleteUsers(string id)
        {
            KHACHHANG users = data.KHACHHANGs.Where(row => row.MAKH == id).FirstOrDefault();
            data.KHACHHANGs.Remove(users);
            data.SaveChanges();

            return RedirectToAction("ManageUsers");
        }

        public ActionResult DeleteProducts(string id)
        {
            SANPHAM p = data.SANPHAMs.FirstOrDefault(pr=>pr.MASP == id);
            data.SANPHAMs.Remove(p);
            data.SaveChanges();
            return RedirectToAction("ManageProducts");
        }

        public ActionResult EditUsers(string id)
        {
            KHACHHANG users = data.KHACHHANGs.Where(row => row.MAKH == id).FirstOrDefault();
            return View(users);
        }

        [HttpPost]

        public ActionResult EditUsers(KHACHHANG user)
        {
            KHACHHANG users = data.KHACHHANGs.Where(row => row.MAKH == user.MAKH).FirstOrDefault();

            users.HOTEN = user.HOTEN;
            users.EMAIL = user.EMAIL;
            users.DIACHI = user.DIACHI;
            users.DIEN_THOAI = user.DIEN_THOAI;
            users.GIOITINH = user.GIOITINH;

            data.SaveChanges();
            return View(users);
        }

        public ActionResult EditProducts(SANPHAM product)
        {
            SANPHAM products = data.SANPHAMs.Where(row => row.MASP == product.MASP).FirstOrDefault();

            products.TENSP = product.TENSP;
            products.MOTA = product.MOTA;
            products.GIA = product.GIA;
            products.MA_DANHMUC = product.MA_DANHMUC;
            products.SOLUONGTON = product.SOLUONGTON;
            products.HINHANH = product.HINHANH;

            data.SaveChanges();
            return View(product);
        }
        public ActionResult Orders()
        {
            var orders = data.DONHANGs.ToList();
            return View(orders);
        }

        public ActionResult OrderDetails(string id)
        {
            // Lấy đơn hàng từ cơ sở dữ liệu
            var order = data.DONHANGs
                .Include(o => o.CHITIETDHs)
                .FirstOrDefault(o => o.MADH == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

    }
}