namespace TPParcial1.Modelos;

// Catálogo. Baja SIEMPRE lógica (Activo = false).
public class Producto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public decimal Precio { get; set; }
    public bool Activo { get; set; } = true;
}
