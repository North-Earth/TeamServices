USE <<SERVERNAME>>
GO

/*** ����� ��� �������� ��������� �������� ***/

DROP SCHEMA IF EXISTS TeamServices
GO

CREATE SCHEMA TeamServices
GO

/*** ������� � ����������� � ����������� ������ ***/

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
     '���������� �� ������� �����'
    ,'��������� �� ���������� ���������� �� ������� �����.'
    ,'������ ���������� �� ������� �����.docx'
),
(
     '�������������� ������'
    ,'��������� �� �������������� ������.'
    ,'������ ��������� ������������ ������.docx'
),
(
     '������������ ������'
    ,'��������� �� ��������� ������ � ����������� ���������� �����.'
    ,'������ �������������� ������.docx'
)
GO

/***  ������� � ����������� � ������������� ***/

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
    ,'�������� ������� �������������'
    ,'1998-02-27'
    ,'192.168.1.179'
),
(
     'Venus'
    ,'��������� �������� ����������'
    ,'2000-03-10'
    ,'192.168.1.93'
)

/*** ��������� ���������� ***/

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
     '��������� ����������'
    ,'��������� ���������� � ��������� ���������'
    ,'SELECT * FROM Development.TeamServices.Dictionary'
)

/*** ������� ����������� ***/

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

/*** ������� ��������� ����������� ***/

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

/*** ������� ���������� � ������������� ***/

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