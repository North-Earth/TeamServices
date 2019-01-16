USE Development
GO

DROP VIEW IF EXISTS TeamServices.vReports
GO

CREATE VIEW TeamServices.vReports
AS
SELECT
	 Name	
	,UserName
	,ReportHour
	,ReportDtm
FROM TeamServices.Reports AS rpr