CREATE PROCEDURE [dbo].[spIsBlogPostExist]
	@Id int
AS
BEGIN
	SELECT * FROM BlogPost nolock 
	WHERE Id = @Id
END
