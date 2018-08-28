using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webthanhtamstore.Models;////////////////
using PagedList;////////////////////////////////////////
using PagedList.Mvc;///

namespace Webthanhtamstore.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        THANHTAMSTOREEntities1 db = new THANHTAMSTOREEntities1();
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f,int ?page)//formcolection truyền vào cái biến để ta có thể để ta có thể lấy dc các cái value trong ô tìm kiếm thông qua cái name
        {
            string sTuKhoa = f["txtfind"].ToString();// ta đã lấy dc giá trị ta tìm

            ViewBag.TuKhoa = sTuKhoa;//đối chiếu với ViewBag.TuKhoa bên KetQuaTimKiem, có nghĩa là nó vẫn giữ dc cái từ khóa để nó search tiếp cho ta

            List<MENU> lstKQTK=db.MENUs.Where(n=>n.TenSP.Contains(sTuKhoa)).ToList();// Bởi vì mình tìm kiếm sách nên mình phải tạo ra 1 cái list để tìm
            //phân trang
            int pageNumber = (page ?? 1);// mặc định là 1 nếu k có sản phẩm nào
            int pageSize = 4; // 4 sp trên 1 trang

            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm cần tìm";
                return View(db.MENUs.ToList().ToPagedList(pageNumber,pageSize));// không tìm thấy thì cho ng ta xuất ra hết sản phẩm
            }
            ViewBag.ThongBao = "Đã tìm thấy" + lstKQTK.Count + "kết quả";
            return View(lstKQTK.ToPagedList(pageNumber,pageSize));
        }

        [HttpGet] //hàm get để khi phân trang vẫn tìm thấy kết quả
        public ActionResult KetQuaTimKiem( int? page, string sTuKhoa)//formcolection truyền vào cái biến để ta có thể để ta có thể lấy dc các cái value trong ô tìm kiếm thông qua cái name
        {
            ViewBag.TuKhoa = sTuKhoa;// tại vì từ khóa là viewbag mà viewbag nó chỉ lưu 1 lần, khi ta đổi  Action thì nó mất nên ta phải lưu nó lại xài dài dài


            List<MENU> lstKQTK = db.MENUs.Where(n => n.TenSP.Contains(sTuKhoa)).ToList();
            //phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 4;

            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm cần tìm";
                return View(db.MENUs.ToList().ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy" + lstKQTK.Count + "kết quả";
            return View(lstKQTK.ToPagedList(pageNumber, pageSize));
        }
    }
}