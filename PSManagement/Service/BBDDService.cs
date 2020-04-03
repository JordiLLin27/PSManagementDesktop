using PSManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            _context.cantidadpornumeros.Load();
            _context.cantidadportallas.Load();

            _context.facturas.Load();
            _context.detallesfactura.Load();
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

        public static int AddCantidadPorTallas(cantidadportallas cantidadTAdd)
        {
            _context.cantidadportallas.Add(cantidadTAdd);
            return SaveChanges();
        }

        public static int AddCantidadPorNumeros(cantidadpornumeros cantidadNAdd)
        {
            _context.cantidadpornumeros.Add(cantidadNAdd);
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

        public static ObservableCollection<cantidadpornumeros> GetCantidadpornumeros()
        {
            return _context.cantidadpornumeros.Local;
        }

        public static ObservableCollection<cantidadportallas> GetCantidadportallas()
        {
            return _context.cantidadportallas.Local;
        }

        public static ObservableCollection<facturas> GetFacturas()
        {
            return _context.facturas.Local;
        }

        public static ObservableCollection<detallesfactura> GetDetallesfactura(facturas facturaDetalles)
        {
            var query = from df in _context.detallesfactura
                        where df.IdFactura == facturaDetalles.IdFactura
                        select df;

            return new ObservableCollection<detallesfactura>(query.ToList());
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

        public static int DeleteCantidadPorTallas(cantidadportallas cantidadTDel)
        {
            _context.cantidadportallas.Remove(cantidadTDel);
            return SaveChanges();
        }

        public static int DeleteCantidadPorNumeros(cantidadpornumeros cantidadNDel)
        {
            _context.cantidadpornumeros.Remove(cantidadNDel);
            return SaveChanges();
        }
    }
}
