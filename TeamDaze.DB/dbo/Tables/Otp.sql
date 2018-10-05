CREATE TABLE [dbo].[Otp]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Bvn] nvarchar(20) null,
	[GeneratedOtp] nvarchar(10),
	[IsUsed] int default(0),
	[CreatedOn] datetime default(GetDate())

)
