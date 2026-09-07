using Microsoft.Data.SqlClient;
using TPParcial1.Comun;
using TPParcial1.Modelos;

namespace TPParcial1.Datos;

// Emisión en UNA transacción: cabecera + detalle van juntos
// o se hace rollback completo (TP §4.2.5).
// El PrecioUnitario se relee de Productos DENTRO de la tx y se
// copia al detalle: la factura vieja no cambia si el precio cambia.
public class FacturaDatos
{
    private readonly string _cs;
    public FacturaDatos() : this(FabricaConexiones.ObtenerCadenaConexion()) { }
    public FacturaDatos(string connectionString) => _cs = connectionString;

    // Devuelve el Numero asignado por la SEQUENCE.
    public int Emitir(Factura factura)
    {
        if (factura.Detalles.Count == 0)
            throw new ExcepcionValidacion("La factura debe tener al menos una línea.");
        if (string.IsNullOrWhiteSpace(factura.ClienteNombre))
            throw new ExcepcionValidacion("El nombre del cliente no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(factura.ClienteDocumento))
            throw new ExcepcionValidacion("El documento del cliente no puede estar vacío.");

        try
        {
            using var cn = new SqlConnection(_cs);
            cn.Open();
            using var tx = cn.BeginTransaction();

            try
            {
                // 1) Releer precio vigente y validar activo, DENTRO de la tx.
                var precios = new Dictionary<int, (decimal Precio, string Codigo, string Nombre)>();
                foreach (var d in factura.Detalles)
                {
                    using var cmdP = new SqlCommand(
                        "SELECT Precio, Activo, Codigo, Nombre FROM Productos WHERE Id = @Id;", cn, tx);
                    cmdP.Parameters.AddWithValue("@Id", d.ProductoId);
                    using var rd = cmdP.ExecuteReader();
                    if (!rd.Read())
                        throw new ExcepcionValidacion($"El producto Id={d.ProductoId} no existe.");
                    decimal precio = rd.GetDecimal(0);
                    bool activo = rd.GetBoolean(1);
                    string codigo = rd.GetString(2);
                    string nombre = rd.GetString(3);
                    if (!activo)
                        throw new ExcepcionValidacion($"El producto '{nombre}' está inactivo y no puede facturarse.");
                    if (d.Cantidad <= 0)
                        throw new ExcepcionValidacion($"Cantidad inválida en '{nombre}'.");
                    precios[d.ProductoId] = (precio, codigo, nombre);
                }

                // 2) Calcular total con precios vigentes (no confiamos en lo que mandó la UI).
                decimal total = 0;
                foreach (var d in factura.Detalles)
                {
                    d.PrecioUnitario = precios[d.ProductoId].Precio;
                    d.Recalcular();
                    total += d.Subtotal;
                }

                // 3) INSERT cabecera (Numero lo asigna la SEQUENCE por DEFAULT).
                const string sqlCab =
                    @"INSERT INTO Facturas (Fecha, ClienteNombre, ClienteDocumento, Total)
                      OUTPUT INSERTED.Id, INSERTED.Numero
                      VALUES (@Fecha, @Cliente, @Doc, @Total);";
                int facturaId, numero;
                using (var cmdC = new SqlCommand(sqlCab, cn, tx))
                {
                    cmdC.Parameters.AddWithValue("@Fecha", factura.Fecha);
                    cmdC.Parameters.AddWithValue("@Cliente", factura.ClienteNombre.Trim());
                    cmdC.Parameters.AddWithValue("@Doc", factura.ClienteDocumento.Trim());
                    cmdC.Parameters.AddWithValue("@Total", total);
                    using var rd = cmdC.ExecuteReader();
                    rd.Read();
                    facturaId = rd.GetInt32(0);
                    numero = rd.GetInt32(1);
                }

                // 4) INSERT detalle.
                const string sqlDet =
                    @"INSERT INTO FacturaDetalle (FacturaId, ProductoId, Cantidad, PrecioUnitario, Subtotal)
                      VALUES (@F, @P, @C, @PU, @S);";
                foreach (var d in factura.Detalles)
                {
                    using var cmdD = new SqlCommand(sqlDet, cn, tx);
                    cmdD.Parameters.AddWithValue("@F", facturaId);
                    cmdD.Parameters.AddWithValue("@P", d.ProductoId);
                    cmdD.Parameters.AddWithValue("@C", d.Cantidad);
                    cmdD.Parameters.AddWithValue("@PU", d.PrecioUnitario);
                    cmdD.Parameters.AddWithValue("@S", d.Subtotal);
                    cmdD.ExecuteNonQuery();
                }

                tx.Commit();
                factura.Id = facturaId;
                factura.Numero = numero;
                factura.Total = total;
                return numero;
            }
            catch
            {
                tx.Rollback(); // Falla cualquier INSERT -> rollback completo.
                throw;
            }
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Emisión de factura"); }
    }

    // Listado con filtro por fechas y/o texto de cliente.
    // Si "hasta" viene sin hora (00:00), se extiende al fin de ese día
    // para no excluir las facturas del día (el Form suele pasar solo la fecha).
    public List<Factura> BuscarCabeceras(DateTime? desde, DateTime? hasta, string? cliente)
    {
        if (hasta.HasValue && hasta.Value.TimeOfDay == TimeSpan.Zero)
            hasta = hasta.Value.Date.AddDays(1).AddTicks(-1);
        const string sql =
            @"SELECT Id, Numero, Fecha, ClienteNombre, ClienteDocumento, Total, Anulada, FechaAnulacion
              FROM Facturas
              WHERE (@Desde IS NULL OR Fecha >= @Desde)
                AND (@Hasta IS NULL OR Fecha <= @Hasta)
                AND (@Cli IS NULL OR ClienteNombre LIKE '%' + @Cli + '%' OR ClienteDocumento LIKE '%' + @Cli + '%')
              ORDER BY Numero DESC;";
        try
        {
            using var cn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Desde", desde ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Hasta", hasta ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Cli", string.IsNullOrWhiteSpace(cliente) ? DBNull.Value : cliente.Trim());
            cn.Open();
            using var rd = cmd.ExecuteReader();
            var lista = new List<Factura>();
            while (rd.Read()) lista.Add(MapearCabecera(rd));
            return lista;
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Consulta de facturas"); }
    }

    // Cabecera + detalle en solo lectura (facturas emitidas no se editan).
    public Factura? ObtenerCompleta(int id)
    {
        try
        {
            using var cn = new SqlConnection(_cs);
            cn.Open();

            Factura? f = null;
            using (var cmd = new SqlCommand(
                "SELECT Id, Numero, Fecha, ClienteNombre, ClienteDocumento, Total, Anulada, FechaAnulacion FROM Facturas WHERE Id = @Id;", cn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using var rd = cmd.ExecuteReader();
                if (!rd.Read()) return null;
                f = MapearCabecera(rd);
            }

            using (var cmd = new SqlCommand(
                @"SELECT d.Id, d.FacturaId, d.ProductoId, p.Codigo, p.Nombre, d.Cantidad, d.PrecioUnitario, d.Subtotal
                  FROM FacturaDetalle d INNER JOIN Productos p ON p.Id = d.ProductoId
                  WHERE d.FacturaId = @Id ORDER BY d.Id;", cn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    f.Detalles.Add(new FacturaDetalle
                    {
                        Id = rd.GetInt32(0),
                        FacturaId = rd.GetInt32(1),
                        ProductoId = rd.GetInt32(2),
                        CodigoProducto = rd.GetString(3),
                        NombreProducto = rd.GetString(4),
                        Cantidad = rd.GetInt32(5),
                        PrecioUnitario = rd.GetDecimal(6),
                        Subtotal = rd.GetDecimal(7)
                    });
                }
            }
            return f;
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Lectura de factura"); }
    }

    // Anulación por estado. No existe DELETE de facturas. No se puede anular dos veces.
    public void Anular(int id)
    {
        const string sql = @"UPDATE Facturas SET Anulada = 1, FechaAnulacion = SYSDATETIME()
                             WHERE Id = @Id AND Anulada = 0;";
        try
        {
            using var cn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Id", id);
            cn.Open();
            if (cmd.ExecuteNonQuery() == 0)
                throw new ExcepcionValidacion("La factura no existe o ya estaba anulada.");
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Anulación de factura"); }
    }

    private static Factura MapearCabecera(SqlDataReader rd) => new()
    {
        Id = rd.GetInt32(0),
        Numero = rd.GetInt32(1),
        Fecha = rd.GetDateTime(2),
        ClienteNombre = rd.GetString(3),
        ClienteDocumento = rd.GetString(4),
        Total = rd.GetDecimal(5),
        Anulada = rd.GetBoolean(6),
        FechaAnulacion = rd.IsDBNull(7) ? null : rd.GetDateTime(7)
    };
}
