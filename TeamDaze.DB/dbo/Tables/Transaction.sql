CREATE TABLE [dbo].[Transaction]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[CustomerId] INT,
	[FromAccount] nvarchar(100),
	[ToAccount] nvarchar(100),
	[MerchantId] INT,
	[Amount] numeric(18,2),
	[CreatedOn] Datetime,
	[Status] int
)
