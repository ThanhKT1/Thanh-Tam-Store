using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Webthanhtamstore.Models
{
    
    public class GioHang
    {
        THANHTAMSTOREEntities1 db = new THANHTAMSTOREEntities1();
        public int iMaSP { get; set; }

        public string sTenSP { get; set; }

        public string iAnh { get; set; }

        public double dGia { get; set; }

        public int iSoLuong { get; set; }

        public double dThanhTien {
            get { return iSoLuong * dGia; }
        }

        public GioHang(int MaSP) //Hàm tạo cho giỏ hàng
        {
            iMaSP = MaSP;
            MENU menu = db.MENUs.Single(n => n.MaSP == iMaSP);
            sTenSP = menu.TenSP;
            iAnh = menu.Anh;
            dGia = double.Parse(menu.Gia.ToString());
            iSoLuong = 1;
            
    }
    }

 
}