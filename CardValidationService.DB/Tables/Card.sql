﻿CREATE TABLE [dbo].[Card]
(
	[Number] NUMERIC(16) NOT NULL CONSTRAINT [PK_Card] PRIMARY KEY,
	[ExpiryDate] DATE NOT NULL,
)
