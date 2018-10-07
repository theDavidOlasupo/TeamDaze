CREATE TABLE [dbo].[Employer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(200) NULL, 
	[BVN] nvarchar(12),
    [Status] INT NULL DEFAULT 1, 
    [Address] NVARCHAR(50) NULL, 
    [Type] NVARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GetDate()
)
