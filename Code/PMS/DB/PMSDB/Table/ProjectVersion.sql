CREATE TABLE [dbo].[ProjectVersion]
(
	[VersionId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProjectId] UNIQUEIDENTIFIER NOT NULL, 
	[Creator] UNIQUEIDENTIFIER NOT NULL,
    [VersionName] NVARCHAR(256) NOT NULL, 
    [CreateTime] DATETIME NOT NULL, 
    [StartTime] DATETIME NULL, 
    [EndTime] DATETIME NULL, 
    [Status] TINYINT NOT NULL, 
    CONSTRAINT [FK_Version_ToProject] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([ProjectId]), 
    CONSTRAINT [FK_Version_ToUser] FOREIGN KEY ([Creator]) REFERENCES [User]([UserId]), 
    
)
