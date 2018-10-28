CREATE FUNCTION [dbo].[IsExpiryDateValid]
(
	@cardType VARCHAR(10),
	@expiryDate DATE
)
RETURNS BIT
AS
BEGIN
	IF (@cardType IS NULL OR @expiryDate IS NULL)
		RETURN NULL;

	IF (@cardType = 'Visa')
		RETURN IIF(YEAR(@expiryDate) % 4 = 0, 1, 0);

	IF (@cardType = 'MasterCard')
		RETURN [dbo].[IsPrimeNumber](YEAR(@expiryDate));

	IF (@cardType = 'Amex')
		RETURN 1;

	IF (@cardType = 'JCB')
		RETURN 1;
	
	RETURN 0;
END;
