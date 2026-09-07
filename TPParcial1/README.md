# Trabajo Práctico — Facturación Simple

Aplicación de escritorio para emitir y consultar facturas de un comercio, con persistencia en SQL Server mediante **ADO.NET en modo conectado**.

| | |
|---|---|
| **Tecnologías** | .NET 8 · Windows Forms · ADO.NET (modo conectado) · SQL Server 2019 |
| **Materia** | Desarrollo y Arquitectura de Software |
| **Profesora** | Gimenez Martina |
| **Integrantes** | Laureano Sciacaluga y Emilia Ravasio |
| **Fecha de entrega** | 18-09-2026 |

---

## 1. Descripción

El sistema permite gestionar el catálogo de productos de un comercio y emitir facturas con formato maestro-detalle. Los datos del cliente se copian en la cabecera de la factura al momento de emitirla (no existe entidad Cliente). El precio de venta también se copia a cada línea del detalle, por lo que las facturas ya emitidas no se alteran si el precio del producto cambia después.

---

## 2. Funcionalidades

### 2.1. Productos (ABM completo)
- Alta, modificación y baja **lógica** (`Activo = 0`).
- Listado con búsqueda por código o nombre.
- Validaciones: código único y no vacío, nombre no vacío, precio ≥ 0.

### 2.2. Emisión de factura (caso principal)
- Pantalla maestro-detalle: cabecera (fecha, nombre y documento del cliente) + líneas (producto activo, cantidad, precio vigente y subtotal).
- Si se agrega dos veces el mismo producto, se **suman las cantidades**.
- El total se recalcula en pantalla a medida que se agregan o quitan líneas.
- La cabecera y el detalle se graban en **una sola transacción**: si falla cualquier `INSERT`, se hace rollback completo.
- No se puede emitir una factura sin líneas ni con cliente vacío.
- Tras grabar, se muestra el número de factura asignado.

### 2.3. Consulta de facturas
- Listado (número, fecha, cliente, total) con filtro por rango de fechas y/o texto de cliente.
- Al seleccionar una factura se ven cabecera y detalle en **solo lectura** (las facturas emitidas no se editan).
- Anulación por estado (`Anulada = 1` + fecha de anulación) en lugar de `DELETE`. **Las facturas jamás se eliminan** y no se puede anular dos veces.

### 2.4. Informe simple
- Por producto: cantidad facturada y monto total en un período (excluye facturas anuladas). Resultado en grilla.

---

## 3. Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (o Visual Studio 2022 con carga de trabajo de escritorio .NET).
- SQL Server 2019 (instancia local `.\SQLEXPRESS`, LocalDB o servidor remoto).
- SQL Server Management Studio (SSMS) para ejecutar el script de base de datos.

---

## 4. Cómo crear la base de datos

1. Abrir SSMS y conectarse a la instancia de SQL Server 2019.
2. Abrir y ejecutar el script `BaseDeDatos/01_CrearBase.sql`.

El script crea:

- Base de datos `FacturacionTP1`.
- Secuencia `SeqFacturaNumero` para el número correlativo de factura.
- Tabla `Productos` (`Id` identity PK, `Codigo` único, `Nombre`, `Precio ≥ 0`, `Activo`).
- Tabla `Facturas` (`Id` identity PK, `Numero` único asignado por la secuencia, `Fecha`, `ClienteNombre`, `ClienteDocumento`, `Total`, `Anulada`, `FechaAnulacion`).
- Tabla `FacturaDetalle` (`Id` identity PK, `FacturaId` FK → `Facturas`, `ProductoId` FK → `Productos`, `Cantidad > 0`, `PrecioUnitario`, `Subtotal`).
- Datos de ejemplo (3 productos) si la tabla está vacía.

---

## 5. Cadena de conexión

La conexión se configura en `App.config`, llave `Facturacion`. Cambiar `Data Source` según el entorno:

```xml
<!-- SQL Server Express local con seguridad integrada -->
<add name="Facturacion"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=FacturacionTP1;Integrated Security=True;TrustServerCertificate=True;"
     providerName="Microsoft.Data.SqlClient" />
```

```xml
<!-- LocalDB -->
<add name="Facturacion"
     connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FacturacionTP1;Integrated Security=True;TrustServerCertificate=True;"
     providerName="Microsoft.Data.SqlClient" />
```

```xml
<!-- Servidor remoto con autenticación SQL Server -->
<add name="Facturacion"
     connectionString="Data Source=SERVIDOR;Initial Catalog=FacturacionTP1;User ID=usuario;Password=clave;TrustServerCertificate=True;"
     providerName="Microsoft.Data.SqlClient" />
```

La cadena se lee en un único lugar (`Datos/FabricaConexiones.cs`); los formularios nunca la usan directamente.

---

## 6. Cómo compilar y ejecutar

Desde una terminal, en la carpeta de la solución:

```
dotnet build
dotnet run --project TPParcial1
```

O abrir `TPParcial1.sln` en Visual Studio y presionar `F5`.

---

## 7. Estructura del proyecto

```
TPParcial1/
├── App.config                  Cadena de conexión (llave "Facturacion")
├── Program.cs                  Punto de entrada WinForms
├── Modelos/                    Entidades: Producto, Factura, FacturaDetalle, InformeProducto
├── Datos/                      Acceso a datos ADO.NET conectado
│   ├── FabricaConexiones.cs    Crea SqlConnection desde App.config
│   ├── ProductoDatos.cs        ABM y búsquedas de productos
│   ├── FacturaDatos.cs         Emisión transaccional, consulta y anulación
│   ├── InformeDatos.cs         Ventas por producto en un período
│   └── AyudanteExcepciones.cs  Traduce SqlException a mensajes claros
├── Servicios/                  Reglas de negocio para la UI
│   ├── ServicioProducto.cs     Validaciones de producto
│   ├── BorradorFactura.cs      Factura en memoria (suma duplicados, total en vivo)
│   └── ServicioFactura.cs      Emitir, buscar, anular e informe
├── Comun/
│   └── ExcepcionValidacion.cs  Error de regla de negocio (la UI lo muestra tal cual)
├── BaseDeDatos/
│   └── 01_CrearBase.sql        Script de creación (SQL Server 2019)
└── Documentos/
    └── diagrama-clases.puml    Diagrama de clases (PlantUML)
```

---

## 8. Arquitectura

```
Formulario → Servicio → Datos → SqlCommand → SQL Server
```

- **UI (formularios):** solo llaman a `Servicios`. No contienen SQL embebido.
- **Servicios:** validan reglas de negocio y orquestan. Lanzan `ExcepcionValidacion` ante datos inválidos.
- **Datos:** único lugar con SQL. Usa `SqlConnection`, `SqlCommand` con parámetros (`@Nombre`, nunca concatenación), `SqlDataReader` y `SqlTransaction`. La conexión se abre el menor tiempo posible (`using`).
- No se usan `SqlDataAdapter`, `DataSet`, `DataTable` ni ORMs.

El diagrama de clases está en `Documentos/diagrama-clases.puml` (se puede visualizar con la extensión PlantUML de VS Code o en [plantuml.com](https://www.plantuml.com/plantuml/uml/)).

---

## 9. Decisiones de diseño

- **El precio vive en el detalle.** Al facturar, `FacturaDatos.Emitir` relee el precio vigente de `Productos` dentro de la transacción y lo persiste en `PrecioUnitario`. Consultar una factura vieja lee el detalle, nunca el catálogo, por eso un cambio de precio no altera facturas emitidas.
- **Cabecera + detalle van en transacción.** `Emitir` abre un `SqlTransaction`: inserta la cabecera, luego todas las líneas y recién ahí hace `Commit`. Si falla cualquier `INSERT`, se ejecuta `Rollback` y no queda nada a medio grabar.
- **Número correlativo sin `MAX + 1`.** `Numero` se asigna con la secuencia `SeqFacturaNumero` (valor por defecto de la columna). Evita condiciones de carrera entre dos emisiones simultáneas y evita el problema de que SQL Server permite una sola columna `IDENTITY` por tabla.
- **Las facturas jamás se eliminan.** No existe `DELETE` sobre `Facturas`; anular es `UPDATE Anulada = 1 + FechaAnulacion`, y la condición `WHERE Anulada = 0` impide anular dos veces. El informe excluye anuladas.
- **Productos con baja lógica.** `Baja` es siempre `UPDATE Activo = 0`; un producto inactivo no puede usarse en facturas nuevas pero el historial se conserva.
- **Líneas duplicadas se suman.** Si el borrador ya tiene el producto, `BorradorFactura.AgregarLinea` suma la cantidad y recalcula el subtotal con el precio vigente.
- **Validación en ambos lados.** La UI valida antes de guardar (campos vacíos, cantidades, total en vivo); los `Servicios` revalidan reglas de negocio; SQL impone integridad (`UNIQUE`, `FK`, `CHECK`).
- **Errores claros.** `AyudanteExcepciones` distingue error de conexión (instancia apagada o cadena inválida), violación de único (códigos 2627/2601) y violación de FK (código 547). Nunca se tragan excepciones con `catch` vacío.

---

## 10. Guía de uso de la capa lógica desde la UI

```csharp
using TPParcial1.Comun;
using TPParcial1.Modelos;
using TPParcial1.Servicios;

var prodSvc = new ServicioProducto();
var factSvc = new ServicioFactura();

// --- Productos ---
int id = prodSvc.Alta("P010", "Fideos 500g", 1500m);
prodSvc.Modificar(id, "P010", "Fideos 500g", 1600m, activo: true);

// Baja lógica: pedir confirmación antes (operación destructiva)
if (MessageBox.Show("¿Dar de baja el producto?", "Confirmar",
        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
    prodSvc.Baja(id);
grillaProductos.DataSource = prodSvc.Buscar(txtBuscar.Text);

// --- Emisión maestro-detalle ---
var borrador = factSvc.NuevoBorrador(txtCliente.Text, txtDoc.Text, dtpFecha.Value);
borrador.AgregarLinea(productoSeleccionado, (int)numCantidad.Value); // si se repite, SUMA
borrador.QuitarLinea(productoId);
lblTotal.Text = borrador.Total.ToString("C");
int numero = factSvc.Emitir(borrador);
MessageBox.Show($"Factura N° {numero} grabada.");

// --- Consulta (solo lectura) ---
grillaFacturas.DataSource = factSvc.Buscar(desde, hasta, txtCliente.Text);
Factura? fac = factSvc.ObtenerCompleta(idSeleccionado);

// --- Anulación (jamás DELETE): pedir confirmación antes ---
if (MessageBox.Show($"¿Anular la factura seleccionada? Esta acción no se puede deshacer.",
        "Confirmar anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
    factSvc.Anular(idSeleccionado);

// --- Informe ---
grillaInforme.DataSource = factSvc.InformeVentas(desde, hasta);
if (grillaInforme.Rows.Count == 0)
    MessageBox.Show("No hay facturas en el período.");
```

Patrón de manejo de errores recomendado en formularios:

```csharp
try
{
    factSvc.Emitir(borrador);
}
catch (ExcepcionValidacion ex)
{
    MessageBox.Show(ex.Message, "Validación"); // regla de negocio: mostrar tal cual
}
catch (Exception ex)
{
    MessageBox.Show(ex.Message, "Error"); // conexión o base: mensaje ya traducido
}
```
