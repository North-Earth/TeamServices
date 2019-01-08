/***  Таблица с информацией о пользователях ***/

USE Development
GO

DROP TABLE IF EXISTS TeamServices.Users

CREATE TABLE TeamServices.Users
(
     Id              INTEGER IDENTITY(1,1) NOT NULL
    ,MachineName     NVARCHAR(256)         NOT NULL
    ,Name            NVARCHAR(256)         NOT NULL
    ,Birthday        DATE                  NOT NULL
    ,IpAddress       NVARCHAR(16)          NOT NULL
    ,HardwareAddress NVARCHAR(32)              NULL
    ,CONSTRAINT PkUsers PRIMARY KEY CLUSTERED 
    (
	    Id ASC
    )
)