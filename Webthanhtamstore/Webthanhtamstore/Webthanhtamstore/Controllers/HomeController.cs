using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webthanhtamstore.Models;
using PagedList; //Mình phải using 2 cái này để dùng đc PageList
using PagedList.Mvc;//Mình phải using 2 cái này để dùng đc PageList


namespace Webthanhtamstore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        THANHTAMSTOREEntities1 db = new THANHTAMSTOREEntities1();
        public PartialViewResult Index(int? page)//"page" này có nghĩa là bấm vào số trang nào nó gửi đi trang đó
        {
            //Tạo biến sản phẩm trên trang, Gỉa sử 3 sản phẩm trên 1 trang
            int pageSize = 3;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);//VD 1 trang yêu cầu phải có 3 sản phẩm mà chỉ có 2 sản phẩm thôi thì mặc định nó là pageNumber= 1
            return PartialView(db.MENUs.ToList().OrderBy(n=>n.Gia).ToPagedList(pageNumber,pageSize));
        }
    }
}