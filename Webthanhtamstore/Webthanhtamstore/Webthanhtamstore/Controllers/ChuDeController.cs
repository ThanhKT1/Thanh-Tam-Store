using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webthanhtamstore.Models;

namespace Webthanhtamstore.Controllers
{
    public class ChuDeController : Controller
    {
        // GET: ChuDe
        THANHTAMSTOREEntities1 db = new THANHTAMSTOREEntities1();
        public PartialViewResult ChuDePartial()
        {
            return PartialView(db.CHUDEs.ToList());
        }

        public ViewResult SachTheoCD(int MaChuDe=0)
        {
            CHUDE cd = db.CHUDEs.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<MENU> listSP = db.MENUs.Where(n => n.MaChuDe == MaChuDe).OrderBy(n => n.Gia).ToList();
            if (listSP.Count == 0)
            {
                ViewBag.Menu = "Không có sản phẩm cần tìm";
            }
            return View(listSP);
        }
    }
}