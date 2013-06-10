CREATE TABLE [dbo].[Requirement]
(
	[RequirementId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [VersionId] UNIQUEIDENTIFIER NOT NULL, 
	[ParentId] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(256) NOT NULL, 
    [Content] NVARCHAR(MAX) NOT NULL,
    [CreateTime] DATETIME NOT NULL, 
	[UpdateTime] DATETIME NOT NULL,
    [IsValid] BIT NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_Requirement_ToProjectVersion] FOREIGN KEY ([VersionId]) REFERENCES [ProjectVersion]([VersionId]), 
    CONSTRAINT [FK_Requirement_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
    
)
