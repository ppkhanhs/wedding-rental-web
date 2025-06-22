
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_thuedocuoi.Models;

namespace web_thuedocuoi.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        QL_THUE_DO_CUOIEntities1 data = new QL_THUE_DO_CUOIEntities1();
        public ActionResult Index()
        {
            List<SANPHAM> ds = data.SANPHAMs.ToList();
            return View(ds);
        }

        public ActionResult CartView()
        {
            GioHang gh = (GioHang)Session["gh"];
            if(gh == null)
            {
                gh = new GioHang();
            }    
            return View(gh);
        }

        [HttpPost]
        public ActionResult AddToCart(string productId, string productName, string productImage, double productPrice, int quantity)
        {
            GioHang gh = (GioHang)Session["gh"];
            if (gh == null)
                gh = new GioHang();

            CartItem existingItem = gh.lst.FirstOrDefault(i => i.sMaSanPham == productId);

            if (existingItem == null)
            {
                CartItem newItem = new CartItem(productId)
                {
                    sMaSanPham = productId,
                    sTenSanPham = productName,
                    sAnhBia = productImage,
                    dDonGia = productPrice,
                    iSoLuong = quantity
                };
                gh.lst.Add(newItem);
            }
            else
            {
                existingItem.iSoLuong += quantity;
            }

            Session["gh"] = gh;

            return RedirectToAction("CartView", "Cart");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(string productId)
        {
            GioHang cart = (GioHang)Session["gh"];
            var itemRemove = cart.lst.FirstOrDefault(c => c.sMaSanPham == productId);
            if (itemRemove != null)
            {
                cart.lst.Remove(itemRemove);
                Session["gh"] = cart;
            }

            return RedirectToAction("CartView", "Cart");
        }

        [HttpPost]
        public ActionResult UpdateCart(string productId, int quantity)
        {
            var cart = Session["gh"] as GioHang;
            if (cart != null)
            {
                var existItem = cart.lst.FirstOrDefault(i => i.sMaSanPham == productId);
                if (existItem != null)
                {
                    existItem.iSoLuong = quantity;
                }
                Session["gh"] = cart;
            }
            return RedirectToAction("CartView", "Cart");
        }
        //==================================================
        public ActionResult Checkout(string productId)
        {
            GioHang cart = (GioHang)Session["gh"];
            if (cart != null)
            {
                CartItem itemToCheckout = cart.lst.FirstOrDefault(c => c.sMaSanPham == productId);
                if (itemToCheckout != null)
                {
                    cart.lst.Remove(itemToCheckout);
                    List<Invoice> invoiceItems = Session["invoiceItems"] as List<Invoice>;
                    if (invoiceItems == null)
                    {
                        invoiceItems = new List<Invoice>();
                    }

                    var invoiceItem = new Invoice
                    {
                        ProductId = itemToCheckout.sMaSanPham,
                        ProductName = itemToCheckout.sTenSanPham,
                        ProductImage = itemToCheckout.sAnhBia,
                        Price = itemToCheckout.dDonGia,
                        Quantity = itemToCheckout.iSoLuong,
                        Total = itemToCheckout.ThanhTien
                    };

                    invoiceItems.Add(invoiceItem);
                    Session["invoiceItems"] = invoiceItems;

                    Session["gh"] = cart;
                }
            }

            return RedirectToAction("InvoiceView", "Cart");
        }
        public ActionResult InvoiceView()
        {
            List<Invoice> invoiceItems = Session["invoiceItems"] as List<Invoice>;
    if (invoiceItems == null)
    {
        invoiceItems = new List<Invoice>();
    }
    return View(invoiceItems);
        }

    }
}