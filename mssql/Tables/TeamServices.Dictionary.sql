/*** Командный справочник ***/

USE Development
GO

DROP TABLE IF EXISTS TeamServices.Dictionary
GO

CREATE TABLE TeamServices.Dictionary
(
     Id            INTEGER IDENTITY(1,1) NOT NULL
    ,Name          NVARCHAR(256)         NOT NULL
    ,Description   NVARCHAR(1024)            NULL
    ,SqlExpression NVARCHAR(2048)        NOT NULL
    ,CONSTRAINT PkDictionary PRIMARY KEY CLUSTERED 
    (
	    Id ASC
    )
)