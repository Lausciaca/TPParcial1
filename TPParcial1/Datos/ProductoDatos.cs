using Microsoft.Data.SqlClient;
using TPParcial1.Comun;
using TPParcial1.Modelos;

namespace TPParcial1.Datos;

// ADO.NET modo conectado: SqlConnection + SqlCommand con
// parámetros @ + SqlDataReader. Conexión abierta el menor
// tiempo posible (using). Sin DataAdapter/DataSet/DataTable.
public class ProductoDatos
{
    private readonly string _cs;
    public ProductoDatos() : this(FabricaConexiones.ObtenerCadenaConexion()) { }
    public ProductoDatos(string connectionString) => _cs = connectionString;

    public int Crear(Producto p)
    {
        const string sql = @"INSERT INTO Productos (Codigo, Nombre, Precio, Activo)
                             OUTPUT INSERTED.Id
                             VALUES (@Codigo, @Nombre, @Precio, @Activo);";
        try
        {
            using var cn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Codigo", p.Codigo.Trim());
            cmd.Parameters.AddWithValue("@Nombre", p.Nombre.Trim());
            cmd.Parameters.AddWithValue("@Precio", p.Precio);
            cmd.Parameters.AddWithValue("@Activo", p.Activo);
            cn.Open();
            return (int)cmd.ExecuteScalar()!;
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Alta de producto"); }
    }

    public void Modificar(Producto p)
    {
        const string sql = @"UPDATE Productos
                             SET Codigo = @Codigo, Nombre = @Nombre, Precio = @Precio, Activo = @Activo
                             WHERE Id = @Id;";
        try
        {
            using var cn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@Codigo", p.Codigo.Trim());
            cmd.Parameters.AddWithValue("@Nombre", p.Nombre.Trim());
            cmd.Parameters.AddWithValue("@Precio", p.Precio);
            cmd.Parameters.AddWithValue("@Activo", p.Activo);
            cn.Open();
            if (cmd.ExecuteNonQuery() == 0)
                throw new ExcepcionValidacion("El producto no existe.");
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Modificación de producto"); }
    }

    // Baja SIEMPRE lógica (decisión del equipo).
    public void Desactivar(int id)
    {
        const string sql = "UPDATE Productos SET Activo = 0 WHERE Id = @Id;";
        try
        {
            using var cn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Id", id);
            cn.Open();
            if (cmd.ExecuteNonQuery() == 0)
                throw new ExcepcionValidacion("El producto no existe.");
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Baja de producto"); }
    }

    public Producto? ObtenerPorId(int id)
    {
        const string sql = "SELECT Id, Codigo, Nombre, Precio, Activo FROM Productos WHERE Id = @Id;";
        try
        {
            using var cn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Id", id);
            cn.Open();
            using var rd = cmd.ExecuteReader();
            return rd.Read() ? Mapear(rd) : null;
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Búsqueda de producto"); }
    }

    // Búsqueda por código o nombre (para listado + combo de factura).
    public List<Producto> Buscar(string? texto, bool soloActivos = false)
    {
        var sql = @"SELECT Id, Codigo, Nombre, Precio, Activo FROM Productos
                    WHERE (@Texto IS NULL OR Codigo LIKE '%' + @Texto + '%' OR Nombre LIKE '%' + @Texto + '%')";
        if (soloActivos) sql += " AND Activo = 1";
        sql += " ORDER BY Nombre;";
        try
        {
            using var cn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Texto", string.IsNullOrWhiteSpace(texto) ? DBNull.Value : texto.Trim());
            cn.Open();
            using var rd = cmd.ExecuteReader();
            var lista = new List<Producto>();
            while (rd.Read()) lista.Add(Mapear(rd));
            return lista;
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Listado de productos"); }
    }

    public bool ExisteCodigo(string codigo, int excluirId = 0)
    {
        const string sql = "SELECT COUNT(*) FROM Productos WHERE Codigo = @Codigo AND Id <> @Excluir;";
        try
        {
            using var cn = new SqlConnection(_cs);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Codigo", codigo.Trim());
            cmd.Parameters.AddWithValue("@Excluir", excluirId);
            cn.Open();
            return (int)cmd.ExecuteScalar()! > 0;
        }
        catch (SqlException ex) { throw AyudanteExcepciones.Traducir(ex, "Validación de código"); }
    }

    private static Producto Mapear(SqlDataReader rd) => new()
    {
        Id = rd.GetInt32(0),
        Codigo = rd.GetString(1),
        Nombre = rd.GetString(2),
        Precio = rd.GetDecimal(3),
        Activo = rd.GetBoolean(4)
    };
}
