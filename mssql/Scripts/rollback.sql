USE <<SERVERNAME>>
GO

/***  ***/

DROP TABLE IF EXISTS TeamServices.<<NAME>>
GO

/***  ***/

DROP VIEW IF EXISTS TeamServices.<<NAME>>
GO

/**************************************************/

/*** ������� � ����������� � ����������� ������ ***/

DROP TABLE IF EXISTS TeamServices.Files
GO

/*** ������� � ����������� � ����������� ������ ***/

DROP VIEW IF EXISTS TeamServices.FilesView
GO


/*** ����� ��� �������� ��������� �������� ***/

DROP SCHEMA IF EXISTS TeamServices
GO
