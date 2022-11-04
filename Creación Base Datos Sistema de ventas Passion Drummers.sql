CREATE DATABASE DBSISTEMA_VENTA

GO

USE DBSISTEMA_VENTA

GO

CREATE TABLE ROL(
IdRol int primary key identity,
Decripcion varchar(50),
FechaRegistro datetime default getdate()
)

go

CREATE TABLE PERMISO(
IdPermiso int primary key identity,
IdRol int references ROL(IdRol),
NombreMenu varchar(100),
FechaRegistro datetime default getdate()
)

go

CREATE TABLE PROVEEDOR(
IdProveedor int primary key identity,
Documento varchar(50),
RazonSocial varchar(50),
Correo varchar(50),
Telefono varchar(50),
Estado bit,
FechaRegistro datetime default getdate()
)

go

CREATE TABLE CLIENTE(
IdCliente int primary key identity,
Documento varchar(50),
NombreCompleto varchar(50),
Correo varchar(50),
Telefono varchar(50),
Estado bit,
FechaRegistro datetime default getdate()
)

go

CREATE TABLE USUARIO(
IdUsuario int primary key identity,
Documento varchar(50),
NombreCompleto varchar(50),
Correo varchar(50),
Clave varchar(50),
IdRol int references ROL(IdRol),
Estado bit,
FechaRegistro datetime default getdate()
)

go

CREATE TABLE CATEGORIA(
IdCategoria int primary key identity,
Descripcion varchar(100),
Estado bit,
FechaRegistro datetime default getdate()
)

go

CREATE TABLE PRODUCTO(
IdProducto int primary key identity,
Codigo varchar(50),
Nombre varchar(50),
Descripcion varchar(50),
IdCategoria int references CATEGORIA(IdCAtegoria),
Stock int not null default 0,
PrecioCompra decimal(10,2) default 0,
PrecioVenta decimal(10,2) default 0,
Estado bit,
FechaRegistro datetime default getdate()
)

go

CREATE TABLE COMPRA(
IdCompra int primary key identity,
IdUsuario int references USUARIO(IdUsuario),
IdProveedor int references PROVEEDOR(IdProveedor),
TipoDocumento varchar(50),
NumeroDocumento varchar(50),
MontoTotal decimal(10,2),
FechaRegistro datetime default getdate()
)

go

CREATE TABLE DETALLE_COMPRA(
IdDetalleCompra int primary key identity,
IdCompra int references COMPRA(IdCompra) ,
IdProducto int references PRODUCTO(IdProducto),
PrecioCompra decimal(10,2) default 0,
PrecioVenta decimal(10,2) default 0,
Cantidad int,
MontoTotal decimal(10,2),
FechaRegistro datetime default getdate()
)

go

CREATE TABLE VENTA(
IdVenta int primary key identity,
IdUsuario int references USUARIO(IdUsuario),
TipoDocumento varchar(50),
NumeroDocumento varchar(50),
DocumentoCliente varchar(50),
NombreCliente varchar(100),
MontoPago decimal(10,2),
MontoCambio decimal(10,2),
MontoTotal decimal(10,2),
FechaRegistro datetime default getdate()
)

go

CREATE TABLE DETALLE_VENTA(
IdDetalleVenta int primary key identity,
IdVenta int references VENTA(IdVenta) ,
IdProducto int references PRODUCTO(IdProducto),
PrecioVenta decimal(10,2),
Cantidad int,
SubTotal decimal(10,2),
FechaRegistro datetime default getdate()
)

go

INSERT INTO ROL(Decripcion) VALUES ('ADMINISTRADOR')
INSERT INTO ROL(Decripcion) VALUES ('EMPLEADO')

GO

INSERT INTO USUARIO(Documento,NombreCompleto,Correo,Clave,IdRol,Estado)
VALUES
('1235340177','Carlos Zambrano','cmiguelzambrano@unicesar.edu.co','2312',1,1)

INSERT INTO USUARIO(Documento,NombreCompleto,Correo,Clave,IdRol,Estado)
VALUES
('1006888183','Gian Oñate','gfonate@unicesar.edu.co','1423',2,1)

go

SELECT *FROM USUARIO

--PROCEDIMIENTOS PARA USUARIO----------------------------------------------------------------------------------------------

create proc SP_REGISTRARUSUARIO(
@Documento VARCHAR(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Clave varchar(50),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @IdUsuarioResultado = 0
	set @Mensaje = ''

	if not exists(select *from USUARIO where Documento = @Documento)
	begin
		insert into USUARIO(Documento,NombreCompleto,Correo,Clave,IdRol,Estado) values
		(@Documento,@NombreCompleto,@Correo,@Clave,@IdRol,@Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'

end

go

declare @idusuariogenerado int
declare @mensaje varchar(500)

exec SP_REGISTRARUSUARIO '32751463','Martha Padilla','marthap@gmail.com','1904',1,1,@idusuariogenerado output,@mensaje output

select @idusuariogenerado
select @mensaje

GO

create proc SP_EDITARUSUARIO(
@IdUsuario int,
@Documento VARCHAR(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Clave varchar(50),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''

	if not exists(select *from USUARIO where Documento = @Documento and IdUsuario != @IdUsuario)
	begin
		update USUARIO set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Clave = @Clave,
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario

		set @Respuesta = 1
	end
	else
		set @Mensaje = 'No se puede repetir el documento para más de un usuario'

end

go


create proc SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM COMPRA C
	INNER JOIN USUARIO U ON U.IdUsuario = C.IdUsuario
	WHERE U.IdUsuario = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una compra\n'
	END

	IF EXISTS (SELECT * FROM VENTA V
	INNER JOIN USUARIO U ON U.IdUsuario = V.IdUsuario
	WHERE U.IdUsuario = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una venta\n'
	END

	 if(@pasoreglas = 1)
	 BEGIN
		DELETE FROM USUARIO WHERE IdUsuario = @IdUsuario
		SET @Respuesta = 1
	 END
end

go

-----------------------------------------------------------------------------------------------------------------------

--Consulta para reiniciar el IdUsuario en caso de borrar un usuario
--DBCC CHECKIDENT ([USUARIO], RESEED, 2)
--Consulta para ver que numero tiene el IdUduario
--DBCC CHECKIDENT ([USUARIO], NORESEED)


--PROCEDIMIENTOS PARA CATEGORIA-------------------------------------------------------------------------------------------

--PROCEDIMIENTO PARA GUARDAR CATEGORIA
CREATE PROC SP_RegistrarCategoria(
@Descripcion varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT *FROM CATEGORIA WHERE Descripcion = @Descripcion)
	begin
		insert into CATEGORIA(Descripcion, Estado) values (@Descripcion,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
end

go

--PROCEDIMIENTO PARA MODIFICAR CATEGORIA
CREATE PROC SP_EditarCategoria(
@IdCategoria int,
@Descripcion varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT *FROM CATEGORIA WHERE Descripcion = @Descripcion and IdCategoria != @IdCategoria)
		
		update CATEGORIA set
		Descripcion = @Descripcion,
		Estado = @Estado
		where IdCategoria = @IdCategoria
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
	end
end

go

--PROCEDIMIENTO PARA ELIMINAR CATEGORIA
CREATE PROC SP_EliminarCategoria(
@IdCategoria int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
		SELECT *FROM CATEGORIA c
		inner join PRODUCTO p on p.IdCategoria = c.IdCategoria
		where c.IdCategoria = @IdCategoria
	)begin
		delete top(1) from CATEGORIA where IdCategoria = @IdCategoria
	end	
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La categoria se encuentra relacionada a un producto'
	end
end

go


SELECT *FROM CATEGORIA

INSERT INTO CATEGORIA(Descripcion,Estado) VALUES ('Accesorios',1)
INSERT INTO CATEGORIA(Descripcion,Estado) VALUES ('Percusión',1)
INSERT INTO CATEGORIA(Descripcion,Estado) VALUES ('Teclados',1)
INSERT INTO CATEGORIA(Descripcion,Estado) VALUES ('Vientos',1)
INSERT INTO CATEGORIA(Descripcion,Estado) VALUES ('Micrófonos',1)
INSERT INTO CATEGORIA(Descripcion,Estado) VALUES ('Sistemas de Audio',1)
INSERT INTO CATEGORIA(Descripcion,Estado) VALUES ('Otros',1)

go

--PROCEDIMIENTOS PARA PRODUCTOS----------------------------------------------------------------------------------------------
--PROCEDIMIENTO PARA REGISTRAR PRODUCTOS-

CREATE PROC SP_RegistrarProducto(
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar (30),
@IdCategoria int,
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT *FROM PRODUCTO WHERE Codigo = @Codigo)
	begin
		INSERT INTO PRODUCTO(Codigo,Nombre,Descripcion,IdCategoria,Estado) VALUES (@Codigo,@Nombre,@Descripcion,@IdCategoria,@Estado)
		SET @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		SET @Mensaje = 'Ya existe un producto con el mismo código'
end

go

--PROCEDIMIENTO PARA MODIFICAR PRODUCTOS-

CREATE PROC SP_ModificarProducto(
@IdProducto int,
@Codigo varchar(20),
@Nombre varchar(30),
@Descripcion varchar (30),
@IdCategoria int,
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT *FROM PRODUCTO WHERE Codigo = @Codigo and IdProducto != @IdProducto)
		UPDATE PRODUCTO SET
		Codigo = @Codigo,
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdCategoria = @IdCategoria
		Estado = @Estado
		WHERE IdProducto = @IdProducto
	ELSE
	begin
		SET @Resultado = 0
		SET @Mensaje = 'Ya existe un producto con el mismo código'
	end
end

go

--PROCEDIMIENTO PARA ELIMINAR PRODUCTOS-

CREATE PROC SP_EliminarProduto(
@IdProducto int,
@Respuesta bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Respuesta = 0
	SET @Mensaje = ''
	DECLARE @pasoreglas bit = 1

	IF EXISTS (SELECT *FROM DETALLE_COMPRA dc
	INNER JOIN PRODUCTO p ON p.IdProducto = dc.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado con una compra\n'
	END

	IF EXISTS (SELECT *FROM DETALLE_VENTA dv
	INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
	WHERE p.IdProducto = @IdProducto
	)
	BEGIN
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relacionado con una venta\n'
	END

	IF(@pasoreglas = 1)
	BEGIN
		DELETE FROM PRODUCTO WHERE IdProducto = @IdProducto
		SET @Respuesta = 1
	END
end

go

SELECT IdProducto, Codigo, Nombre, p.Descripcion, c.IdCategoria, c.Descripcion[DescripcionCategoria], Stock, PrecioCompra, PrecioVenta, p.Estado FROM PRODUCTO p
inner join CATEGORIA c ON c.IdCategoria = p.IdCategoria 

go

--PROCEDIMIENTOS PARA CLIENTES----------------------------------------------------------------------------------------------
--PROCEDIMIENTO PARA REGISTRAR CLIENTES-------------------------------------------------------------------------------------

CREATE PROC SP_RegistrarCliente(
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT *FROM CLIENTE WHERE Documento = @Documento)
	BEGIN
		INSERT INTO CLIENTE(Documento,NombreCompleto,Correo,Telefono,Estado) VALUES (
		@Documento,@NombreCompleto,@Correo,@Telefono,@Estado)

		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El número de documento ya existe'
end

go

--PROCEDIMIENTO PARA MODIFICAR CLIENTES-------------------------------------------------------------------------------------

CREATE PROC SP_ModificarCliente(
@IdCliente int,
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT *FROM CLIENTE WHERE Documento = @Documento AND IdCliente != @IdCliente)
	begin
		UPDATE CLIENTE SET
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		WHERE IdCliente = @IdCliente
	end
	ELSE
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'El número de documento ya existe'
	END
end

go

SELECT IdCliente,Documento,NombreCompleto,Correo,Telefono,Estado FROM CLIENTE
INSERT INTO CLIENTE(Documento,NombreCompleto,Correo,Telefono,Estado) VALUES ('1143249417','Loraine Zambranio','lorainez@gmail.com','3023815235',1)
SELECT *FROM CLIENTE

--Consulta para reiniciar el IdCliente en caso de borrar un cliente
--DBCC CHECKIDENT ([CLIENTE], RESEED, 1)
--Consulta para ver que numero tiene el IdUduario
--DBCC CHECKIDENT (CLIENTE, NORESEED)


--PROCEDIMIENTOS PARA PROVEEDORES----------------------------------------------------------------------------------------------
--PROCEDIMIENTO PARA REGISTRAR PROVEEDORES-------------------------------------------------------------------------------------

CREATE PROC SP_RegistrarProveedor(
@Documento varchar(50),
@RazonSocial varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT *FROM PROVEEDOR WHERE Documento = @Documento)
	begin
		INSERT INTO PROVEEDOR(Documento,RazonSocial,Correo,Telefono,Estado) VALUES (
		@Documento,@RazonSocial,@Correo,@Telefono,@Estado)

		SET @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		SET @Mensaje = 'El número de documento ya existe'
end

go

--PROCEDIMIENTO PARA MODIFICAR PROVEEDOR-------------------------------------------------------------------------------------

CREATE PROC SP_ModificarProveedor(
@IdProveedor int,
@Documento varchar(50),
@RazonSocial varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT *FROM PROVEEDOR WHERE Documento = @Documento AND IdProveedor != @IdProveedor)
	begin
		UPDATE PROVEEDOR SET
		Documento = @Documento,
		RazonSocial = @RazonSocial,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		WHERE IdProveedor = @IdProveedor
	end
	ELSE
	begin
		SET @Resultado = 0
		SET @Mensaje = 'El número de documento ya existe'
	end
end

go

--PROCEDIMIENTO PARA ELIMINAR PROVEEDOR-------------------------------------------------------------------------------------

CREATE PROC SP_EliminarProveedor(
@IdProveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	SELECT *FROM PROVEEDOR p
	INNER JOIN COMPRA c ON p.IdProveedor = c.IdProveedor
	WHERE p.IdProveedor = @IdProveedor
	)
	begin
		DELETE TOP(1) FROM PROVEEDOR WHERE IdProveedor = @IdProveedor
	end
	ELSE
	begin
		SET @Resultado = 0
		SET @Mensaje = 'El proveedor se encuentra relacionado a una compra'
	end
end

go

SELECT IdProveedor,Documento,RazonSocial,Correo,Telefono,Estado FROM PROVEEDOR
INSERT INTO PROVEEDOR(Documento,RazonSocial,Correo,Telefono,Estado) VALUES ('3201595','Yamaha','yamaha@gmail.com','5808481',1)

SELECT *FROM PROVEEDOR

--Consulta para reiniciar el IdProveedor en caso de borrar un proveedor
--DBCC CHECKIDENT (PROVEEDOR, RESEED, 2)
--Consulta para ver que numero tiene el IdUduario
--DBCC CHECKIDENT (PROVEEDOR, NORESEED)

go