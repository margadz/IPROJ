USE HomeServer

DELETE FROM Configuration
GO

INSERT INTO Configuration VALUES ('MQServerIp', '192.168.1.10', 'Core') 
INSERT INTO Configuration VALUES ('MQServerPort', '5672', 'Core') 
INSERT INTO Configuration VALUES ('MQServerVHost', '/', 'Core') 
INSERT INTO Configuration VALUES ('CodePage', '65001', 'Core') 
INSERT INTO Configuration VALUES ('MQServerUser', 'wanda', 'ConnectionBroker')
INSERT INTO Configuration VALUES ('MQServerPass', 'wanda', 'ConnectionBroker') 
INSERT INTO Configuration VALUES ('MQServerUser', 'lidia', 'HomeServer')
INSERT INTO Configuration VALUES ('MQServerPass', 'lidia', 'HomeServer') 