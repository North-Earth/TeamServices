USE Development
GO

DROP VIEW IF EXISTS TeamServices.vOverTimeWorkReport
GO

CREATE VIEW TeamServices.vOverTimeWorkReport
AS
SELECT
	 owr.Name	
	,owr.UserName
	,owr.OverTimeHour
	,owr.LoadDtm
FROM TeamServices.OverTimeWorkReport AS owr