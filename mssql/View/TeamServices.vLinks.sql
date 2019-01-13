/*** Витрина с ссылками ***/

USE Development
GO

DROP VIEW IF EXISTS TeamServices.vLinks
GO

CREATE VIEW TeamServices.vLinks
AS
SELECT
	 lnk.Id   
	,lnk.Name 
	,lnk.Url  
FROM TeamServices.Links AS lnk