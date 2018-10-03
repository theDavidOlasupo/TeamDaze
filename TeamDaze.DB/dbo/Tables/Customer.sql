CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[FirstName] nvarchar(200),
	[LastName] nvarchar(200),
	[BVN] nvarchar(10),
	[PhoneNumber] nvarchar(11),
	[EmailAddress] nvarchar(20),
	[PanicFinger] nvarchar(10),
	[CreatedOn] Datetime,
	[CreatedBy] nvarchar(100),
	[Status] int, 
    [MaxAmount] DECIMAL(18, 2) NULL,
)
