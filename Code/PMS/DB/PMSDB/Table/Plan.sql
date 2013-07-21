CREATE TABLE [dbo].[Plan]
(
	[PlanId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(128) NOT NULL, 
    [TaskParticipatorId] UNIQUEIDENTIFIER NOT NULL, 
    [AllDay] BIT NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL, 
    [Status] TINYINT NOT NULL, 
    CONSTRAINT [FK_Plan_ToTaskParticipator] FOREIGN KEY ([TaskParticipatorId]) REFERENCES [TaskParticipator]([TaskParticipatorId])
)
