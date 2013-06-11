CREATE TABLE [dbo].[TaskParticipator]
(
	[TaskParticipatorId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TaskId] UNIQUEIDENTIFIER NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NULL, 
    [Roles] SMALLINT NOT NULL, 
    [Status] TINYINT NOT NULL, 
    CONSTRAINT [FK_TaskParticipator_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_TaskParticipator_ToProjectTask] FOREIGN KEY ([TaskId]) REFERENCES [ProjectTask]([TaskId])
)
