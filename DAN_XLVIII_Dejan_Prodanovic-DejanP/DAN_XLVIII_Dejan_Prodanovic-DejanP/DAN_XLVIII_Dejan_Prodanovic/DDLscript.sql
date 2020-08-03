--we create database  
CREATE DATABASE PizzeriaDatB;
GO

use PizzeriaDatB;

GO

 --we delete tables in case they exist
DROP TABLE IF EXISTS tblPizza;
DROP TABLE IF EXISTS tblOrder;
DROP TABLE IF EXISTS tblPizzaOrder;


--we create table tblPizza
 CREATE TABLE tblPizza (
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    PizzaType varchar(50),
	Price decimal,
	 
);

 --we create table tblOrder
 
 CREATE TABLE tblOrder (
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    DateAndTimeOfOrder datetime,
	JMBG varchar(50),
    OrderStatus varchar(50),
	TotalPrice decimal   

);

--we create table tblPizzaOrder
 
 CREATE TABLE tblPizzaOrder (
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Amount int,
    OrderID int FOREIGN KEY REFERENCES tblOrder(ID) ON DELETE CASCADE,
	PizzaID int FOREIGN KEY REFERENCES tblPizza(ID) ON DELETE CASCADE

);

 
INSERT INTO tblPizza values('Pepperoni Pizza',10);
INSERT INTO tblPizza values('Meat Pizza',15);
INSERT INTO tblPizza values('Pizza Margherita',10);
INSERT INTO tblPizza values('Hawaiian Pizza',20);

INSERT INTO tblPizza values('Buffalo  Pizza',22);
INSERT INTO tblPizza values('Pizza Marinara',17);
INSERT INTO tblPizza values('Pizza Capricciosa',22);
INSERT INTO tblPizza values('Pizza Mexicana',19);
INSERT INTO tblPizza values('Pizza Napolitana',25);

 
 