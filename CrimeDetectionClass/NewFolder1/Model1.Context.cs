﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrimeDetectionClass.NewFolder1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CrimeEntities : DbContext
    {
        public CrimeEntities()
            : base("name=CrimeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CrimeComment> CrimeComments { get; set; }
        public virtual DbSet<CrimeUser> CrimeUsers { get; set; }
        public virtual DbSet<tblCrime> tblCrimes { get; set; }
    }
}
