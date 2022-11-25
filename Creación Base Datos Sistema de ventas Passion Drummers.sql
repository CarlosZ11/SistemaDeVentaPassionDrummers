SET LANGUAGE us_english

GO

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
		IdCategoria = @IdCategoria,
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

CREATE TABLE NEGOCIO(
IdNegocio int primary key,
Nombre varchar(60),
RUC varchar(60),
Direccion varchar(60),
Logo varbinary(max) null
)

go

SELECT *FROM NEGOCIO

go

INSERT INTO NEGOCIO(IdNegocio,Nombre,RUC,Direccion) VALUES (1, 'Passion Drummers', '318294', 'Carrera 19 #13-35')

go

--PROCESOS PARA REGISTRAR UNA COMPRA-------------------------------------------------------------------------------------

CREATE TYPE [dbo].[EDetalle_Compra] AS TABLE(
	[IdProducto] int null,
	[PrecioCompra] decimal(18,2) null,
	[PrecioVenta] decimal (18,2) null,
	[Cantidad] int null,
	[MontoTotal] decimal(18,2) null
)

GO



CREATE PROC SP_RegistrarCompra(
@IdUsuario int,
@IdProveedor int,
@TipoDocumento varchar(500),
@NumeroDocumento varchar(500),
@MontoTotal  decimal(18,2),
@DetalleCompra [EDetalle_Compra] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	begin try
		declare @IdCompra int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin transaction registro

			insert into COMPRA(IdUsuario,IdProveedor,TipoDocumento,NumeroDocumento,MontoTotal)
			values (@IdUsuario,@IdProveedor,@TipoDocumento,@NumeroDocumento,@MontoTotal)

			set @IdCompra = SCOPE_IDENTITY()

			insert into DETALLE_COMPRA(IdCompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal)
			select @IdCompra,IdProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal from @DetalleCompra

			update p set p.Stock = p.Stock + dc.Cantidad,
			p.PrecioCompra = dc.PrecioCompra,
			p.PrecioVenta = dc.PrecioVenta
			from PRODUCTO p
			inner join @DetalleCompra dc on dc.IdProducto = p.IdProducto

		commit transaction registro

	end try
	begin catch
		
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro

	end catch
end

go

--CONSULTA PARA VER DETALLE COMPRA-------------------------------------------------------------------------------------
SELECT c.IdCompra,
u.NombreCompleto,
pr.Documento, pr.RazonSocial,
c.TipoDocumento, c.NumeroDocumento, c.MontoTotal, convert(char(10),c.FechaRegistro,103)[FechaRegistro]
FROM COMPRA c
INNER JOIN USUARIO u ON u.IdUsuario = c.IdUsuario
INNER JOIN PROVEEDOR pr ON pr.IdProveedor = c.IdProveedor
WHERE c.NumeroDocumento = '00001'

go

--CONSULTA PARA SABER TODOS LOS PRODUCTOS RELACIONADOS EN UNA COMPRA-----------------------------------------------------
SELECT p.Nombre, dc.PrecioCompra, dc.Cantidad, dc.MontoTotal
FROM DETALLE_COMPRA dc
INNER JOIN PRODUCTO p ON p.IdProducto = dc.IdProducto
WHERE dc.IdCompra = 1

go


--PROCESOS PARA REGISTRAR UNA VENTA-------------------------------------------------------------------------------------

CREATE TYPE [dbo].[EDetalle_Venta] AS TABLE(
	[IdProducto] int null,
	[PrecioVenta] decimal(18,2) null,
	[Cantidad] int null,
	[SubTotal] decimal(18,2) null
)

GO

CREATE PROC SP_RegistrarVenta(
@IdUsuario int,
@TipoDocumento varchar(500),
@NumeroDocumento varchar(500),
@DocumentoCliente varchar(500),
@NombreCliente varchar(500),
@MontoPago decimal(18,2),
@MontoCambio decimal(18,2),
@MontoTotal decimal(18,2),
@DetalleVenta [EDetalle_Venta] READONLY,
@Resultado bit output,
@Mensaje varchar(500) output
)
AS
BEGIN
	BEGIN TRY
		
		DECLARE @idventa int = 0
		SET @Resultado = 1
		SET @Mensaje = ''

		BEGIN TRANSACTION registrto
			
			INSERT INTO VENTA(IdUsuario,TipoDocumento,NumeroDocumento,DocumentoCliente,NombreCliente,MontoPago,MontoCambio,MontoTotal)
			VALUES(@IdUsuario,@TipoDocumento,@NumeroDocumento,@DocumentoCliente,@NombreCliente,@MontoPago,@MontoCambio,@MontoTotal)

			SET @idventa =  SCOPE_IDENTITY()

			INSERT INTO DETALLE_VENTA(IdVenta,IdProducto,PrecioVenta,Cantidad,SubTotal)
			SELECT @idventa, IdProducto, PrecioVenta, Cantidad, SubTotal FROM @DetalleVenta

		COMMIT TRANSACTION registro

	END TRY
	BEGIN CATCH

		SET @Resultado = 0
		SET @Mensaje = ERROR_MESSAGE()
		ROLLBACK TRANSACTION registro

	END CATCH

END

GO

SELECT V.IdVenta, u.NombreCompleto,
v.DocumentoCliente, v.NombreCliente,
v.TipoDocumento, v.NumeroDocumento,
v.MontoPago, v.MontoCambio, v.MontoTotal,
convert(char(10),v.FechaRegistro,103)[FechaRegistro]
FROM VENTA v
INNER JOIN USUARIO u ON u.IdUsuario = v.IdUsuario
WHERE v.NumeroDocumento = '00001'


SELECT p.Nombre, dv.PrecioVenta, dv.Cantidad, dv.SubTotal FROM DETALLE_VENTA dv
INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
where dv.IdVenta = 1


SELECT *FROM VENTA

GO

SELECT *FROM COMPRA

GO

SELECT 
CONVERT(char(10),c.FechaRegistro,103)[FechaRegistro],c.TipoDocumento,c.NumeroDocumento,c.MontoTotal,
u.NombreCompleto[UsuarioComprador],
pr.Documento[DocumentoProveedor],pr.RazonSocial,
p.Codigo[CodigoProducto],p.Nombre[NombreProducto],ca.Descripcion[Categoria],dc.PrecioCompra,dc.PrecioVenta,dc.Cantidad,dc.MontoTotal[SubTotal]
FROM COMPRA c
INNER JOIN USUARIO u ON u.IdUsuario = c.IdUsuario
INNER JOIN PROVEEDOR pr ON pr.IdProveedor = c.IdProveedor
INNER JOIN DETALLE_COMPRA dc ON dc.IdCompra = c.IdCompra
INNER JOIN PRODUCTO p ON p.IdProducto = dc.IdProducto
INNER JOIN CATEGORIA ca ON ca.IdCategoria = p.IdCategoria
WHERE CONVERT(date,c.FechaRegistro) between '11/11/2022' and '14/11/2022' and pr.IdProveedor = 1

GO

--PROCEDIMIENTO PARA REPORTE DE COMPRAS-------------------------------------------------------------------------------------
CREATE PROC SP_ReporteCompras(
@fechainicio varchar(10),
@fechafin varchar(10),
@idproveedor int
)
as
begin
	SET DATEFORMAT dmy;
	SELECT 
	CONVERT(char(10),c.FechaRegistro,103)[FechaRegistro],c.TipoDocumento,c.NumeroDocumento,c.MontoTotal,
	u.NombreCompleto[UsuarioComprador],
	pr.Documento[DocumentoProveedor],pr.RazonSocial,
	p.Codigo[CodigoProducto],p.Nombre[NombreProducto],ca.Descripcion[Categoria],dc.PrecioCompra,dc.PrecioVenta,dc.Cantidad,dc.MontoTotal[SubTotal]
	FROM COMPRA c
	INNER JOIN USUARIO u ON u.IdUsuario = c.IdUsuario
	INNER JOIN PROVEEDOR pr ON pr.IdProveedor = c.IdProveedor
	INNER JOIN DETALLE_COMPRA dc ON dc.IdCompra = c.IdCompra
	INNER JOIN PRODUCTO p ON p.IdProducto = dc.IdProducto
	INNER JOIN CATEGORIA ca ON ca.IdCategoria = p.IdCategoria
	WHERE CONVERT(date,c.FechaRegistro) between @fechainicio and @fechafin
	and pr.IdProveedor = iif(@idproveedor = 0, pr.IdProveedor, @idproveedor)

end

GO


CREATE PROC SP_ReporteVentas(
@fechainicio varchar(10),
@fechafin varchar(10)
)
as
begin
	SET DATEFORMAT dmy;
	SELECT 
	CONVERT(char(10),v.FechaRegistro,103)[FechaRegistro],v.TipoDocumento,v.NumeroDocumento,v.MontoTotal,
	u.NombreCompleto[UsuarioVendedor],
	v.DocumentoCliente,v.NombreCliente,
	p.Codigo[CodigoProducto],p.Nombre[NombreProducto],ca.Descripcion[Categoria],dv.PrecioVenta,dv.PrecioVenta,dv.Cantidad,dv.SubTotal
	FROM VENTA v
	INNER JOIN USUARIO u ON u.IdUsuario = v.IdUsuario
	INNER JOIN DETALLE_VENTA dv ON dv.IdVenta = v.IdVenta
	INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
	INNER JOIN CATEGORIA ca ON ca.IdCategoria = p.IdCategoria
	WHERE CONVERT(date,v.FechaRegistro) between @fechainicio and @fechafin

end

GO

--PROCEDIMIENTO PARA CONSULTAR LAS COMPRAS DE LOS CLIENTES
CREATE PROC SP_ComprasPorClientes
AS
select v.NombreCliente,
sum(cantidad) [Cantidad de productos comprados]
from VENTA v
inner join CLIENTE c on c.NombreCompleto = v.NombreCliente
inner join DETALLE_VENTA dv on dv.IdVenta = v.IdVenta
group by v.NombreCliente

GO

CREATE PROC SP_MontoProductosComprados
AS
select v.NombreCliente,
sum(MontoTotal) [Monto de productos comprados]
from VENTA v
inner join CLIENTE c on c.NombreCompleto = v.NombreCliente
group by v.NombreCliente

GO

--Inventario de productos
SELECT Nombre,Stock FROM PRODUCTO

GO

--PROCEDIMIENTO PARA CONSULTAR LOS PRODUCTOS VENDIDOS
CREATE PROC SP_ProductosVendidos
@fechaInicio Date,
@fechaFin Date
AS
select p.Nombre,
sum(cantidad) [Cantidad de productos vendidos]
from DETALLE_VENTA dv
inner join PRODUCTO p on p.IdProducto = dv.IdProducto
WHERE dv.FechaRegistro between @fechaInicio and @fechaFin
group by p.Nombre

GO
