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
    
    public partial class detallesfactura
    {
        public int CodFactura { get; set; }
        public string CodArticulo { get; set; }
        public int CantidadArticulo { get; set; }
        public string TallaONum { get; set; }
    
        public virtual articulos ARTICULO { get; set; }
        public virtual facturas FACTURA { get; set; }
    }
}
