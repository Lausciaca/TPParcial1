using TPParcial1.Comun;
using TPParcial1.Datos;
using TPParcial1.Modelos;

namespace TPParcial1.Servicios;

// Validaciones de producto (§4.1): código único, nombre no vacío, precio >= 0.
// Los Forms llaman acá, nunca a Datos directo.
public class ServicioProducto
{
    private readonly ProductoDatos _datos;
    public ServicioProducto() : this(new ProductoDatos()) { }
    public ServicioProducto(ProductoDatos datos) => _datos = datos;

    public int Alta(string codigo, string nombre, decimal precio)
    {
        Validar(codigo, nombre, precio, 0);
        return _datos.Crear(new Producto
        {
            Codigo = codigo.Trim(), Nombre = nombre.Trim(), Precio = precio, Activo = true
        });
    }

    public void Modificar(int id, string codigo, string nombre, decimal precio, bool activo)
    {
        if (id <= 0) throw new ExcepcionValidacion("Id de producto inválido.");
        Validar(codigo, nombre, precio, id);
        _datos.Modificar(new Producto
        {
            Id = id, Codigo = codigo.Trim(), Nombre = nombre.Trim(), Precio = precio, Activo = activo
        });
    }

    // Baja siempre lógica.
    public void Baja(int id) => _datos.Desactivar(id);

    public List<Producto> Buscar(string? texto) => _datos.Buscar(texto);
    public List<Producto> ListarActivosParaFactura(string? texto = null) => _datos.Buscar(texto, soloActivos: true);
    public Producto? Obtener(int id) => _datos.ObtenerPorId(id);

    private void Validar(string codigo, string nombre, decimal precio, int excluirId)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ExcepcionValidacion("El código no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ExcepcionValidacion("El nombre no puede estar vacío.");
        if (precio < 0)
            throw new ExcepcionValidacion("El precio no puede ser negativo.");
        if (_datos.ExisteCodigo(codigo, excluirId))
            throw new ExcepcionValidacion($"El código '{codigo.Trim()}' ya existe.");
    }
}
