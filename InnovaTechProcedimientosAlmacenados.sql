
USE [InnovaTechDB]
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODAS LAS UBICACIONES | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODOS LOS DISTRITOS | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarDistritos
AS
BEGIN
	SELECT	IdUbicacion, NombreDistrito
	FROM	Ubicaciones U;
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UN CANTON | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarCanton
@IdUbicacion           BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Ubicaciones WHERE IdUbicacion = @IdUbicacion)
	BEGIN
		SELECT	C.IdCanton, C.NombreCanton
		FROM	Cantones C
		INNER JOIN Ubicaciones U
		ON C.IdCanton = U.IdCanton
		WHERE U.IdUbicacion = @IdUbicacion
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UNA PROVINCIA | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarProvincia
@IdCanton           BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Cantones WHERE IdCanton = @IdCanton)
	BEGIN
		SELECT	P.IdProvincia, P.NombreProvincia
		FROM	Provincias P
		INNER JOIN Cantones C
		ON C.IdProvincia = P.IdProvincia
		WHERE C.IdCanton = @IdCanton
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR EL STOCK EN EL INVENTARIO | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarInventario 
@IdProducto            BIGINT,
@Incrementar           BIT
AS
BEGIN
    IF (@Incrementar = 1)
    BEGIN
        UPDATE Inventarios
        SET    Stock += 1
		WHERE IdInventario = @IdProducto
    END
    IF (@Incrementar = 0)
    BEGIN
        UPDATE Inventarios
        SET    Stock -= 1
		WHERE IdInventario = @IdProducto
    END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UNA CATEGORIA | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODAS LAS CATEGORIAS | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CREAR UNA CATEGORIA | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UNA CATEGORIA | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA DESHABILITAR UNA CATEGORIA | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE DeshabilitarCategoria
@IdCategoria            BIGINT,
@Estado					BIT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Categorias WHERE IdCategoria = @IdCategoria)
	BEGIN
		IF @Estado = 1
		BEGIN
			UPDATE Categorias
			SET Estado = 0
			WHERE IdCategoria = @IdCategoria
		END
		ELSE
		BEGIN
			UPDATE Categorias
			SET Estado = 1
			WHERE IdCategoria = @IdCategoria
		END
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UNA CATEGORIA | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UN PRODUCTO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODOS LOS PRODUCTOS | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA REGISTRAR UN PRODUCTO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UN PRODUCTO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UNA IMAGEN DE UN PRODUCTO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA DESHABILITAR UN PRODUCTO | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE DeshabilitarProducto
@IdProducto				BIGINT,
@Estado					BIT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Productos WHERE IdProducto = @IdProducto)
	BEGIN	
		IF @Estado = 1
		BEGIN
			UPDATE Productos
			SET Estado = 0
			WHERE IdProducto = @IdProducto
		END
		ELSE
		BEGIN
			UPDATE Productos
			SET Estado = 1
			WHERE IdProducto = @IdProducto
		END
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UN PRODUCTO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UN ROL | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarRol
@IdRol		           BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Roles WHERE IdRol = @IdRol)
	BEGIN
		SELECT	IdRol, NombreRol, DescripcionRol, Estado, IconoRol
		FROM	Roles
		WHERE IdRol = @IdRol;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODOS LOS ROLES | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarRoles
@MostrarTodos			BIT
AS
BEGIN
	IF(@MostrarTodos = 1)
	BEGIN
		SELECT	IdRol, NombreRol, DescripcionRol, Estado, IconoRol
		FROM	Roles;
	END
	ELSE
	BEGIN
		SELECT	IdRol, NombreRol, DescripcionRol, Estado, IconoRol
		FROM	Roles
		WHERE	Estado = 1;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CREAR UN ROL | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE CrearRol
@NombreRol				VARCHAR(42),
@DescripcionRol		    VARCHAR(160),
@IconoRol				Varchar(140)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Roles WHERE NombreRol = @NombreRol)
	BEGIN
		INSERT INTO Roles (NombreRol, DescripcionRol, Estado, IconoRol)
		VALUES (@NombreRol, @DescripcionRol, 1, @IconoRol)
		SELECT CONVERT(BIGINT,@@IDENTITY) Id
	END
	ELSE
	BEGIN
		SELECT CONVERT(BIGINT,-1) Id
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UN ROL | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarRol
@IdRol					BIGINT,
@NombreRol				VARCHAR(42),
@DescripcionRol		    VARCHAR(160),
@IconoRol				Varchar(140)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Roles WHERE IdRol = @IdRol)
	BEGIN
		UPDATE	Roles
		SET		NombreRol= @NombreRol,
				DescripcionRol = @DescripcionRol,
				IconoRol = @IconoRol
		WHERE	IdRol = @IdRol;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA DESHABILITAR UN ROL | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE DeshabilitarRol
@IdRol		            BIGINT,
@Estado					BIT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Roles WHERE IdRol = @IdRol)
	BEGIN
		IF @Estado = 1
		BEGIN
			UPDATE	Roles
			SET		Estado = 0
			WHERE	IdRol = @IdRol;
		END
		ELSE
		BEGIN
			UPDATE	Roles
			SET		Estado = 1
			WHERE	IdRol = @IdRol;
		END
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UN ROL | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR UN USUARIO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA VISUALIZAR EL PERFIL DE UN CLIENTE | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR TODOS LOS USUARIOS | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarUsuarios
@IdUsuario				BIGINT
AS
BEGIN
	SELECT	U.IdUsuario,
			U.NombreUsuario,
			U.ApellidoUsuario,
			U.Edad,
			U.Correo,
			R.NombreRol,
			D.NombreDistrito,
			U.ImagenUsuario,
			U.Estado
	FROM	Usuarios U
	INNER JOIN Ubicaciones D
	ON U.IdUbicacion = D.IdUbicacion
	INNER JOIN Roles R
	ON U.IdRol = R.IdRol
	WHERE IdUsuario != @IdUsuario;
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA EL INICIO DE SESION DE USUARIO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA REGISTRAR UN USUARIO | [B] [A]
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
		VALUES (@IdUbicacion, 1, @NombreUsuario,@ApellidoUsuario,@Edad,@Correo,@Clave, 0, GETDATE(), 1)
		SELECT CONVERT(BIGINT,@@IDENTITY) Id
	END
	ELSE
	BEGIN
		SELECT CONVERT(BIGINT,-1) Id
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR UN USUARIO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR EL PERFIL DE UN USUARIO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR LA IMAGEN DE UN USUARIO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CAMBIAR LA CLAVE DE UN USUARIO | [B] [A]
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
		SET		Clave = @ClaveNueva,
				Estado = 1,
				Temporal = 0
		WHERE	IdUsuario = @IdUsuario
		AND		Clave = @ClaveAnterior;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA GENERAR UNA CLAVE TEMPORAL | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA DESHABILITAR UN USUARIO | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE DeshabilitarUsuario
@IdUsuario		        BIGINT,
@Estado					BIT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuarios WHERE IdUsuario = @IdUsuario)
	BEGIN
		IF @Estado = 1
		BEGIN
			UPDATE	Usuarios
			SET		Estado = 0 
			WHERE	IdUsuario = @IdUsuario
		END
		ELSE
		BEGIN
			UPDATE	Usuarios
			SET		Estado = 1
			WHERE	IdUsuario = @IdUsuario
		END		
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UN USUARIO | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR SI UN USUARIO TIENE AGREGADO UN PRODUCTO FAVOTITO | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarFavorito
@IdUsuario		        BIGINT,
@IdProducto		        BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Favoritos WHERE IdProducto = @IdProducto
									   AND IdUsuario = @IdUsuario)
	BEGIN
		SELECT 1 AS Valor
	END
	ELSE
	BEGIN
		SELECT 0 AS Valor
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR LOS PRODUCTOS FAVORITOS POR CLIENTE | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA AGREGAR UN PRODUCTO FAVORITO DEL CLIENTE | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UN PRODUCTO FAVORITO DEL CLIENTE | [B] [A]
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
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR LA VALORACION DE UN PRODUCTO | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarValoracion
@IdUsuario		        BIGINT,
@IdProducto		        BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Valoraciones WHERE IdUsuario = @IdUsuario
										  AND	IdProducto = @IdProducto)
	BEGIN
		SELECT		Calificacion
		FROM		Valoraciones
		WHERE		IdUsuario = @IdUsuario
		AND			IdProducto = @IdProducto;
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA AGREGAR O ACTUALIZAR UNA VALORACION A UN PRODUCTO | [B] [A]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE AgregarValoracion
@IdUsuario				BIGINT,
@IdProducto			    BIGINT,
@Calificacion			INT
AS 
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Valoraciones WHERE IdUsuario = @IdUsuario AND IdProducto = @IdProducto)
	BEGIN
		INSERT INTO Valoraciones (IdUsuario, IdProducto, Calificacion)
		VALUES (@IdUsuario, @IdProducto, @Calificacion)
	END
	ELSE
	BEGIN
		UPDATE Valoraciones
		SET	   Calificacion = @Calificacion
		WHERE IdUsuario = @IdUsuario
		AND	  IdProducto = @IdProducto
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR SI UN PRODUCTO HA SIDO AÑADIDO | [B]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarAgregado
@IdUsuario		        BIGINT,
@IdProducto		        BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Carrito WHERE IdProducto = @IdProducto
									 AND IdUsuario = @IdUsuario)
	BEGIN
		SELECT 1 AS Valor
	END
	ELSE
	BEGIN
		SELECT 0 AS Valor
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA CONSULTAR LOS PRODUCTOS DEL CARRITO | [B]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarCarrito
@IdUsuario				BIGINT
AS
BEGIN
	SELECT C.IdCarrito,
		   C.IdProducto,
		   C.FechaCarrito,
		   C.Cantidad,
		   P.PrecioUnitario,
		   C.Impuestos,
		   C.SubTotal,
		   C.Total
	  FROM Carrito C
	  INNER JOIN Productos P
	  ON C.IdProducto = P.IdProducto
	  WHERE C.IdUsuario = @IdUsuario	
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA AGREGAR UN PRODUCTO AL CARRITO | [B]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE AgregarCarrito
@IdUsuario				 BIGINT,
@IdProducto				 BIGINT
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM Carrito WHERE IdUsuario = @IdUsuario 
									    AND	  IdProducto = @IdProducto)
	BEGIN
		INSERT INTO Carrito (IdUsuario, IdProducto, FechaCarrito, Cantidad, Impuestos, SubTotal, Total)
		SELECT @IdUsuario, @IdProducto, GETDATE(), 1, (1 * P.PrecioUnitario) * 0.13, 1 * P.PrecioUnitario, (1 * P.PrecioUnitario) + (1 * P.PrecioUnitario) * 0.13
		FROM Productos P
		WHERE P.IdProducto = @IdProducto
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ACTUALIZAR LA CANTIDAD DEL CARRITO | [B]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ActualizarCarrito
@IdCarrito				 BIGINT,
@Cantidad				 INT
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Carrito WHERE @IdCarrito = @IdCarrito)
	BEGIN
		IF (@Cantidad <= (SELECT I.Stock 
						 FROM Inventarios I 
						 INNER JOIN Productos P 
						 ON I.IdInventario = P.IdInventario 
						 INNER JOIN Carrito C
						 ON P.IdProducto = C.IdProducto
						 WHERE C.IdCarrito = @IdCarrito)
		AND	@Cantidad >= 1)
		BEGIN 
			UPDATE	C
			SET		FechaCarrito = GETDATE(),
					Cantidad = @Cantidad,
					Impuestos = (@Cantidad * P.PrecioUnitario) * 0.13,
					SubTotal = @Cantidad * P.PrecioUnitario,
					Total = (@Cantidad * P.PrecioUnitario) + (@Cantidad * P.PrecioUnitario) * 0.13
			FROM	Carrito C
			INNER JOIN Productos P
			ON C.IdProducto = P.IdProducto
			WHERE	C.IdCarrito = @IdCarrito
		END		
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA ELIMINAR UN PRODUCTO DEL CARRITO | [B]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE EliminarCarrito
@IdCarrito		BIGINT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Carrito WHERE IdCarrito = @IdCarrito)
	BEGIN
		DELETE FROM Carrito 
		WHERE IdCarrito = @IdCarrito
	END
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA MOSTRAR EL DETALLE DE LAS FACTURAS | [B]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarDetalleFacturas
@IdOrden		BIGINT
AS
BEGIN
	SELECT	D.IdDetalleOrden,
			D.IdOrden,
			P.NombreProducto,
			Cantidad,
			D.Precio,
			SubTotal,
			Impuestos,
			Total
	FROM	DetallesOrdenes D
	INNER	JOIN Productos P 
	ON D.IdProducto = P.IdProducto
	WHERE	IdOrden = @IdOrden	
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA MOSTRAR LAS FACTURAS | [B]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE ConsultarFacturas
@IdUsuario		 BIGINT
AS
BEGIN
	SELECT	O.IdOrden,
			U.NombreUsuario,
			O.FechaOrden,
			O.SubTotal,
			O.Impuestos,
			O.Total
	FROM	Ordenes O
	INNER	JOIN Usuarios U 
	ON O.IdUsuario = U.IdUsuario
	WHERE	O.IdUsuario = @IdUsuario	
END
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA REGISTRAR UNA ORDEN | [B]
----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE RegistrarOrden
@IdUsuario		 BIGINT
AS
BEGIN
	IF NOT EXISTS(	SELECT 1 FROM Carrito C
					INNER JOIN Productos P 
					ON		C.IdProducto = P.IdProducto
					INNER JOIN Inventarios I
					ON		P.IdInventario = I.IdInventario
					WHERE	IdUsuario = @IdUsuario
					AND		(I.Stock - C.Cantidad) < 0)
	BEGIN	
		-- INSERTAR ORDEN
		INSERT INTO Ordenes(IdUsuario, FechaOrden, Impuestos, SubTotal, Total)
		SELECT	C.IdUsuario, GETDATE(), C.Impuestos, C.SubTotal, C.Total
		FROM	Carrito C
		INNER	JOIN Productos P 
		ON C.IdProducto = P.IdProducto
		WHERE	IdUsuario = @IdUsuario
	
		-- INSERTAR DETALLE
		INSERT	INTO DetallesOrdenes(IdOrden, IdProducto, Cantidad, Precio, Impuestos, SubTotal, Total)
		SELECT	@@IDENTITY, C.IdProducto, C.Cantidad, P.PrecioUnitario, C.Impuestos, C.SubTotal, C.Total
		FROM	Carrito C
		INNER	JOIN Productos P 
		ON C.IdProducto = P.IdProducto
		WHERE	IdUsuario = @IdUsuario
	   
		-- MODIFICAR INVENTARIO
		UPDATE	I
		SET		I.Stock = Stock - C.Cantidad
		FROM	Inventarios I
		INNER JOIN Productos p
		ON I.IdInventario = P.IdInventario
		INNER	JOIN Carrito C 
		ON P.IdProducto = C.IdProducto
		WHERE	IdUsuario = @IdUsuario

		-- LIMPIAR EL CARRITO
		DELETE	FROM Carrito 
		WHERE	IdUsuario = @IdUsuario
	END
END
GO


----------------------------------------------------------------------------------------------------------
-- PROCEDIMIENTO ALMACENADO PARA REGISTRAR LOS ERRORES DEL SISTEMA | [B]
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

