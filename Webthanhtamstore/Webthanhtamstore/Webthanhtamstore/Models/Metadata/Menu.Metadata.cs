using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế class Metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;// cái này cho chúng ta thêm những cái thuộc tính valuedation cũng như Displayname

namespace Webthanhtamstore.Models
{
    //Để xây dụng thuộc tính metadata:
    [MetadataTypeAttribute(typeof(MenuMetadata))]//MetadatatypeAttribute nghĩa là các thuộc tính cho cái nhóm trong typeof
    public partial class Menu  //ta có từ khóa "partial", có nghĩa là cái class này sẽ được kết nối với cái class trong Model của ta
    {
        internal sealed class MenuMetadata   //"internal": chỉ riêng 1 cái class này thôi; "sealed":k cho phép kế thừa 
        {   //trước ta code trên MENU của MOdel thì h ta chuyển qua Metadata để code
            [Required(ErrorMessage ="Vui lòng không nhập dữ liệu cho trường này")]
            [Display(Name = "Mã sản phẩm")]

            public int MaSP { get; set; }
           

            [Required(ErrorMessage = "Vui lòng không nhập dữ liệu cho trường này")]
            [Display(Name = "Tên sản phẩm")]

            public string TenSP { get; set; }

            [Required(ErrorMessage = "Vui lòng không nhập dữ liệu cho trường này")]
            [Display(Name = "Giá")]

            public Nullable<decimal> Gia { get; set; }

            [Required(ErrorMessage = "Vui lòng không nhập dữ liệu cho trường này")]
            [Display(Name = "Mô tả")]

            public string MoTa { get; set; }

           
            [Display(Name = "Ảnh sản phẩm")]

            public string Anh { get; set; }

            [Required(ErrorMessage = "Vui lòng không nhập dữ liệu cho trường này")]
            [Display(Name = "Mã chủ đề")]
            public Nullable<int> MaChuDe { get; set; }

          
        } 
    }
}