CREATE DATABASE LUXURY_CARS
GO

USE LUXURY_CARS
GO

CREATE TABLE TipoCombustibles (
    IdCombustible INT IDENTITY(1, 1) PRIMARY KEY,
    TipoCombustible VARCHAR(30) UNIQUE
);
GO

CREATE TABLE TipoTransmisiones (
    IdTransmision INT IDENTITY(1, 1) PRIMARY KEY,
    TipoTransmision VARCHAR(30) UNIQUE
)
GO

CREATE TABLE TipoFinanciamientos (
    IdFinanciamiento INT IDENTITY(1, 1) PRIMARY KEY,
    TipoFinanciamiento VARCHAR(30) UNIQUE
)
GO

CREATE TABLE MarcasCarros (
    IdMarca INT IDENTITY(1, 1) PRIMARY KEY,
    Marca VARCHAR(30) UNIQUE
)
GO

CREATE TABLE ModelosCarros (
    IdModelo INT IDENTITY(1, 1) PRIMARY KEY,
    IdMarca INT,
    Modelo VARCHAR(50) UNIQUE,
    FOREIGN KEY (IdMarca) REFERENCES MarcasCarros(IdMarca)
)
GO

CREATE TABLE Carros (
    IdCarro INT IDENTITY(1, 1) PRIMARY KEY,
    IdModelo INT,
    Estilo VARCHAR(50),
    Anio INT,
    IdCombustible INT,
    IdTransmision INT,
    NumeroPuertas INT,
    ColorExterior VARCHAR(50),
    ColorInterior VARCHAR(50),
    FechaIngreso DATE,
    Placa VARCHAR(25),
    IdFinanciamiento INT,
    Apartado VARCHAR(3),
    Precio DECIMAL(10, 2),
    FOREIGN KEY (IdModelo) REFERENCES ModelosCarros(IdModelo),
    FOREIGN KEY (IdCombustible) REFERENCES TipoCombustibles(IdCombustible),
    FOREIGN KEY (IdTransmision) REFERENCES TipoTransmisiones(IdTransmision),
    FOREIGN KEY (IdFinanciamiento) REFERENCES TipoFinanciamientos(IdFinanciamiento)
)
GO

CREATE TABLE CarrosImagenes (
    IdImagen INT IDENTITY(1, 1) PRIMARY KEY,
    IdCarro INT,
    ImagenPath VARCHAR(255),
    FOREIGN KEY (IdCarro) REFERENCES Carros(IdCarro)
)
GO

CREATE TABLE Categoria (
    IdCategoria INT IDENTITY(1, 1) PRIMARY KEY,
    TipoCategoria VARCHAR(30) UNIQUE
)
GO

CREATE TABLE Proveedores (
    IdProveedor INT IDENTITY(1, 1) PRIMARY KEY,
    NombreProveedor VARCHAR(100),
    UbicacionEmpresa VARCHAR(300),
    CorreoElectronicoEmpresa VARCHAR(50),
    NumeroTelefonicoEmpresa VARCHAR(25)
)
GO

CREATE TABLE Estados (
    IdEstado INT IDENTITY(1, 1) PRIMARY KEY,
    NombreEstado VARCHAR(30) UNIQUE
)
GO

CREATE TABLE Productos (
    IdProducto INT IDENTITY(1, 1) PRIMARY KEY,
	IdCategoria INT,
	IdProveedor INT,
    NombreProducto VARCHAR(30),
    Precio DECIMAL(10, 2),
    CantidadStock INT,
    Descripcion VARCHAR(300),
	Estado BIT,
	FOREIGN KEY (IdProveedor) REFERENCES Proveedores(IdProveedor),
    FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria)
)
GO

CREATE TABLE ImagenesProductos (
    IdImagen INT IDENTITY(1, 1) PRIMARY KEY,
    IdProducto INT,
    ImagenPath VARCHAR(255),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
)
Go

CREATE TABLE Genero (
    IdGenero INT IDENTITY(1, 1) PRIMARY KEY,
    TipoGenero VARCHAR(30)
)
GO

CREATE TABLE TipoIdentificaciones (
    IdIdentificacion INT IDENTITY(1, 1) PRIMARY KEY,
    TipoIdentificacion VARCHAR(30)
)
GO

CREATE TABLE Roles (
    IdRol INT IDENTITY(1, 1) PRIMARY KEY,
    NombreRol VARCHAR(30) UNIQUE
)
GO

CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1, 1) PRIMARY KEY,
	IdTipoIdentificacion INT,
	IdRol INT,
    Identificacion VARCHAR(20) UNIQUE,
    Nombre VARCHAR(50),
    Apellido_Materno VARCHAR(50),
	Apellido_Paterno VARCHAR(50),
	Email VARCHAR(255) UNIQUE,
	Contrasenna VARCHAR(255),
    IdGenero INT,
    Telefono VARCHAR(20) UNIQUE,
    Direccion VARCHAR(300),
    FOREIGN KEY (IdGenero) REFERENCES Genero(IdGenero),
    FOREIGN KEY (IdTipoIdentificacion) REFERENCES TipoIdentificaciones(IdIdentificacion),
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol)
)
GO

CREATE TABLE MetodosDePago (
    IdMetodoPago INT IDENTITY(1, 1) PRIMARY KEY,
    NombreMetodo VARCHAR(255) UNIQUE
)
GO

CREATE TABLE CarritoDeCompras (
    IdCarrito INT IDENTITY(1, 1) PRIMARY KEY,
    IdProducto INT,
    CantidadProducto INT,
    PrecioUnitario DECIMAL(10, 2),
    IdEstado INT,
    Total DECIMAL(10, 2),
    FechaCreacion DATETIME,
    IdUsuario INT,
	FOREIGN KEY (IdEstado) REFERENCES Estados(IdEstado),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
)
GO

CREATE TABLE Facturacion (
    IdFactura INT IDENTITY(1, 1) PRIMARY KEY,
    IdUsuario INT,
    Descripcion VARCHAR(300),
    Fecha DATE,
    IdEstado INT,
    TotalFactura DECIMAL(10, 2),
    DireccionEnvio VARCHAR(300),
    IdMetodoPago INT,
    NumeroFactura VARCHAR(20) UNIQUE,
	FOREIGN KEY (IdEstado) REFERENCES Estados(IdEstado),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdMetodoPago) REFERENCES MetodosDePago(IdMetodoPago)
)
GO

CREATE TABLE HistorialCompraCarro (
    IdHistorialCompraCarro INT IDENTITY(1, 1) PRIMARY KEY,
    IdUsuario INT,
    IdCarro INT,
    Descripcion VARCHAR(300),
    Fecha DATETIME,
    IdEstado INT,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdCarro) REFERENCES Carros(IdCarro),
    FOREIGN KEY (IdEstado) REFERENCES Estados(IdEstado)
)
GO

INSERT INTO TipoCombustibles (TipoCombustible) VALUES ('GASOLINA')
GO
INSERT INTO TipoCombustibles (TipoCombustible) VALUES ('DIESEL')
GO
INSERT INTO TipoCombustibles (TipoCombustible) VALUES ('GAS NATURAL')
GO

INSERT INTO TipoTransmisiones (TipoTransmision) VALUES ('AUTOMATICA')
GO
INSERT INTO TipoTransmisiones (TipoTransmision) VALUES ('MANUAL')
GO
INSERT INTO TipoTransmisiones (TipoTransmision) VALUES ('CVT')
GO


INSERT INTO TipoFinanciamientos (TipoFinanciamiento) VALUES ('PRESTAMO PERSONAL')
GO
INSERT INTO TipoFinanciamientos (TipoFinanciamiento) VALUES ('TARJETA DE CREDITO')
GO
INSERT INTO TipoFinanciamientos (TipoFinanciamiento) VALUES ('HIPOTECA')
GO

INSERT INTO MarcasCarros (Marca) VALUES ('TOYOTA')
GO
INSERT INTO MarcasCarros (Marca) VALUES ('HONDA')
GO
INSERT INTO MarcasCarros (Marca) VALUES ('FORD')
GO
INSERT INTO MarcasCarros (Marca) VALUES ('CHEVROLET')
GO
INSERT INTO MarcasCarros (Marca) VALUES ('NISSAN')
GO
INSERT INTO MarcasCarros (Marca) VALUES ('VOLKSWAGEN')
GO
INSERT INTO MarcasCarros (Marca) VALUES ('BMW')
GO

INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (1, 'COROLLA')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (1, 'CAMRY')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (2, 'CIVIC')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (2, 'ACCORD')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (3, 'MUSTANG')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (3, 'F-150')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (4, 'SILVERADO')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (4, 'CRUZE')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (5, 'ALTIMA')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (5, 'ROGUE')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (6, 'GOLF')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (6, 'JETTA')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (7, '3 SERIES')
GO
INSERT INTO ModelosCarros (IdMarca, Modelo) VALUES (7, 'X5')
GO

INSERT INTO CATEGORIA (TIPOCATEGORIA) VALUES ('NEUMÁTICOS')
GO
INSERT INTO CATEGORIA (TIPOCATEGORIA) VALUES ('ACEITES Y LUBRICANTES')
GO
INSERT INTO CATEGORIA (TIPOCATEGORIA) VALUES ('ACCESORIOS INTERIORES')
GO

INSERT INTO ESTADOS (NOMBREESTADO) VALUES ('DISPONIBLE')
GO
INSERT INTO ESTADOS (NOMBREESTADO) VALUES ('EN PROCESO')
GO
INSERT INTO ESTADOS (NOMBREESTADO) VALUES ('VENDIDO')
GO
INSERT INTO ESTADOS (NOMBREESTADO) VALUES ('CANCELADO')
GO

INSERT INTO GENERO (TIPOGENERO) VALUES ('MASCULINO')
GO
INSERT INTO GENERO (TIPOGENERO) VALUES ('FEMENINO')
GO
INSERT INTO GENERO (TIPOGENERO) VALUES ('OTRO')
GO

INSERT INTO ROLES (NOMBREROL) VALUES ('ADMINISTRADOR')
GO
INSERT INTO ROLES (NOMBREROL) VALUES ('USUARIO')
GO
INSERT INTO ROLES (NOMBREROL) VALUES ('CLIENTE')
GO

INSERT INTO TipoIdentificaciones(TIPOIDENTIFICACION) VALUES ('CÉDULA DE IDENTIDAD')
GO
INSERT INTO TipoIdentificaciones (TIPOIDENTIFICACION) VALUES ('PASAPORTE')
GO
INSERT INTO TipoIdentificaciones (TIPOIDENTIFICACION) VALUES ('CÉDULA JURÍDICA')
GO
INSERT INTO METODOSDEPAGO (NOMBREMETODO) VALUES ('TARJETA DE CRÉDITO')
GO
INSERT INTO METODOSDEPAGO (NOMBREMETODO) VALUES ('PAYPAL')
GO
INSERT INTO METODOSDEPAGO (NOMBREMETODO) VALUES ('TRANSFERENCIA BANCARIA')
GO