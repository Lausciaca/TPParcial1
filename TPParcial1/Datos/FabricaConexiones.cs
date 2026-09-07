using System.Configuration;
using Microsoft.Data.SqlClient;

namespace TPParcial1.Datos;

// Único lugar que conoce la cadena de conexión.
// Los Forms NO la usan directo: pasan por Datos/Servicios.
public static class FabricaConexiones
{
    public static string ObtenerCadenaConexion()
    {
        var cs = ConfigurationManager.ConnectionStrings["Facturacion"]?.ConnectionString;
        if (string.IsNullOrWhiteSpace(cs))
            throw new InvalidOperationException(
                "Falta la cadena de conexión 'Facturacion' en App.config.");
        return cs;
    }

    public static SqlConnection CrearConexion() => new(ObtenerCadenaConexion());
}
