
USE [InnovaTechDB]
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR EL STOCK EN EL INVENTARIO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarInventario 
@IdInventario          BIGINT,
@Incrementar           BIT
AS
BEGIN
    IF (@Incrementar = 1)
    BEGIN
        UPDATE Inventarios
        SET    Stock += 1;
    END
    IF (@Incrementar = 0)
    BEGIN
        UPDATE Inventarios
        SET    Stock -= 1;
    END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UNA CATEGORIA
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarCategoria
@IdCategoria           BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Categorias WHERE IdCategoria = @IdCategoria)
	BEGIN
		SELECT IdCategoria, NombreCategoria, DescripcionCategoria, Estado, IconoCategoria
		FROM Categorias
		WHERE IdCategoria = @IdCategoria
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODAS LAS CATEGORIAS
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarCategorias
@MostrarTodos           BIT
AS
BEGIN
	IF(@MostrarTodos = 1)
	BEGIN
			SELECT IdCategoria, NombreCategoria, DescripcionCategoria, Estado, IconoCategoria
			FROM Categorias;
	END
	ELSE
	BEGIN
			SELECT IdCategoria, NombreCategoria, DescripcionCategoria, Estado, IconoCategoria
			FROM Categorias
			WHERE Estado = 1;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CREAR UNA CATEGORIA
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE CrearCategoria
@NombreCategoria        VARCHAR(42),
@DescripcionCategoria   VARCHAR(160),
@IconoCategoria         VARCHAR(140)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Categorias WHERE NombreCategoria = @NombreCategoria)
	BEGIN
		INSERT INTO Categorias (NombreCategoria, DescripcionCategoria, Estado, IconoCategoria)
		VALUES (@DescripcionCategoria, @DescripcionCategoria, 1, @IconoCategoria)
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UNA CATEGORIA
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarCategoria
@IdCategoria           BIGINT,
@NombreCategoria       VARCHAR(42),
@DescripcionCategoria  VARCHAR(160),
@IconoCategoria        VARCHAR(140)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Categorias WHERE IdCategoria = @IdCategoria)
	BEGIN
		UPDATE Categorias
		SET NombreCategoria = @NombreCategoria,
			DescripcionCategoria = @DescripcionCategoria,
			IconoCategoria = @IconoCategoria
		WHERE IdCategoria = @IdCategoria
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UNA CATEGORIA
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE EliminarCategoria
@IdCategoria           BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Categorias WHERE IdCategoria = @IdCategoria)
	BEGIN
		DELETE FROM Categorias
		WHERE IdCategoria = @IdCategoria
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UN PRODUCTO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarProducto
@IdProducto		       BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Productos WHERE IdProducto = @IdProducto)
	BEGIN
		SELECT	P.IdProducto, P.IdInventario, P.IdCategoria, P.NombreProducto, C.NombreCategoria, P.PrecioUnitario, I.Stock, P.Color, P.Estado, P.ImagenProducto
		FROM	Productos P
		INNER JOIN Categorias C 
		ON P.IdCategoria = C.IdCategoria
		INNER JOIN Inventarios I
		ON P.IdInventario = I.IdInventario
		WHERE	P.IdProducto = @IdProducto;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODOS LOS PRODUCTOS
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarProductos
@MostrarTodos			BIT
AS
BEGIN

	IF(@MostrarTodos = 1)
	BEGIN
		SELECT	P.IdProducto, P.NombreProducto, C.NombreCategoria, P.PrecioUnitario, I.Stock, P.Color, P.Estado, P.ImagenProducto
		FROM	Productos P
		INNER JOIN Categorias C 
		ON P.IdCategoria = C.IdCategoria
		INNER JOIN Inventarios I
		ON P.IdInventario = I.IdInventario
	END
	ELSE
	BEGIN
		SELECT	P.IdProducto, P.NombreProducto, C.NombreCategoria, P.PrecioUnitario, I.Stock, P.Color, P.Estado, P.ImagenProducto
		FROM	Productos P
		INNER JOIN Categorias C 
		ON P.IdCategoria = C.IdCategoria
		INNER JOIN Inventarios I
		ON P.IdInventario = I.IdInventario
		WHERE	I.Stock > 0
		AND		P.Estado = 1;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA REGISTRAR UN PRODUCTO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE RegistrarProducto
@IdCategoria			BIGINT,
@NombreProducto			VARCHAR(42),
@PrecioUnitario			DECIMAL(18, 2),
@Color					VARCHAR(24),
@Stock					INT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Productos WHERE NombreProducto = @NombreProducto) AND @Stock > 0
	BEGIN
		INSERT INTO Inventarios (Stock)
		VALUES (@Stock)

		INSERT INTO Productos (IdInventario,IdCategoria,NombreProducto,PrecioUnitario,Color,Estado)
		VALUES (IDENT_CURRENT('Inventarios'),@IdCategoria,@NombreProducto,@PrecioUnitario,@Color,1)
		SELECT CONVERT(BIGINT,@@IDENTITY) Id
	END
	ELSE
	BEGIN
		SELECT CONVERT(BIGINT,-1) Id
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UN PRODUCTO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarProducto
@IdProducto			   BIGINT,
@IdCategoria		   BIGINT,
@NombreProducto		   VARCHAR(42),
@PrecioUnitario		   DECIMAL(18, 2),
@Color				   VARCHAR(24),
@Stock				   INT 
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Productos WHERE IdProducto = @IdProducto)
	BEGIN
		
		UPDATE  Inventarios
		SET		Stock = @Stock
		WHERE	IdInventario = @IdProducto;

		UPDATE	Productos
		SET		IdCategoria = @IdCategoria,
				NombreProducto = @NombreProducto,
				PrecioUnitario = @PrecioUnitario,
				Color = @Color
		WHERE	IdProducto = @IdProducto
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UNA IMAGEN DE UN PRODUCTO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarImagenProducto
@IdProducto			  BIGINT,
@ImagenProducto	  	  VARCHAR(140)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Productos WHERE IdProducto = @IdProducto)
	BEGIN
		UPDATE Productos
		SET ImagenProducto = @ImagenProducto
		WHERE IdProducto = @IdProducto
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UN PRODUCTO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE EliminarProducto
@IdProducto           BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Productos WHERE IdProducto = @IdProducto)
	BEGIN
			DELETE FROM Productos
			WHERE IdProducto = @IdProducto

			DELETE FROM Inventarios
			WHERE IdInventario = @IdProducto
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UN ROL
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarRol
@IdRol		           BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Roles WHERE IdRol = @IdRol)
	BEGIN
		SELECT	IdRol, NombreRol, DescripcionRol, Estado, ImagenRol
		FROM	Roles
		WHERE IdRol = @IdRol;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODOS LOS ROLES
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarRoles
@MostrarTodos			BIT
AS
BEGIN
	IF(@MostrarTodos = 1)
	BEGIN
		SELECT	IdRol, NombreRol, DescripcionRol, Estado, ImagenRol
		FROM	Roles;
	END
	ELSE
	BEGIN
		SELECT	IdRol, NombreRol, DescripcionRol, Estado, ImagenRol
		FROM	Roles
		WHERE	Estado = 1;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CREAR UN ROL
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE CrearRol
@NombreRol				VARCHAR(42),
@DescripcionRol		    VARCHAR(160)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Roles WHERE NombreRol = @NombreRol)
	BEGIN
		INSERT INTO Roles (NombreRol, DescripcionRol, Estado)
		VALUES (@NombreRol, @DescripcionRol, 1)
		SELECT CONVERT(BIGINT,@@IDENTITY) Id
	END
	ELSE
	BEGIN
		SELECT CONVERT(BIGINT,-1) Id
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UN ROL
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarRol
@IdRol					BIGINT,
@NombreRol				VARCHAR(42),
@DescripcionRol		    VARCHAR(160)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Roles WHERE IdRol = @IdRol)
	BEGIN
		UPDATE	Roles
		SET		NombreRol= @NombreRol,
				DescripcionRol = @DescripcionRol
		WHERE	IdRol = @IdRol;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR LA IMAGEN DE UN ROL
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarImagenRol
@IdRol					BIGINT,
@ImagenRol				VARCHAR(140)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Roles WHERE IdRol = @IdRol)
	BEGIN
		UPDATE Roles
		SET ImagenRol = @ImagenRol
		WHERE IdRol = @IdRol;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UN ROL
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE EliminarRol
@IdRol		            BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Roles WHERE IdRol = @IdRol)
	BEGIN
		DELETE FROM Roles
		WHERE IdRol = @IdRol
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UBICACIONES
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarUbicaciones
AS
BEGIN
	SELECT	U.IdUbicacion, U.NombreDistrito, C.IdCanton, C.NombreCanton, P.IdProvincia, P.NombreProvincia
	FROM	Ubicaciones U
	INNER JOIN Cantones C
	ON U.IdCanton = C.IdCanton
	INNER JOIN Provincias P
	ON C.IdProvincia = P.IdProvincia;
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UN USUARIO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarUsuario
@IdUsuario		           BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = @IdUsuario)
	BEGIN
		SELECT	IdUsuario,
				IdUbicacion,
				IdRol,
				NombreUsuario,
				ApellidoUsuario,
				Edad,
				Correo,
				Estado,
				ImagenUsuario
		FROM	Usuarios
		WHERE	IdUsuario = @IdUsuario;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA VISUALIZAR EL PERFIL DE UN CLIENTE
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE VisualizarPerfil
@IdUsuario		           BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = @IdUsuario)
	BEGIN
		SELECT	U.NombreUsuario,
				U.ApellidoUsuario,
				U.Edad,
				U.Correo,
				D.NombreDistrito,
				U.ImagenUsuario
		FROM	Usuarios U
		INNER JOIN Ubicaciones D
		ON U.IdUbicacion = D.IdUbicacion
		WHERE	U.IdUsuario = @IdUsuario;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODOS LOS USUARIOS
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarUsuarios
AS
BEGIN
	SELECT	U.IdUsuario,
			U.NombreUsuario,
			U.ApellidoUsuario,
			U.Edad,
			U.Correo,
			R.NombreRol,
			D.NombreDistrito,
			U.ImagenUsuario
	FROM	Usuarios U
	INNER JOIN Ubicaciones D
	ON U.IdUbicacion = D.IdUbicacion
	INNER JOIN Roles R
	ON U.IdRol = R.IdRol;
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA EL INICIO DE SESION DE USUARIO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE IniciarSesionUsuario
@Correo			           VARCHAR(120),
@Clave					   VARCHAR(42)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuarios WHERE Correo = @Correo)
	BEGIN
		SELECT	U.IdUsuario,
				U.IdUbicacion,
				U.IdRol,
				U.NombreUsuario,
				U.ApellidoUsuario,
				R.NombreRol,
				U.Edad,
				U.Correo,
				U.Estado,
				U.Temporal,
				U.Vencimiento,
				U.ImagenUsuario
		FROM	Usuarios U
		INNER JOIN Roles R
		ON U.IdRol = R.IdRol
		WHERE	U.Correo = @Correo
		AND		U.Clave = @Clave
		AND		U.Estado = 1;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA REGISTRAR UN USUARIO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE RegistrarUsuario
@IdUbicacion			BIGINT,
@NombreUsuario			VARCHAR(42),
@ApellidoUsuario		VARCHAR(42),
@Edad					INT,
@Correo					VARCHAR(120),
@Clave					VARCHAR(42)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Correo = @Correo)
	BEGIN
		INSERT INTO Usuarios (IdUbicacion, IdRol, NombreUsuario, ApellidoUsuario, Edad, Correo, Clave, Temporal, Vencimiento, Estado)
		VALUES (@IdUbicacion, 3, @NombreUsuario,@ApellidoUsuario,@Edad,@Correo,@Clave, 0, GETDATE(), 1)
		SELECT CONVERT(BIGINT,@@IDENTITY) Id
	END
	ELSE
	BEGIN
		SELECT CONVERT(BIGINT,-1) Id
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UN USUARIO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarUsuario
@IdUsuario				BIGINT,
@IdUbicacion			BIGINT,
@IdRol					BIGINT,
@NombreUsuario			VARCHAR(42),
@ApellidoUsuario		VARCHAR(42),
@Edad					INT,
@Correo					VARCHAR(120)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = @IdUsuario)
	BEGIN
		UPDATE	Usuarios
		SET		IdUbicacion = @IdUbicacion,
				IdRol = @IdRol,
				NombreUsuario = @NombreUsuario,
				ApellidoUsuario = @ApellidoUsuario,
				Edad = @Edad,
				Correo = @Correo
		WHERE	IdUsuario = @IdUsuario;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR EL PERFIL DE UN CLIENTE
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarPerfil
@IdUsuario				BIGINT,
@IdUbicacion			BIGINT,
@NombreUsuario			VARCHAR(42),
@ApellidoUsuario		VARCHAR(42),
@Edad					INT,
@Correo					VARCHAR(120)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = @IdUsuario)
	BEGIN
		UPDATE	Usuarios
		SET		IdUbicacion = @IdUbicacion,
				NombreUsuario = @NombreUsuario,
				ApellidoUsuario = @ApellidoUsuario,
				Edad = @Edad,
				Correo = @Correo
		WHERE	IdUsuario = @IdUsuario;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR LA IMAGEN DE UN USUARIO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarImagenUsuario
@IdUsuario					BIGINT,
@ImagenUsuario				VARCHAR(140)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = @IdUsuario)
	BEGIN
		UPDATE Usuarios
		SET ImagenUsuario = @ImagenUsuario
		WHERE IdUsuario = @IdUsuario;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CAMBIAR LA CLAVE DE UN USUARIO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE CambiarClave
@IdUsuario				BIGINT,
@ClaveAnterior          VARCHAR(42),
@ClaveNueva             VARCHAR(42)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = @IdUsuario AND Clave = @ClaveAnterior)
	BEGIN
		UPDATE	Usuarios
		SET		Clave = @ClaveNueva
		WHERE	Clave = @ClaveAnterior;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA GENERAR UNA CLAVE TEMPORAL
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE GenerarClave
@Correo					VARCHAR(120)
AS
BEGIN
	DECLARE @IdUsuario BIGINT

	SELECT	@IdUsuario = IdUsuario
	FROM	Usuarios WHERE	Correo = @Correo

	IF @IdUsuario IS NOT NULL
	BEGIN
		UPDATE	Usuarios
		SET		Clave = LEFT(NEWID(),8),
				Temporal = 1,
				Vencimiento = DATEADD(HOUR, 1, GETDATE())  
		WHERE	IdUsuario = @IdUsuario
	END

	SELECT	IdUsuario, Correo, Clave, NombreUsuario, ApellidoUsuario, Estado, Temporal, Vencimiento
	FROM	Usuarios
	WHERE	IdUsuario = @IdUsuario
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UN USUARIO
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE EliminarUsuario
@IdUsuario		        BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = @IdUsuario)
	BEGIN
		DELETE FROM Usuarios
		WHERE IdUsuario = @IdUsuario
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR LOS PRODUCTOS FAVORITOS POR CLIENTE
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarFavoritos
@IdUsuario		        BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Favoritos WHERE IdUsuario = @IdUsuario)
	BEGIN
		SELECT		P.IdProducto, P.NombreProducto, C.NombreCategoria, P.PrecioUnitario, I.Stock, P.Color, P.Estado, P.ImagenProducto
		FROM		Favoritos F
		INNER JOIN	Productos P
		ON F.IdProducto = P.IdProducto
		INNER JOIN	Inventarios I
		ON P.IdInventario = I.IdInventario
		INNER JOIN	Categorias C
		ON P.IdCategoria = C.IdCategoria
		WHERE		IdUsuario = @IdUsuario;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA AGREGAR UN PRODUCTO FAVORITO DEL CLIENTE
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE AgregarFavorito
@IdUsuario				BIGINT,
@IdProducto			    BIGINT
AS 
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Favoritos WHERE IdUsuario = @IdUsuario AND IdProducto = @IdProducto)
	BEGIN
		INSERT INTO Favoritos (IdUsuario, IdProducto)
		VALUES (@IdUsuario, @IdProducto)
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UN PRODUCTO FAVORITO DEL CLIENTE
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE EliminarFavorito
@IdUsuario			     BIGINT,
@IdProducto			     BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Favoritos WHERE IdUsuario = @IdUsuario)
	BEGIN
		DELETE FROM Favoritos
		WHERE IdProducto = @IdProducto
		AND	  IdUsuario = @IdUsuario;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA REGISTRAR LOS ERRORES DEL SISTEMA
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE RegistrarError
@CodigoError			INT,
@MensajeError			VARCHAR(64)
AS
BEGIN
	INSERT INTO ErroresSistema (CodigoError, MensajeError, FechaError)
	VALUES (@CodigoError, @MensajeError, GETDATE());
END
GO
