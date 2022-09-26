CREATE TABLE [dbo].[UserFollower]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [UserFollowerId] INT NOT NULL, 
    CONSTRAINT [FK_UserFollower_User] FOREIGN KEY ([UserFollowerId]) REFERENCES [User]([Id])
)
