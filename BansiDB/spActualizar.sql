CREATE PROCEDURE [dbo].[spActualizar]
	@id int,
	@nombre varchar(255),
	@descripcion varchar(255)
AS
BEGIN
	IF EXISTS (SELECT IdExamen FROM tblExamen WHERE IdExamen=@id)
		BEGIN
		BEGIN TRANSACTION 
			BEGIN TRY
			UPDATE tblExamen SET Nombre=@nombre, Descripcion=@descripcion WHERE IdExamen=@id;
			SELECT 0 AS 'CodigoRetorno', 'Registro actualizado satisfactoriamente' AS 'DescripcionRetorno';
			COMMIT TRANSACTION 
			END TRY
		BEGIN CATCH
			SELECT ERROR_NUMBER() AS 'CodigoRetorno', ERROR_MESSAGE() AS 'DescripcionRetorno';
			ROLLBACK TRANSACTION
		END CATCH
	END
	ELSE
	BEGIN
		SELECT 1 AS 'CodigoRetorno', 'El ID: '+RTRIM(@id)+' NO existe.' AS 'DescripcionRetorno';
	END
END