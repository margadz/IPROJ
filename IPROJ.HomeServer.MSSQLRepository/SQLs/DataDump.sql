USE [HomeServer]
GO
INSERT [dbo].[Devices] ([DeviceId], [Name], [TypeOfReading], [TypeOfDevice], [IsActive], [Host], [CustomId]) VALUES (N'3e1f0946-0a80-4dad-b968-17634e9016e0', N'something', 1, 1, 1, N'192.168.1.202:9999', N'')
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
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (97, CAST(N'2018-05-05 19:58:34.730' AS DateTime), CAST(0.38 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (98, CAST(N'2018-05-05 19:58:34.867' AS DateTime), CAST(0.57 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (153, CAST(N'2018-05-06 19:48:47.103' AS DateTime), CAST(1.33 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (154, CAST(N'2018-05-06 19:48:47.137' AS DateTime), CAST(1.87 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (157, CAST(N'2018-04-28 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (158, CAST(N'2018-04-28 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (159, CAST(N'2018-04-29 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (160, CAST(N'2018-04-29 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (161, CAST(N'2018-04-30 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (162, CAST(N'2018-04-30 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (163, CAST(N'2018-05-01 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (164, CAST(N'2018-05-01 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (165, CAST(N'2018-05-02 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (166, CAST(N'2018-05-02 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (167, CAST(N'2018-05-03 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (168, CAST(N'2018-05-03 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (169, CAST(N'2018-05-04 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (170, CAST(N'2018-05-04 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (171, CAST(N'2018-05-09 20:16:23.237' AS DateTime), CAST(0.41 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (172, CAST(N'2018-05-09 20:16:23.347' AS DateTime), CAST(0.55 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (173, CAST(N'2018-05-12 10:58:45.990' AS DateTime), CAST(1.68 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (174, CAST(N'2018-05-12 10:58:45.993' AS DateTime), CAST(1.01 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (179, CAST(N'2018-05-11 11:00:45.940' AS DateTime), CAST(0.91 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (180, CAST(N'2018-05-11 11:00:45.940' AS DateTime), CAST(0.41 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (181, CAST(N'2018-05-10 11:01:13.940' AS DateTime), CAST(0.58 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (182, CAST(N'2018-05-10 11:01:13.940' AS DateTime), CAST(0.32 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (183, CAST(N'2018-05-08 11:01:32.803' AS DateTime), CAST(0.51 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (184, CAST(N'2018-05-08 11:01:32.803' AS DateTime), CAST(0.33 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (187, CAST(N'2018-05-07 11:03:04.533' AS DateTime), CAST(1.48 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (188, CAST(N'2018-05-07 11:03:04.533' AS DateTime), CAST(1.18 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (189, CAST(N'2018-05-13 10:09:32.883' AS DateTime), CAST(1.31 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (190, CAST(N'2018-05-13 10:09:55.843' AS DateTime), CAST(0.98 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (191, CAST(N'2018-05-14 10:10:26.610' AS DateTime), CAST(0.38 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (192, CAST(N'2018-05-14 10:10:26.610' AS DateTime), CAST(0.28 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (193, CAST(N'2018-05-15 10:10:43.170' AS DateTime), CAST(0.48 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (194, CAST(N'2018-05-15 10:10:43.170' AS DateTime), CAST(0.31 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (195, CAST(N'2018-05-16 10:11:00.497' AS DateTime), CAST(0.42 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (196, CAST(N'2018-05-16 10:11:00.497' AS DateTime), CAST(0.30 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (197, CAST(N'2018-05-16 10:11:20.283' AS DateTime), CAST(0.52 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (198, CAST(N'2018-05-16 10:11:20.283' AS DateTime), CAST(0.41 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (199, CAST(N'2018-05-17 10:11:37.727' AS DateTime), CAST(0.73 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (200, CAST(N'2018-05-17 10:11:37.727' AS DateTime), CAST(0.44 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (201, CAST(N'2018-05-18 10:12:00.710' AS DateTime), CAST(1.51 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (202, CAST(N'2018-05-18 10:12:00.710' AS DateTime), CAST(1.19 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (203, CAST(N'2018-05-19 10:12:41.367' AS DateTime), CAST(1.55 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (204, CAST(N'2018-05-19 10:12:41.367' AS DateTime), CAST(1.01 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (205, CAST(N'2018-05-20 10:13:03.700' AS DateTime), CAST(0.39 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (206, CAST(N'2018-05-20 10:13:03.700' AS DateTime), CAST(0.18 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (207, CAST(N'2018-05-21 10:13:21.910' AS DateTime), CAST(0.50 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (208, CAST(N'2018-05-21 10:13:21.910' AS DateTime), CAST(0.37 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (209, CAST(N'2018-05-22 10:13:41.010' AS DateTime), CAST(0.43 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (210, CAST(N'2018-05-22 10:13:41.010' AS DateTime), CAST(0.30 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (211, CAST(N'2018-05-23 10:14:11.090' AS DateTime), CAST(0.55 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (212, CAST(N'2018-05-23 10:14:11.090' AS DateTime), CAST(0.29 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (213, CAST(N'2018-05-24 10:14:35.683' AS DateTime), CAST(0.51 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (214, CAST(N'2018-05-24 10:14:35.683' AS DateTime), CAST(0.31 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (215, CAST(N'2018-05-25 10:14:53.933' AS DateTime), CAST(0.58 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (216, CAST(N'2018-05-25 10:14:53.937' AS DateTime), CAST(0.34 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (217, CAST(N'2018-05-26 10:15:10.350' AS DateTime), CAST(1.68 AS Decimal(20, 2)), N'b07a7606-9835-4ef0-a052-6f6e3d14d496', 1, 1)
GO
INSERT [dbo].[DeviceReadings] ([Id], [ReadingTimeStamp], [Value], [DeviceID], [TypeOfReading], [ReadingCharacter]) VALUES (218, CAST(N'2018-05-26 10:15:10.350' AS DateTime), CAST(1.28 AS Decimal(20, 2)), N'35fe1ac8-b8df-47b8-a98d-6c9499eb571c', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[DeviceReadings] OFF
GO
