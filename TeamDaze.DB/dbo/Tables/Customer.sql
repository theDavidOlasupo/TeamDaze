CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[FirstName] nvarchar(200),
	[LastName] nvarchar(200),
	[BVN] nvarchar(12),
	[PhoneNumber] nvarchar(11),
	[EmailAddress] nvarchar(100),
	[PanicFinger] nvarchar(10),
	[EnrollmentType] nvarchar(20),
	[CardType] nvarchar(20),
	[CardToken] nvarchar(200),
	[CreatedOn] Datetime,
	[CreatedBy] nvarchar(100),
	[Status] int, 
    [MaxAmount] DECIMAL(18, 2) NULL,
)
