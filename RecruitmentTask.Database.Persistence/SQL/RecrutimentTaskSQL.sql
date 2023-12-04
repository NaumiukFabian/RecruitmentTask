drop view if exists VProductInfo;
drop table if exists Products;
drop table if exists Prices;
drop table if exists Inventory;


create table Products
(
	Id serial primary key,
	Name text,
	SKU text unique,
	EAN text,
	Productername text,
	Category text,
	Shipping text,
	DefaultImage text
);

create table Inventory
(
	Id serial primary key,
	SKU text unique,
	Quantity numeric,
	Unit text,
	Shippincost numeric,
	Shipping text
);

create table Prices
(
	Id text primary key,
	SKU text unique,
	Nettproductpice numeric
);

create or replace view VProductInfo as
select Products.SKU, Products.Name, Products.EAN, Products.Productername,
Products.DefaultImage, Inv.Quantity, Inv.Unit,
Inv.Shipping, Pric.Nettproductpice from Products
left join Inventory Inv on Inv.SKU = Products.SKU
left join Prices Pric on Pric.SKU = Products.SKU;
