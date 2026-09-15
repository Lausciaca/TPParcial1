using TPParcial1.Comun;
using TPParcial1.Modelos;
using TPParcial1.Servicios;

namespace TPParcial1
{
    /// <summary>
    /// Única pantalla de la aplicación, organizada en 4 solapas:
    /// Productos (ABM) · Emisión · Consulta · Informe.
    ///
    /// REGLA DE ORO DE ESTE ARCHIVO: acá NO hay SQL, ni SqlConnection,
    /// ni cadenas de conexión. El formulario solo habla con la capa
    /// Servicios (ServicioProducto / ServicioFactura / BorradorFactura).
    /// </summary>
    public partial class Form1 : Form
    {
        // ------------------------------------------------------------------
        // Estado del formulario
        // ------------------------------------------------------------------

        // Servicios: se instancian en el Load para poder atrapar el error
        // de "falta la cadena de conexión" con un MessageBox y no con un crash.
        private ServicioProducto _servicioProducto = null!;
        private ServicioFactura _servicioFactura = null!;

        // Borrador en memoria de la factura que se está armando (maestro-detalle).
        private BorradorFactura? _borrador;

        // Id del producto que se está editando. 0 = modo ALTA.
        private int _productoEnEdicion = 0;

        // Evita que los eventos de selección disparen mientras el código
        // rellena grillas o combos (bindear una grilla cambia la selección).
        private bool _cargando = false;

        // Se pone en true al final del Load: protege a los eventos que podrían
        // dispararse antes de que los servicios existan.
        private bool _inicializado = false;

        public Form1()
        {
            InitializeComponent();
        }

        // ==================================================================
        // 1) CARGA INICIAL
        // ==================================================================

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Si falta App.config o la cadena "Facturacion", esto lanza
                // InvalidOperationException (lo tira FabricaConexiones).
                _servicioProducto = new ServicioProducto();
                _servicioFactura = new ServicioFactura();

                // Columnas de todas las grillas (definidas por código para no
                // depender de configuraciones perdidas en el Diseñador).
                ConfigurarGrillaProductos();
                ConfigurarGrillaDetalle();
                ConfigurarGrillaFacturas();
                ConfigurarGrillaDetalleFactura();
                ConfigurarGrillaInforme();

                // Valores por defecto de fechas.
                dtpFecha.Value = DateTime.Today;
                dtpDesde.Value = DateTime.Today.AddMonths(-1);
                dtpHasta.Value = DateTime.Today;
                dtpInfDesde.Value = DateTime.Today.AddMonths(-1);
                dtpInfHasta.Value = DateTime.Today;

                chkFiltrarFechas.Checked = false;
                AplicarEstadoFiltroFechas();

                CargarProductos();       // grilla del ABM
                CargarComboProductos();  // combo de la emisión (solo activos)
                ReiniciarBorrador();     // factura vacía
                LimpiarFormularioProducto();

                _inicializado = true;
            }
            catch (Exception ex)
            {
                Manejar(ex);
            }
        }

        // ==================================================================
        // 2) SOLAPA PRODUCTOS (ABM)
        // ==================================================================

        private void ConfigurarGrillaProductos()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();
            dgvProductos.Columns.Add(ColTexto("Codigo", "Código", ancho: 100));
            dgvProductos.Columns.Add(ColTexto("Nombre", "Nombre")); // Fill
            dgvProductos.Columns.Add(ColTexto("Precio", "Precio", "N2", 110, derecha: true));
            dgvProductos.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colActivo",
                DataPropertyName = "Activo",
                HeaderText = "Activo",
                Width = 60
            });
        }

        /// <summary>Lista productos aplicando el texto de búsqueda (código o nombre).</summary>
        private void CargarProductos()
        {
            var lista = _servicioProducto.Buscar(txtBuscarProducto.Text);

            _cargando = true;
            try { Bindear(dgvProductos, lista); }
            finally { _cargando = false; }

            // Estado vacío (pedido en §5.6 de la consigna).
            lblEstadoProductos.Text = lista.Count == 0
                ? "No se encontraron productos con ese criterio."
                : $"{lista.Count} producto(s) listado(s).";
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try { CargarProductos(); }
            catch (Exception ex) { Manejar(ex); }
        }

        /// <summary>Permite buscar apretando Enter dentro del TextBox.</summary>
        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true; // evita el "beep" de Windows
            btnBuscarProducto_Click(sender, EventArgs.Empty);
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            try
            {
                txtBuscarProducto.Clear();
                CargarProductos();
            }
            catch (Exception ex) { Manejar(ex); }
        }

        /// <summary>Al seleccionar una fila, se pasa a modo MODIFICACIÓN.</summary>
        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            if (dgvProductos.CurrentRow?.DataBoundItem is not Producto p) return;

            _productoEnEdicion = p.Id;
            txtCodigo.Text = p.Codigo;
            txtNombre.Text = p.Nombre;
            txtPrecio.Text = p.Precio.ToString("0.00");
            chkActivo.Checked = p.Activo;
            chkActivo.Enabled = true; // en el alta siempre nace activo
            lblModoProducto.Text = $"Modo: MODIFICACIÓN (Id {p.Id})";
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            LimpiarFormularioProducto();
            txtCodigo.Focus();
        }

        private void btnLimpiarProducto_Click(object sender, EventArgs e)
        {
            LimpiarFormularioProducto();
        }

        private void LimpiarFormularioProducto()
        {
            _productoEnEdicion = 0;
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            chkActivo.Checked = true;
            chkActivo.Enabled = false;
            lblModoProducto.Text = "Modo: ALTA";

            // Se limpia la selección sin volver a disparar la carga de campos.
            _cargando = true;
            try { dgvProductos.ClearSelection(); }
            finally { _cargando = false; }
        }

        /// <summary>
        /// Un solo botón para alta y modificación: decide según _productoEnEdicion.
        /// Las validaciones de acá son "de usabilidad": la validación real
        /// (código único, precio ≥ 0, nombre no vacío) vive en ServicioProducto.
        /// </summary>
        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    Avisar("Ingrese el código del producto.");
                    txtCodigo.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    Avisar("Ingrese el nombre del producto.");
                    txtNombre.Focus();
                    return;
                }
                // TryParse usa la cultura de Windows: en es-AR el separador decimal es la coma.
                if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
                {
                    Avisar("El precio no es un número válido.");
                    txtPrecio.Focus();
                    return;
                }
                if (precio < 0)
                {
                    Avisar("El precio no puede ser negativo.");
                    txtPrecio.Focus();
                    return;
                }

                if (_productoEnEdicion == 0)
                {
                    int id = _servicioProducto.Alta(txtCodigo.Text, txtNombre.Text, precio);
                    MessageBox.Show($"Producto creado correctamente (Id {id}).",
                        "Alta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _servicioProducto.Modificar(_productoEnEdicion, txtCodigo.Text,
                        txtNombre.Text, precio, chkActivo.Checked);
                    MessageBox.Show("Producto modificado correctamente.",
                        "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CargarProductos();
                CargarComboProductos(); // el combo de emisión puede haber cambiado
                LimpiarFormularioProducto();
            }
            catch (Exception ex) { Manejar(ex); }
        }

        /// <summary>
        /// Baja LÓGICA (Activo = 0). Nunca DELETE: el producto puede estar
        /// referenciado por el detalle de facturas ya emitidas.
        /// </summary>
        private void btnBajaProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (_productoEnEdicion == 0)
                {
                    Avisar("Seleccione un producto de la grilla para darlo de baja.");
                    return;
                }

                // Confirmación en operación destructiva (§5.6).
                var r = MessageBox.Show(
                    $"¿Confirma dar de baja el producto '{txtNombre.Text}'?\n\n" +
                    "La baja es lógica: el producto deja de poder facturarse, " +
                    "pero las facturas ya emitidas no se modifican.",
                    "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r != DialogResult.Yes) return;

                _servicioProducto.Baja(_productoEnEdicion);

                CargarProductos();
                CargarComboProductos();
                LimpiarFormularioProducto();
            }
            catch (Exception ex) { Manejar(ex); }
        }

        // ==================================================================
        // 3) SOLAPA EMISIÓN DE FACTURA (maestro-detalle + transacción)
        // ==================================================================

        private void ConfigurarGrillaDetalle()
        {
            dgvDetalle.AutoGenerateColumns = false;
            dgvDetalle.Columns.Clear();
            dgvDetalle.Columns.Add(ColTexto("CodigoProducto", "Código", ancho: 100));
            dgvDetalle.Columns.Add(ColTexto("NombreProducto", "Producto"));
            dgvDetalle.Columns.Add(ColTexto("Cantidad", "Cantidad", ancho: 90, derecha: true));
            dgvDetalle.Columns.Add(ColTexto("PrecioUnitario", "Precio unit.", "N2", 120, true));
            dgvDetalle.Columns.Add(ColTexto("Subtotal", "Subtotal", "N2", 120, true));
        }

        /// <summary>Carga el combo SOLO con productos activos (§4.2.2).</summary>
        private void CargarComboProductos()
        {
            var activos = _servicioProducto.ListarActivosParaFactura();

            _cargando = true;
            try
            {
                cboProductos.DataSource = null;
                cboProductos.DisplayMember = "Nombre"; // qué texto se ve
                cboProductos.ValueMember = "Id";       // qué valor representa
                cboProductos.DataSource = activos;
                cboProductos.SelectedIndex = activos.Count > 0 ? 0 : -1;
            }
            finally { _cargando = false; }

            ActualizarPrecioYSubtotal();
        }

        private void btnRecargarProductos_Click(object sender, EventArgs e)
        {
            try { CargarComboProductos(); }
            catch (Exception ex) { Manejar(ex); }
        }

        private void cboProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            ActualizarPrecioYSubtotal();
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            ActualizarPrecioYSubtotal();
        }

        /// <summary>Muestra el precio vigente y el subtotal tentativo de la línea.</summary>
        private void ActualizarPrecioYSubtotal()
        {
            if (cboProductos.SelectedItem is Producto p)
            {
                lblPrecioVigente.Text = $"Precio: {p.Precio:C2}";
                lblSubtotalLinea.Text = $"Subtotal: {(p.Precio * nudCantidad.Value):C2}";
            }
            else
            {
                lblPrecioVigente.Text = "Precio: —";
                lblSubtotalLinea.Text = "Subtotal: —";
            }
        }

        /// <summary>Crea un borrador vacío y limpia la pantalla de emisión.</summary>
        private void ReiniciarBorrador()
        {
            _borrador = _servicioFactura.NuevoBorrador("", "", DateTime.Today);

            txtClienteNombre.Clear();
            txtClienteDocumento.Clear();
            dtpFecha.Value = DateTime.Today;
            nudCantidad.Value = 1;

            RefrescarDetalle();
        }

        /// <summary>Vuelve a pintar la grilla de líneas y recalcula el total en pantalla (§4.2.4).</summary>
        private void RefrescarDetalle()
        {
            _cargando = true;
            try { Bindear(dgvDetalle, _borrador!.Lineas.ToList()); }
            finally { _cargando = false; }

            lblTotal.Text = $"TOTAL: {_borrador!.Total:C2}";
        }

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboProductos.SelectedItem is not Producto p)
                {
                    Avisar("No hay productos activos disponibles para facturar.");
                    return;
                }

                // El borrador aplica el criterio documentado: si el producto ya
                // está en el detalle, SUMA las cantidades (no duplica la línea).
                _borrador!.AgregarLinea(p, (int)nudCantidad.Value);

                RefrescarDetalle();
                nudCantidad.Value = 1;
                cboProductos.Focus();
            }
            catch (Exception ex) { Manejar(ex); }
        }

        private void btnQuitarLinea_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalle.CurrentRow?.DataBoundItem is not FacturaDetalle linea)
                {
                    Avisar("Seleccione una línea del detalle para quitarla.");
                    return;
                }

                _borrador!.QuitarLinea(linea.ProductoId);
                RefrescarDetalle();
            }
            catch (Exception ex) { Manejar(ex); }
        }

        private void btnCancelarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (_borrador!.Lineas.Count > 0)
                {
                    var r = MessageBox.Show("¿Descartar la factura en curso?",
                        "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r != DialogResult.Yes) return;
                }
                ReiniciarBorrador();
            }
            catch (Exception ex) { Manejar(ex); }
        }

        /// <summary>
        /// Emite la factura. Cabecera + detalle se graban en UNA SqlTransaction
        /// dentro de FacturaDatos.Emitir: si falla algún INSERT, rollback total.
        /// </summary>
        private void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                // Se vuelcan los datos de cabecera al borrador antes de validar.
                _borrador!.ClienteNombre = txtClienteNombre.Text;
                _borrador.ClienteDocumento = txtClienteDocumento.Text;
                _borrador.Fecha = dtpFecha.Value.Date;

                // Valida cliente no vacío y al menos una línea (§4.2.7).
                // Si algo falta, lanza ExcepcionValidacion y no se pregunta nada.
                _borrador.ValidarParaEmitir();

                var r = MessageBox.Show(
                    $"¿Confirma emitir la factura?\n\n" +
                    $"Cliente: {_borrador.ClienteNombre}\n" +
                    $"Líneas: {_borrador.Lineas.Count}\n" +
                    $"Total: {_borrador.Total:C2}",
                    "Confirmar emisión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r != DialogResult.Yes) return;

                int numero = _servicioFactura.Emitir(_borrador);

                // Tras grabar se muestra el número asignado (§4.2).
                MessageBox.Show($"Factura N° {numero} emitida correctamente.",
                    "Emisión exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ReiniciarBorrador();
            }
            catch (Exception ex) { Manejar(ex); }
        }

        // ==================================================================
        // 4) SOLAPA CONSULTA DE FACTURAS (solo lectura + anulación)
        // ==================================================================

        private void ConfigurarGrillaFacturas()
        {
            dgvFacturas.AutoGenerateColumns = false;
            dgvFacturas.Columns.Clear();
            dgvFacturas.Columns.Add(ColTexto("Numero", "N°", ancho: 70, derecha: true));
            dgvFacturas.Columns.Add(ColTexto("Fecha", "Fecha", "dd/MM/yyyy", 110));
            dgvFacturas.Columns.Add(ColTexto("ClienteNombre", "Cliente"));
            dgvFacturas.Columns.Add(ColTexto("ClienteDocumento", "Documento", ancho: 130));
            dgvFacturas.Columns.Add(ColTexto("Total", "Total", "N2", 120, true));
            dgvFacturas.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colAnulada",
                DataPropertyName = "Anulada",
                HeaderText = "Anulada",
                Width = 70
            });
        }

        private void ConfigurarGrillaDetalleFactura()
        {
            dgvDetalleFactura.AutoGenerateColumns = false;
            dgvDetalleFactura.Columns.Clear();
            dgvDetalleFactura.Columns.Add(ColTexto("CodigoProducto", "Código", ancho: 100));
            dgvDetalleFactura.Columns.Add(ColTexto("NombreProducto", "Producto"));
            dgvDetalleFactura.Columns.Add(ColTexto("Cantidad", "Cantidad", ancho: 90, derecha: true));
            dgvDetalleFactura.Columns.Add(ColTexto("PrecioUnitario", "Precio unit.", "N2", 120, true));
            dgvDetalleFactura.Columns.Add(ColTexto("Subtotal", "Subtotal", "N2", 120, true));
        }

        private void chkFiltrarFechas_CheckedChanged(object sender, EventArgs e)
        {
            AplicarEstadoFiltroFechas();
        }

        /// <summary>Los DateTimePicker solo se habilitan si el filtro por fechas está tildado.</summary>
        private void AplicarEstadoFiltroFechas()
        {
            dtpDesde.Enabled = chkFiltrarFechas.Checked;
            dtpHasta.Enabled = chkFiltrarFechas.Checked;
        }

        private void btnBuscarFacturas_Click(object sender, EventArgs e)
        {
            try { BuscarFacturas(); }
            catch (Exception ex) { Manejar(ex); }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                chkFiltrarFechas.Checked = false;
                txtFiltroCliente.Clear();
                dtpDesde.Value = DateTime.Today.AddMonths(-1);
                dtpHasta.Value = DateTime.Today;
                BuscarFacturas();
            }
            catch (Exception ex) { Manejar(ex); }
        }

        private void BuscarFacturas()
        {
            // Los parámetros son nullable: si el usuario no filtra por fechas
            // se mandan null y la consulta ignora esa condición.
            DateTime? desde = chkFiltrarFechas.Checked ? (DateTime?)dtpDesde.Value.Date : null;
            DateTime? hasta = chkFiltrarFechas.Checked ? (DateTime?)dtpHasta.Value.Date : null;

            if (desde.HasValue && hasta.HasValue && desde > hasta)
                throw new ExcepcionValidacion("La fecha 'Desde' no puede ser posterior a 'Hasta'.");

            var lista = _servicioFactura.Buscar(desde, hasta, txtFiltroCliente.Text);

            _cargando = true;
            try { Bindear(dgvFacturas, lista); }
            finally { _cargando = false; }

            if (lista.Count == 0)
            {
                lblEstadoConsulta.Text = "No hay facturas en el período.";
                LimpiarDetalleConsulta();
            }
            else
            {
                lblEstadoConsulta.Text = $"{lista.Count} factura(s) encontrada(s).";
                dgvFacturas.Rows[0].Selected = true;
                MostrarFacturaSeleccionada();
            }
        }

        private void dgvFacturas_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando) return;
            try { MostrarFacturaSeleccionada(); }
            catch (Exception ex) { Manejar(ex); }
        }

        /// <summary>
        /// Trae cabecera + detalle de la factura seleccionada y los muestra
        /// en SOLO LECTURA: una factura emitida no se edita (§4.3).
        /// </summary>
        private void MostrarFacturaSeleccionada()
        {
            if (dgvFacturas.CurrentRow?.DataBoundItem is not Factura cabecera)
            {
                LimpiarDetalleConsulta();
                return;
            }

            var completa = _servicioFactura.ObtenerCompleta(cabecera.Id);
            if (completa == null)
            {
                LimpiarDetalleConsulta();
                return;
            }

            string estado = completa.Anulada
                ? $"ANULADA el {completa.FechaAnulacion:dd/MM/yyyy}"
                : "VIGENTE";

            lblCabeceraSeleccionada.Text =
                $"Factura N° {completa.Numero}  |  {completa.Fecha:dd/MM/yyyy}  |  " +
                $"{completa.ClienteNombre} ({completa.ClienteDocumento})  |  " +
                $"Total: {completa.Total:C2}  |  {estado}";

            Bindear(dgvDetalleFactura, completa.Detalles);

            // No se puede anular dos veces.
            btnAnular.Enabled = !completa.Anulada;
        }

        private void LimpiarDetalleConsulta()
        {
            lblCabeceraSeleccionada.Text = "Seleccione una factura del listado para ver su detalle.";
            dgvDetalleFactura.DataSource = null;
            btnAnular.Enabled = false;
        }

        /// <summary>Anulación por estado (Anulada = 1). NUNCA se borra una factura.</summary>
        private void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFacturas.CurrentRow?.DataBoundItem is not Factura cabecera)
                {
                    Avisar("Seleccione una factura para anular.");
                    return;
                }
                if (cabecera.Anulada)
                {
                    Avisar("La factura ya está anulada.");
                    return;
                }

                var r = MessageBox.Show(
                    $"¿Confirma anular la factura N° {cabecera.Numero}?\n\n" +
                    "La factura no se elimina: queda registrada como anulada " +
                    "y deja de contar en los informes.",
                    "Confirmar anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r != DialogResult.Yes) return;

                _servicioFactura.Anular(cabecera.Id);

                MessageBox.Show($"Factura N° {cabecera.Numero} anulada.",
                    "Anulación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BuscarFacturas(); // refresca el listado con el nuevo estado
            }
            catch (Exception ex) { Manejar(ex); }
        }

        // ==================================================================
        // 5) SOLAPA INFORME (§4.4)
        // ==================================================================

        private void ConfigurarGrillaInforme()
        {
            dgvInforme.AutoGenerateColumns = false;
            dgvInforme.Columns.Clear();
            dgvInforme.Columns.Add(ColTexto("Codigo", "Código", ancho: 110));
            dgvInforme.Columns.Add(ColTexto("Nombre", "Producto"));
            dgvInforme.Columns.Add(ColTexto("CantidadFacturada", "Cant. facturada", ancho: 130, derecha: true));
            dgvInforme.Columns.Add(ColTexto("Monto", "Monto", "N2", 140, true));
        }

        private void btnGenerarInforme_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime desde = dtpInfDesde.Value.Date;

                // El "hasta" se estira al final del día para no perder las
                // facturas emitidas hoy (la columna Fecha es DATETIME2, con hora).
                DateTime hasta = dtpInfHasta.Value.Date.AddDays(1).AddTicks(-1);

                if (desde > hasta)
                    throw new ExcepcionValidacion("La fecha 'Desde' no puede ser posterior a 'Hasta'.");

                var lista = _servicioFactura.InformeVentas(desde, hasta);

                Bindear(dgvInforme, lista);

                lblTotalInforme.Text = lista.Count == 0
                    ? "No hay ventas en el período."
                    : $"Productos con ventas: {lista.Count}   |   " +
                      $"Monto total del período: {lista.Sum(x => x.Monto):C2}";
            }
            catch (Exception ex) { Manejar(ex); }
        }

        // ==================================================================
        // 6) NAVEGACIÓN ENTRE SOLAPAS
        // ==================================================================

        private void tabPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_inicializado) return;

            try
            {
                // Al entrar a Emisión se refresca el combo: puede haberse dado
                // de alta o de baja un producto en la otra solapa.
                if (tabPrincipal.SelectedTab == tabEmision)
                    CargarComboProductos();
            }
            catch (Exception ex) { Manejar(ex); }
        }

        // ==================================================================
        // 7) HELPERS COMUNES
        // ==================================================================

        /// <summary>Crea una columna de texto enlazada a una propiedad de la entidad.</summary>
        private static DataGridViewTextBoxColumn ColTexto(
            string propiedad, string titulo, string? formato = null,
            int ancho = 0, bool derecha = false)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = "col" + propiedad,
                DataPropertyName = propiedad,
                HeaderText = titulo
            };

            if (formato != null) col.DefaultCellStyle.Format = formato;
            if (derecha) col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (ancho > 0) col.Width = ancho;
            else col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // la que se estira

            return col;
        }

        /// <summary>
        /// Enlaza una List&lt;T&gt; a la grilla. Se asigna null primero para forzar
        /// el refresco (una List no avisa sola cuando cambia su contenido).
        /// Ojo: es binding a objetos en memoria, NO hay DataSet/DataTable.
        /// </summary>
        private static void Bindear<T>(DataGridView dgv, List<T> datos)
        {
            dgv.DataSource = null;
            dgv.DataSource = datos;
            dgv.ClearSelection();
        }

        private static void Avisar(string mensaje) =>
            MessageBox.Show(mensaje, "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

        /// <summary>
        /// Punto único de manejo de errores (§5.5):
        /// - ExcepcionValidacion  -> problema del usuario, ícono de advertencia.
        /// - Resto (conexión, unique, FK ya traducidos por AyudanteExcepciones)
        ///   -> error de sistema, ícono de error. Nunca se traga la excepción.
        /// </summary>
        private static void Manejar(Exception ex)
        {
            if (ex is ExcepcionValidacion)
                Avisar(ex.Message);
            else
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
