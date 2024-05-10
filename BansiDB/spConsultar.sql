CREATE PROCEDURE [dbo].[spConsultar]
	@nombre varchar(255),
	@descripcion varchar(255)
AS
BEGIN
	SELECT IdExamen AS 'ID', Nombre, Descripcion FROM tblExamen 
	WHERE (NULLIF(@nombre,'') IS NULL OR Nombre LIKE '%'+@nombre+'%')
	AND (NULLIF(@descripcion,'') IS NULL OR Descripcion LIKE '%'+@descripcion+'%')
END