/*Creamos un procedimiento almacenado que nos eliminara todo el pedido en caso de que a la hora de dar de alta nos falle alguna de las 
lineas de pedido*/

/*Codigo de prueba*/
DECLARE @v_salida SMALLINT
EXEC PR_EliminarPedido '20190017 ',  @v_salida output
PRINT @v_salida
/*Codigo Procedimiento*/

alter PROCEDURE PR_EliminarPedido
@p_codpedido char(9),
@p_salida SMALLINT output

AS
	DECLARE @v_codpedido char(9),
			@v_codarticulo char(6),
			@v_nlinea tinyint
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY

		/*Declaramos el cursor*/

			DECLARE CUR_EliminarLineasPedido CURSOR
			FOR SELECT CodPedido
				FROM proyecto.DetallePedido
				WHERE CodPedido=@p_codpedido

				SET @v_codpedido=@p_codpedido

				/*Abrimos el cursor*/
				OPEN CUR_EliminarLineasPedido


				/*Recorremos las filasr*/
				FETCH CUR_EliminarLineasPedido INTO @v_codpedido
				BEGIN
					BEGIN
						DELETE FROM proyecto.DetallePedido
						WHERE CodPedido=@v_codpedido 
					END

					FETCH CUR_EliminarLineasPedido INTO @v_codpedido
				END
				
				CLOSE CUR_EliminarLineasPedido
				DEALLOCATE CUR_EliminarLineasPedido

				/*ELIMINAMOS LA CABECERA DEL PEDIDO*/

				DELETE FROM proyecto.CabeceraPedido
				WHERE CodPedido=@p_codpedido

				SET @p_salida=1

		COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			SET @p_salida=@@ERROR
		END CATCH

		RETURN @p_salida
END