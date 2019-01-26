/*** Витрина справочника ***/

USE Development
GO

DROP VIEW IF EXISTS TeamServices.vDictionary
GO

CREATE VIEW TeamServices.vDictionary
AS
SELECT
     dct.Id
    ,dct.Name
    ,dct.Description
    ,dct.SqlExpression
FROM TeamServices.Dictionary AS dct
