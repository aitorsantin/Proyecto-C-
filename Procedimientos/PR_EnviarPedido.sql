/*Procedimiento que actualiza el estado del pedido  albran y factura a enviados*/


/*Codigo de prueba*/
DECLARE @v_salida tinyint
exec PR_EnviarPedido '20180001', @v_salida output
return @v_salida

/*Codigo del procedimiento*/
CREATE PROCEDURE PR_EnviarPedido
@p_codpedido char(9),
@p_salida tinyint output
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
			/*Actualizamos el pedido el albaran y la factura a enviados*/
			UPDATE proyecto.CabeceraPedido 
			SET Estado = 'E'
			WHERE CodPedido = @p_codpedido

			UPDATE proyecto.CabeceraAlbaran
			SET Estado='E'
			WHERE CodPedido = @p_codpedido

			  UPDATE proyecto.CabeceraFactura
			  SET Estado='E'
			   WHERE CodPedido = @p_codpedido

			   SET @p_salida=1
			   
		COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			SET @p_salida=@@ERROR
		END CATCH
	RETURN @p_salida
END