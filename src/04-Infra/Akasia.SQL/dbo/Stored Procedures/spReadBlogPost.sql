CREATE PROCEDURE [dbo].[spReadBlogPost]
AS
BEGIN
	SELECT * FROM BlogPost nolock
END