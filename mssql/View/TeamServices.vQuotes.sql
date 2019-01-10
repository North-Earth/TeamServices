USE Development
GO

DROP VIEW IF EXISTS TeamServices.vQuotes
GO

CREATE VIEW TeamServices.vQuotes
AS
SELECT
	 qts.id
	,qts.text
	,qts.author
FROM TeamServices.Quotes AS qts
GO