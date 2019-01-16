/*** —правочник отработок и переработок команды ***/

USE Development
GO

DROP TABLE IF EXISTS TeamServices.Reports

CREATE TABLE TeamServices.Reports
(
	 Name		NVARCHAR(256) NOT NULL
	,UserName	VARCHAR(128)  NOT NULL
	,ReportHour TINYINT		  NOT NULL
	,ReportDtm  DATETIME2(3)  NOT NULL
)