Use master;
Go

Drop Database RRHH_MAIN;
Go

Create Database RRHH_MAIN;
Go

Use RRHH_MAIN;
Go

DBCC CHECKIDENT(EnterpriseHierarchyCatalog, RESEED, 0);
Go

Create Table Country(
	id int identity(1,1)primary key not null,
	"name" varchar(100)not null unique
);
Go

Create Table Region(
	id int identity(1,1)primary key not null,
	id_country int not null,
	"name" varchar(100)not null unique,
	foreign key(id_country) references Country(id)
);
Go

Create Table City(
	id int identity(1,1) primary key not null,
	id_region int not null,
	"name" varchar(100)not null unique,
	foreign key(id_region)references Region(id)
);
Go

Create Table Roles(
	id int identity(1,1) primary key not null,
	rolename varchar(100) not null unique,
	"status" bit not null default 0,
);
Go

Create Table Enterprise(
	id int identity(1,1) primary key not null,
	id_city int not null,
	commercial_name varchar(100) not null,
	telephone varchar(20) not null default 'N/A', -- N/A Means "NOT ASSIGNED"
	email varchar(100) not null default 'N/A',
	enterprise_address varchar(300) not null,
	identification varchar(100) not null default 'N/A', --ex: here in nicaragua every business needs a RUC.
	has_branches bit not null default 0, -- has_branches is used to identify if the business has any other child.
	latitude float NOT NULL, 
    longitude float NOT NULL, 
	created_at datetime not null default getdate(),
	"status" bit not null default 1,
	foreign key(id_city) references City(id)
);
Go

Create Table EnterpriseContact(
	id int identity(1,1) primary key not null,
	id_enterprise int not null,
	email varchar(150) not null default 'N/A',
	telephone varchar(15) not null default 'N/A',
	contact_position varchar(100) not null, --Ex: Manager, CEO, VicePresident
	foreign key(id_enterprise) references Enterprise(id)
);
Go

Create Table Branched_Enterprise(
	id int identity(1,1) primary key not null,
	id_enterprise int not null,
	identification varchar(100) not null default 'N/A', --ex: here in nicaragua every business needs a RUC.
	direction varchar(300) not null default 'N/A',
	latitude float NOT NULL, 
    longitude float NOT NULL, 
	"status" bit not null default 0,
	foreign key(id_enterprise) references Enterprise(id)
);
Go

Create Table Blacklist(
	id int identity(1,1) primary key not null,
	public_ip varbinary(16) not null
);
Go

Create Table SystemParameters(
	code varchar(20) primary key not null,
	description varchar(200)not null,
	"value" varchar(100)not null,
	dataType varchar(100)not null,
	status bit not null default 1
);
Go

Create Table "User"(
	id int identity(1,1) primary key not null,
	id_role int not null,
	id_enterprise int not null,
	id_country int not null, -- nationality
    nickname varchar(100) not null unique,
    email varchar(150) not null unique,
    phone_number varchar(50) unique,
    hashPassword varchar(max) not null,
	activation_code varchar(max),
    google_access_token varchar(max) not null,
    facebook_access_token varchar(max) not null,
    firebase_token varchar(max) not null,
    is_account_activated bit default 0,
    profile_picture varchar(max) not null default 'assets/profilePictures/default.jpg',
    status bit not null default 0,
    foreign key (id_role) references Roles(id),
	foreign key (id_enterprise) references Enterprise(id),
	foreign key (id_country) references Country(id)
);
Go

Create Table "Sessions"(
	id int identity(1,1) primary key not null,
	id_user int not null,
    session_token varchar(max) not null default '', -- When the user check the remember me, the session isn't gonna expire, so the token is gonna be saved in the table Session.
    started datetime default getdate(),
    ended timestamp,
    public_ip varbinary(16) not null,
    macaddress char(12) not null,
    hostname varchar(20) default 'N/A',
    foreign key (id_user) references "User"(id)
);
Go

Create Table "Views"(
	id int identity(1,1) primary key not null,
	code varchar(10) unique not null,
	"route" varchar(300) not null,
	"name" varchar(50)not null default '',
);
Go

/*Create Table HierarchyView(
	id int identity(1,1) primary key not null,
	parent int not null,
	children int not null,
	foreign key(parent) references Views(id),
	foreign key(children) references Views(id),
);
Go*/

Create Table UserView(
	id int identity(1,1) primary key not null,
	id_user int not null,
	parent int not null,
	children int not null,
	foreign key(id_user) references "User"(id),
	foreign key(parent) references "Views"(id),
	foreign key(children) references "Views"(id)
);
Go

Create Table "Catalog"(
	id int identity(1,1) primary key not null,
	code varchar(50) not null,
	"name" varchar(50) not null,
	"description" varchar(300) not null default 'N/A',
	status bit not null default 0,
);
Go

Create Table EnterpriseHierarchyCatalog(
	id int identity(1,1) primary key not null,
	id_enterprise int not null,
	parent int not null,
	children int not null,
	foreign key(id_enterprise) references Enterprise(id),
	foreign key(parent) references "Catalog"(id),
	foreign key(children) references "Catalog"(id)
);
Go

Create Table "Permissions"(
	id int identity(1,1) primary key not null,
	permission varchar(50)not null unique
);
Go

Create Table Cards(
	id int identity(1,1) primary key not null,
	"card" varchar(40)not null unique
);
Go

Create Table UserCardGranted(
	id int identity(1,1) primary key not null,
	id_user int not null,
	id_card int not null,
	foreign key(id_user)references "User"(id),
	foreign key(id_card)references Cards(id)
);
Go

Create Table ViewCard(
	id int identity(1,1) primary key not null,
	id_view int not null,
	id_card int not null,
	foreign key(id_view)references "Views"(id),
	foreign key(id_card)references "Cards"(id)
);
Go

Create Table UserCardPermissions(
	id int identity(1,1) primary key not null,
	id_card_granted int not null,
	id_permission int not null,
	foreign key(id_card_granted)references UserCardGranted(id),
	foreign key(id_permission)references "Permissions"(id)
);
Go

insert into catalog(code,name,description,status) values
('catalogo','Catalogo','Catalogo Descripcion',1),
('comer','Comercios','Comercio Descripcion',1),
('masivo','Masivos','Masivos Descripcion',1),
('factura','Facturables','Facturables Descripcion',1),
('prin','Principal','Principal Descripcion',1),
('noRec','No Recurrente','noRecurrente Descripcion',1),
('Rec','Recurrente','Recurrente Descripcion',1),
('Ambient','Ambientes','Ambientes Descripcion',1),
('nuevo','Nuevo','Nuevo Descripcion',1);
Go

insert into Roles(rolename, "status") values ('Administrador', 1);
Go

insert into Country("name") values ('Ecuador');
Go

insert into Region(id_country, "name") values (1, 'Ecuador');
Go

insert into City(id_region, "name") values (1, 'Ecuador');
Go

INSERT INTO [dbo].[Enterprise]([id_city],[commercial_name],[telephone],[email],[enterprise_address],[identification],[has_branches],[latitude],[longitude],[status]) VALUES
(1,'Dummy','87654321','dummy@gmail.com','dummy Address','876532410D',0,1111,1111, 1);
GO

insert into "User"(google_access_token,facebook_access_token, firebase_token,id_role, id_enterprise, id_country,nickname,email,phone_number,hashPassword,status,profile_picture,is_account_activated) values 
('','','',1,1,1,'administrador','desarrollo.sistemas@qph.com.ec','87654321','10000.H2cZ26g32FkK/vrT25p0xA==.42AEKYOjYdoeskLAJAbaeov55uYEuy881ICeCM5E5Zw=',1,'N/A',1)
Go

INSERT INTO "Permissions"(permission) values ('crear'),('actualizar'),('eliminar'),('ordenar');
Go

insert into "Views"(code, "name", "route") values ('root','root', ''),('admin','Administracion','Administracion'),('usuario','Usuarios','Usuarios'),('menu','Vistas o Accesos del menu','Acceso del menu'),('rol','Roles','Roles'),('permiso','Permisos','Permisos'),
('catalog','Catalogo','Catalogo'),('param','Parametrizacion','Parametrizacion');
Go

insert into Cards("card") values ('crearUsuario'),('actualizarUsuario'),('eliminarUsuario'),
('crearVistas'),('actualizarVistas'),('eliminarVistas'),('ordenarVistas'),
('crearRoles'),('actualizarRoles'),('eliminarRoles'),
('crearPermisos'),('actualizarPermisos'),('eliminarPermisos'),
('crearCatalogos'),('actualizarCatalogos'),('eliminarCatalogos'),
('crearParametrizacion'),('actualizarParametrizacion'),('eliminarParametrizacion');
Go

insert into ViewCard(id_view, id_card) values (3,1),(3,2),(3,3),(4,4),(4,5),(4,6),(4,7),(5,8),(5,9),(5,10),
(6,11),(6,12),(6,13),(7,14),(7,15),(7,16),(8,17),(8,18),(8,19);
Go

Insert into UserCardGranted(id_user, id_card) values (1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10),(1,11),(1,12),(1,13),(1,14),(1,15),(1,16),(1,17),(1,18),(1,19);
Go

Insert into UserCardPermissions(id_card_granted, id_permission) values (1,1),(2,2),(3,3),(4,1),(5,2),(6,3),(7,4),(8,1),(9,2),(10,3),(11,1),(12,2),(13,3)
,(14,1),(15,2),(16,3),(17,1),(18,2),(19,3);
Go

insert into UserView(id_user, parent, children) values(1,1,2),(1,2,3),(1,2,4),(1,2,5),(1,2,6),(1,2,7),(1,2,8);
Go

insert into EnterpriseHierarchyCatalog(id_enterprise,parent,children)values(1,1,2),(1,2,3),(1,3,5),(1,2,4),(1,4,6),(1,4,7),(1,1,8),(1,8,9);
Go

Create or Alter Procedure PermissionStatus(@idUser int, @idView int)
As
Begin
Select p.id, p.permission, (case when exists(select cast(1 as bit) from UserCardPermissions ucp 
inner join UserCardGranted ucg on ucg.id = ucp.id_card_granted 
inner join ViewCard vc on vc.id_card = ucg.id_card
where ucp.id_permission = p.id and vc.id_view = @idView and ucg.id_user = @idUser)then 1 else 0 end) as status from "Permissions" p
End;
Go

Create or Alter Procedure BuildCardsByView(@idView int)
As
begin
	select c.id, c.card from Cards c
	inner join ViewCard vc on vc.id_card = c.id
	inner join "Views" v on v.id = vc.id_view
	where v.id = @idView
end;
Go

Create or Alter Procedure RemoveHierarchyViewByUser(@idUser int)
As
begin
With starting as (
		select t.children, t.parent
		from UserView as t
		where t.parent = 1 and t.id_user = @idUser
	),
	descendants as (
		select t.children as id, t.children, t.parent, children."name" as title, children.ruta
		from starting t join  "Views" as children on children.id = t.children
		union all 
		select t.children as id, t.children, t.parent, children."name" as title, children.ruta
		from UserView as t join descendants as d on t.parent = d.children join "Views" as children on children.id = t.children
	)
	delete from HierarchyView where children in( select children  from descendants where id in (select id_view from UserView where id_user = @idUser) group by id, children, parent, title);
End
Go


Create or Alter Procedure RemoveHierarchyViewByUserNew(@idUser int)
As
begin
	delete from UserView where id_user = @idUser;
End
Go

Create or Alter Procedure RemoveCatalogHierarchyByEnterpriseNew(@idEnterprise int)
As
begin
	delete from EnterpriseHierarchyCatalog where id_enterprise = @idEnterprise;
End
Go

/*
Create or Alter Procedure RemoveUserViewAndViewByUserId(@idUser int)
As
begin
	delete from "Views" where id in(select id_view from UserView where id_user = @idUser and id_view != 1);
End
Go
*/

/*Create or Alter Procedure HierarchyViewByUser(@idUser int)
As
begin

With starting as (
		select t.children, t.parent
		from HierarchyView as t
		where t.parent = 1
	),
	descendants as (
		select t.children as id, t.children, t.parent, children."name" as title
		from starting t join  "Views" as children on children.id = t.children
		union all 
		select t.children as id, t.children, t.parent, children."name" as title
		from HierarchyView as t join descendants as d on t.parent = d.children join "Views" as children on children.id = t.children
	),
	ancestors as (
		select t.children as id, t.children, t.parent, "Views"."name" as title
		from HierarchyView t
		join "Views" on "Views".id = t.children
		where t.children in ( select parent from starting )
		union all
		select t.children as id, t.children, t.parent, c."name" as title
		from HierarchyView as t join ancestors as a on t.children = a.parent
		join "Views" c on t.children = c.id
	)
	select id, children, parent, title  from descendants where id in (select id_view from UserView where id_user = @idUser)
	union all select * from ancestors where id in (select id_view from UserView where id_user = @idUser) group by id, children, parent, title;
end
Go
*/

Create or Alter Procedure HierarchyViewByUserNew(@idUser int)
As
begin

With starting as (
		select t.children, t.parent
		from UserView as t
		where t.parent = 1 and t.id_user = @idUser
	),
	descendants as (
		select t.children as id, t.children, t.parent, children."name" as title, children."route"
		from starting t join  "Views" as children on children.id = t.children
		union all 
		select t.children as id, t.children, t.parent, children."name" as title, children."route"
		from UserView as t join descendants as d on t.parent = d.children join "Views" as children on children.id = t.children
	),
	ancestors as (
		select t.children as id, t.children, t.parent, "Views"."name" as title, "Views"."route"
		from UserView t
		join "Views" on "Views".id = t.children
		where t.children in ( select parent from starting )
		union all
		select t.children as id, t.children, t.parent, c."name" as title, c."route"
		from UserView as t join ancestors as a on t.children = a.parent
		join "Views" c on t.children = c.id
	)
	select id, children, parent, title, "route"  from descendants group by id, children, parent, title, "route"
	union select * from ancestors group by id, children, parent, title, "route";
end
Go

Create or Alter Procedure HierarchyCatalogByEnterpriseNew(@idEnterprise int)
As
begin

With starting as (
		select t.children, t.parent
		from EnterpriseHierarchyCatalog as t
		where t.parent = 1 and t.id_enterprise = @idEnterprise
	),
	descendants as (
		select t.children as id, t.children, t.parent, children."name" as title
		from starting t join "Catalog" as children on children.id = t.children
		union all 
		select t.children as id, t.children, t.parent, children."name" as title
		from EnterpriseHierarchyCatalog as t join descendants as d on t.parent = d.children join "Catalog" as children on children.id = t.children
	),
	ancestors as (
		select t.children as id, t.children, t.parent, "Catalog"."name" as title
		from EnterpriseHierarchyCatalog t
		join "Catalog" on "Catalog".id = t.children
		where t.children in ( select parent from starting )
		union all
		select t.children as id, t.children, t.parent, c."name" as title
		from EnterpriseHierarchyCatalog as t join ancestors as a on t.children = a.parent
		join "Catalog" c on t.children = c.id
	)
	select id, children, parent, title  from descendants group by id, children, parent, title
	union select * from ancestors group by id, children, parent, title;
end
Go