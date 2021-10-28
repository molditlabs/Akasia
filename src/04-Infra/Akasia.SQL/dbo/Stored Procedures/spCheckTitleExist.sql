CREATE PROCEDURE [dbo].[spCheckTitleExist]
	@Title nvarchar(1000)
AS
BEGIN
	SELECT * FROM BlogPost nolock 
	WHERE Title = @Title
END
