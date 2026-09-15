namespace TPParcial1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // ---------- Contenedor principal ----------
        private System.Windows.Forms.TabControl tabPrincipal;
        private System.Windows.Forms.TabPage tabProductos;
        private System.Windows.Forms.TabPage tabEmision;
        private System.Windows.Forms.TabPage tabConsulta;
        private System.Windows.Forms.TabPage tabInforme;

        // ---------- Solapa Productos ----------
        private System.Windows.Forms.Label lblBuscarProd;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.Button btnVerTodos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox grpProducto;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Button btnNuevoProducto;
        private System.Windows.Forms.Button btnGuardarProducto;
        private System.Windows.Forms.Button btnBajaProducto;
        private System.Windows.Forms.Button btnLimpiarProducto;
        private System.Windows.Forms.Label lblModoProducto;
        private System.Windows.Forms.Label lblEstadoProductos;

        // ---------- Solapa Emisión ----------
        private System.Windows.Forms.GroupBox grpCabecera;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblClienteNombre;
        private System.Windows.Forms.TextBox txtClienteNombre;
        private System.Windows.Forms.Label lblClienteDoc;
        private System.Windows.Forms.TextBox txtClienteDocumento;
        private System.Windows.Forms.GroupBox grpLinea;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cboProductos;
        private System.Windows.Forms.Button btnRecargarProductos;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label lblPrecioVigente;
        private System.Windows.Forms.Label lblSubtotalLinea;
        private System.Windows.Forms.Button btnAgregarLinea;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Button btnQuitarLinea;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCancelarFactura;
        private System.Windows.Forms.Button btnEmitir;

        // ---------- Solapa Consulta ----------
        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.CheckBox chkFiltrarFechas;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblFiltroCliente;
        private System.Windows.Forms.TextBox txtFiltroCliente;
        private System.Windows.Forms.Button btnBuscarFacturas;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.Label lblCabeceraSeleccionada;
        private System.Windows.Forms.DataGridView dgvDetalleFactura;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Label lblEstadoConsulta;

        // ---------- Solapa Informe ----------
        private System.Windows.Forms.Label lblInfDesde;
        private System.Windows.Forms.DateTimePicker dtpInfDesde;
        private System.Windows.Forms.Label lblInfHasta;
        private System.Windows.Forms.DateTimePicker dtpInfHasta;
        private System.Windows.Forms.Button btnGenerarInforme;
        private System.Windows.Forms.DataGridView dgvInforme;
        private System.Windows.Forms.Label lblTotalInforme;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.tabPrincipal = new System.Windows.Forms.TabControl();
            this.tabProductos = new System.Windows.Forms.TabPage();
            this.tabEmision = new System.Windows.Forms.TabPage();
            this.tabConsulta = new System.Windows.Forms.TabPage();
            this.tabInforme = new System.Windows.Forms.TabPage();

            this.lblBuscarProd = new System.Windows.Forms.Label();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.btnVerTodos = new System.Windows.Forms.Button();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.grpProducto = new System.Windows.Forms.GroupBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.btnNuevoProducto = new System.Windows.Forms.Button();
            this.btnGuardarProducto = new System.Windows.Forms.Button();
            this.btnBajaProducto = new System.Windows.Forms.Button();
            this.btnLimpiarProducto = new System.Windows.Forms.Button();
            this.lblModoProducto = new System.Windows.Forms.Label();
            this.lblEstadoProductos = new System.Windows.Forms.Label();

            this.grpCabecera = new System.Windows.Forms.GroupBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblClienteNombre = new System.Windows.Forms.Label();
            this.txtClienteNombre = new System.Windows.Forms.TextBox();
            this.lblClienteDoc = new System.Windows.Forms.Label();
            this.txtClienteDocumento = new System.Windows.Forms.TextBox();
            this.grpLinea = new System.Windows.Forms.GroupBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cboProductos = new System.Windows.Forms.ComboBox();
            this.btnRecargarProductos = new System.Windows.Forms.Button();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblPrecioVigente = new System.Windows.Forms.Label();
            this.lblSubtotalLinea = new System.Windows.Forms.Label();
            this.btnAgregarLinea = new System.Windows.Forms.Button();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.btnQuitarLinea = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCancelarFactura = new System.Windows.Forms.Button();
            this.btnEmitir = new System.Windows.Forms.Button();

            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.chkFiltrarFechas = new System.Windows.Forms.CheckBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lblFiltroCliente = new System.Windows.Forms.Label();
            this.txtFiltroCliente = new System.Windows.Forms.TextBox();
            this.btnBuscarFacturas = new System.Windows.Forms.Button();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.lblCabeceraSeleccionada = new System.Windows.Forms.Label();
            this.dgvDetalleFactura = new System.Windows.Forms.DataGridView();
            this.btnAnular = new System.Windows.Forms.Button();
            this.lblEstadoConsulta = new System.Windows.Forms.Label();

            this.lblInfDesde = new System.Windows.Forms.Label();
            this.dtpInfDesde = new System.Windows.Forms.DateTimePicker();
            this.lblInfHasta = new System.Windows.Forms.Label();
            this.dtpInfHasta = new System.Windows.Forms.DateTimePicker();
            this.btnGenerarInforme = new System.Windows.Forms.Button();
            this.dgvInforme = new System.Windows.Forms.DataGridView();
            this.lblTotalInforme = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleFactura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.tabPrincipal.SuspendLayout();
            this.tabProductos.SuspendLayout();
            this.tabEmision.SuspendLayout();
            this.tabConsulta.SuspendLayout();
            this.tabInforme.SuspendLayout();
            this.grpProducto.SuspendLayout();
            this.grpCabecera.SuspendLayout();
            this.grpLinea.SuspendLayout();
            this.grpFiltros.SuspendLayout();
            this.SuspendLayout();

            // ==========================================================
            // tabPrincipal
            // ==========================================================
            this.tabPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPrincipal.Controls.Add(this.tabProductos);
            this.tabPrincipal.Controls.Add(this.tabEmision);
            this.tabPrincipal.Controls.Add(this.tabConsulta);
            this.tabPrincipal.Controls.Add(this.tabInforme);
            this.tabPrincipal.Location = new System.Drawing.Point(12, 12);
            this.tabPrincipal.Name = "tabPrincipal";
            this.tabPrincipal.SelectedIndex = 0;
            this.tabPrincipal.Size = new System.Drawing.Size(976, 616);
            this.tabPrincipal.TabIndex = 0;
            this.tabPrincipal.SelectedIndexChanged += new System.EventHandler(this.tabPrincipal_SelectedIndexChanged);

            // ==========================================================
            // tabProductos
            // ==========================================================
            this.tabProductos.Controls.Add(this.lblBuscarProd);
            this.tabProductos.Controls.Add(this.txtBuscarProducto);
            this.tabProductos.Controls.Add(this.btnBuscarProducto);
            this.tabProductos.Controls.Add(this.btnVerTodos);
            this.tabProductos.Controls.Add(this.dgvProductos);
            this.tabProductos.Controls.Add(this.grpProducto);
            this.tabProductos.Controls.Add(this.lblEstadoProductos);
            this.tabProductos.Location = new System.Drawing.Point(4, 24);
            this.tabProductos.Name = "tabProductos";
            this.tabProductos.Padding = new System.Windows.Forms.Padding(3);
            this.tabProductos.Size = new System.Drawing.Size(968, 588);
            this.tabProductos.TabIndex = 0;
            this.tabProductos.Text = "Productos";
            this.tabProductos.UseVisualStyleBackColor = true;

            this.lblBuscarProd.AutoSize = true;
            this.lblBuscarProd.Location = new System.Drawing.Point(12, 16);
            this.lblBuscarProd.Name = "lblBuscarProd";
            this.lblBuscarProd.Size = new System.Drawing.Size(150, 15);
            this.lblBuscarProd.TabIndex = 0;
            this.lblBuscarProd.Text = "Buscar (código o nombre):";

            this.txtBuscarProducto.Location = new System.Drawing.Point(175, 12);
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(250, 23);
            this.txtBuscarProducto.TabIndex = 1;
            this.txtBuscarProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarProducto_KeyDown);

            this.btnBuscarProducto.Location = new System.Drawing.Point(435, 11);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(90, 25);
            this.btnBuscarProducto.TabIndex = 2;
            this.btnBuscarProducto.Text = "Buscar";
            this.btnBuscarProducto.UseVisualStyleBackColor = true;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);

            this.btnVerTodos.Location = new System.Drawing.Point(531, 11);
            this.btnVerTodos.Name = "btnVerTodos";
            this.btnVerTodos.Size = new System.Drawing.Size(90, 25);
            this.btnVerTodos.TabIndex = 3;
            this.btnVerTodos.Text = "Ver todos";
            this.btnVerTodos.UseVisualStyleBackColor = true;
            this.btnVerTodos.Click += new System.EventHandler(this.btnVerTodos_Click);

            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(12, 45);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(620, 480);
            this.dgvProductos.TabIndex = 4;
            this.dgvProductos.SelectionChanged += new System.EventHandler(this.dgvProductos_SelectionChanged);

            this.grpProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
            this.grpProducto.Controls.Add(this.lblCodigo);
            this.grpProducto.Controls.Add(this.txtCodigo);
            this.grpProducto.Controls.Add(this.lblNombre);
            this.grpProducto.Controls.Add(this.txtNombre);
            this.grpProducto.Controls.Add(this.lblPrecio);
            this.grpProducto.Controls.Add(this.txtPrecio);
            this.grpProducto.Controls.Add(this.chkActivo);
            this.grpProducto.Controls.Add(this.btnNuevoProducto);
            this.grpProducto.Controls.Add(this.btnGuardarProducto);
            this.grpProducto.Controls.Add(this.btnBajaProducto);
            this.grpProducto.Controls.Add(this.btnLimpiarProducto);
            this.grpProducto.Controls.Add(this.lblModoProducto);
            this.grpProducto.Location = new System.Drawing.Point(645, 45);
            this.grpProducto.Name = "grpProducto";
            this.grpProducto.Size = new System.Drawing.Size(305, 285);
            this.grpProducto.TabIndex = 5;
            this.grpProducto.TabStop = false;
            this.grpProducto.Text = "Datos del producto";

            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(15, 32);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(50, 15);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código:";

            this.txtCodigo.Location = new System.Drawing.Point(110, 29);
            this.txtCodigo.MaxLength = 50;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(175, 23);
            this.txtCodigo.TabIndex = 1;

            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(15, 67);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(55, 15);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre:";

            this.txtNombre.Location = new System.Drawing.Point(110, 64);
            this.txtNombre.MaxLength = 150;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(175, 23);
            this.txtNombre.TabIndex = 3;

            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(15, 102);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(45, 15);
            this.lblPrecio.TabIndex = 4;
            this.lblPrecio.Text = "Precio:";

            this.txtPrecio.Location = new System.Drawing.Point(110, 99);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(175, 23);
            this.txtPrecio.TabIndex = 5;
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.chkActivo.AutoSize = true;
            this.chkActivo.Enabled = false;
            this.chkActivo.Location = new System.Drawing.Point(110, 132);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(61, 19);
            this.chkActivo.TabIndex = 6;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;

            this.btnNuevoProducto.Location = new System.Drawing.Point(15, 170);
            this.btnNuevoProducto.Name = "btnNuevoProducto";
            this.btnNuevoProducto.Size = new System.Drawing.Size(130, 30);
            this.btnNuevoProducto.TabIndex = 7;
            this.btnNuevoProducto.Text = "Nuevo";
            this.btnNuevoProducto.UseVisualStyleBackColor = true;
            this.btnNuevoProducto.Click += new System.EventHandler(this.btnNuevoProducto_Click);

            this.btnGuardarProducto.Location = new System.Drawing.Point(155, 170);
            this.btnGuardarProducto.Name = "btnGuardarProducto";
            this.btnGuardarProducto.Size = new System.Drawing.Size(130, 30);
            this.btnGuardarProducto.TabIndex = 8;
            this.btnGuardarProducto.Text = "Guardar";
            this.btnGuardarProducto.UseVisualStyleBackColor = true;
            this.btnGuardarProducto.Click += new System.EventHandler(this.btnGuardarProducto_Click);

            this.btnBajaProducto.Location = new System.Drawing.Point(15, 210);
            this.btnBajaProducto.Name = "btnBajaProducto";
            this.btnBajaProducto.Size = new System.Drawing.Size(130, 30);
            this.btnBajaProducto.TabIndex = 9;
            this.btnBajaProducto.Text = "Baja (lógica)";
            this.btnBajaProducto.UseVisualStyleBackColor = true;
            this.btnBajaProducto.Click += new System.EventHandler(this.btnBajaProducto_Click);

            this.btnLimpiarProducto.Location = new System.Drawing.Point(155, 210);
            this.btnLimpiarProducto.Name = "btnLimpiarProducto";
            this.btnLimpiarProducto.Size = new System.Drawing.Size(130, 30);
            this.btnLimpiarProducto.TabIndex = 10;
            this.btnLimpiarProducto.Text = "Limpiar";
            this.btnLimpiarProducto.UseVisualStyleBackColor = true;
            this.btnLimpiarProducto.Click += new System.EventHandler(this.btnLimpiarProducto_Click);

            this.lblModoProducto.AutoSize = true;
            this.lblModoProducto.Location = new System.Drawing.Point(15, 252);
            this.lblModoProducto.Name = "lblModoProducto";
            this.lblModoProducto.Size = new System.Drawing.Size(80, 15);
            this.lblModoProducto.TabIndex = 11;
            this.lblModoProducto.Text = "Modo: ALTA";

            this.lblEstadoProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))));
            this.lblEstadoProductos.AutoSize = true;
            this.lblEstadoProductos.Location = new System.Drawing.Point(12, 535);
            this.lblEstadoProductos.Name = "lblEstadoProductos";
            this.lblEstadoProductos.Size = new System.Drawing.Size(0, 15);
            this.lblEstadoProductos.TabIndex = 6;

            // ==========================================================
            // tabEmision
            // ==========================================================
            this.tabEmision.Controls.Add(this.grpCabecera);
            this.tabEmision.Controls.Add(this.grpLinea);
            this.tabEmision.Controls.Add(this.dgvDetalle);
            this.tabEmision.Controls.Add(this.btnQuitarLinea);
            this.tabEmision.Controls.Add(this.lblTotal);
            this.tabEmision.Controls.Add(this.btnCancelarFactura);
            this.tabEmision.Controls.Add(this.btnEmitir);
            this.tabEmision.Location = new System.Drawing.Point(4, 24);
            this.tabEmision.Name = "tabEmision";
            this.tabEmision.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmision.Size = new System.Drawing.Size(968, 588);
            this.tabEmision.TabIndex = 1;
            this.tabEmision.Text = "Emisión de factura";
            this.tabEmision.UseVisualStyleBackColor = true;

            this.grpCabecera.Controls.Add(this.lblFecha);
            this.grpCabecera.Controls.Add(this.dtpFecha);
            this.grpCabecera.Controls.Add(this.lblClienteNombre);
            this.grpCabecera.Controls.Add(this.txtClienteNombre);
            this.grpCabecera.Controls.Add(this.lblClienteDoc);
            this.grpCabecera.Controls.Add(this.txtClienteDocumento);
            this.grpCabecera.Location = new System.Drawing.Point(12, 10);
            this.grpCabecera.Name = "grpCabecera";
            this.grpCabecera.Size = new System.Drawing.Size(938, 70);
            this.grpCabecera.TabIndex = 0;
            this.grpCabecera.TabStop = false;
            this.grpCabecera.Text = "Cabecera";

            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(15, 32);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(42, 15);
            this.lblFecha.TabIndex = 0;
            this.lblFecha.Text = "Fecha:";

            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(63, 28);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(120, 23);
            this.dtpFecha.TabIndex = 1;

            this.lblClienteNombre.AutoSize = true;
            this.lblClienteNombre.Location = new System.Drawing.Point(205, 32);
            this.lblClienteNombre.Name = "lblClienteNombre";
            this.lblClienteNombre.Size = new System.Drawing.Size(50, 15);
            this.lblClienteNombre.TabIndex = 2;
            this.lblClienteNombre.Text = "Cliente:";

            this.txtClienteNombre.Location = new System.Drawing.Point(261, 28);
            this.txtClienteNombre.MaxLength = 150;
            this.txtClienteNombre.Name = "txtClienteNombre";
            this.txtClienteNombre.Size = new System.Drawing.Size(230, 23);
            this.txtClienteNombre.TabIndex = 3;

            this.lblClienteDoc.AutoSize = true;
            this.lblClienteDoc.Location = new System.Drawing.Point(510, 32);
            this.lblClienteDoc.Name = "lblClienteDoc";
            this.lblClienteDoc.Size = new System.Drawing.Size(105, 15);
            this.lblClienteDoc.TabIndex = 4;
            this.lblClienteDoc.Text = "Documento (DNI/CUIT):";

            this.txtClienteDocumento.Location = new System.Drawing.Point(660, 28);
            this.txtClienteDocumento.MaxLength = 50;
            this.txtClienteDocumento.Name = "txtClienteDocumento";
            this.txtClienteDocumento.Size = new System.Drawing.Size(180, 23);
            this.txtClienteDocumento.TabIndex = 5;

            this.grpLinea.Controls.Add(this.lblProducto);
            this.grpLinea.Controls.Add(this.cboProductos);
            this.grpLinea.Controls.Add(this.btnRecargarProductos);
            this.grpLinea.Controls.Add(this.lblCantidad);
            this.grpLinea.Controls.Add(this.nudCantidad);
            this.grpLinea.Controls.Add(this.lblPrecioVigente);
            this.grpLinea.Controls.Add(this.lblSubtotalLinea);
            this.grpLinea.Controls.Add(this.btnAgregarLinea);
            this.grpLinea.Location = new System.Drawing.Point(12, 88);
            this.grpLinea.Name = "grpLinea";
            this.grpLinea.Size = new System.Drawing.Size(938, 78);
            this.grpLinea.TabIndex = 1;
            this.grpLinea.TabStop = false;
            this.grpLinea.Text = "Agregar línea";

            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(15, 33);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(60, 15);
            this.lblProducto.TabIndex = 0;
            this.lblProducto.Text = "Producto:";

            this.cboProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductos.FormattingEnabled = true;
            this.cboProductos.Location = new System.Drawing.Point(81, 29);
            this.cboProductos.Name = "cboProductos";
            this.cboProductos.Size = new System.Drawing.Size(260, 23);
            this.cboProductos.TabIndex = 1;
            this.cboProductos.SelectedIndexChanged += new System.EventHandler(this.cboProductos_SelectedIndexChanged);

            this.btnRecargarProductos.Location = new System.Drawing.Point(349, 28);
            this.btnRecargarProductos.Name = "btnRecargarProductos";
            this.btnRecargarProductos.Size = new System.Drawing.Size(85, 25);
            this.btnRecargarProductos.TabIndex = 2;
            this.btnRecargarProductos.Text = "Recargar";
            this.btnRecargarProductos.UseVisualStyleBackColor = true;
            this.btnRecargarProductos.Click += new System.EventHandler(this.btnRecargarProductos_Click);

            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(450, 33);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(60, 15);
            this.lblCantidad.TabIndex = 3;
            this.lblCantidad.Text = "Cantidad:";

            this.nudCantidad.Location = new System.Drawing.Point(516, 29);
            this.nudCantidad.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(70, 23);
            this.nudCantidad.TabIndex = 4;
            this.nudCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.ValueChanged += new System.EventHandler(this.nudCantidad_ValueChanged);

            this.lblPrecioVigente.AutoSize = true;
            this.lblPrecioVigente.Location = new System.Drawing.Point(600, 33);
            this.lblPrecioVigente.Name = "lblPrecioVigente";
            this.lblPrecioVigente.Size = new System.Drawing.Size(60, 15);
            this.lblPrecioVigente.TabIndex = 5;
            this.lblPrecioVigente.Text = "Precio: —";

            this.lblSubtotalLinea.AutoSize = true;
            this.lblSubtotalLinea.Location = new System.Drawing.Point(725, 33);
            this.lblSubtotalLinea.Name = "lblSubtotalLinea";
            this.lblSubtotalLinea.Size = new System.Drawing.Size(70, 15);
            this.lblSubtotalLinea.TabIndex = 6;
            this.lblSubtotalLinea.Text = "Subtotal: —";

            this.btnAgregarLinea.Location = new System.Drawing.Point(845, 27);
            this.btnAgregarLinea.Name = "btnAgregarLinea";
            this.btnAgregarLinea.Size = new System.Drawing.Size(80, 27);
            this.btnAgregarLinea.TabIndex = 7;
            this.btnAgregarLinea.Text = "Agregar";
            this.btnAgregarLinea.UseVisualStyleBackColor = true;
            this.btnAgregarLinea.Click += new System.EventHandler(this.btnAgregarLinea_Click);

            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Location = new System.Drawing.Point(12, 175);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(938, 295);
            this.dgvDetalle.TabIndex = 2;

            this.btnQuitarLinea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))));
            this.btnQuitarLinea.Location = new System.Drawing.Point(12, 480);
            this.btnQuitarLinea.Name = "btnQuitarLinea";
            this.btnQuitarLinea.Size = new System.Drawing.Size(150, 30);
            this.btnQuitarLinea.TabIndex = 3;
            this.btnQuitarLinea.Text = "Quitar línea";
            this.btnQuitarLinea.UseVisualStyleBackColor = true;
            this.btnQuitarLinea.Click += new System.EventHandler(this.btnQuitarLinea_Click);

            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(650, 478);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(150, 28);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "TOTAL: $ 0,00";

            this.btnCancelarFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))));
            this.btnCancelarFactura.Location = new System.Drawing.Point(620, 520);
            this.btnCancelarFactura.Name = "btnCancelarFactura";
            this.btnCancelarFactura.Size = new System.Drawing.Size(155, 38);
            this.btnCancelarFactura.TabIndex = 5;
            this.btnCancelarFactura.Text = "Cancelar factura";
            this.btnCancelarFactura.UseVisualStyleBackColor = true;
            this.btnCancelarFactura.Click += new System.EventHandler(this.btnCancelarFactura_Click);

            this.btnEmitir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right))));
            this.btnEmitir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEmitir.Location = new System.Drawing.Point(790, 520);
            this.btnEmitir.Name = "btnEmitir";
            this.btnEmitir.Size = new System.Drawing.Size(160, 38);
            this.btnEmitir.TabIndex = 6;
            this.btnEmitir.Text = "EMITIR FACTURA";
            this.btnEmitir.UseVisualStyleBackColor = true;
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);

            // ==========================================================
            // tabConsulta
            // ==========================================================
            this.tabConsulta.Controls.Add(this.grpFiltros);
            this.tabConsulta.Controls.Add(this.dgvFacturas);
            this.tabConsulta.Controls.Add(this.lblCabeceraSeleccionada);
            this.tabConsulta.Controls.Add(this.dgvDetalleFactura);
            this.tabConsulta.Controls.Add(this.btnAnular);
            this.tabConsulta.Controls.Add(this.lblEstadoConsulta);
            this.tabConsulta.Location = new System.Drawing.Point(4, 24);
            this.tabConsulta.Name = "tabConsulta";
            this.tabConsulta.Padding = new System.Windows.Forms.Padding(3);
            this.tabConsulta.Size = new System.Drawing.Size(968, 588);
            this.tabConsulta.TabIndex = 2;
            this.tabConsulta.Text = "Consulta de facturas";
            this.tabConsulta.UseVisualStyleBackColor = true;

            this.grpFiltros.Controls.Add(this.chkFiltrarFechas);
            this.grpFiltros.Controls.Add(this.lblDesde);
            this.grpFiltros.Controls.Add(this.dtpDesde);
            this.grpFiltros.Controls.Add(this.lblHasta);
            this.grpFiltros.Controls.Add(this.dtpHasta);
            this.grpFiltros.Controls.Add(this.lblFiltroCliente);
            this.grpFiltros.Controls.Add(this.txtFiltroCliente);
            this.grpFiltros.Controls.Add(this.btnBuscarFacturas);
            this.grpFiltros.Controls.Add(this.btnLimpiarFiltros);
            this.grpFiltros.Location = new System.Drawing.Point(12, 10);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Size = new System.Drawing.Size(938, 70);
            this.grpFiltros.TabIndex = 0;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Filtros";

            this.chkFiltrarFechas.AutoSize = true;
            this.chkFiltrarFechas.Location = new System.Drawing.Point(15, 31);
            this.chkFiltrarFechas.Name = "chkFiltrarFechas";
            this.chkFiltrarFechas.Size = new System.Drawing.Size(120, 19);
            this.chkFiltrarFechas.TabIndex = 0;
            this.chkFiltrarFechas.Text = "Filtrar por fechas";
            this.chkFiltrarFechas.UseVisualStyleBackColor = true;
            this.chkFiltrarFechas.CheckedChanged += new System.EventHandler(this.chkFiltrarFechas_CheckedChanged);

            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(150, 32);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(44, 15);
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde:";

            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(200, 28);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(110, 23);
            this.dtpDesde.TabIndex = 2;

            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(325, 32);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(40, 15);
            this.lblHasta.TabIndex = 3;
            this.lblHasta.Text = "Hasta:";

            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(371, 28);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(110, 23);
            this.dtpHasta.TabIndex = 4;

            this.lblFiltroCliente.AutoSize = true;
            this.lblFiltroCliente.Location = new System.Drawing.Point(500, 32);
            this.lblFiltroCliente.Name = "lblFiltroCliente";
            this.lblFiltroCliente.Size = new System.Drawing.Size(80, 15);
            this.lblFiltroCliente.TabIndex = 5;
            this.lblFiltroCliente.Text = "Cliente / Doc.:";

            this.txtFiltroCliente.Location = new System.Drawing.Point(590, 28);
            this.txtFiltroCliente.Name = "txtFiltroCliente";
            this.txtFiltroCliente.Size = new System.Drawing.Size(180, 23);
            this.txtFiltroCliente.TabIndex = 6;

            this.btnBuscarFacturas.Location = new System.Drawing.Point(781, 27);
            this.btnBuscarFacturas.Name = "btnBuscarFacturas";
            this.btnBuscarFacturas.Size = new System.Drawing.Size(70, 25);
            this.btnBuscarFacturas.TabIndex = 7;
            this.btnBuscarFacturas.Text = "Buscar";
            this.btnBuscarFacturas.UseVisualStyleBackColor = true;
            this.btnBuscarFacturas.Click += new System.EventHandler(this.btnBuscarFacturas_Click);

            this.btnLimpiarFiltros.Location = new System.Drawing.Point(857, 27);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(70, 25);
            this.btnLimpiarFiltros.TabIndex = 8;
            this.btnLimpiarFiltros.Text = "Limpiar";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);

            this.dgvFacturas.AllowUserToAddRows = false;
            this.dgvFacturas.AllowUserToDeleteRows = false;
            this.dgvFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Location = new System.Drawing.Point(12, 90);
            this.dgvFacturas.MultiSelect = false;
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.RowHeadersVisible = false;
            this.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas.Size = new System.Drawing.Size(938, 200);
            this.dgvFacturas.TabIndex = 1;
            this.dgvFacturas.SelectionChanged += new System.EventHandler(this.dgvFacturas_SelectionChanged);

            this.lblCabeceraSeleccionada.AutoSize = true;
            this.lblCabeceraSeleccionada.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCabeceraSeleccionada.Location = new System.Drawing.Point(12, 298);
            this.lblCabeceraSeleccionada.Name = "lblCabeceraSeleccionada";
            this.lblCabeceraSeleccionada.Size = new System.Drawing.Size(300, 15);
            this.lblCabeceraSeleccionada.TabIndex = 2;
            this.lblCabeceraSeleccionada.Text = "Seleccione una factura del listado para ver su detalle.";

            this.dgvDetalleFactura.AllowUserToAddRows = false;
            this.dgvDetalleFactura.AllowUserToDeleteRows = false;
            this.dgvDetalleFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalleFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleFactura.Location = new System.Drawing.Point(12, 320);
            this.dgvDetalleFactura.MultiSelect = false;
            this.dgvDetalleFactura.Name = "dgvDetalleFactura";
            this.dgvDetalleFactura.ReadOnly = true;
            this.dgvDetalleFactura.RowHeadersVisible = false;
            this.dgvDetalleFactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleFactura.Size = new System.Drawing.Size(938, 195);
            this.dgvDetalleFactura.TabIndex = 3;

            this.btnAnular.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))));
            this.btnAnular.Enabled = false;
            this.btnAnular.Location = new System.Drawing.Point(12, 528);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(160, 32);
            this.btnAnular.TabIndex = 4;
            this.btnAnular.Text = "Anular factura";
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);

            this.lblEstadoConsulta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))));
            this.lblEstadoConsulta.AutoSize = true;
            this.lblEstadoConsulta.Location = new System.Drawing.Point(190, 537);
            this.lblEstadoConsulta.Name = "lblEstadoConsulta";
            this.lblEstadoConsulta.Size = new System.Drawing.Size(0, 15);
            this.lblEstadoConsulta.TabIndex = 5;

            // ==========================================================
            // tabInforme
            // ==========================================================
            this.tabInforme.Controls.Add(this.lblInfDesde);
            this.tabInforme.Controls.Add(this.dtpInfDesde);
            this.tabInforme.Controls.Add(this.lblInfHasta);
            this.tabInforme.Controls.Add(this.dtpInfHasta);
            this.tabInforme.Controls.Add(this.btnGenerarInforme);
            this.tabInforme.Controls.Add(this.dgvInforme);
            this.tabInforme.Controls.Add(this.lblTotalInforme);
            this.tabInforme.Location = new System.Drawing.Point(4, 24);
            this.tabInforme.Name = "tabInforme";
            this.tabInforme.Padding = new System.Windows.Forms.Padding(3);
            this.tabInforme.Size = new System.Drawing.Size(968, 588);
            this.tabInforme.TabIndex = 3;
            this.tabInforme.Text = "Informe de ventas";
            this.tabInforme.UseVisualStyleBackColor = true;

            this.lblInfDesde.AutoSize = true;
            this.lblInfDesde.Location = new System.Drawing.Point(12, 20);
            this.lblInfDesde.Name = "lblInfDesde";
            this.lblInfDesde.Size = new System.Drawing.Size(44, 15);
            this.lblInfDesde.TabIndex = 0;
            this.lblInfDesde.Text = "Desde:";

            this.dtpInfDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInfDesde.Location = new System.Drawing.Point(65, 16);
            this.dtpInfDesde.Name = "dtpInfDesde";
            this.dtpInfDesde.Size = new System.Drawing.Size(120, 23);
            this.dtpInfDesde.TabIndex = 1;

            this.lblInfHasta.AutoSize = true;
            this.lblInfHasta.Location = new System.Drawing.Point(205, 20);
            this.lblInfHasta.Name = "lblInfHasta";
            this.lblInfHasta.Size = new System.Drawing.Size(40, 15);
            this.lblInfHasta.TabIndex = 2;
            this.lblInfHasta.Text = "Hasta:";

            this.dtpInfHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInfHasta.Location = new System.Drawing.Point(253, 16);
            this.dtpInfHasta.Name = "dtpInfHasta";
            this.dtpInfHasta.Size = new System.Drawing.Size(120, 23);
            this.dtpInfHasta.TabIndex = 3;

            this.btnGenerarInforme.Location = new System.Drawing.Point(390, 15);
            this.btnGenerarInforme.Name = "btnGenerarInforme";
            this.btnGenerarInforme.Size = new System.Drawing.Size(130, 26);
            this.btnGenerarInforme.TabIndex = 4;
            this.btnGenerarInforme.Text = "Generar informe";
            this.btnGenerarInforme.UseVisualStyleBackColor = true;
            this.btnGenerarInforme.Click += new System.EventHandler(this.btnGenerarInforme_Click);

            this.dgvInforme.AllowUserToAddRows = false;
            this.dgvInforme.AllowUserToDeleteRows = false;
            this.dgvInforme.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInforme.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInforme.Location = new System.Drawing.Point(12, 52);
            this.dgvInforme.MultiSelect = false;
            this.dgvInforme.Name = "dgvInforme";
            this.dgvInforme.ReadOnly = true;
            this.dgvInforme.RowHeadersVisible = false;
            this.dgvInforme.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInforme.Size = new System.Drawing.Size(938, 465);
            this.dgvInforme.TabIndex = 5;

            this.lblTotalInforme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left))));
            this.lblTotalInforme.AutoSize = true;
            this.lblTotalInforme.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalInforme.Location = new System.Drawing.Point(12, 530);
            this.lblTotalInforme.Name = "lblTotalInforme";
            this.lblTotalInforme.Size = new System.Drawing.Size(0, 19);
            this.lblTotalInforme.TabIndex = 6;

            // ==========================================================
            // Form1
            // ==========================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.tabPrincipal);
            this.MinimumSize = new System.Drawing.Size(1016, 679);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TP1 - Facturación simple";
            this.Load += new System.EventHandler(this.Form1_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleFactura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.grpProducto.ResumeLayout(false);
            this.grpProducto.PerformLayout();
            this.grpCabecera.ResumeLayout(false);
            this.grpCabecera.PerformLayout();
            this.grpLinea.ResumeLayout(false);
            this.grpLinea.PerformLayout();
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            this.tabProductos.ResumeLayout(false);
            this.tabProductos.PerformLayout();
            this.tabEmision.ResumeLayout(false);
            this.tabEmision.PerformLayout();
            this.tabConsulta.ResumeLayout(false);
            this.tabConsulta.PerformLayout();
            this.tabInforme.ResumeLayout(false);
            this.tabInforme.PerformLayout();
            this.tabPrincipal.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
