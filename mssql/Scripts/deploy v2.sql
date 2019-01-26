USE <<SERVERNAME>>
GO

/*** ����� ��� �������� ��������� �������� ***/

DROP SCHEMA IF EXISTS TeamServices
GO

CREATE SCHEMA TeamServices
GO

/*** ������� ��������� ���������� ***/

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

/*** ������� ���������� ����������� ***/

DROP VIEW IF EXISTS TeamServices.DictionaryView
GO

CREATE VIEW TeamServices.DictionaryView
AS
SELECT
     dct.Id
    ,dct.Name
    ,dct.Description
    ,dct.SqlExpression
FROM TeamServices.Dictionary AS dct
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

/*** ������� � ����������� � ����������� ������ ***/
DROP VIEW IF EXISTS TeamServices.FilesView
GO

CREATE VIEW TeamServices.FilesView
AS
SELECT
     fls.Id
    ,fls.Name
    ,fls.Description
    ,fls.FileName
FROM TeamServices.Files AS fls
GO

/*** ������ �������� ***/

DROP TABLE IF EXISTS TeamServices.Links
GO

CREATE TABLE TeamServices.Links
(
     Id       INTEGER       NOT NULL /* ID �� TeamServices.Projects */
    ,TypeId   TINYINT       NOT NULL /* ��� ������ 0 - �����, 1 - Git, 2 - Confluence TODO: ������� ���������� � ������*/
    ,Name     NVARCHAR(32)  NOT NULL
    ,Url      NVARCHAR(256) NOT NULL
    ,IcoClass VARCHAR(16)       NULL /* ����� ������ �� ������ ������ FontAwesome */
)
GO

DROP VIEW IF EXISTS TeamServices.LinksView
GO

CREATE VIEW TeamServices.LinksView
AS
SELECT
     lnk.Id
    ,TypeId
    ,lnk.Name
    ,lnk.Url
    ,lnk.IcoClass
FROM TeamServices.Links AS lnk
GO

INSERT INTO TeamServices.Links
(
     lnk.Id
    ,TypeId
    ,lnk.Name
    ,lnk.Url
    ,lnk.IcoClass
)
VALUES
(
     1
    ,1
    ,'GitHub'
    ,'#'
    ,NULL
),
(
     1
    ,2
    ,'Confluence'
    ,'#'
    ,NULL
)

/*** ���������� ��������� � ����������� ������� ***/

DROP TABLE IF EXISTS TeamServices.WorkReport

CREATE TABLE TeamServices.WorkReport
(
     Name           NVARCHAR(256) NOT NULL
    ,Description    NVARCHAR(256) NOT NULL
    ,UserName       VARCHAR(128)  NOT NULL
    ,TimeHour       SMALLINT      NOT NULL
    ,LoadDtm        DATETIME2(3)  NOT NULL
)

/* ������� ���������� ��������� � ����������� ������� */
--TODO: ������� �������

/*** ���������� ��������� �������� ***/

DROP TABLE IF EXISTS TeamServices.Projects
GO

CREATE TABLE TeamServices.Projects
(
     Id          INTEGER IDENTITY(1,1)  NOT NULL
    ,Name        NVARCHAR(32)           NOT NULL
    ,Description NVARCHAR(256)              NULL
    ,LogoPath    NVARCHAR(256)              NULL
    ,CONSTRAINT PkProjects PRIMARY KEY CLUSTERED
    (
        Id ASC
    )
)
GO

DROP VIEW IF EXISTS TeamServices.ProjectsView
GO

CREATE VIEW TeamServices.ProjectsView
AS
SELECT
     prj.Id
    ,prj.Name
    ,prj.Description
    ,prj.LogoPath
FROM TeamServices.ProjectsView AS prj
GO

INSERT INTO TeamServices.Projects
(
     prj.Id
    ,prj.Name
    ,prj.Description
    ,prj.LogoPath
)
VALUES
(
     'TeamToolkit'
    ,'��������� ��������������'
    ,'prjLogo1.jpg'
)
,(
     'TeamToolkit'
    ,'��������� ��������������'
    ,'prjLogo1.jpg'
)

/*** �������� ������ ������ ������� ***/

DROP TABLE IF EXISTS TeamServices.Quotes
GO

CREATE TABLE TeamServices.Quotes
(
     id      INTEGER IDENTITY(1,1) NOT NULL
    ,text    NVARCHAR(256)         NOT NULL
    ,author  NVARCHAR(64)          NOT NULL
    ,CONSTRAINT PkQuotes PRIMARY KEY CLUSTERED
    (
        id ASC
    )
)
GO

DROP VIEW IF EXISTS TeamServices.QuotesView

CREATE VIEW TeamServices.QuotesView
(
     id
    ,text
    ,author
)

INSERT INTO TeamServices.QuotesView
(
     text
    ,author
)
,(
     'TRUNACATE ��������, DROP TABLE ����������'
    ,'�������� ������� �������������'
)

/***  ������� � ����������� � ��������� ***/

DROP TABLE IF EXISTS TeamServices.Staff
GO

CREATE TABLE TeamServices.Staff
(
     Id              INTEGER IDENTITY(1,1) NOT NULL
    ,UserName        VARCHAR(64)           NOT NULL
    ,MachineName     VARCHAR(64)           NOT NULL
    ,Name            NVARCHAR(64)          NOT NULL
    ,Birthday        DATE                  NOT NULL
    ,IpAddress       VARCHAR(16)           NOT NULL
    ,CONSTRAINT PkUsers PRIMARY KEY CLUSTERED 
    (
        Id ASC
    )
)

DROP VIEW IF EXISTS TeamServices.StaffView
GO

CREATE VIEW TeamServices.StaffView
AS
SELECT
     stf.Id
    ,stf.UserName
    ,stf.MachineName
    ,stf.Name
    ,stf.Birthday
    ,stf.IpAddress
FROM TeamServices.Staff AS stf

INSERT INTO TeamServices.Staff
(
     UserName
    ,MachineName
    ,Name
    ,Birthday
    ,IpAddress
)
VALUES
(
     'kukushkin_aa'
    ,'Venus'
    ,'�������� ������� �������������'
    ,'1998-02-27'
    ,'192.168.1.93'
)