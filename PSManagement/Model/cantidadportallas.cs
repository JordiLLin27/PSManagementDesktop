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
    
    public partial class cantidadportallas
    {
        public int IdArticulo { get; set; }
        public int XS { get; set; }
        public int S { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public int XL { get; set; }
    
        public virtual articulos ARTICULOS { get; set; }
    }
}
