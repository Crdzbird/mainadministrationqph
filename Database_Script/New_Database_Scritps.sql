Use master;
Go

Drop Database RRHH_MAIN;
Go

Create Database RRHH_MAIN;
Go

Use RRHH_MAIN;
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

--This table is used to store any numbers of contacts related to any enterprise
--NOTE: This table isn't related to the user table. however it can be linked via the email.
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
	code varchar(10) not null,
	"name" varchar(50)not null default '',
);
Go

Create Table HierarchyView(
	id int identity(1,1) primary key not null,
	parent int not null,
	children int not null,
	foreign key(parent) references Views(id),
	foreign key(children) references Views(id),
);
Go

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

Create Table "Permissions"(
	id int identity(1,1) primary key not null,
	permission varchar(50)not null unique
);

insert into Cards("card") values ('A5'),('B25'),('C52'),('D52'),('E52'),('G52'),('F52'),('H52'),('I52');

select * from "Views";

select * from "Cards";


insert into ViewCard(id_view, id_card) values (2,1),(2,2),(3,3),(4,4),(5,5),(5,6),(6,7),(7,8),(8,9),(9,10),
(10,11),(10,12),(11,13),(12,14),(12,15),(11,16),(7,17),(9,19),(3,18),(2,20);


INSERT INTO "Permissions"(permission) values ('reupload'),('read'),('update'),('download'),('delete'),('filter');

Insert into UserCardGranted(id_user, id_card) values (1,1),(1,10),(1,19),(1,2),(1,11),(1,20),(1,3),(1,12),(1,21);

Insert into UserCardPermissions(id_card_granted, id_permission) values (1,1),(1,2),(2,3),(2,4),(3,5),(4,6),(4,2),(4,4),(5,3),(6,1),(7,6),(8,2);



create or replace function win_cardsbyviewtrees_with_title(parm1 integer) returns TABLE(id_view integer, id_card integer, card text, title text)
	language sql
as $$
Select v.id_view_principal,c.id_card, c.card, c.title from win_cards c
  Inner Join win_view_card vco on vco.id_card = c.id_card
  Inner Join win_views_principal v on v.id_view_principal = vco.id_view
  WHERE v.id_view_principal = $1

$$;

Create Table Cards(
	id int identity(1,1) primary key not null,
	"card" varchar(40)not null unique
);

Create Table UserCardGranted(
	id int identity(1,1) primary key not null,
	id_user int not null,
	id_card int not null,
	foreign key(id_user)references "User"(id),
	foreign key(id_card)references Cards(id)
);

Create Table ViewCard(
	id int identity(1,1) primary key not null,
	id_view int not null,
	id_card int not null,
	foreign key(id_view)references "Views"(id),
	foreign key(id_card)references "Cards"(id)
);

insert UserCardPermissions(id_card_granted, id_permission) values (10, 1),(10, 5),(10, 3);

//TODO HACER EL ENDPOINT USERCARDPERMISSION.

Create Table UserCardPermissions(
	id int identity(1,1) primary key not null,
	id_card_granted int not null,
	id_permission int not null,
	foreign key(id_card_granted)references UserCardGranted(id_card_granted),
	foreign key(id_permission)references "Permissions"(id)
);

Create Table "Catalog"(
	id int identity(1,1) primary key not null,
	code varchar(50) not null,
	"name" varchar(50) not null,
	"description" varchar(300) not null default 'N/A',
	status bit not null default 0,
);
Go

Create Table HierarchyCatalog(
	id int identity(1,1) primary key not null,
	parent int not null,
	children int not null,
	foreign key(parent) references "Catalog"(id),
	foreign key(children) references "Catalog"(id)
);
Go

Create Table EnterpriseCatalog(
	id int identity(1,1) primary key not null,
	id_enterprise int not null,
	id_catalog int not null,
	foreign key(id_enterprise) references Enterprise(id),
	foreign key(id_catalog) references "Catalog"(id)
);

Go
select * from "User" where activation_code = '*My9uTtSoVSN RDT.!zF8LR08XocYd8rpEmp';
--HIERARCHICAL QUERY

exec HierarchyViewByParentId @parentId=14 order by children);
Go


select * from "User"


select * from "User";

insert into "Views"(code, "name") values ('root','root'),('padreA','padreA'),('padreB','padreB'),('hijoA','hijoA'),('hijoB','hijoB'),('hijoC','hijoC'),
('nietoA','nietoA'),('nietoB','nietoB'),('nietoC','nietoC'),('nietoD','nietoD'),('subNietoA','subNietoA'),('subNietoB','subNietoB');
Go

select * from UserView

select * from "Views"

insert into HierarchyView(parent, children) values (1,2),(1,3),(2,4),(2,5),(4,7),(3,6),(4,8),(5,9),(5,10),(10,11),(9,12);
Go

delete from UserView

insert into UserView(id_user, parent, children) values(1,1,2),(1,1,3),(1,2,4),(1,2,5),(1,4,7),(1,3,6),(1,4,8),(1,5,9),(1,5,10),(1,10,11),(1,9,12);
Go

exec HierarchyViewByUser @idUser = 1 order by (select "orderBy" from UserView where id_user = 1);
Go

exec PermissionStatus @idUser = 1, @idView =3;



Create or Alter Procedure PermissionStatus(@idUser int, @idView int)
As
Begin
Select p.permission, (case when exists(select cast(1 as bit) from UserCardPermissions ucp 
inner join UserCardGranted ucg on ucg.id = ucp.id_card_granted 
inner join ViewCard vc on vc.id_card = ucg.id_card
where ucp.id_permission = p.id and vc.id_view = @idView and ucg.id_user = @idUser)then 1 else 0 end) as status from "Permissions" p
End;


Create or Alter Procedure BuildCardsByView(@idView int)
As
begin
	select c.id, c.card from Cards c
	inner join ViewCard vc on vc.id_card = c.id
	inner join "Views" v on v.id = vc.id_view
	where v.id = @idView
end;

Create or Alter Procedure RemoveHierarchyViewByUser(@idUser int)
As
begin
With starting as (
		select t.children, t.parent
		from UserView as t
		where t.parent = 1 and t.id_user = @idUser
	),
	descendants as (
		select t.children as id, t.children, t.parent, children."name" as title
		from starting t join  "Views" as children on children.id = t.children
		union all 
		select t.children as id, t.children, t.parent, children."name" as title
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

/*
Create or Alter Procedure RemoveUserViewAndViewByUserId(@idUser int)
As
begin
	delete from "Views" where id in(select id_view from UserView where id_user = @idUser and id_view != 1);
End
Go
*/
select * from UserView

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


exec HierarchyViewByUserNew @idUser = 1;

Create or Alter Procedure HierarchyViewByUserNew(@idUser int)
As
begin

With starting as (
		select t.children, t.parent
		from UserView as t
		where t.parent = 1 and t.id_user = @idUser
	),
	descendants as (
		select t.children as id, t.children, t.parent, children."name" as title
		from starting t join  "Views" as children on children.id = t.children
		union all 
		select t.children as id, t.children, t.parent, children."name" as title
		from UserView as t join descendants as d on t.parent = d.children join "Views" as children on children.id = t.children
	),
	ancestors as (
		select t.children as id, t.children, t.parent, "Views"."name" as title
		from UserView t
		join "Views" on "Views".id = t.children
		where t.children in ( select parent from starting )
		union all
		select t.children as id, t.children, t.parent, c."name" as title
		from UserView as t join ancestors as a on t.children = a.parent
		join "Views" c on t.children = c.id
	)
	select id, children, parent, title  from descendants
	union all select * from ancestors group by id, children, parent, title;
end
Go