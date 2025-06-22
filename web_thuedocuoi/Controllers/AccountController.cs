using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using web_thuedocuoi.Models;

namespace web_thuedocuoi.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private QL_THUE_DO_CUOIEntities1 data = new QL_THUE_DO_CUOIEntities1();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {

            var user = data.KHACHHANGs.FirstOrDefault(u => u.EMAIL == Email && u.PASSWORD == Password);

            if (user != null)
            {
                Session["UserID"] = user.MAKH;
                Session["Email"] = user.EMAIL;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Email hoặc mật khẩu không chính xác.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string HOTEN, string EMAIL, string DIEN_THOAI, string DIACHI, string GIOITINH, string PASSWORD, string CONFIRM_PASSWORD)
        {
            if (PASSWORD != CONFIRM_PASSWORD)
            {
                ViewBag.ErrorMessage = "Mật khẩu không khớp.";
                return View();
            }

            if (data.KHACHHANGs.Any(u => u.EMAIL == EMAIL))
            {
                ViewBag.ErrorMessage = "Email đã tồn tại.";
                return View();
            }

            KHACHHANG newCustomer = new KHACHHANG
            {
                MAKH = GenerateRandomCode(5),
                HOTEN = HOTEN,
                EMAIL = EMAIL,
                DIEN_THOAI = DIEN_THOAI,
                DIACHI = DIACHI,
                GIOITINH = GIOITINH,
                PASSWORD = PASSWORD,
                NGAYTAO = DateTime.Now,
                XEPLOAI = "Mới",
                TONGDONHANG = 0,
                TYPE = "user"
            };

            try
            {
                data.KHACHHANGs.Add(newCustomer);
                data.SaveChanges();

                // Đăng ký thành công
                Session["UserID"] = newCustomer.MAKH;
                Session["Email"] = newCustomer.EMAIL;
                return RedirectToAction("Index", "Home");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = $"Validation errors: {fullErrorMessage}";

                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi đăng ký. Vui lòng thử lại. Chi tiết lỗi: " + exceptionMessage;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi đăng ký. Vui lòng thử lại. Chi tiết lỗi: " + ex.Message;
                return View();
            }
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}