USE HomeServer
GO

IF (OBJECT_ID('DeviceReadings') IS NOT NULL)
  DROP TABLE [HomeServer].[dbo].[DeviceReadings]
GO

IF (OBJECT_ID('Devices') IS NOT NULL)
  DROP TABLE [HomeServer].[dbo].[Devices]
GO

CREATE TABLE [HomeServer].[dbo].[Devices]
(
	DeviceID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(),
	Name NVARCHAR(100) NOT NULL,
	TypeOfReading VARCHAR(20) NOT NULL,
	IsActive BIT NOT NULL,
	Host VARCHAR(100) NOT NULL,
	CustomId VARCHAR (100) NULL,
)
GO


CREATE TABLE [HomeServer].[dbo].[DeviceReadings]
(
	ReadingTimeStamp DATETIME NOT NULL,
	Value DECIMAL(20,2) NOT NULL,
	DeviceID UNIQUEIDENTIFIER NOT NULL,
	TypeOfReading VARCHAR(20) NOT NULL,
	PRIMARY KEY(ReadingTimeStamp, DeviceID),
	FOREIGN KEY (DeviceID) REFERENCES Devices (DeviceID)
)
GO