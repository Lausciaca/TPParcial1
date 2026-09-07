-- =============================================
-- TP Parcial 1 - Facturacion simple
-- SQL Server 2019
-- Ejecutar en SSMS como administrador.
-- 1) Crea la base FacturacionTP1
-- 2) Crea SEQUENCE para Numero correlativo
-- 3) Crea Productos, Facturas, FacturaDetalle
-- Reglas:
--  - Facturas JAMAS se eliminan (no hay DELETE, solo Anulada=1)
--  - Precio se copia al detalle al facturar
-- =============================================

IF DB_ID('FacturacionTP1') IS NULL
    CREATE DATABASE FacturacionTP1;
GO

USE FacturacionTP1;
GO

-- Secuencia para el Numero de factura (correlativo sin race conditions).
-- Evita el problema de MAX(Numero)+1 hecho en la app.
IF OBJECT_ID('dbo.SeqFacturaNumero', 'SO') IS NULL
    CREATE SEQUENCE dbo.SeqFacturaNumero AS INT START WITH 1 INCREMENT BY 1;
GO

-- ---------- Productos ----------
IF OBJECT_ID('dbo.Productos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Productos (
        Id      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Codigo  VARCHAR(50)       NOT NULL UNIQUE,
        Nombre  NVARCHAR(150)     NOT NULL,
        Precio  DECIMAL(18,2)     NOT NULL CONSTRAINT CK_Productos_Precio CHECK (Precio >= 0),
        Activo  BIT               NOT NULL CONSTRAINT DF_Productos_Activo DEFAULT (1),
        CONSTRAINT CK_Productos_Codigo_NoVacio CHECK (LEN(LTRIM(RTRIM(Codigo))) > 0),
        CONSTRAINT CK_Productos_Nombre_NoVacio CHECK (LEN(LTRIM(RTRIM(Nombre))) > 0)
    );
    CREATE INDEX IX_Productos_Codigo ON dbo.Productos(Codigo);
    CREATE INDEX IX_Productos_Nombre ON dbo.Productos(Nombre);
END
GO

-- ---------- Facturas (cabecera) ----------
IF OBJECT_ID('dbo.Facturas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Facturas (
        Id                INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Numero            INT NOT NULL UNIQUE
            CONSTRAINT DF_Facturas_Numero DEFAULT (NEXT VALUE FOR dbo.SeqFacturaNumero),
        Fecha             DATETIME2       NOT NULL CONSTRAINT DF_Facturas_Fecha DEFAULT (SYSDATETIME()),
        ClienteNombre     NVARCHAR(150)   NOT NULL,
        ClienteDocumento  VARCHAR(50)     NOT NULL,
        Total             DECIMAL(18,2)   NOT NULL CONSTRAINT CK_Facturas_Total CHECK (Total >= 0),
        Anulada           BIT             NOT NULL CONSTRAINT DF_Facturas_Anulada DEFAULT (0),
        FechaAnulacion    DATETIME2       NULL,
        CONSTRAINT CK_Facturas_Cliente_NoVacio CHECK (LEN(LTRIM(RTRIM(ClienteNombre))) > 0),
        CONSTRAINT CK_Facturas_Anulacion_Coherente CHECK (
            (Anulada = 0 AND FechaAnulacion IS NULL) OR
            (Anulada = 1 AND FechaAnulacion IS NOT NULL)
        )
    );
    CREATE INDEX IX_Facturas_Fecha ON dbo.Facturas(Fecha);
    CREATE INDEX IX_Facturas_Cliente ON dbo.Facturas(ClienteNombre);
END
GO

-- ---------- FacturaDetalle (lineas) ----------
IF OBJECT_ID('dbo.FacturaDetalle', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.FacturaDetalle (
        Id             INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FacturaId      INT NOT NULL
            CONSTRAINT FK_Detalle_Factura FOREIGN KEY REFERENCES dbo.Facturas(Id),
        ProductoId     INT NOT NULL
            CONSTRAINT FK_Detalle_Producto FOREIGN KEY REFERENCES dbo.Productos(Id),
        Cantidad       INT NOT NULL CONSTRAINT CK_Detalle_Cantidad CHECK (Cantidad > 0),
        PrecioUnitario DECIMAL(18,2) NOT NULL CONSTRAINT CK_Detalle_Precio CHECK (PrecioUnitario >= 0),
        Subtotal       DECIMAL(18,2) NOT NULL CONSTRAINT CK_Detalle_Subtotal CHECK (Subtotal >= 0)
    );
    CREATE INDEX IX_Detalle_FacturaId ON dbo.FacturaDetalle(FacturaId);
    CREATE INDEX IX_Detalle_ProductoId ON dbo.FacturaDetalle(ProductoId);
END
GO

-- Datos de ejemplo (opcional)
IF NOT EXISTS (SELECT 1 FROM dbo.Productos)
BEGIN
    INSERT INTO dbo.Productos (Codigo, Nombre, Precio, Activo) VALUES
        ('P001', 'Yerba 1kg', 4500.00, 1),
        ('P002', 'Azucar 1kg', 1200.00, 1),
        ('P003', 'Cafe 500g', 6800.00, 1);
END
GO
