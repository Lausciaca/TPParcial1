namespace TPParcial1.Comun;

// Error de regla de negocio (validación). Los Forms lo muestran
// con MessageBox sin tratarlo como error de sistema.
public class ExcepcionValidacion : Exception
{
    public ExcepcionValidacion(string message) : base(message) { }
}
