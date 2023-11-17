CREATE SCHEMA [TruckData];
GO


CREATE TABLE [TruckData].[Trucks]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
	[Code] NVARCHAR(100) NOT NULL, 	
	[Name] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(100),
	[Status] INT NOT NULL,

	CONSTRAINT [PK_TruckData_Trucks] PRIMARY KEY ([Id]),
	CONSTRAINT UC_TruckData_Trucks_Code UNIQUE (Code)
)