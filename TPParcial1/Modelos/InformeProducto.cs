namespace TPParcial1.Modelos;

// Fila del informe: por producto, cantidad y monto en un período.
// Solo cuenta facturas NO anuladas.
public class InformeProducto
{
    public int ProductoId { get; set; }
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public int CantidadFacturada { get; set; }
    public decimal Monto { get; set; }
}
