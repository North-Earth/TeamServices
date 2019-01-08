/*** Таблица с информацией о статических файлах ***/

USE Development
GO

DROP TABLE IF EXISTS TeamServices.Files

CREATE TABLE TeamServices.Files
(
     Id          INTEGER IDENTITY(1,1)  NOT NULL
    ,Name        NVARCHAR(256)          NOT NULL
    ,Description NVARCHAR(1024)             NULL
    ,FileName    NVARCHAR(256)          NOT NULL
    ,CONSTRAINT PkFiles PRIMARY KEY CLUSTERED 
    (
	    Id ASC
    )
)