CREATE PROCEDURE [dbo].[spCreateBlogPost]
	@Title nvarchar(1000),
	@Content nvarchar(max)
AS
BEGIN
	INSERT INTO BlogPost (Title, Content, PostDate, Status, CreatedDate)
    OUTPUT Inserted.Id
    VALUES (@Title, @Content, GETDATE(), 1, GETDATE())
END
