IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PosterrDatabase')
    CREATE DATABASE PosterrDatabase
USE PosterrDatabase
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Post')
BEGIN
    CREATE TABLE [dbo].[Post] (
        [Id]              UNIQUEIDENTIFIER NOT NULL,
        [UserId]          INT              NOT NULL,
        [Content]         VARCHAR (777)    NOT NULL,
        [Date]            DATE             NOT NULL,
        [RepostId]        UNIQUEIDENTIFIER NULL,
        [Type]            INT DEFAULT 0 NOT NULL,
        [QuoteCommentary] NVARCHAR (777)   NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
   
    ALTER TABLE [dbo].[Post] WITH NOCHECK
        ADD CONSTRAINT [FK_Post_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]);

    ALTER TABLE [dbo].[Post] WITH NOCHECK
        ADD CONSTRAINT [FK_Post_Post] FOREIGN KEY ([RepostId]) REFERENCES [dbo].[Post] ([Id]);

    ALTER TABLE [dbo].[Post] WITH CHECK CHECK CONSTRAINT [FK_Post_User];
        ALTER TABLE [dbo].[Post] WITH CHECK CHECK CONSTRAINT [FK_Post_Post];    
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='User')
BEGIN
    CREATE TABLE [dbo].[User] (
        [Id]           INT          NOT NULL,
        [Name]         VARCHAR (14) NOT NULL,
        [CreationDate] DATE         NOT NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserFollower')
BEGIN
    CREATE TABLE [dbo].[UserFollower] (
        [Id]             UNIQUEIDENTIFIER NOT NULL,
        [UserId]         INT              NOT NULL,
        [UserFollowerId] INT              NOT NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    ALTER TABLE [dbo].[UserFollower] WITH NOCHECK
        ADD CONSTRAINT [FK_UserFollower_User] FOREIGN KEY ([UserFollowerId]) REFERENCES [dbo].[User] ([Id]);

    ALTER TABLE [dbo].[UserFollower] WITH CHECK CHECK CONSTRAINT [FK_UserFollower_User];
END
GO

GO
IF(NOT EXISTS (SELECT TOP 1 1 FROM [User]))
BEGIN
	DECLARE @userId1 INT = 1;
	DECLARE @userId2 INT = 2;
	DECLARE @userId3 INT = 3;
	DECLARE @userId4 INT = 4;

	INSERT INTO [User] VALUES (@userId1, 'Jordan Rudess', GETDATE());

	INSERT INTO [User] VALUES (@userId2, 'Alexander', GETDATE());
	INSERT INTO [User] VALUES (@userId3, 'Obdolbos', GETDATE());
	INSERT INTO [User] VALUES (@userId4, 'Someone', GETDATE());

	INSERT INTO [Post] ([Id], [UserId], [Content], [Date]) VALUES (NEWID(), @userId1, 'Lorem ipsum dolor sit amet!', GETDATE())
	INSERT INTO [Post] ([Id], [UserId], [Content], [Date]) VALUES (NEWID(), @userId1, 'Veniam asd ewq u bah!', GETDATE())

	INSERT INTO [Post] ([Id], [UserId], [Content], [Date]) VALUES (NEWID(), @userId2, 'Duis aute irure dolor in.', GETDATE())
	INSERT INTO [Post] ([Id], [UserId], [Content], [Date]) VALUES (NEWID(), @userId3, 'Nemo enim ipsam voluptatem', GETDATE())
	INSERT INTO [Post] ([Id], [UserId], [Content], [Date]) VALUES (NEWID(), @userId4, 'Ut enim ad minima veniam', GETDATE())

	INSERT INTO [UserFollower] VALUES (NEWID(), @userId1, @userId2)
	INSERT INTO [UserFollower] VALUES (NEWID(), @userId1, @userId4)
END