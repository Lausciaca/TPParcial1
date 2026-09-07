using TPParcial1.Datos;
using TPParcial1.Modelos;

namespace TPParcial1.Servicios;

// Orquesta emisión, consulta y anulación. Los Forms llaman acá.
public class ServicioFactura
{
    private readonly FacturaDatos _datos;
    private readonly InformeDatos _informe;
    public ServicioFactura() : this(new FacturaDatos(), new InformeDatos()) { }
    public ServicioFactura(FacturaDatos datos, InformeDatos informe)
    {
        _datos = datos;
        _informe = informe;
    }

    public BorradorFactura NuevoBorrador(string cliente, string doc, DateTime fecha) =>
        new(cliente, doc, fecha);

    // Valida en memoria y graba cabecera+detalle en una transacción.
    // Devuelve el Numero asignado para mostrarlo en pantalla (§4.2).
    public int Emitir(BorradorFactura borrador)
    {
        borrador.ValidarParaEmitir();
        var factura = borrador.ToFactura();
        return _datos.Emitir(factura);
    }

    public List<Factura> Buscar(DateTime? desde, DateTime? hasta, string? cliente) =>
        _datos.BuscarCabeceras(desde, hasta, cliente);

    public Factura? ObtenerCompleta(int id) => _datos.ObtenerCompleta(id);

    public void Anular(int id) => _datos.Anular(id);

    public List<InformeProducto> InformeVentas(DateTime desde, DateTime hasta) =>
        _informe.VentasPorProducto(desde, hasta);
}
