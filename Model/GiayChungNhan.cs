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
    
    public partial class GiayChungNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GiayChungNhan()
        {
            this.CoSoChanNuois = new HashSet<CoSoChanNuoi>();
            this.CoSoCheBiens = new HashSet<CoSoCheBien>();
        }
    
        public string MaGCN { get; set; }
        public string TenGCN { get; set; }
        public string MotaGCN { get; set; }
        public string MaCSGCN { get; set; }
    
        public virtual CoSoCapGCN CoSoCapGCN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoSoChanNuoi> CoSoChanNuois { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoSoCheBien> CoSoCheBiens { get; set; }
    }
}
