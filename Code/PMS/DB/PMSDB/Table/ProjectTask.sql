CREATE TABLE [dbo].[ProjectTask]
(
	[TaskId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	 [Title] NVARCHAR(128) NOT NULL , 
    [ProjectId] UNIQUEIDENTIFIER NOT NULL, 
    [RequirementId] UNIQUEIDENTIFIER NULL, 
    [Content] NVARCHAR(MAX) NOT NULL, 
	[Creator] UNIQUEIDENTIFIER NOT NULL,
    [Status] TINYINT NOT NULL, 
    [History] NVARCHAR(MAX) NOT NULL, 
    [CreateTime] DATETIME NOT NULL, 
    [UpdateTime] DATETIME NOT NULL, 
   
    CONSTRAINT [FK_ProjectTask_ToRequirement] FOREIGN KEY ([RequirementId]) REFERENCES [Requirement]([RequirementId]), 
    CONSTRAINT [FK_ProjectTask_ToUser] FOREIGN KEY ([Creator]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_ProjectTask_ToProject] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([ProjectId]), 
   
)
