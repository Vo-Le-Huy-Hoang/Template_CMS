﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CsK24T11Entities : DbContext
    {
        public CsK24T11Entities()
            : base("name=CsK24T11Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BANG_KHACH_HANG> BANG_KHACH_HANG { get; set; }
        public virtual DbSet<BANG_TAI_KHOAN_KHACH_HANG> BANG_TAI_KHOAN_KHACH_HANG { get; set; }
        public virtual DbSet<CHI_TIET_HOA_DON> CHI_TIET_HOA_DON { get; set; }
        public virtual DbSet<CHI_TIET_SAN_PHAM> CHI_TIET_SAN_PHAM { get; set; }
        public virtual DbSet<HOA_DON> HOA_DON { get; set; }
        public virtual DbSet<LOAI_SAN_PHAM> LOAI_SAN_PHAM { get; set; }
        public virtual DbSet<NHA_CUNG_CAP> NHA_CUNG_CAP { get; set; }
        public virtual DbSet<NHA_VAN_CHUYEN> NHA_VAN_CHUYEN { get; set; }
        public virtual DbSet<NHAN_VIEN_VAN_CHUYEN> NHAN_VIEN_VAN_CHUYEN { get; set; }
        public virtual DbSet<QUAN_TRI_VIEN> QUAN_TRI_VIEN { get; set; }
        public virtual DbSet<SAN_PHAM> SAN_PHAM { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
