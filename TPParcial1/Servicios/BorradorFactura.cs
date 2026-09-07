using TPParcial1.Comun;
using TPParcial1.Modelos;

namespace TPParcial1.Servicios;

// Borrador en memoria de la factura maestro-detalle.
// - No permite dos líneas del mismo producto: SUMA cantidades (criterio documentado).
// - Recalcula Total en pantalla a medida que se agregan/quitan líneas.
// Los Forms lo usan así:
//   var b = new BorradorFactura("Juan", "20123456", DateTime.Today);
//   b.AgregarLinea(producto, 2);  // producto viene de ServicioProducto
//   labelTotal.Text = b.Total.ToString();
public class BorradorFactura
{
    public string ClienteNombre { get; set; }
    public string ClienteDocumento { get; set; }
    public DateTime Fecha { get; set; }

    private readonly List<FacturaDetalle> _lineas = new();
    public IReadOnlyList<FacturaDetalle> Lineas => _lineas;
    public decimal Total => _lineas.Sum(l => l.Subtotal);

    public BorradorFactura(string clienteNombre, string clienteDocumento, DateTime fecha)
    {
        ClienteNombre = clienteNombre;
        ClienteDocumento = clienteDocumento;
        Fecha = fecha;
    }

    public void AgregarLinea(Producto producto, int cantidad)
    {
        if (!producto.Activo)
            throw new ExcepcionValidacion($"El producto '{producto.Nombre}' está inactivo.");
        if (cantidad <= 0)
            throw new ExcepcionValidacion("La cantidad debe ser mayor a cero.");
        if (producto.Precio < 0)
            throw new ExcepcionValidacion("El producto tiene precio inválido.");

        var existente = _lineas.FirstOrDefault(l => l.ProductoId == producto.Id);
        if (existente != null)
        {
            // Criterio: sumar cantidades.
            existente.Cantidad += cantidad;
            existente.PrecioUnitario = producto.Precio; // precio vigente al momento
            existente.Recalcular();
        }
        else
        {
            var d = new FacturaDetalle
            {
                ProductoId = producto.Id,
                CodigoProducto = producto.Codigo,
                NombreProducto = producto.Nombre,
                Cantidad = cantidad,
                PrecioUnitario = producto.Precio
            };
            d.Recalcular();
            _lineas.Add(d);
        }
    }

    public void QuitarLinea(int productoId) =>
        _lineas.RemoveAll(l => l.ProductoId == productoId);

    public void ValidarParaEmitir()
    {
        if (string.IsNullOrWhiteSpace(ClienteNombre))
            throw new ExcepcionValidacion("El nombre del cliente no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(ClienteDocumento))
            throw new ExcepcionValidacion("El documento del cliente no puede estar vacío.");
        if (_lineas.Count == 0)
            throw new ExcepcionValidacion("La factura debe tener al menos una línea.");
    }

    // Convierte a entidad para Datos (Datos relee precios vigentes en la tx).
    public Factura ToFactura() => new()
    {
        Fecha = Fecha,
        ClienteNombre = ClienteNombre.Trim(),
        ClienteDocumento = ClienteDocumento.Trim(),
        Detalles = _lineas.Select(l => new FacturaDetalle
        {
            ProductoId = l.ProductoId,
            Cantidad = l.Cantidad
        }).ToList()
    };
}
