USE DevelopBase
GO

DROP TABLE IF EXISTS twt.Users

CREATE TABLE twt.Users
(
     Id         INTEGER IDENTITY(1,1) NOT NULL
    ,SystemName NVARCHAR(256)         NOT NULL
    ,Name       NVARCHAR(256)         NOT NULL
    ,Birthday   DATE                  NOT NULL
    ,ipv4       NVARCHAR(16)          NOT NULL
    ,Mac        NVARCHAR(32)          NULL
)

INSERT INTO twt.Users
(
     SystemName
    ,Name
    ,Birthday
    ,ipv4
    ,Mac    
)
