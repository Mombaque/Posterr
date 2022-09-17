CREATE OR ALTER PROCEDURE [dbo].[PosterrSeed]
AS
BEGIN
	IF(NOT EXISTS (SELECT TOP 1 1 FROM [User]))
	BEGIN
		INSERT INTO [User] VALUES (1, 'Jordan Rudess', GETDATE());
		INSERT INTO [User] VALUES (2, 'Alexander', GETDATE());
		INSERT INTO [User] VALUES (3, 'Obdolbos', GETDATE());
		INSERT INTO [User] VALUES (4, 'Someone', GETDATE());

		INSERT INTO [Post] VALUES (NEWID(), 1, 'Lorem ipsum dolor sit amet!', GETDATE())
		INSERT INTO [Post] VALUES (NEWID(), 2, 'Duis aute irure dolor in.', GETDATE())
		INSERT INTO [Post] VALUES (NEWID(), 3, 'Nemo enim ipsam voluptatem', GETDATE())
		INSERT INTO [Post] VALUES (NEWID(), 4, 'Ut enim ad minima veniam', GETDATE())
	END

END