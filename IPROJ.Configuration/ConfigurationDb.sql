use HomeServer
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

