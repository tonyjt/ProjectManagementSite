CREATE TABLE [dbo].[Project]
(
	[ProjectId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(64) NOT NULL, 
    [Creator] UNIQUEIDENTIFIER NOT NULL, 
    [CreateTime] DATETIME NOT NULL, 
    [Status] TINYINT NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [StartDate] DATE NULL, 
    CONSTRAINT [FK_Project_ToUser] FOREIGN KEY ([Creator]) REFERENCES [User]([UserId])
)
