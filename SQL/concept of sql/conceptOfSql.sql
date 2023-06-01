--SQL CREATE DATABASE Statement
create database himanshuDB;

--SQL DROP DATABASE Statement
drop database himanshuDB;

--SQL BACKUP DATABASE for SQL Server
backup database w3schools to disk = 'C:\SQL\w3schools.bak';
--changes in sql database
--update customers set CustomerName = 'HALadva' where CustomerID = 1;
--backup with difference
backup database w3schools to disk = 'C:\SQL\w3schools.bak' with DIFFERENTIAL;

--SQL CREATE TABLE Statement
create table biodata (
   PersonId int not null,
   firstName varchar(255) not null,
   Address varchar(300) not null,
   City varchar(200) not null,
   CityCode varchar(100) not null,
);
--create table from another table
select * into testDB from biodata where 1 = 0;

--drop table 
drop table testDB;

--truncate table 
truncate table testDB;

--alter table 
alter table biodata add lastName varchar(255);
alter table biodata drop column  lastName;
alter table biodata alter column CityCode varchar(10);

--not null
alter table biodata alter column City varchar(200) null;
alter table biodata alter column City varchar(200) not null;

--unique
create table houseFind (
   HouseId int not null,
   Hloca varchar(200) not null,
   ownerNo varchar(25) not null,
   Price varchar(10) null,
   constraint H_House unique (HouseId, ownerNo)
);
--SQL UNIQUE Constraint on ALTER TABLE
alter table biodata add unique(PersonId);
alter table biodata add constraint B_Biodata unique (PersonId, firstName);
--DROP a UNIQUE Constraint
alter table	biodata drop constraint B_Biodata;

--primary key 
alter table houseFind add constraint PK_HouseFind primary key (HouseId, Hloca);
alter table biodata add constraint PK_biodata primary key (PersonId);
--drop a primary key
alter table houseFind drop constraint PK_HouseFind;
alter table biodata drop constraint PK_biodata;
	
--foreign key
create table orders (
   orderId int not null,
   orderNumber int not null,
   PersonId int foreign key references biodata(PersonId)
);
--with alter table
alter table houseFind add PersonId int;
alter table houseFind add foreign key (PersonId) references biodata(PersonId);
--drop fk 
ALTER TABLE houseFind drop CONSTRAINT FK__houseFind__Perso__66603565;

--index 
create index idx_fname on biodata (firstName);
drop index biodata.idx_fname;

--SQL AUTO INCREMENT Field
create table Persons (
   PersonId int identity(1,1) primary key,
   lastName varchar(200) not null,
   firstName varchar(200) not null,
   age int
);
insert into Persons (firstName, lastName) values ('Himanshu', 'Ladva');
insert into Persons (firstName, lastName) values ('Himanshu', 'Ladva');
insert into Persons (firstName, lastName) values ('Himanshu', 'Ladva');
insert into Persons (firstName, lastName) values ('Himanshu', 'Ladva');
select * from Persons;

--sql view
create view my_view as select firstName, lastName from Persons;
select * from my_view;
--SQL CREATE OR REPLACE VIEW Syntax
alter view my_view as select firstName, lastName, age from Persons;
--SQL drop view 
drop view my_view;

