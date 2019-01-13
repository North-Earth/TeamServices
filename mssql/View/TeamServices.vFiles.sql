/*** Витрина файлового репозитория ***/

USE Development
GO

DROP VIEW IF EXISTS TeamServices.vFiles
GO

CREATE VIEW TeamServices.vFiles
AS
SELECT 
     fls.Id
    ,fls.Name
    ,fls.Description
    ,fls.FileName 
FROM TeamServices.Files AS fls