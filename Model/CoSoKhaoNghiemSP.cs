//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTPMUDMVVM.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CoSoKhaoNghiemSP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CoSoKhaoNghiemSP()
        {
            this.SanPhamXuLyChatThais = new HashSet<SanPhamXuLyChatThai>();
        }
    
        public string MaKN { get; set; }
        public string TenKN { get; set; }
        public string SoDT { get; set; }
        public string MaXa { get; set; }
        public string MaLCS { get; set; }
    
        public virtual Xa Xa { get; set; }
        public virtual LoaiCoSo LoaiCoSo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamXuLyChatThai> SanPhamXuLyChatThais { get; set; }
    }
}
