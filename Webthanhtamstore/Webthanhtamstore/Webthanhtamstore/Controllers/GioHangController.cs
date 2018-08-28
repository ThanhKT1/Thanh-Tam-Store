using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webthanhtamstore.Models;

namespace Webthanhtamstore.Controllers
{
   
    public class GioHangController : Controller
    {
        // GET: GioHang
        #region GIỎ HÀNG
        THANHTAMSTOREEntities1 db = new THANHTAMSTOREEntities1();
        //lẤY GIỎ HÀNG
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;// ép kiểu
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tạo mới lstGiohang cũng như Session Gio3 hàng
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMaSP, string strUrl) //gửi vào strUrl để khi bấm vào chữ MUA HÀNG nó vẫn gữ ở cái trang đó
        {
            MENU menu = db.MENUs.SingleOrDefault(n => n.MaSP==iMaSP);
            if (menu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hảng
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.Find(n => n.iMaSP == iMaSP);//tức nếu đặt sản phẩm 1 lần rồi thì cộng số lượng lên còn nếu chưa đặt ta sẽ khởi tạo nó và add vào cái lst này
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSP);//nếu chưa tồn tại thì khởi tạo nó
                lstGioHang.Add(sanpham);
                return Redirect(strUrl);// địa chỉ cái trang đang đặt mua
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strUrl);
            }

        }
        //KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
        public ActionResult CapNhatGioHang(int iMaSP,FormCollection f)
        {
            MENU menu = db.MENUs.SingleOrDefault(n => n.MaSP == iMaSP);
            if (menu == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
                
            }
            return RedirectToAction("GioHang");
        }
        //kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk
        public ActionResult XoaGioHang(int iMaSP,FormCollection f)
        {
            MENU menu = db.MENUs.SingleOrDefault(n => n.MaSP == iMaSP);
            if (menu == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSP);
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }

        //Xây dựng giỏ hànggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }

        // Tính thành tiền
        private double ThanhTien()
        {
            double dThanhTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dThanhTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dThanhTien;
        }
      //TẠO partial giỏ hàng
      public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = ThanhTien();
            return PartialView();
        }

        // XÂY DỰNG 1 view cho người dùng CHỈNH SỬA GIỎ HÀNG
        public ActionResult SuaGioHoang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        #endregion

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

        #region ĐẶT HÀNG
        // XÂY DỰNG TÍNH NĂNG ĐẶT HÀNG
        [HttpPost]
        public ActionResult DatHang()
        {
            // Trước hết kiểm tra khách hàng đã đăng nhập vào chưa
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            //Tiếp là kiểu tra giỏ hàng, kiếm tra nó đã có cái session gio hang chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //THêm đơn hàng
            DONHANGGG ddh = new DONHANGGG();
            KHACHHANGGG kh = (KHACHHANGGG) Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();//phương thức này để lấy cái gh từ session gio hang ra
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.DONHANGGGs.Add(ddh);
            db.SaveChanges();
            //thêm đơn hàng vào csdl
            
            //Thêm chi tiết đơn hàng, đối chiếu với từng trường trong csdl lôi nó ra
            foreach (var item in gh)//item trong Giỏ Hàng
            {
                CHITIETDONHANGGGG ctDH = new CHITIETDONHANGGGG();
                ctDH.MaDonHang = ddh.MaDonHang;
                ctDH.MaSP = item.iMaSP;
                ctDH.SoLuong = item.iSoLuong;
                ctDH.DonGia = (string)item.dGia.ToString();
                db.CHITIETDONHANGGGGs.Add(ctDH);
            }

            db.SaveChanges();
            return RedirectToAction("GioHang", "GioHang");//SAU KHI đặt hàng thành công thì return về trang chủ
            
        }
        #endregion
    }
}