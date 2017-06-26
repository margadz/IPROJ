use HomeServer

----------------------------------------------
--DEVICES
IF (OBJECT_ID('Devices') IS NOT NULL)
  DROP TABLE Devices
GO
CREATE TABLE Devices
(
	DeviceID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(),
	Name NVARCHAR(100) NOT NULL,
	TypeOfReading VARCHAR(20) NOT NULL,
	ReadingInterval INT DEFAULT 0,
	IsActive BIT NOT NULL
)
GO

----------------------------------------------
--InstrumentReadings
IF (OBJECT_ID('InstrumentReadings') IS NOT NULL)
  DROP TABLE InstrumentReadings
GO
CREATE TABLE InstrumentReadings
(
	ReadingTimeStamp DATETIME NOT NULL,
	Value DECIMAL(20,2) NOT NULL,
	DeviceID UNIQUEIDENTIFIER NOT NULL,
	TypeOfReading VARCHAR(20) NOT NULL,
	PRIMARY KEY(ReadingTimeStamp, DeviceID),
	FOREIGN KEY (DeviceID) REFERENCES Devices (DeviceID)
)
GO


----------------------------------------------
--NetConfig
IF (OBJECT_ID('NetConfig') IS NOT NULL)
  DROP TABLE NetConfig
GO
--CREATE TABLE NetConfig
--(
--	ConfigID INT NOT NULL PRIMARY KEY IDENTITY,
--	Name NVARCHAR(50) NOT NULL,
--	IsActive BIT NOT NULL,
--	Created DATETIME NOT NULL DEFAULT GETDATE()
--)
--GO

--IF (OBJECT_ID('SingleActiveCheck') IS NOT NULL)
--  DROP FUNCTION SingleActiveCheck
--GO
--CREATE FUNCTION SingleActiveCheck()
--RETURNS INT
--AS
--BEGIN 
--	DECLARE @result int
--	SELECT @result = COUNT(*) FROM NetConfig
--					WHERE IsActive = 1
--	RETURN @result
--END
--GO

--ALTER TABLE NetConfig
--ADD CONSTRAINT SingleCheck CHECK (dbo.SingleActiveCheck() <= 1)
--GO


----------------------------------------------
--DEVICES
IF (OBJECT_ID('Devices') IS NOT NULL)
  DROP TABLE Devices
GO
CREATE TABLE Devices
(
	DeviceID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(),
	Name NVARCHAR(100) NOT NULL,
	TypeOfReading VARCHAR(20) NOT NULL,
)

----------------------------------------------
--DeviceConfigs
IF (OBJECT_ID('DeviceConfigs') IS NOT NULL)
  DROP TABLE DeviceConfigs
GO
--CREATE TABLE DeviceConfigs
--(
--	ConfigID INT NOT NULL,
--	DeviceID UNIQUEIDENTIFIER NOT NULL,
--	PRIMARY KEY (ConfigID, DeviceID),
--	FOREIGN KEY (ConfigID) REFERENCES NetConfig (ConfigID),
--	FOREIGN KEY (DeviceID) REFERENCES Devices (DeviceID)
--)
--GO


----------------------------------------------
--InstrumentReadings
IF (OBJECT_ID('InstrumentReadings') IS NOT NULL)
  DROP TABLE InstrumentReadings
GO
CREATE TABLE InstrumentReadings
(
	ReadingTimeStamp DATETIME NOT NULL,
	Value DECIMAL(20,2) NOT NULL,
	DeviceID UNIQUEIDENTIFIER NOT NULL,
	PRIMARY KEY(ReadingTimeStamp, DeviceID),
	FOREIGN KEY (DeviceID) REFERENCES Devices (DeviceID)
)
GO

