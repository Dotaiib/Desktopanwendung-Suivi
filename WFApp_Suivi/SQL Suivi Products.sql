create database Products
use Products
Go

Create Table Sites(
ID int primary key identity NOT NULL,
Code_Site int NOT NULL,
Désignation_Site varchar(max) NOT NULL,
Date_Time DateTime default getdate() NOT NULL,
);

create Table Departements(
Code_Departement int primary key identity(301,1) NOT NULL,
Désignation_Departement varchar(max) NOT NULL,
Date_Time DateTime default getdate() NOT NULL,
);

create Table Materiel(
Code_Materiel int primary key identity(501,1) NOT NULL,
Désignation_Materiel varchar(max) NOT NULL,
Fabricant nvarchar(max),
Autre nvarchar(max),
Date_Time DateTime default getdate() NOT NULL
);

create Table Receveur(
Code_Receveur int primary key identity(701,1) NOT NULL,
FullName varchar(max) NOT NULL,
Date_Time DateTime default getdate() NOT NULL,
);

create Table Produits(
ID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
Site_Produit varchar(max) NOT NULL,
Departement_Produit varchar(max) NOT NULL,
Materiel_Produit varchar(max) NOT NULL,
Destinatin_Produit varchar(max) NOT NULL,
Code_Bar_Produit varchar(max) NOT NULL,
Date_Entree_Produit varchar(max) NOT NULL,
Date_Sortie_Produit varchar(max),
Details nvarchar(max) Not Null,
Date_Time DateTime default getdate() NOT NULL,
);

create Table Produits_Sortie(
ID_Old INT NOT NULL PRIMARY KEY,
Site_Produit varchar(max) NOT NULL,
Departement_Produit varchar(max) NOT NULL,
Materiel_Produit varchar(max) NOT NULL,
Destinatin_Produit varchar(max) NOT NULL,
Code_Bar_Produit varchar(max) NOT NULL,
Date_Entree_Produit varchar(max) NOT NULL,
Date_Sortie_Produit varchar(max) Not Null,
Details nvarchar(max) Not Null,
Date_Time DateTime default getdate() NOT NULL,
);

/*Trigger to clears outgoign Products*/
create trigger refresh_database on Produits_Sortie
after insert
as delete from Produits where ID IN (select ID from Produits, Produits_Sortie where Produits_Sortie.ID_Old=Produits.ID)

select Site_Produit,Departement_Produit,Materiel_Produit,FullName,Code_Bar_Produit,Date_Entree_Produit,Details from Produits, Receveur where Produits.Destinatin_Produit= Receveur.Code_Receveur order by Produits.Date_Time desc



insert into Produits_Sortie select ID, Site_Produit, Departement_Produit, Materiel_Produit, Destinatin_Produit, Code_Bar_Produit, Date_Entree_Produit, Date_Sortie_Produit='15/12/2020', Details, getdate() from Produits where Code_Bar_Produit = '** 102X02X003X0004 **'

select * from Produits_Sortie

insert into Produits values ('201','00056','ML5','Marouane','** CIBEL: 101--00056--ML5 **','24','29',getdate())

select top 5 * from Produits order by Date_Time desc

select Code_Site, Désignation from Sites

SELECT Site_Produit,Désignation_Departement,Désignation_Materiel,FullName,Code_Bar_Produit,Date_Entree_Produit from Produits,Departements,Materiel,Receveur 

where Code_Departement=Departement_Produit or Code_Materiel=Materiel_Produit or Code_Receveur=Destinatin_Produit

select Code_Site from sites


update Produits set Details = Details + ' # bnh ** ', Date_Time= GETDATE() where Code_Bar_Produit='** 102X02X003X0004 **'