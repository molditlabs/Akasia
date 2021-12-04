CREATE PROCEDURE [dbo].[spReadAllBlogPost]
AS
BEGIN
	SELECT * FROM BlogPost nolock
	WHERE IsDeleted = 0
END