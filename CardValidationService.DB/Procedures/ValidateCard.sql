CREATE PROCEDURE [dbo].[ValidateCard]
	@cardNumber NUMERIC(16)
AS
	IF @cardNumber IS NULL
		RETURN 0;

	DECLARE @cardType VARCHAR(10) = [dbo].[GetCardType](@cardNumber);

	DECLARE @expiryDate DATE = (
		SELECT [ExpiryDate]
		FROM [Card]
		WHERE [Number] = @cardNumber);

	DECLARE @isValid BIT = IIF(@expiryDate IS NOT NULL, [dbo].[IsExpiryDateValid](@cardType, @expiryDate), NULL);

	SELECT
		@cardType AS [CardType],
		CASE @isValid
				WHEN 0 THEN 'Invalid'
				WHEN 1 THEN 'Valid'
				ELSE 'Does not exist'
		END
		AS [ValidationStatus];

	RETURN 0;
