USE DevelopBase
GO

DROP TABLE IF EXISTS twt.Dictionary

CREATE TABLE twt.Dictionary
(
     Id            INTEGER IDENTITY(1,1) PRIMARY KEY CLUSTERED
    ,Name          NVARCHAR(256)
    ,Description   NVARCHAR(1024)
    ,SqlExpression NVARCHAR(2048)
)

INSERT INTO twt.Dictionary
(
         Name  
        ,Description
        ,SqlExpression
)
SELECT 'Тестовая запись','Тестовое описание','SELECT ''Test Expression'''