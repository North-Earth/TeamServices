/*** Витрина информации о пользователях ***/

USE Development
GO

DROP VIEW IF EXISTS TeamServices.vUsers
GO

CREATE VIEW TeamServices.vUsers
AS
SELECT
     usr.Id             
    ,usr.MachineName    
    ,usr.Name           
    ,usr.Birthday       
    ,usr.IpAddress      
    --,usr.HardwareAddress
FROM TeamServices.Users AS usr