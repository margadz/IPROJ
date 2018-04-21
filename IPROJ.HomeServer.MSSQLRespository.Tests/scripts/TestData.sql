
INSERT INTO [HomeServer].[dbo].[Devices] (Name, TypeOfReading, IsActive, Host, TypeOfDevice) VALUES ('Temperatura w pokoju', 3 ,1, 'Host', 'someType')
INSERT INTO [HomeServer].[dbo].[Devices] (Name, TypeOfReading, IsActive, Host, TypeOfDevice) VALUES ('Temperatura w kuchni', 3 ,1, 'Host', 'someType')
INSERT INTO [HomeServer].[dbo].[Devices] (Name, TypeOfReading, IsActive, Host, TypeOfDevice) VALUES ('Temperatura w sypialni', 3,1, 'Host', 'someType')
INSERT INTO [HomeServer].[dbo].[Devices] (Name, TypeOfReading, IsActive, Host, TypeOfDevice) VALUES ('Temperatura w korytarzu', 3, 0, 'Host', 'someType')

INSERT INTO [HomeServer].[dbo].[Devices] (DeviceId, Name, TypeOfReading, IsActive, Host, TypeOfDevice) VALUES ('D28B2B0C-831A-4027-9B6D-3894F5A7EB69', 'Energia elektryczna w pokoju', 1 ,1, 'Host', 'someOtherType')
INSERT INTO [HomeServer].[dbo].[Devices] (Name, TypeOfReading, IsActive, Host, TypeOfDevice) VALUES ('Energia elektryczna w kuchni', 1 , 1, 'Host', 'someOtherType')
INSERT INTO [HomeServer].[dbo].[Devices] (Name, TypeOfReading, IsActive, Host, TypeOfDevice) VALUES ('Energia elektryczna w sypialni', 1, 1, 'Host', 'someOtherType')


INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -1, DATEADD(DAY, -1, GETDATE())), 22.1, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w pokoju'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -2, DATEADD(DAY, -2, GETDATE())), 22.8, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w pokoju'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -3, DATEADD(DAY, -3, GETDATE())), 23.1, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w pokoju'), 3, 0)

INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -1, DATEADD(DAY, -1, GETDATE())), 21.1, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w kuchni'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -2, DATEADD(DAY, -2, GETDATE())), 23.8, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w kuchni'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -3, DATEADD(DAY, -3, GETDATE())), 22.1, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w kuchni'), 3, 0)

INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -1, DATEADD(DAY, -1, GETDATE())), 22.2, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w sypialni'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -2, DATEADD(DAY, -2, GETDATE())), 22.4, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w sypialni'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -3, DATEADD(DAY, -3, GETDATE())), 24.2, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w sypialni'), 3, 0)

INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -1, DATEADD(DAY, -1, GETDATE())), 22.1, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w korytarzu'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -2, DATEADD(DAY, -2, GETDATE())), 22.8, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w korytarzu'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -3, DATEADD(DAY, -3, GETDATE())), 23.1, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w korytarzu'), 3, 0)

INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -3, DATEADD(DAY, -4, GETDATE())), 25.1, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w korytarzu'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -3, DATEADD(DAY, -5, GETDATE())), 22.3, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w korytarzu'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -3, DATEADD(DAY, -6, GETDATE())), 27.8, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w korytarzu'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -3, DATEADD(DAY, -7, GETDATE())), 20.0, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w korytarzu'), 3, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(SECOND, -3, DATEADD(DAY, -8, GETDATE())), 19.1, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Temperatura w korytarzu'), 3, 0)

INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(DAY, -3, DATEFROMPARTS(2017, 2, 11)), 120.2, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Energia elektryczna w pokoju'), 1, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(DAY, -2, DATEFROMPARTS(2017, 2, 12)), 103.4, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Energia elektryczna w pokoju'), 1, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(DAY, -1, DATEFROMPARTS(2017, 2, 13)), 150.2, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Energia elektryczna w pokoju'), 1, 0)
INSERT INTO [HomeServer].[dbo].[DeviceReadings] (ReadingTimeStamp, Value, DeviceId, TypeOfReading, ReadingCharacter) VALUES (DATEADD(DAY, 0, DATEFROMPARTS(2017, 2, 14)), 120.2, (SELECT TOP(1)DeviceId FROM [HomeServer].[dbo].[Devices] WHERE Name = 'Energia elektryczna w pokoju'), 1, 0)

