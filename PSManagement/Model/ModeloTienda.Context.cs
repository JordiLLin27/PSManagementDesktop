﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PSManagement.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bdtiendaEntities : DbContext
    {
        public bdtiendaEntities()
            : base("name=bdtiendaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<articulos> articulos { get; set; }
        public virtual DbSet<categorias> categorias { get; set; }
        public virtual DbSet<colores> colores { get; set; }
        public virtual DbSet<detallesfactura> detallesfactura { get; set; }
        public virtual DbSet<facturas> facturas { get; set; }
        public virtual DbSet<inventarios> inventarios { get; set; }
        public virtual DbSet<numeroscalzado> numeroscalzado { get; set; }
        public virtual DbSet<tallastextiles> tallastextiles { get; set; }
    }
}
