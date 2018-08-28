using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webthanhtamstore.Models;


namespace Webthanhtamstore.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        THANHTAMSTOREEntities1 db = new THANHTAMSTOREEntities1();
      

        public ViewResult XemChiTiet(int MaSP = 0)
        {
            MENU menu = db.MENUs.SingleOrDefault(n => n.MaSP == MaSP);
            if (menu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //CHUDE cd = db.CHUDEs.Single(n => n.MaChuDe == menu.MaChuDe);
            //ViewBag.TenCD = cd.TenChuDe;
            ViewBag.TenChuDe = db.CHUDEs.Single(n => n.MaChuDe == menu.MaChuDe).TenChuDe;
            return View(menu);
        }
      
    }
}