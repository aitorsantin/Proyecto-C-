/*Procedimiento almacenado que se usa para almacenar los articulos que hemos solicitado al distribuidor
Sirve para simular una compra*/

/*Codigo de Prueba*/
EXEC PR_SolicitudDeArticulos 'ART016', 10


/*Procedimiento almacenado*/
CREATE PROCEDURE PR_SolicitudDeArticulos
@P_codArticulo char(6),
@p_cantidad smallint
AS
DECLARE @v_nSolicitud tinyint,
		@v_FechaOperacion date
BEGIN
	SET @v_FechaOperacion=CONVERT(date,GETDATE())

	SET @v_nSolicitud=(SELECT COUNT(NSolicitud)+1
						FROM proyecto.P_ArticulosPendientes
						WHERE CodArticulo=@P_codArticulo AND Fecha=@v_FechaOperacion)

	BEGIN TRY
		INSERT INTO proyecto.P_ArticulosPendientes
		(CodArticulo,NSolicitud, Cantidad,Fecha )
		VALUES
		(@P_codArticulo, @v_nSolicitud, @p_cantidad, @v_FechaOperacion)
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH
END