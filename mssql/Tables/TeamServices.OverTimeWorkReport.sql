/*** —правочник отработок и переработок команды ***/

USE Development
GO

DROP TABLE IF EXISTS TeamServices.OverTimeWorkReport

CREATE TABLE TeamServices.OverTimeWorkReport
(
	 Name		  NVARCHAR(256) NOT NULL
	,Description  NVARCHAR(256) NOT NULL
	,UserName	  VARCHAR(128)  NOT NULL
	,OverTimeHour SMALLINT      NOT NULL
	,LoadDtm      DATETIME2(3)  NOT NULL
)