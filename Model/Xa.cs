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
    
    public partial class Xa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Xa()
        {
            this.ChiCucThuYs = new HashSet<ChiCucThuY>();
            this.CoSoCapGCNs = new HashSet<CoSoCapGCN>();
            this.CoSoChanNuois = new HashSet<CoSoChanNuoi>();
            this.CoSoCheBiens = new HashSet<CoSoCheBien>();
            this.CoSoGietMoes = new HashSet<CoSoGietMo>();
            this.CoSoKhaoNghiemSPs = new HashSet<CoSoKhaoNghiemSP>();
            this.CoSoSanXuatSPs = new HashSet<CoSoSanXuatSP>();
            this.DaiLyBanThuocs = new HashSet<DaiLyBanThuoc>();
            this.KhuTamGius = new HashSet<KhuTamGiu>();
            this.Nguoi_dung = new HashSet<Nguoi_dung>();
        }
    
        public string MaXa { get; set; }
        public string TenXa { get; set; }
        public string MaHuyen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiCucThuY> ChiCucThuYs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoSoCapGCN> CoSoCapGCNs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoSoChanNuoi> CoSoChanNuois { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoSoCheBien> CoSoCheBiens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoSoGietMo> CoSoGietMoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoSoKhaoNghiemSP> CoSoKhaoNghiemSPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoSoSanXuatSP> CoSoSanXuatSPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DaiLyBanThuoc> DaiLyBanThuocs { get; set; }
        public virtual Huyen Huyen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhuTamGiu> KhuTamGius { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Nguoi_dung> Nguoi_dung { get; set; }
    }
}
