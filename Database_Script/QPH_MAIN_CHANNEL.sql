Create Database RRHH_MAIN_CHANNEL
Go

Use RRHH_MAIN_CHANNEL
Go

Create Table Channel(
	id int identity(1,1) primary key not null,
	fecha date not null,
	segmento varchar(300) not null,
	puntoEmision varchar(300) not null,
	ambiente varchar(300) not null,
	iva numeric(2,2) not null,
	codigoProducto varchar(300) not null,
	nombreProducto varchar(300) not null,
	categoriaCliente varchar(300) not null,
	cuentaContable varchar(300) not null,
	grupoCredito varchar(300) not null,
	documentoElectronico varchar(10) not null,
	relacionado varchar(10) not null,
	vendorSeccion varchar(300) not null,
	listaPrecioContado varchar(300) not null,
	listaPrecioCredito varchar(300) not null,
	limiteCredito varchar(300) not null,
	uge varchar(300) not null,
	bodega varchar(300) not null,
	formaPago varchar(300) not null,
	status varchar(300) not null
);
Go