using System;
using System.Collections.Generic;

namespace QPH_MAIN.Core.QueryFilters
{
    public class ChannelQueryFilter
    {
        public string filter { get; set; }
        public DateTime Fecha { get; set; }
        public string Segmento { get; set; }
        public string PuntoEmision { get; set; }
        public string Ambiente { get; set; }
        public double Iva { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string CategoriaCliente { get; set; }
        public string CuentaContable { get; set; }
        public string GrupoCredito { get; set; }
        public string DocumentoElectronico { get; set; }
        public string Relacionado { get; set; }
        public string VendorSeccion { get; set; }
        public string ListaPrecioContado { get; set; }
        public string ListaPrecioCredito { get; set; }
        public string LimiteCredito { get; set; }
        public string Uge { get; set; }
        public string Bodega { get; set; }
        public string FormaPago { get; set; }
        public string Status { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<SortModel> orderedBy { get; set; }
    }
}