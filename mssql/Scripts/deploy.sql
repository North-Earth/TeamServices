USE <<SERVERNAME>>
GO

/*** Схема для объектов командных сервисов ***/

DROP SCHEMA IF EXISTS TeamServices
GO

CREATE SCHEMA TeamServices
GO

/*** Таблица с информацией о статических файлах ***/

DROP TABLE IF EXISTS TeamServices.Files
GO

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
GO

INSERT INTO TeamServices.Files
(
     Name
    ,Description
    ,FileName    
)
VALUES
(
     'Отсутствие на рабочем месте'
    ,'Заявление об отсутствии сотрудника на рабочем месте.'
    ,'Шаблон отсутствие на рабочем месте.docx'
),
(
     'Неоплачиваемый отпуск'
    ,'Заявление на неоплачиваемый отпуск.'
    ,'Шаблон ежегодный оплачиваемый отпуск.docx'
),
(
     'Оплачиваемый отпуск'
    ,'Заявление на ежегодный отпуск с сохранением заработной платы.'
    ,'Шаблон неоплачиваемый отпуск.docx'
)
GO

/***  Таблица с информацией о пользователях ***/

DROP TABLE IF EXISTS TeamServices.Staff

CREATE TABLE TeamServices.Staff
(
     Id              INTEGER IDENTITY(1,1) NOT NULL
    ,MachineName     NVARCHAR(256)         NOT NULL
    ,Name            NVARCHAR(256)         NOT NULL
    ,Birthday        DATE                  NOT NULL
    ,IpAddress       NVARCHAR(16)          NOT NULL
    ,CONSTRAINT PkUsers PRIMARY KEY CLUSTERED 
    (
	    Id ASC
    )
)

INSERT INTO TeamServices.Staff
(
     MachineName
    ,Name
    ,Birthday
    ,IpAddress 
)
VALUES
(
     'Earth'
    ,'Кукушкин Алексей Александрович'
    ,'1998-02-27'
    ,'192.168.1.179'
),
(
     'Venus'
    ,'Замчалова Кристина Витальевна'
    ,'2000-03-10'
    ,'192.168.1.93'
)

/*** Командный справочник ***/

DROP TABLE IF EXISTS TeamServices.Dictionary

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
GO

INSERT INTO TeamServices.Dictionary
(
     Name  
    ,Description
    ,SqlExpression
)
VALUES
(
     'Командный справочник'
    ,'Командный справочник с полезными запросами'
    ,'SELECT * FROM Development.TeamServices.Dictionary'
)

/*** Витрина справочника ***/

DROP VIEW IF EXISTS TeamServices.vDictionary
GO

CREATE VIEW TeamServices.vDictionary
AS
SELECT
     dct.Id
    ,dct.Name
    ,dct.Description
    ,dct.SqlExpression
FROM TeamServices.Dictionary AS dct
GO

/*** Витрина файлового репозитория ***/

DROP VIEW IF EXISTS TeamServices.vFiles
GO

CREATE VIEW TeamServices.vFiles
AS
SELECT 
     fls.Id
    ,fls.Name
    ,fls.Description
    ,fls.FileName 
FROM TeamServices.Files AS fls
GO

/*** Витрина информации о пользователях ***/

DROP VIEW IF EXISTS TeamServices.vUsers
GO

CREATE VIEW TeamServices.vUsers
AS
SELECT
     usr.Id             
    ,usr.MachineName    
    ,usr.Name           
    ,usr.Birthday       
    ,usr.IpAddress      
    --,usr.HardwareAddress
FROM TeamServices.Users AS usr
GO