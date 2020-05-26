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
    public static class BbddService
    {
        private static readonly balmis_pdam04bEntities _context = new balmis_pdam04bEntities();

        static BbddService()
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

        /// <summary>
        /// Añade un inventario a la base de datos
        /// </summary>
        /// <param name="inventarioAdd">Objeto inventario a añadir a la base de datos</param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos</returns>
        public static int AddInventario(inventarios inventarioAdd)
        {
            _context.inventarios.Add(inventarioAdd);
            return SaveChanges();
        }

        /// <summary>
        /// Añade una categoría a la base de datos
        /// </summary>
        /// <param name="categoriaAdd">Objeto categoria a añadir a la base de datos</param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos</returns>
        public static int AddCategoria(categorias categoriaAdd)
        {
            _context.categorias.Add(categoriaAdd);
            return SaveChanges();
        }

        /// <summary>
        /// Añade un color a la base de datos
        /// </summary>
        /// <param name="colorAdd">Objeto color a añadir a la base de datos</param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos</returns>
        public static int AddColor(colores colorAdd)
        {
            _context.colores.Add(colorAdd);
            return SaveChanges();
        }

        /// <summary>
        /// Añade un artículo a la base de datos
        /// </summary>
        /// <param name="articuloAdd">Objeto articulo a añadir a la base de datos</param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos</returns>
        public static int AddArticulo(articulos articuloAdd)
        {
            _context.articulos.Add(articuloAdd);
            return SaveChanges();
        }

        /// <summary>
        /// Añade una factura a la base de datos
        /// </summary>
        /// <param name="facturaAdd">Objeto factura a añadir a la base de datos</param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos</returns>
        public static int AddFactura(facturas facturaAdd)
        {
            _context.facturas.Add(facturaAdd);
            return SaveChanges();
        }

        /// <summary>
        /// Añade detalles de una factura a la base de datos
        /// </summary>
        /// <param name="detallesFacturaAdd">Objeto detallesfactura a añadir a la base de datos</param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos</returns>
        public static int AddDetallesAFactura(detallesfactura detallesFacturaAdd)
        {
            _context.detallesfactura.Add(detallesFacturaAdd);
            return SaveChanges();
        }

        /// <summary>
        /// Añade información sobre tallas textiles de un artículo a la base de datos
        /// </summary>
        /// <param name="tallasTextilAdd">Objeto tallastextiles a añadir a la base de datos</param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos</returns>
        public static int AddTallasTextiles(tallastextiles tallasTextilAdd)
        {
            _context.tallastextiles.Add(tallasTextilAdd);
            return SaveChanges();
        }

        /// <summary>
        /// Añade información sobre números de calzado de un artículo a la base de datos
        /// </summary>
        /// <param name="numerosCalzAdd">Objeto numeroscalzado a añadir a la base de datos</param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos</returns>
        public static int AddNumerosCalzado(numeroscalzado numerosCalzAdd)
        {
            _context.numeroscalzado.Add(numerosCalzAdd);
            return SaveChanges();
        }

        /// <summary>
        /// Añade un descuento a la base de datos
        /// </summary>
        /// <param name="descuentoAdd">Objeto descuento a añadir a la base de datos</param>
        /// <returns>Devuelve el número de cambios realizados en la base de datos</returns>
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

        /// <summary>
        /// Consulta para detalles de una factura.
        /// </summary>
        /// <param name="factura">Factura para consultar sus detalles</param>
        /// <returns>Lista de detalles de una factura determinada</returns>
        public static ObservableCollection<detallesfactura> GetDetallesfactura(facturas factura)
        {
            var query = from df in _context.detallesfactura
                        where df.CodFactura == factura.IdFactura
                        select df;

            return new ObservableCollection<detallesfactura>(query.ToList());
        }

        /// <summary>
        /// Consulta para buscar un artículo en la base de datos.
        /// </summary>
        /// <param name="articuloFind">Artículo a buscar</param>
        /// <returns>El primer artículo encontrado, null en cualquier otro caso.</returns>
        public static articulos FindArticulo(articulos articuloFind)
        {

            var query = from art in _context.articulos
                        where art.CodArticulo == articuloFind.CodArticulo
                        select art;

            return query.First();
        }

        /// <summary>
        /// Consulta para obtener una colección de artículos según la categoría indicada.
        /// </summary>
        /// <param name="idCategoria">Categoría para realizar la consulta</param>
        /// <returns>Colección de artículos agrupados por una categoría determinada</returns>
        public static CollectionViewSource GetArticulosPorCategoria(int idCategoria)
        {
            CollectionViewSource collection = new CollectionViewSource();

            var query = from art in _context.articulos
                        where art.Categoria == idCategoria &&
                        (art.TALLASTEXTILES.TotalCantidadArticulo > 0 || art.NUMEROSCALZADO.TotalCantidadArticulo > 0) && art.StockDisponible == 1
                        select art;

            collection.Source = query.ToList();

            return collection;
        }

        /// <summary>
        /// Consulta para obtener las tallas textiles de un artículo.
        /// </summary>
        /// <param name="articuloBuscar">Artículo para la consulta</param>
        /// <returns>Tallas textiles del artículo, null en otro caso</returns>
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

        /// <summary>
        /// Consulta para obtener los números de calzado de un artículo
        /// </summary>
        /// <param name="articuloBuscar">Artículo para la consulta</param>
        /// <returns>Números de calzado del artículo, null en otro caso</returns>
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

        /// <summary>
        /// Consulta para obtener el último id de una factura
        /// </summary>
        /// <returns>Último id de la tabla facturas</returns>
        public static int GetIdFactura()
        {
            var query = from fact in _context.facturas
                        select fact;

            if (query.Count() > 0)
                return query.Last().IdFactura;
            else
                return 0;
        }

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

        /// <summary>
        /// Elimina el registro indicado
        /// </summary>
        /// <param name="inventarioDel">Inventario a eliminar</param>
        /// <returns>Número de cambios en la base de datos</returns>
        public static int DeleteInventario(inventarios inventarioDel)
        {
            _context.inventarios.Remove(inventarioDel);
            return SaveChanges();
        }

        /// <summary>
        /// Elimina el registro indicado
        /// </summary>
        /// <param name="categoriaDel">Categoría a eliminar</param>
        /// <returns>Número de cambios en la base de datos</returns>
        public static int DeleteCategoria(categorias categoriaDel)
        {
            _context.categorias.Remove(categoriaDel);
            return SaveChanges();
        }

        /// <summary>
        /// Elimina el registro indicado
        /// </summary>
        /// <param name="colorDel">color a eliminar</param>
        /// <returns>Número de cambios en la base de datos</returns>
        public static int DeleteColor(colores colorDel)
        {
            _context.colores.Remove(colorDel);
            return SaveChanges();
        }

        /// <summary>
        /// Elimina el registro indicado
        /// </summary>
        /// <param name="articuloDel">artículo a eliminar</param>
        /// <returns>Número de cambios en la base de datos</returns>
        public static int DeleteArticulo(articulos articuloDel)
        {
            _context.articulos.Remove(articuloDel);
            return SaveChanges();
        }

        /// <summary>
        /// Elimina el registro indicado
        /// </summary>
        /// <param name="facturaDel">Factura a eliminar</param>
        /// <returns>Número de cambios en la base de datos</returns>
        public static int DeleteFactura(facturas facturaDel)
        {
            _context.facturas.Remove(facturaDel);
            return SaveChanges();
        }

        /// <summary>
        /// Elimina el registro indicado
        /// </summary>
        /// <param name="detallesFacturaDel">Registro de detalles de factura a eliminar</param>
        /// <returns>Número de cambios en la base de datos</returns>
        public static int DeleteDetallesAFactura(detallesfactura detallesFacturaDel)
        {
            _context.detallesfactura.Remove(detallesFacturaDel);
            return SaveChanges();
        }

        /// <summary>
        /// Elimina el registro indicado
        /// </summary>
        /// <param name="tallasTextilDel">Tallas textiles de un artículo a eliminar</param>
        /// <returns>Número de cambios en la base de datos</returns>
        public static int DeleteTallasTextiles(tallastextiles tallasTextilDel)
        {
            _context.tallastextiles.Remove(tallasTextilDel);
            return SaveChanges();
        }

        /// <summary>
        /// Elimina el registro indicado
        /// </summary>
        /// <param name="numerosCalzDel">Números de calzado de un artículo a eliminar</param>
        /// <returns>Número de cambios en la base de datos</returns>
        public static int DeleteNumerosCalzado(numeroscalzado numerosCalzDel)
        {
            _context.numeroscalzado.Remove(numerosCalzDel);
            return SaveChanges();
        }

        /// <summary>
        /// Elimina el registro indicado
        /// </summary>
        /// <param name="descuentosDel">Descuento a eliminar</param>
        /// <returns>Número de cambios en la base de datos</returns>
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
                    //El code behind del EntityModel, cambia el estado de una entidad de 'modificado' a 'sin cambios'. 
                    //Primero establece los valores de todas las propiedades a los valores originales que se leyeron de la base de datos cuando se consultó, 
                    //y luego marca la entidad como 'Sin cambios'. 
                    //Esto también rechazará los cambios en las relaciones FK ya que se restaurará el valor original de FK.
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // Si el EntityState es 'Deleted', recarga los datos desde la base de datos.   
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// Comprueba si existe un detalle de factura en una factura
        /// </summary>
        /// <param name="talla">Talla del detalle de la factura a comprobar</param>
        /// <returns>Verdadero si encuentra el registro, falso en otro caso.</returns>
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
