using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webthanhtamstore.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;//để xài dc cái "Lưu tên của file"

namespace Webthanhtamstore.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        THANHTAMSTOREEntities1 db = new THANHTAMSTOREEntities1();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            return View(db.MENUs.ToList().OrderBy(n => n.Gia).ToPagedList(pageNumber, pageSize));
        }

        //THÊM mới dữ lie6u5
        [HttpGet]
        public ActionResult ThemMoi()
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.CHUDEs.ToList(), "MaChuDe", "TenChuDe");
            return View();
        }
        //Upload ảnh
        [HttpPost]

        [ValidateInput(false)]//Phải có cái này, k là nó k chạy, khi chèn thẻ html vào code

        public ActionResult ThemMoi(MENU menu, HttpPostedFileBase fileUpload)
        {

            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.CHUDEs.ToList(), "MaChuDe", "TenChuDe");

            //Kiểm tra đường dẫn Ảnhn
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }

            //Thêm vào CSDL
            //kiểm tra valuedation
            if (ModelState.IsValid)//Nếu nó thỏa mãn tất cả cả đk của các cái control ta đã nhập vào
            {
                //Lưu tên của file
                var fileName = Path.GetFileName(fileUpload.FileName);//để lấy cái tên của nó r lưu vô csld
                                                                     //Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/HinhAnhSP/"), fileName);//truyền 2 tham số: đường dẫn tới hình ảnh VÀ fileName
                                                                                  //Kiểm tra nếu hình ảnh này nếu tồn tại r dựa vào cái tên thì mình sẽ k cho nó lưu vào, chưa tồn tại thì lưu
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }

                menu.Anh = fileUpload.FileName;

                // db.MENUs.Add(menu);

                db.SaveChanges();

            }
            return View();
        }
        //CHỈNH SỬA SẢN PHÂMmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
        [HttpGet]
        public ActionResult ChinhSua(int MaSP)
        {
            //Lấy ra đối tượng menu theo mã
            MENU menu = db.MENUs.SingleOrDefault(n => n.MaSP == MaSP);
            if (menu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaChuDe = new SelectList(db.CHUDEs.ToList(), "MaChuDe", "TenChuDe", menu.MaChuDe);

            return View(menu);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(MENU menu, FormCollection f)
        {

            //Thêm vào csdl
            if (ModelState.IsValid)
            {
                //Thực hiện cập nhật trong model
                db.Entry(menu).State = System.Data.Entity.EntityState.Modified;//Nó lấy cái biến "menu" nó thực hiện những cái thay đổi đó ở trong Model, r ta SAVECHANGE nó sẽ update lại db 
                db.SaveChanges();
            }
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.CHUDEs.ToList(), "MaChuDe", "TenChuDe", menu.MaChuDe);

            return View(menu);

        }

        //HIỂN THỊIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII
        public ActionResult HienThi(int MaSP)
        {
            //Lấy ra đối tượng menu theo mã
            MENU menu = db.MENUs.SingleOrDefault(n => n.MaSP == MaSP);
            if (menu == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(menu);
        }

        //XÓA SẢN PHẨMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
        [HttpGet]
        public ActionResult Xoa(int MaSP)
        {
            //Lấy ra đối tượng menu theo mã
            MENU menu = db.MENUs.SingleOrDefault(n => n.MaSP == MaSP);
            if (menu == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(menu);
        }
        [HttpPost, ActionName ("Xoa")]
        public ActionResult XacNhanXoa(int MaSP)
        {
            //Lấy ra đối tượng menu theo mã
            MENU menu = db.MENUs.SingleOrDefault(n => n.MaSP == MaSP);
            if (menu == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            db.MENUs.Remove(menu);
            return RedirectToAction("Index");
        }

    }
}