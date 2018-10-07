CREATE TABLE [dbo].[Staff]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[EmployerId] int NOT NULL,
	[BVN] nvarchar(12) NOT NULL, 
    [Status] INT NULL DEFAULT 1, 
    [CreatedOn] DATETIME NULL DEFAULT GetDate(),
)
