/*** Забавные цитаты членов команды ***/
USE Development
GO

DROP TABLE IF EXISTS TeamServices.Quotes
GO

CREATE TABLE TeamServices.Quotes
(
	 id		INTEGER IDENTITY(1,1) NOT NULL
	,text  NVARCHAR(256)         NOT NULL
	,author NVARCHAR(64)          NOT NULL
	,CONSTRAINT PkQuotes PRIMARY KEY CLUSTERED
	(
		id ASC
	)
)
GO