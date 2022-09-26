﻿CREATE TABLE [dbo].[Post]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL,
    [Content] VARCHAR(777) NOT NULL, 
    [Date] DATE NOT NULL, 
    [RepostId] UNIQUEIDENTIFIER NULL, 
    [Type] INT NOT NULL DEFAULT 0, 
    [QuoteCommentary] NVARCHAR(777) NULL, 
    CONSTRAINT [FK_Post_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_Post_Post] FOREIGN KEY ([RepostId]) REFERENCES [Post]([Id])
)