﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataEntities : DbContext
    {
        public DataEntities()
            : base("name=DataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChiCucThuY> ChiCucThuYs { get; set; }
        public virtual DbSet<CoSoCapGCN> CoSoCapGCNs { get; set; }
        public virtual DbSet<CoSoChanNuoi> CoSoChanNuois { get; set; }
        public virtual DbSet<CoSoCheBien> CoSoCheBiens { get; set; }
        public virtual DbSet<CoSoGietMo> CoSoGietMoes { get; set; }
        public virtual DbSet<CoSoKhaoNghiemSP> CoSoKhaoNghiemSPs { get; set; }
        public virtual DbSet<CoSoSanXuatSP> CoSoSanXuatSPs { get; set; }
        public virtual DbSet<DaiLyBanThuoc> DaiLyBanThuocs { get; set; }
        public virtual DbSet<Dichbenh> Dichbenhs { get; set; }
        public virtual DbSet<DieuKienChanNuoi> DieuKienChanNuois { get; set; }
        public virtual DbSet<DongVat> DongVats { get; set; }
        public virtual DbSet<GiayChungNhan> GiayChungNhans { get; set; }
        public virtual DbSet<Huyen> Huyens { get; set; }
        public virtual DbSet<KhuTamGiu> KhuTamGius { get; set; }
        public virtual DbSet<LoaiCoSo> LoaiCoSoes { get; set; }
        public virtual DbSet<Nguoi_dung> Nguoi_dung { get; set; }
        public virtual DbSet<SanPhamXuLyChatThai> SanPhamXuLyChatThais { get; set; }
        public virtual DbSet<ThongKeChanNuoi> ThongKeChanNuois { get; set; }
        public virtual DbSet<Thuoc> Thuocs { get; set; }
        public virtual DbSet<Tinh> Tinhs { get; set; }
        public virtual DbSet<ToChucCaNhan> ToChucCaNhans { get; set; }
        public virtual DbSet<VungAnToan> VungAnToans { get; set; }
        public virtual DbSet<Xa> Xas { get; set; }
    }
}
