using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using web_thuedocuoi.Models;

namespace web_thuedocuoi.Models
{
    public class CartItem
    {
        public string sMaSanPham { get; set; }
        public string sTenSanPham { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public DateTime NgayTao { get; set; }
        public string sMoTa { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        QL_THUE_DO_CUOIEntities1 data = new QL_THUE_DO_CUOIEntities1();
        public CartItem() { }

        public CartItem(string MaSP)
        {
            QL_THUE_DO_CUOIEntities1 data = new QL_THUE_DO_CUOIEntities1();
            SANPHAM sp = data.SANPHAMs.FirstOrDefault(n => n.MASP == MaSP);

            if (sp != null)
            {
                sMaSanPham = MaSP;
                sTenSanPham = sp.TENSP;
                sAnhBia = sp.HINHANH;
                dDonGia = double.Parse(sp.GIA.ToString());
                iSoLuong = 1;
                sMoTa = sp.MOTA;
                NgayTao = DateTime.Now;
            }
        }
    }
    public class GioHang
    {
        public List<CartItem> lst;

        public GioHang()
        {
            lst = new List<CartItem>();
        }

        public GioHang(List<CartItem> lstGH)
        {
            lst = lstGH;
        }

        public int SoMatHang()
        {
            int count = 0;
            foreach (CartItem item in lst)
            {
                count++;
            }
            return count;
        }

        public int TongSLHang()
        {
            int sum = 0;
            sum = lst.Sum(t => t.iSoLuong);
            return sum;
        }

        public double TongThanhTien()
        {
            double sum = 0;
            foreach (CartItem item in lst)
            {
                sum += item.ThanhTien;
            }
            return sum;
        }

        public int Them(string sMaSP)
        {
            CartItem sanpham = lst.Find(n => n.sMaSanPham == sMaSP);

            if (sanpham == null)
            {
                CartItem sach = new CartItem(sMaSP);
                if (sach == null)
                    return -1;
                lst.Add(sach);
            }
            else
            {
                sanpham.iSoLuong++;
            }
            return 1;
        }
    }
}


