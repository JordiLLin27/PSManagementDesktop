using PSManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PSManagement.Service
{
    public static class BBDDService
    {
        private static readonly bdtiendaEntities _context = new bdtiendaEntities();

        static BBDDService()
        {

            _context.inventarios.Load();
            _context.categorias.Load();
            _context.colores.Load();
            _context.articulos.Load();

            _context.numeroscalzado.Load();
            _context.tallastextiles.Load();

            _context.facturas.Load();
            _context.detallesfactura.Load();

            _context.descuentos.Load();
        }

        //*******
        // ADD (CREATE)
        //*******
        public static int AddInventario(inventarios inventarioAdd)
        {
            _context.inventarios.Add(inventarioAdd);
            return SaveChanges();
        }

        public static int AddCategoria(categorias categoriaAdd)
        {
            _context.categorias.Add(categoriaAdd);
            return SaveChanges();
        }

        public static int AddColor(colores colorAdd)
        {
            _context.colores.Add(colorAdd);
            return SaveChanges();
        }

        public static int AddArticulo(articulos articuloAdd)
        {
            _context.articulos.Add(articuloAdd);
            return SaveChanges();
        }

        public static int AddFactura(facturas facturaAdd)
        {
            _context.facturas.Add(facturaAdd);
            return SaveChanges();
        }

        public static int AddDetallesAFactura(detallesfactura detallesFacturaAdd)
        {
            _context.detallesfactura.Add(detallesFacturaAdd);
            return SaveChanges();
        }

        public static int AddTallasTextiles(tallastextiles tallasTextilAdd)
        {
            _context.tallastextiles.Add(tallasTextilAdd);
            return SaveChanges();
        }

        public static int AddNumerosCalzado(numeroscalzado numerosCalzAdd)
        {
            _context.numeroscalzado.Add(numerosCalzAdd);
            return SaveChanges();
        }

        public static int AddDescuento(descuentos descuentoAdd)
        {
            _context.descuentos.Add(descuentoAdd);
            return SaveChanges();
        }

        //*******
        //GETTERS (READ)
        //******
        public static ObservableCollection<inventarios> GetInventarios()
        {
            return _context.inventarios.Local;
        }

        public static ObservableCollection<categorias> GetCategorias()
        {
            return _context.categorias.Local;
        }

        public static ObservableCollection<colores> GetColores()
        {
            return _context.colores.Local;
        }

        public static ObservableCollection<articulos> GetArticulos()
        {
            return _context.articulos.Local;
        }

        public static ObservableCollection<numeroscalzado> GetNumerosCalzado()
        {
            return _context.numeroscalzado.Local;
        }

        public static ObservableCollection<tallastextiles> GetTallasTextiles()
        {
            return _context.tallastextiles.Local;
        }

        public static ObservableCollection<facturas> GetFacturas()
        {
            return _context.facturas.Local;
        }

        public static ObservableCollection<descuentos> GetDescuentos()
        {
            return _context.descuentos.Local;
        }

        public static ObservableCollection<detallesfactura> GetDetallesfactura(facturas factura)
        {
            var query = from df in _context.detallesfactura
                        where df.CodFactura == factura.IdFactura
                        select df;

            return new ObservableCollection<detallesfactura>(query.ToList());
        }

        public static articulos FindArticulo(articulos articuloFind)
        {

            var query = from art in _context.articulos
                        where art.CodArticulo == articuloFind.CodArticulo
                        select art;

            return query.First();
        }

        public static CollectionViewSource GetArticulosPorCategoria(int idCategoria)
        {
            CollectionViewSource collection = new CollectionViewSource();

            var query = from art in _context.articulos
                        where art.Categoria == idCategoria &&
                        (art.TALLASTEXTILES.TotalCantidadArticulo > 0 || art.NUMEROSCALZADO.TotalCantidadArticulo > 0)
                        select art;

            collection.Source = query.ToList();

            return collection;
        }

        private static bool TieneTallas(articulos articulo)
        {
            if (articulo.TALLASTEXTILES != null)
            {
                if (articulo.TALLASTEXTILES.TotalCantidadArticulo <= 0)
                {
                    return false;

                }
                else
                    return true;
            }
            else if (articulo.NUMEROSCALZADO != null)
            {
                if (articulo.NUMEROSCALZADO.TotalCantidadArticulo <= 0)
                {
                    return false;
                }
                else
                    return true;
            }
            else return false;
        }

        public static tallastextiles GetArticuloTallasTextiles(articulos articuloBuscar)
        {
            var query = from tallas in _context.tallastextiles
                        where tallas.ARTICULO.CodArticulo == articuloBuscar.CodArticulo
                        select tallas;

            if (query.Count() == 0)
                return null;
            else
                return query.First();
        }

        public static numeroscalzado GetArticuloNumerosCalzado(articulos articuloBuscar)
        {
            var query = from numeros in _context.numeroscalzado
                        where numeros.ARTICULO.CodArticulo == articuloBuscar.CodArticulo
                        select numeros;

            if (query.Count() == 0)
                return null;
            else
                return query.First();
        }

        public static int GetIdFactura()
        {
            var query = from fact in _context.facturas
                        select fact;

            if (query.Count() > 0)
                return query.ToList().Last().IdFactura;
            else
                return 0;
        }

        /*
        public static detallesfactura GetDetallesFactura(int idFactura, string CodArt, string talla)
        {
            
        }
        */

        //*******
        // UPDATE
        //*******
        public static int SaveChanges()
        {
            return _context.SaveChanges();
        }

        //*******
        // DELETE
        //*******
        public static int DeleteInventario(inventarios inventarioDel)
        {
            _context.inventarios.Remove(inventarioDel);
            return SaveChanges();
        }

        public static int DeleteCategoria(categorias categoriaDel)
        {
            _context.categorias.Remove(categoriaDel);
            return SaveChanges();
        }

        public static int DeleteColor(colores colorDel)
        {
            _context.colores.Remove(colorDel);
            return SaveChanges();
        }

        public static int DeleteArticulo(articulos articuloDel)
        {
            _context.articulos.Remove(articuloDel);
            return SaveChanges();
        }

        public static int DeleteFactura(facturas facturaDel)
        {
            _context.facturas.Remove(facturaDel);
            return SaveChanges();
        }

        public static int DeleteDetallesAFactura(detallesfactura detallesFacturaDel)
        {
            _context.detallesfactura.Remove(detallesFacturaDel);
            return SaveChanges();
        }

        public static int DeleteTallasTextiles(tallastextiles tallasTextilDel)
        {
            _context.tallastextiles.Remove(tallasTextilDel);
            return SaveChanges();
        }

        public static int DeleteNumerosCalzado(numeroscalzado numerosCalzDel)
        {
            _context.numeroscalzado.Remove(numerosCalzDel);
            return SaveChanges();
        }

        public static int DeleteDescuentos(descuentos descuentosDel)
        {
            _context.descuentos.Remove(descuentosDel);
            return SaveChanges();
        }

        //*******
        // Auxiliares
        //*******
        public static void RevertChanges()
        {
            foreach (DbEntityEntry entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    // Under the covers, changing the state of an entity from  
                    // Modified to Unchanged first sets the values of all  
                    // properties to the original values that were read from  
                    // the database when it was queried, and then marks the  
                    // entity as Unchanged. This will also reject changes to  
                    // FK relationships since the original value of the FK  
                    // will be restored. 
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // If the EntityState is the Deleted, reload the date from the database.   
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }

        public static bool ExisteDetallesArticuloEnFactura(string talla)
        {
            var query = from detalles in _context.detallesfactura
                        where detalles.TallaONum.ToLower().Contains(talla.ToLower())
                        select detalles;

            if (query.ToList().Count > 0)
                return true;
            else
                return false;
        }
    }
}
