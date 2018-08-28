using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webthanhtamstore.Models;

namespace Webthanhtamstore.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        THANHTAMSTOREEntities1 db = new THANHTAMSTOREEntities1();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KHACHHANGGG kh) // (KHACHHANG)-- giống vs cái Model bên View DangKy.cshtml
        {
            if (ModelState.IsValid)//Khi tất cả các validation hợp lệ
            {
                //Gửi dữ liệu và lưu vào csdl
                db.KHACHHANGGGs.Add(kh);
                db.SaveChanges();
                ViewBag.ThongBao = "Đăng ký thành công";
            }

            return View();
        }


        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f["txtMatKhau"].ToString();

            KHACHHANGGG kh = db.KHACHHANGGGs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công";
                Session["TaiKhoan"] = kh;
                return RedirectToAction("GioHang", "GioHang");
            }
            ViewBag.ThongBao = "Sai tài khoản hoặc mật khẩu";
            return View();
        }
    }
}