CREATE TABLE [dbo].[Merchant]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] nvarchar(200),
	[SettlementAccount] nvarchar(10),
	[PhoneNumber] nvarchar(11),
	[EmailAddress] nvarchar(20),
	[Username] nvarchar(20),
	[Password] nvarchar(200),
	[CreatedOn] Datetime,
	[CreatedBy] nvarchar(100),
	[Status] int,
)
