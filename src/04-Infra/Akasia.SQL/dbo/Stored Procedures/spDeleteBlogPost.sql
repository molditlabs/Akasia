CREATE PROCEDURE [dbo].[spDeleteBlogPost]
	@Id int
AS
BEGIN
	UPDATE BlogPost
	SET IsDeleted = 1
	WHERE Id = @Id
END
