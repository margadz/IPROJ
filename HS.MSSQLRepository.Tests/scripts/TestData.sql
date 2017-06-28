USE HomeServer
GO

IF (OBJECT_ID('DeviceReadings') IS NOT NULL)
  DROP TABLE DeviceReadings
GO

IF (OBJECT_ID('Devices') IS NOT NULL)
  DROP TABLE Devices
GO

CREATE TABLE Devices
(
	DeviceID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(),
	Name NVARCHAR(100) NOT NULL,
	TypeOfReading VARCHAR(20) NOT NULL,
	IsActive BIT NOT NULL
)
GO


CREATE TABLE DeviceReadings
(
	ReadingTimeStamp DATETIME NOT NULL,
	Value DECIMAL(20,2) NOT NULL,
	DeviceID UNIQUEIDENTIFIER NOT NULL,
	TypeOfReading VARCHAR(20) NOT NULL,
	PRIMARY KEY(ReadingTimeStamp, DeviceID),
	FOREIGN KEY (DeviceID) REFERENCES Devices (DeviceID)
)
GO




INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Temperatura w pokoju', 'Temperature' ,1 )
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Temperatura w kuchni', 'Temperature' ,1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Temperatura w sypialni', 'Temperature',1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Temperatura w korytarzu', 'Temperature', 0)

INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Energia elektryczna w pokoju', 'PowerConsumtion' ,1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Energia elektryczna w kuchni', 'PowerConsumtion' ,1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Energia elektryczna w sypialni', 'PowerConsumtion',1)


INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 22.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w pokoju'), 'Temperature')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 22.8, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w pokoju'), 'Temperature')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 23.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w pokoju'), 'Temperature')

INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 21.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w kuchni'), 'Temperature')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 23.8, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w kuchni'), 'Temperature')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 22.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w kuchni'), 'Temperature')

INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 22.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w sypialni'), 'Temperature')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 22.4, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w sypialni'), 'Temperature')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 24.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w sypialni'), 'Temperature')

INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 22.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w korytarzu'), 'Temperature')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 22.8, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w korytarzu'), 'Temperature')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 23.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w korytarzu'), 'Temperature')

INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(DAY, -3, DATEFROMPARTS(2017, 2, 15)), 120.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Energia elektryczna w pokoju'), 'PowerConsumtion')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(DAY, -2, DATEFROMPARTS(2017, 2, 15)), 103.4, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Energia elektryczna w pokoju'), 'PowerConsumtion')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(DAY, -1, DATEFROMPARTS(2017, 2, 15)), 150.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Energia elektryczna w pokoju'), 'PowerConsumtion')
INSERT INTO dbo.DeviceReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(DAY, 0, DATEFROMPARTS(2017, 2, 15)), 120.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Energia elektryczna w pokoju'), 'PowerConsumtion')

