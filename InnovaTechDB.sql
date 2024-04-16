USE [master]
GO

-------------------------------------------------------------------------
-- CREACION DE LA BASE DE DATOS
-------------------------------------------------------------------------
CREATE DATABASE [InnovaTechDB];
GO


USE [InnovaTechDB]
GO

-------------------------------------------------------------------------
-- TABLA DE LOS INVENTARIOS
-------------------------------------------------------------------------
CREATE TABLE Inventarios (
  IdInventario             BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  Stock                    INT NOT NULL
);

-------------------------------------------------------------------------
-- TABLA DE LAS CATEGORIAS
-------------------------------------------------------------------------
CREATE TABLE Categorias (
  IdCategoria             BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  NombreCategoria         VARCHAR(42) NOT NULL,
  DescripcionCategoria    VARCHAR(160) NOT NULL,
  Estado                  BIT NOT NULL,
  IconoCategoria          VARCHAR(140) NULL
);

-------------------------------------------------------------------------
-- TABLA DE LOS PRODUCTOS
-------------------------------------------------------------------------
CREATE TABLE Productos (
  IdProducto              BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  IdInventario            BIGINT NOT NULL,
  IdCategoria             BIGINT NOT NULL,
  NombreProducto          VARCHAR(42) NOT NULL,
  PrecioUnitario          DECIMAL(18, 2) NOT NULL,
  Color                   VARCHAR(24) NOT NULL,
  Estado                  BIT NOT NULL,
  ImagenProducto          VARCHAR(140) NULL,
  CONSTRAINT FKProductosInventarios FOREIGN KEY (IdInventario)
    REFERENCES Inventarios (IdInventario),
  CONSTRAINT FKProductosCategorias FOREIGN KEY (IdCategoria)
    REFERENCES Categorias (IdCategoria)
);

-------------------------------------------------------------------------
-- TABLA DE LOS ROLES
-------------------------------------------------------------------------
CREATE TABLE Roles (
  IdRol                   BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  NombreRol               VARCHAR(42) NOT NULL,
  DescripcionRol          VARCHAR(160) NOT NULL,
  Estado                  BIT NOT NULL,
  ImagenRol               VARCHAR(140) NULL
);

-------------------------------------------------------------------------
-- TABLA DE LAS PROVINCIAS
-------------------------------------------------------------------------
CREATE TABLE Provincias (
  IdProvincia             BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  NombreProvincia         VARCHAR(42) NOT NULL
);

-------------------------------------------------------------------------
-- TABLA DE LOS CANTONES
-------------------------------------------------------------------------
CREATE TABLE Cantones (
  IdCanton                BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  IdProvincia             BIGINT NOT NULL,
  NombreCanton            VARCHAR(42) NOT NULL,
  CONSTRAINT FKCantonesProvincias FOREIGN KEY (IdProvincia)
    REFERENCES Provincias (IdProvincia)
);

-------------------------------------------------------------------------
-- TABLA DE LAS UBICACIONES
-------------------------------------------------------------------------
CREATE TABLE Ubicaciones (
  IdUbicacion             BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  IdCanton                BIGINT NOT NULL,
  NombreDistrito          VARCHAR(42) NOT NULL,
  CONSTRAINT FKUbicacionesCantones FOREIGN KEY (IdCanton)
    REFERENCES Cantones (IdCanton)
);

-------------------------------------------------------------------------
-- TABLA DE LOS USUARIOS
-------------------------------------------------------------------------
CREATE TABLE Usuarios (
  IdUsuario               BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  IdUbicacion             BIGINT NOT NULL,
  IdRol                   BIGINT NOT NULL,
  NombreUsuario           VARCHAR(42) NOT NULL,
  ApellidoUsuario         VARCHAR(42) NOT NULL,
  Edad                    INT NOT NULL,
  Correo                  VARCHAR(120) NOT NULL,
  Clave                   VARCHAR(42) NOT NULL,
  Temporal                BIT NOT NULL,
  Vencimiento             DATETIME NOT NULL,
  Estado                  BIT NOT NULL,
  ImagenUsuario           VARCHAR(140) NULL,
  CONSTRAINT FKUsuariosRoles FOREIGN KEY (IdRol)
    REFERENCES Roles (IdRol),
  CONSTRAINT FKUsuariosUbicaciones FOREIGN KEY (IdUbicacion)
    REFERENCES Ubicaciones (IdUbicacion)
);

-------------------------------------------------------------------------
-- TABLA DE LOS PRODUCTOS FAVORITOS POR USUARIO
-------------------------------------------------------------------------
CREATE TABLE Favoritos (
  IdFavorito              BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  IdUsuario               BIGINT NOT NULL,
  IdProducto              BIGINT NOT NULL,
  CONSTRAINT FKFavoritosUsuarios FOREIGN KEY (IdUsuario)
    REFERENCES Usuarios (IdUsuario),
  CONSTRAINT FKFavoritosProductos FOREIGN KEY (IdProducto)
    REFERENCES Productos (IdProducto)
);

-------------------------------------------------------------------------
-- TABLA DE LAS VALORACIONES POR PRODUCTO
-------------------------------------------------------------------------
CREATE TABLE Valoraciones (
  IdValoracion            BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  IdUsuario               BIGINT NOT NULL,
  IdProducto              BIGINT NOT NULL,
  CONSTRAINT FKValoracionesUsuarios FOREIGN KEY (IdUsuario)
    REFERENCES Usuarios (IdUsuario),
  CONSTRAINT FKValoracionesProductos FOREIGN KEY (IdProducto)
    REFERENCES Productos (IdProducto)
);

-------------------------------------------------------------------------
-- TABLA DE LAS ORDENES
-------------------------------------------------------------------------
CREATE TABLE Ordenes (
  IdOrden                 BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  IdUsuario               BIGINT NOT NULL,
  FechaOrden              DATE NOT NULL,
  CONSTRAINT FKOrdenesUsuarios FOREIGN KEY (IdUsuario)
    REFERENCES Usuarios (IdUsuario)
);

-------------------------------------------------------------------------
-- TABLA DE LOS DETALLES DE LAS ORDENES
-------------------------------------------------------------------------
CREATE TABLE DetallesOrdenes (
  IdDetalleOrden          BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  IdProducto              BIGINT NOT NULL,
  IdOrden                 BIGINT NOT NULL,
  Total                   DECIMAL(18, 2) NOT NULL,
  Subtotal                DECIMAL(18, 2) NOT NULL,
  Cantidad                INT NOT NULL,
  CONSTRAINT FKDetallesOrdenesOrdenes FOREIGN KEY (IdOrden)
    REFERENCES Ordenes (IdOrden),
  CONSTRAINT FKDetallesOrdenesProductos FOREIGN KEY (IdProducto)
    REFERENCES Productos (IdProducto)
);

-------------------------------------------------------------------------
-- TABLA DE LOS ERRORES DEL SISTEMA
-------------------------------------------------------------------------
CREATE TABLE ErroresSistema (
  IdError                BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  CodigoError            INT NOT NULL,
  MensajeError           VARCHAR(64) NOT NULL,
  FechaError             DATETIME NOT NULL
);

-------------------------------------------------------------------------
-- ELIMINAR LOS DATOS DE LAS TABLAS
-------------------------------------------------------------------------

DELETE FROM Inventarios;
DBCC CHECKIDENT (Inventarios, RESEED, 0);

DELETE FROM Categorias;
DBCC CHECKIDENT (Categorias, RESEED, 0);

DELETE FROM Productos;
DBCC CHECKIDENT (Productos, RESEED, 0);

DELETE FROM Roles;
DBCC CHECKIDENT (Roles, RESEED, 0);

DELETE FROM Provincias;
DBCC CHECKIDENT (Provincias, RESEED, 0);

DELETE FROM Cantones;
DBCC CHECKIDENT (Cantones, RESEED, 0);

DELETE FROM Ubicaciones;
DBCC CHECKIDENT (Ubicaciones, RESEED, 0);

DELETE FROM Usuarios;
DBCC CHECKIDENT (Usuarios, RESEED, 0);

DELETE FROM Favoritos;
DBCC CHECKIDENT (Favoritos, RESEED, 0);

DELETE FROM Ordenes;
DBCC CHECKIDENT (Ordenes, RESEED, 0);

DELETE FROM DetallesOrdenes;
DBCC CHECKIDENT (DetallesOrdenes, RESEED, 0);

DELETE FROM ErroresSistema;
DBCC CHECKIDENT (ErroresSistema, RESEED, 0);


-------------------------------------------------------------------------
-- ELIMINAR LOS CONSTRAINTS DE LAS TABLAS
-------------------------------------------------------------------------

ALTER TABLE Productos DROP CONSTRAINT FKProductosInventarios;
ALTER TABLE Productos DROP CONSTRAINT FKProductosCategorias;

ALTER TABLE Cantones DROP CONSTRAINT FKCantonesProvincias;

ALTER TABLE Ubicaciones DROP CONSTRAINT FKUbicacionesCantones;

ALTER TABLE Usuarios DROP CONSTRAINT FKUsuariosRoles;
ALTER TABLE Usuarios DROP CONSTRAINT FKUsuariosUbicaciones;

ALTER TABLE Favoritos DROP CONSTRAINT FKFavoritosProductos;
ALTER TABLE Favoritos DROP CONSTRAINT FKFavoritosUsuarios;

ALTER TABLE Ordenes DROP CONSTRAINT FKOrdenesUsuarios;

ALTER TABLE DetallesOrdenes DROP CONSTRAINT FKDetallesOrdenesOrdenes;
ALTER TABLE DetallesOrdenes DROP CONSTRAINT FKDetallesOrdenesProductos;

-------------------------------------------------------------------------
-- BORRAR TABLAS DE LA BASE DE DATOS
-------------------------------------------------------------------------

DROP TABLE Inventarios;
DROP TABLE Categorias;
DROP TABLE Productos;
DROP TABLE Roles;
DROP TABLE Provincias;
DROP TABLE Cantones;
DROP TABLE Ubicaciones;
DROP TABLE Usuarios;
DROP TABLE Favoritos;
DROP TABLE Ordenes;
DROP TABLE DetallesOrdenes;
DROP TABLE ErroresSistema;