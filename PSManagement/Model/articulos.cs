//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class articulos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public articulos()
        {
            this.detallesfactura = new HashSet<detallesfactura>();
        }
    
        public int IdArticulo { get; set; }
        public int Inventario { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public int Categoria { get; set; }
        public int Color { get; set; }
        public double PrecioUnitario { get; set; }
        public int StockMinimo { get; set; }
        public sbyte StockDisponible { get; set; }
    
        public virtual categorias categorias { get; set; }
        public virtual colores colores { get; set; }
        public virtual inventarios inventarios { get; set; }
        public virtual cantidadpornumeros cantidadpornumeros { get; set; }
        public virtual cantidadportallas cantidadportallas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detallesfactura> detallesfactura { get; set; }
    }
}
