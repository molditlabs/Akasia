CREATE PROCEDURE spReadBlogPostById
	@Id int
AS
BEGIN
	SELECT * FROM BlogPost nolock
	WHERE Id = @Id
END