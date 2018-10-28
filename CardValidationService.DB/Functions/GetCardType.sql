CREATE FUNCTION [dbo].[GetCardType]
(
	@cardNumber NUMERIC(16)
)
RETURNS VARCHAR(10)
AS
BEGIN
	IF (@cardNumber IS NULL)
		RETURN NULL;

	DECLARE @systemNumber INT = CAST(@cardNumber / 1000000000000 AS INT);

	IF (@systemNumber BETWEEN 4000 AND 4999)
		RETURN 'Visa';

	IF (@systemNumber BETWEEN 5000 AND 5999)
		RETURN 'MasterCard';

	IF (   @systemNumber BETWEEN 340 AND 349
		OR @systemNumber BETWEEN 370 AND 379)
		RETURN 'Amex';

	IF (@systemNumber BETWEEN 3528 AND 3589)
		RETURN 'JCB';

	RETURN 'Unknown';
END;
