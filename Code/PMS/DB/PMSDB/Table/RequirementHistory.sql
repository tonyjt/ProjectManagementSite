CREATE TABLE [dbo].[RequirementHistory]
(
	[HistoryId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [UserId] UNIQUEIDENTIFIER NOT NULL,  
    [RequirementId] UNIQUEIDENTIFIER NOT NULL, 
    [VersionId] UNIQUEIDENTIFIER NOT NULL, 
    [ParentId] UNIQUEIDENTIFIER NOT NULL, 
    [Title] NVARCHAR(256) NOT NULL, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [CreateDate] DATETIME NOT NULL ,
    CONSTRAINT [FK_RequirementHistory_ToProjectVersion] FOREIGN KEY ([VersionId]) REFERENCES [ProjectVersion]([VersionId]), 
    CONSTRAINT [FK_RequirementHistory_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
