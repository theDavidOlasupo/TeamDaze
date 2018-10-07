CREATE TABLE [dbo].[Incident]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Type nvarchar(20) NOT NULL,
	EmployerId INT,
	[BVN] nvarchar(12),
	CreatedOn datetime DEFAULT GetDate(), 
    [Status] INT NULL DEFAULT 1
)
