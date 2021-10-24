CREATE PROCEDURE [dbo].[spIsBlogPostExist]
	@Title nvarchar(1000)
AS
	SELECT * FROM BlogPost nolock 
	WHERE Title = @Title
RETURN 0
