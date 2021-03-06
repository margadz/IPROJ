﻿USE HomeServer
GO

IF (OBJECT_ID('DeviceReadings') IS NOT NULL)
  DROP TABLE [HomeServer].[dbo].[DeviceReadings]
GO

IF (OBJECT_ID('Devices') IS NOT NULL)
  DROP TABLE [HomeServer].[dbo].[Devices]
GO

CREATE TABLE [HomeServer].[dbo].[Devices]
(
	DeviceId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(),
	Name NVARCHAR(100) NOT NULL,
	TypeOfReading INT NOT NULL,
	TypeOfDevice INT NOT NULL,
	IsActive BIT NOT NULL,
	Host VARCHAR(100) NOT NULL,
	CustomId VARCHAR (100) NULL,
)
GO

IF (OBJECT_ID('Configuration') IS NOT NULL)
  DROP TABLE Configuration
GO

CREATE TABLE Configuration
(ConfigId INTEGER PRIMARY KEY IDENTITY(1, 1),
 ConfigName VARCHAR(20) NOT NULL,
 ConfigValue VARCHAR(20) NOT NULL,
 ConfigCategory VARCHAR(20) NOT NULL
 )
GO


CREATE TABLE [HomeServer].[dbo].[DeviceReadings]
(
	Id INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	ReadingTimeStamp DATETIME NOT NULL,
	Value DECIMAL(20,2) NOT NULL,
	DeviceID UNIQUEIDENTIFIER NULL,
	TypeOfReading INT NOT NULL,
	ReadingCharacter INT NOT NULL,
	FOREIGN KEY (DeviceId) REFERENCES Devices (DeviceId)
)
GO

--INSERT INTO Devices (Name, TypeOfReading, TypeOfDevice, IsActive, Host) VALUES ('Komputer w salonie', 1, 1, 1, '192.168.1.202:9999');
--INSERT INTO Devices (Name, TypeOfReading, TypeOfDevice, IsActive, Host) VALUES ('Zestaw audio w salonie', 1, 2, 1, '192.168.1.227:49153');