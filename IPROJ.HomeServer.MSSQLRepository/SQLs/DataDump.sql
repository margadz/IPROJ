USE [HomeServer]
GO
INSERT [dbo].[Devices] ([DeviceId], [Name], [TypeOfReading], [TypeOfDevice], [IsActive], [Host], [CustomId]) VALUES (N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', N'Zestaw audio w salonie', 1, 2, 1, N'192.168.1.227:49153', NULL)
GO
INSERT [dbo].[Devices] ([DeviceId], [Name], [TypeOfReading], [TypeOfDevice], [IsActive], [Host], [CustomId]) VALUES (N'b07a7606-9835-4ef0-a052-6f6e3d14d496', N'Komputer w salonie', 1, 1, 1, N'192.168.1.202:9999', NULL)
GO
SET IDENTITY_INSERT [dbo].[DeviceReadings] ON 

GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (1, CAST(N'2018-04-21 19:14:37.063' AS DateTime), CAST(0.38 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (2, CAST(N'2018-04-21 19:14:37.063' AS DateTime), CAST(1.67 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (19, CAST(N'2018-04-22 19:20:56.757' AS DateTime), CAST(0.84 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (20, CAST(N'2018-04-22 19:20:56.757' AS DateTime), CAST(2.47 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (21, CAST(N'2018-04-23 19:55:04.297' AS DateTime), CAST(0.43 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (22, CAST(N'2018-04-23 19:55:04.297' AS DateTime), CAST(0.64 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (23, CAST(N'2018-04-24 19:22:48.887' AS DateTime), CAST(0.46 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (24, CAST(N'2018-04-24 19:22:48.890' AS DateTime), CAST(0.86 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (25, CAST(N'2018-04-25 20:13:29.363' AS DateTime), CAST(0.05 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (26, CAST(N'2018-04-25 20:13:29.363' AS DateTime), CAST(0.88 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (29, CAST(N'2018-04-16 20:46:33.633' AS DateTime), CAST(0.64 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (30, CAST(N'2018-04-20 20:46:33.253' AS DateTime), CAST(1.10 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (31, CAST(N'2018-04-18 20:46:33.580' AS DateTime), CAST(0.67 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (32, CAST(N'2018-04-15 20:46:33.660' AS DateTime), CAST(1.27 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (33, CAST(N'2018-04-19 20:46:33.550' AS DateTime), CAST(0.66 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (34, CAST(N'2018-04-17 20:46:33.610' AS DateTime), CAST(0.47 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (35, CAST(N'2018-04-13 20:46:33.703' AS DateTime), CAST(1.02 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (36, CAST(N'2018-04-14 20:46:33.683' AS DateTime), CAST(1.78 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (37, CAST(N'2018-04-12 20:46:33.723' AS DateTime), CAST(0.72 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (38, CAST(N'2018-04-09 20:46:33.797' AS DateTime), CAST(0.68 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (39, CAST(N'2018-04-11 20:46:33.743' AS DateTime), CAST(0.80 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (40, CAST(N'2018-04-10 20:46:33.770' AS DateTime), CAST(0.73 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (41, CAST(N'2018-04-08 20:46:33.820' AS DateTime), CAST(0.84 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (42, CAST(N'2018-04-07 20:46:33.840' AS DateTime), CAST(1.92 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (43, CAST(N'2018-04-06 20:46:33.867' AS DateTime), CAST(1.07 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (44, CAST(N'2018-04-05 20:46:33.890' AS DateTime), CAST(0.67 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (45, CAST(N'2018-04-04 20:46:33.913' AS DateTime), CAST(0.66 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (46, CAST(N'2018-04-03 20:46:33.937' AS DateTime), CAST(0.68 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (47, CAST(N'2018-04-02 20:46:33.960' AS DateTime), CAST(1.62 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (48, CAST(N'2018-04-01 20:46:33.987' AS DateTime), CAST(2.08 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (49, CAST(N'2018-03-31 20:46:34.017' AS DateTime), CAST(0.69 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (50, CAST(N'2018-03-30 20:46:34.033' AS DateTime), CAST(0.23 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (51, CAST(N'2018-03-29 20:46:34.047' AS DateTime), CAST(0.64 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (52, CAST(N'2018-03-28 20:46:34.063' AS DateTime), CAST(1.52 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (79, CAST(N'2018-04-26 20:47:03.860' AS DateTime), CAST(0.28 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (80, CAST(N'2018-04-26 20:47:03.860' AS DateTime), CAST(0.94 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (81, CAST(N'2018-04-27 19:24:06.393' AS DateTime), CAST(1.14 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (82, CAST(N'2018-04-27 19:24:28.270' AS DateTime), CAST(0.67 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[DeviceReadings] OFF
GO
SET IDENTITY_INSERT [dbo].[Configuration] ON 

GO
INSERT [dbo].[Configuration] ([ConfigId], [ConfigName], [ConfigValue], [ConfigCategory]) VALUES (3, N'MQServerIp', N'192.168.1.10', N'Core')
GO
INSERT [dbo].[Configuration] ([ConfigId], [ConfigName], [ConfigValue], [ConfigCategory]) VALUES (4, N'MQServerPort', N'5672', N'Core')
GO
INSERT [dbo].[Configuration] ([ConfigId], [ConfigName], [ConfigValue], [ConfigCategory]) VALUES (5, N'MQServerUser', N'wanda', N'ConnectionBroker')
GO
INSERT [dbo].[Configuration] ([ConfigId], [ConfigName], [ConfigValue], [ConfigCategory]) VALUES (6, N'MQServerPass', N'wanda', N'ConnectionBroker')
GO
INSERT [dbo].[Configuration] ([ConfigId], [ConfigName], [ConfigValue], [ConfigCategory]) VALUES (7, N'MQServerUser', N'lidia', N'HomeServer')
GO
INSERT [dbo].[Configuration] ([ConfigId], [ConfigName], [ConfigValue], [ConfigCategory]) VALUES (8, N'MQServerPass', N'lidia', N'HomeServer')
GO
SET IDENTITY_INSERT [dbo].[Configuration] OFF
GO
