CREATE PROCEDURE [dbo].[spReadAllBlogPost]
AS
BEGIN
	SELECT * FROM BlogPost nolock
END