﻿CREATE DATABASE TodoList;

GO

USE TodoList;

CREATE TABLE Customers (
	Id int IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(60),
	LastName VARCHAR(60),
	Birthday DATE
);

CREATE TABLE Contacts (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Type VARCHAR(10),
	Value VARCHAR(100)
);

CREATE TABLE Tasks (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Description NVARCHAR(1000),
	CreationDate DATETIME,
	Solved BIT
);