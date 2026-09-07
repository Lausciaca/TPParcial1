namespace TPParcial1.Modelos;

// Cabecera. Total = suma de subtotales del detalle.
// Las facturas JAMAS se eliminan: se anulan (Anulada = true).
public class Factura
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string ClienteNombre { get; set; } = "";
    public string ClienteDocumento { get; set; } = "";
    public decimal Total { get; set; }
    public bool Anulada { get; set; }
    public DateTime? FechaAnulacion { get; set; }

    public List<FacturaDetalle> Detalles { get; set; } = new();

    public void RecalcularTotal()
    {
        foreach (var d in Detalles) d.Recalcular();
        Total = Detalles.Sum(d => d.Subtotal);
    }
}
