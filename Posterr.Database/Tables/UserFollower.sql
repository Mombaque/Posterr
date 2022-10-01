CREATE TABLE [dbo].[UserFollower]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [UserFollowerId] INT NOT NULL, 
    CONSTRAINT [FK_UserFollower_User1] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_UserFollower_User2] FOREIGN KEY ([UserFollowerId]) REFERENCES [User]([Id])
)
