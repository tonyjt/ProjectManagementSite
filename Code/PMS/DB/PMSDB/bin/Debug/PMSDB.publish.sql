/*
PMSDB 的部署脚本

此代码由工具生成。
如果重新生成此代码，则对此文件的更改可能导致
不正确的行为并将丢失。
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "PMSDB"
:setvar DefaultFilePrefix "PMSDB"
:setvar DefaultDataPath "C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
请检测 SQLCMD 模式，如果不支持 SQLCMD 模式，请禁用脚本执行。
要在启用 SQLCMD 模式后重新启用脚本，请执行:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'要成功执行此脚本，必须启用 SQLCMD 模式。';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'正在创建 $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'无法修改数据库设置。您必须是 SysAdmin 才能应用这些设置。';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'无法修改数据库设置。您必须是 SysAdmin 才能应用这些设置。';
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'正在创建 [dbo].[Project]...';


GO
CREATE TABLE [dbo].[Project] (
    [ProjectId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (64)    NOT NULL,
    [Creator]     UNIQUEIDENTIFIER NOT NULL,
    [CreateTime]  DATETIME         NOT NULL,
    [Description] NVARCHAR (MAX)   NOT NULL,
    [StartDate]   DATE             NULL,
    PRIMARY KEY CLUSTERED ([ProjectId] ASC)
);


GO
PRINT N'正在创建 [dbo].[ProjectParticipator]...';


GO
CREATE TABLE [dbo].[ProjectParticipator] (
    [ProjectId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]    UNIQUEIDENTIFIER NOT NULL,
    [Roles]     SMALLINT         NOT NULL,
    [JoinTime]  DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ProjectId] ASC)
);


GO
PRINT N'正在创建 [dbo].[Requirement]...';


GO
CREATE TABLE [dbo].[Requirement] (
    [Id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'正在创建 [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
PRINT N'正在创建 [dbo].[Version]...';


GO
CREATE TABLE [dbo].[Version] (
    [VersionId]   UNIQUEIDENTIFIER NOT NULL,
    [ProjectId]   UNIQUEIDENTIFIER NOT NULL,
    [VersionName] NVARCHAR (256)   NOT NULL,
    [CreateTime]  DATETIME         NOT NULL,
    [StartTime]   DATETIME         NULL,
    [EndTime]     DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([VersionId] ASC)
);


GO
PRINT N'正在创建 FK_Project_ToUser...';


GO
ALTER TABLE [dbo].[Project]
    ADD CONSTRAINT [FK_Project_ToUser] FOREIGN KEY ([Creator]) REFERENCES [dbo].[User] ([UserId]);


GO
-- 正在重构步骤以使用已部署的事务日志更新目标服务器

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '3de87430-d16e-49ae-9fde-9f2a89b56db2')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('3de87430-d16e-49ae-9fde-9f2a89b56db2')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'f92ea08c-e886-47b8-b516-592535637bda')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('f92ea08c-e886-47b8-b516-592535637bda')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '82a675de-ac9d-4a16-9a0b-f4346a9ba011')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('82a675de-ac9d-4a16-9a0b-f4346a9ba011')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '033d1f4d-302a-4db4-9e08-e962c40e9a70')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('033d1f4d-302a-4db4-9e08-e962c40e9a70')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9f3dd601-aba0-4b8f-bdb7-a829d1a07418')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9f3dd601-aba0-4b8f-bdb7-a829d1a07418')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'db0bcaf3-7291-469a-851c-67fd06e7e992')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('db0bcaf3-7291-469a-851c-67fd06e7e992')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'e8c5dd41-d621-4b35-a60d-23ee81d9feb7')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('e8c5dd41-d621-4b35-a60d-23ee81d9feb7')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'd329bcf8-6242-494e-9945-9698af029252')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('d329bcf8-6242-494e-9945-9698af029252')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'a0cece4e-15bd-46ab-a145-0cb1f731c8ef')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('a0cece4e-15bd-46ab-a145-0cb1f731c8ef')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '83e7ca3a-34f2-41aa-aba8-0315bc3c7830')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('83e7ca3a-34f2-41aa-aba8-0315bc3c7830')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '96272e5c-7dd0-4ce2-aab3-4593e27abd98')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('96272e5c-7dd0-4ce2-aab3-4593e27abd98')

GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'更新完成。';


GO
