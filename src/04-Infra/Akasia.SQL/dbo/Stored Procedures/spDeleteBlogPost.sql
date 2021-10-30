CREATE PROCEDURE [dbo].[spDeleteBlogPost]
	@Id int
AS
BEGIN
	DELETE FROM BlogPost
	WHERE Id = @Id
END
