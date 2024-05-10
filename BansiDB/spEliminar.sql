CREATE PROCEDURE [dbo].[spEliminar]
	@id int
AS
BEGIN
	IF EXISTS (SELECT IdExamen FROM tblExamen WHERE IdExamen=@id)
		BEGIN
		BEGIN TRANSACTION 
			BEGIN TRY
			DELETE FROM tblExamen WHERE IdExamen=@id;
			SELECT 0 AS 'CodigoRetorno', 'Registro eliminado satisfactoriamente' AS 'DescripcionRetorno';
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