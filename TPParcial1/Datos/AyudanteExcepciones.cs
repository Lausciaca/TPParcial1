using Microsoft.Data.SqlClient;
using TPParcial1.Comun;

namespace TPParcial1.Datos;

// Traduce errores SQL a mensajes claros para la UI.
// Distingue: conexión vs unique vs FK (pedido en el TP §5.5).
public static class AyudanteExcepciones
{
    public static Exception Traducir(SqlException ex, string operacion)
    {
        // 2627 / 2601 = violación de UNIQUE (ej. Codigo o Numero duplicado)
        if (ex.Number == 2627 || ex.Number == 2601)
            return new ExcepcionValidacion($"{operacion}: el dato ya existe (duplicado). Detalle: {ex.Message}");

        // 547 = violación de FK (ej. referenciar producto inexistente)
        if (ex.Number == 547)
            return new ExcepcionValidacion($"{operacion}: dato referenciado inválido. Detalle: {ex.Message}");

        // Conexión / red / instancia / login
        if (EsErrorConexion(ex))
            return new InvalidOperationException(
                $"{operacion}: no se pudo conectar a SQL Server. Verifique que la instancia esté encendida y la cadena de conexión en App.config. Detalle: {ex.Message}", ex);

        return new InvalidOperationException($"{operacion}: error de base de datos ({ex.Number}). {ex.Message}", ex);
    }

    private static bool EsErrorConexion(SqlException ex)
    {
        // -2 timeout, 2 / 53 servidor no encontrado, 4060 base inválida, 18456 login
        foreach (SqlError e in ex.Errors)
        {
            if (e.Number is -2 or 2 or 53 or 4060 or 18456 or 40615 or 40197)
                return true;
            if (e.Message.Contains("network", StringComparison.OrdinalIgnoreCase) ||
                e.Message.Contains("red", StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
}
