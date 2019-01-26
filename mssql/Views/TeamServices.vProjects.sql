/*** Витрина c информацией о проектах ***/

USE Development
GO

DROP VIEW IF EXISTS TeamServices.vProjects
GO

CREATE VIEW TeamServices.vProjects
AS
SELECT
	 prj.Id		     
	,prj.Name		 
	,prj.Description 
	,prj.LogoPath    
FROM TeamServices.Projects AS prj