drop table if exists Inventory;
drop table if exists Prices;
drop table if exists Products;

create table Inventory
(
	Id serial primary key,
	SKU text unique,
	Quantity numeric,
	Unit text,
	Shippincost numeric
	
);

create table Products
(
	Id serial primary key,
	SKU text references Inventory(SKU) unique,
	EAN text,
	Productname text,
	Category text,
	Shipping text,
	DefaultImage text
);

create table Prices
(
	Id serial primary key,
	SKU text references Inventory(SKU) unique,
	Nettproductpice numeric,
);