USE <<SERVERNAME>>
GO

/***  ***/

DROP TABLE IF EXISTS TeamServices.<<NAME>>
GO

/***  ***/

DROP VIEW IF EXISTS TeamServices.<<NAME>>
GO

/**************************************************/

/*** Таблица с информацией о статических файлах ***/

DROP TABLE IF EXISTS TeamServices.Files
GO

/*** Витрина с информацией о статических файлах ***/

DROP VIEW IF EXISTS TeamServices.FilesView
GO


/*** Схема для объектов командных сервисов ***/

DROP SCHEMA IF EXISTS TeamServices
GO
