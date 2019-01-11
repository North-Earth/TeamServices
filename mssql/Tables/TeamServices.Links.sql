USE Development
GO

DROP TABLE IF EXISTS TeamServices.Links

CREATE TABLE TeamServices.Links
(
	 Id   INTEGER	    NOT NULL
	,Name NVARCHAR(32)  NOT NULL
	,Url  NVARCHAR(256) NOT NULL
)