//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webthanhtamstore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHITIETDONHANGGGG
    {
        public int MaDonHang { get; set; }
        public int MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string DonGia { get; set; }
    
        public virtual DONHANGGG DONHANGGG { get; set; }
        public virtual MENU MENU { get; set; }
    }
}