CREATE TABLE [dbo].[Requirement]
(
	[RequirementId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [VersionId] UNIQUEIDENTIFIER NOT NULL, 
	[ParentId] UNIQUEIDENTIFIER NULL,
    [Title] NVARCHAR(256) NOT NULL, 
    [Content] NVARCHAR(MAX) NOT NULL,
    [CreateTime] DATETIME NOT NULL, 
	[UpdateTime] DATETIME NOT NULL,
    [IsValid] BIT NOT NULL, 
    CONSTRAINT [FK_Requirement_ToProjectVersion] FOREIGN KEY ([VersionId]) REFERENCES [ProjectVersion]([VersionId]), 
    CONSTRAINT [FK_Requirement_ToRequirement] FOREIGN KEY ([ParentId]) REFERENCES [Requirement]([RequirementId]), 
    
)
