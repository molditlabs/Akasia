CREATE PROCEDURE [dbo].[spUpdateBlogPost]
	@Id int,
	@Title nvarchar(1000),
	@Content nvarchar(max)
AS
BEGIN
	UPDATE BlogPost
	SET Title = @Title, Content = @Content, PostDate = GETDATE(), Status = 1, CreatedDate = GETDATE()
	WHERE Id = @Id
END
