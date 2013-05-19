CREATE TABLE [dbo].[ProjectParticipator]
(
	[ProjectId] UNIQUEIDENTIFIER NOT NULL , 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Roles] SMALLINT NOT NULL, 
    [JoinTime] DATETIME NOT NULL, 
    PRIMARY KEY ([ProjectId], [UserId]), 
    CONSTRAINT [FK_ProjectParticipator_ToProject] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([ProjectId]), 
    CONSTRAINT [FK_ProjectParticipator_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
