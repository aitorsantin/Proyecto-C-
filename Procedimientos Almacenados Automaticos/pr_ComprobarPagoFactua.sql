/*Este procedimiento se ejecutara todos los días a las 00:00 horas y revisara las facturas que no estan pagadas, comprovando que 
la fecha en la que se genero dicho pedido no supere en 15 dias a la fecha actual*/

/*Codigo de prueba*/
EXEC pr_ComprobarPagoFactua
/*Codigo de procedimiento*/
ALTER PROCEDURE pr_ComprobarPagoFactua
AS
BEGIN
	BEGIN TRANSACTION
	BEGIN TRY

		DECLARE cur_EvaluarPagoFactura cursor
		FOR SELECT CodPedido, Fecha, Pagado, CodCliente, Estado
			FROM proyecto.CabeceraPedido

		OPEN cur_EvaluarPagoFactura

		DECLARE @v_codpedido char(9),
				@v_fecha date,
				@v_pagado char(1),
				@v_factual date,
				@v_codcliente char(6),
				@v_tipocliente char(1),
				@v_estado char(1)

		FETCH NEXT FROM cur_EvaluarPagoFactura INTO @v_codpedido, @v_fecha, @v_pagado, @v_codcliente, @v_estado
		WHILE @@FETCH_STATUS=0
		BEGIN
			BEGIN
				/*Asignamos la fecha actual*/
				SET @v_factual=CONVERT(date, GETDATE())

				/*Cpmprovamos que la fecha actual no supere en 15 días a la fecha en la que se generó el pedido*/
				IF( DATEDIFF(DAY,@v_fecha,@v_factual )>15)
				BEGIN
					
					/*Comprobamos que el cliente no sea premium*/
					SET @v_tipocliente=(SELECT TipoCliente
										FROM proyecto.Clientes
										WHERE CodCliente=@v_codcliente)


					IF @v_tipocliente!='P'
					BEGIN

						/*Comprovamos que el pedido este pendiente*/
						SET @v_estado=(SELECT Estado
										FROM proyecto.CabeceraPedido
										WHERE CodPedido=@v_codpedido)
						
						IF @v_estado='P'
						BEGIN
							/*En caso afirmativo cancelamos el pedido el cliente tendra que volver a solicitar*/
							UPDATE proyecto.CabeceraPedido
							SET Estado='K'
							WHERE CodPedido=@v_codpedido
							END
						END
				END
			END

			FETCH NEXT FROM cur_EvaluarPagoFactura INTO @v_codpedido, @v_fecha, @v_pagado, @v_codcliente, @v_estado
		END

		CLOSE cur_EvaluarPagoFactura
		DEALLOCATE cur_EvaluarPagoFactura

	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END