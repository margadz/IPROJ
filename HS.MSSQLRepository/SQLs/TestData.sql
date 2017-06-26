use HomeServer

DELETE FROM dbo.InstrumentReadings
GO
DELETE FROM dbo.Devices
GO


INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Temperatura w pokoju', 'Temperature' ,1 )
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Temperatura w kuchni', 'Temperature' ,1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Temperatura w sypialni', 'Temperature',1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Temperatura w korytarzu', 'Temperature', 0)

INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Energia elektryczna w pokoju', 'PowerConsumtion' ,1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Energia elektryczna w kuchni', 'PowerConsumtion' ,1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Energia elektryczna w sypialni', 'PowerConsumtion',1)

INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Œwiat³o w pokoju', 'Light' ,1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Œwiat³o w kuchni', 'Light' ,1)
INSERT INTO dbo.Devices (Name, TypeOfReading, IsActive) VALUES ('Œwiat³o w sypialni', 'Light',1)


INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 22.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w pokoju') )
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 22.8, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w pokoju') )
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 23.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w pokoju') )

INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 21.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w kuchni') )
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 23.8, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w kuchni') )
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 22.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w kuchni') )

INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 22.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w sypialni') )
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 22.4, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w sypialni'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 24.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w sypialni'))


INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 40.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Œwiat³o w pokoju'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 41.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Œwiat³o w pokoju'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 43.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Œwiat³o w pokoju'))

INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 50.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Œwiat³o w kuchni'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 51.7, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Œwiat³o w kuchni'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 55.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Œwiat³o w kuchni'))

INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 41.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Œwiat³o w sypialni'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 43.4, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Œwiat³o w sypialni'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 34.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Œwiat³o w sypialni'))

INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -1, GETDATE()), 22.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w korytarzu'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -2, GETDATE()), 22.8, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w korytarzu'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(SECOND, -3, GETDATE()), 23.1, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Temperatura w korytarzu'))

INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(DAY, -3, DATEFROMPARTS(2017, 2, 15)), 120.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Energia elektryczna w pokoju'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(DAY, -2, DATEFROMPARTS(2017, 2, 15)), 103.4, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Energia elektryczna w pokoju'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(DAY, -1, DATEFROMPARTS(2017, 2, 15)), 150.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Energia elektryczna w pokoju'))
INSERT INTO dbo.InstrumentReadings (ReadingTimeStamp, Value, DeviceID, TypeOfReading) VALUES (DATEADD(DAY, 0, DATEFROMPARTS(2017, 2, 15)), 120.2, (SELECT TOP(1)DeviceId FROM dbo.Devices WHERE Name = 'Energia elektryczna w pokoju'))

