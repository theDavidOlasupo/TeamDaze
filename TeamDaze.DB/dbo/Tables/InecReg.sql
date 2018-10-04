CREATE TABLE [dbo].[InecReg]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [BVN] NVARCHAR(50) NOT NULL, 
    [RegistrationDate] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NULL, 
    [MiddleName] NVARCHAR(50) NULL, 
    [DateOfBirth] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(150) NULL, 
    [Gender] NVARCHAR(50) NULL, 
    [LgaOfOrigin] NVARCHAR(150) NULL, 
    [LgaOfResidence] NVARCHAR(MAX) NULL, 
    [ResidentialAddress] NVARCHAR(MAX) NULL, 
    [StateOfOrigin] NVARCHAR(MAX) NOT NULL, 
    [StateOfResidence] NVARCHAR(MAX) NOT NULL, 
    [Base64Image] VARBINARY(MAX) NULL, 
    [CreatedOn] DATETIME NULL
	)