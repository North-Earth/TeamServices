/*** ������ �������� ***/

USE Development
GO

DROP TABLE IF EXISTS TeamServices.Links
GO

CREATE TABLE TeamServices.Links
(
	 Id   INTEGER	    NOT NULL /* ID �� TeamServices.Projects*/
	,Name NVARCHAR(32)  NOT NULL
	,Url  NVARCHAR(256) NOT NULL
)