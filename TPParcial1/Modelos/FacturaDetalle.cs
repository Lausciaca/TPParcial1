namespace TPParcial1.Modelos;

// Una línea de factura. PrecioUnitario es COPIA del precio
// vigente al facturar: si el producto cambia después,
// la factura vieja no se altera.
public class FacturaDetalle
{
    public int Id { get; set; }
    public int FacturaId { get; set; }
    public int ProductoId { get; set; }

    // Solo para mostrar en grilla (vienen del JOIN con Productos).
    public string CodigoProducto { get; set; } = "";
    public string NombreProducto { get; set; } = "";

    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }

    public void Recalcular() => Subtotal = Cantidad * PrecioUnitario;
}
