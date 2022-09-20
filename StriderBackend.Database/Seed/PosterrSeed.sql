CREATE PROCEDURE [dbo].[PosterrSeed]
AS
BEGIN
	--IF(NOT EXISTS (SELECT TOP 1 1 FROM [User]))
	BEGIN
		DECLARE @userId1 INT = 1;
		DECLARE @userId2 INT = 2;
		DECLARE @userId3 INT = 3;
		DECLARE @userId4 INT = 4;

		INSERT INTO [User] VALUES (@userId1, 'Jordan Rudess', GETDATE());
		INSERT INTO [User] VALUES (@userId2, 'Alexander', GETDATE());
		INSERT INTO [User] VALUES (@userId3, 'Obdolbos', GETDATE());
		INSERT INTO [User] VALUES (@userId4, 'Someone', GETDATE());

		INSERT INTO [Post] VALUES (NEWID(), @userId1, 'Lorem ipsum dolor sit amet!', GETDATE())
		INSERT INTO [Post] VALUES (NEWID(), @userId1, 'Veniam asd ewq u bah!', GETDATE())

		INSERT INTO [Post] VALUES (NEWID(), @userId2, 'Duis aute irure dolor in.', GETDATE())
		INSERT INTO [Post] VALUES (NEWID(), @userId3, 'Nemo enim ipsam voluptatem', GETDATE())
		INSERT INTO [Post] VALUES (NEWID(), @userId4, 'Ut enim ad minima veniam', GETDATE())

		INSERT INTO [UserFollower] VALUES (NEWID(), @userId1, @userId2)
		INSERT INTO [UserFollower] VALUES (NEWID(), @userId1, @userId4)
	END

END