﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PerspectivaCliente.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbEjemploEntities : DbContext
    {
        public dbEjemploEntities()
            : base("name=dbEjemploEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<CategoriasInteres> CategoriasInteres { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<PosibilidadesEconomicas> PosibilidadesEconomicas { get; set; }
        public virtual DbSet<Preferencias> Preferencias { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
