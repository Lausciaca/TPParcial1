using Microsoft.Data.SqlClient;
using TPParcial1.Modelos;

namespace TPParcial1.Datos;

// Informe §4.4: por producto, cantidad facturada y monto en un período.
// Excluye facturas anuladas. Resultado listo para bindear a grilla.
public class InformeDatos
{
    private readonly string _cs;
    public InformeDatos() : this(FabricaConexiones.ObtenerCadenaConexion()) { }
    public InformeDatos(string connectionString) => _cs = connectionString;

    public List<InformeProducto> VentasPorProducto(DateTime desde, DateTime hasta)
    {
        const string sql =
            @"SELECT p.Id, p.Codigo, p.Nombre,
                     SUM(d.Cantidad) AS Cantidad,
                     SUM(d.Subtotal) AS Monto
              FROM FacturaDetalle d
              INNER JOIN Facturas f ON f.Id = d.FacturaId
              INNER JOIN Productos p ON p.Id = d.ProductoId
              WHERE f.Anulada = 0 AND f.Fecha >= @Desde AND f.Fecha <= @Hasta
              GROUP BY p.Id, p.Codigo, p.Nombre
              ORDER BY Monto DESC;";
        try
        {
            using var cn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Desde", desde);
            cmd.Parameters.AddWithValue("@Hasta", hasta);
            cn.Open();
            using var rd = cmd.ExecuteReader();
            var lista = new List<InformeProducto>();
            while (rd.Read())
            {
                lista.Add(new InformeProducto
                {
                    ProductoId = rd.GetInt32(0),
                    Codigo = rd.GetString(1),
                    Nombre = rd.GetString(2),
                    CantidadFacturada = rd.IsDBNull(3) ? 0 : Convert.ToInt32(rd.GetValue(3)),
                    Monto = rd.IsDBNull(4) ? 0 : Convert.ToDecimal(rd.GetValue(4))
                });
            }
            return lista;
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Informe de ventas"); }
    }
}
