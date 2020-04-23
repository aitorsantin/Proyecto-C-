/*Procedimiento para dar de alta la cabecera y lineas de la factura*/

/*Codigo de prueba*/
DECLARE @v_salida smallint
exec pr_GenerarFactura '20180002 ', @v_salida output
print @v_salida
/*Procedimiento*/

ALTER PROCEDURE pr_GenerarFactura
@p_codPedido char(9),
@p_salida tinyint output
AS
	DECLARE @v_codfactura char(9),
		@v_fecha date,
		@v_pagado char(1),
		@v_año char(4),
		@V_cero char(5),
		@v_estado char(1),
		@v_codlibro tinyint,
		@v_SubTotal smallmoney,
		@v_iva float,
		@v_total smallmoney,
		@V_codpedido char(9),
		@v_codarticulo char(6),
		@v_cantidad tinyint,
		@v_nlinea tinyint,
		@v_precio smallmoney,
		@v_resultado smallmoney
BEGIN

		/*Generamos un nuevo codigo de factura*/
		SET @v_año=(SELECT YEAR(GETDATE()))

		SET @V_cero='0001'


		SET @v_codfactura=CONCAT(@v_año, @V_cero)

		/*Si ya existe la primera factura del año*/
		IF EXISTS (SELECT CodPedido
			FROM proyecto.CabeceraFactura
			WHERE CodFactura=@v_codfactura)
			BEGIN
				SET @v_codfactura=(SELECT MAX(CodFactura)+1
									FROM proyecto.CabeceraFactura)
			END

		ELSE
			BEGIN
				SET @v_codfactura=@v_año+@V_cero
			END

		/*Asignamos la fecha actual*/
		SET @v_fecha=CONVERT(date,getdate())


		/*Damos el valor al pago*/

		SET @v_pagado=(SELECT Pagado
						FROM proyecto.CabeceraPedido
						WHERE CodPedido=@p_codPedido)

		/*Comprobamos si el pedido existe*/
		if not exists(select CodPedido
					from proyecto.CabeceraPedido
					where CodPedido=@p_codPedido)
		BEGIN
			SET @p_salida=-1
			RETURN @p_salida
	
		END
		ELSE
		BEGIN
			BEGIN TRANSACTION
				BEGIN TRY

				/*Si el pedido existe damos de alta la cabecera de la factura*/

				/*Evaluamos si esta pagado el pedido*/
				IF @v_pagado='N'
				BEGIN
					SET @v_estado='P'

					INSERT INTO proyecto.CabeceraFactura
					(CodFactura, CodPedido, Fecha, Estado, Pagado)
					VALUES
					(@v_codfactura, @p_codPedido, @v_fecha, @v_estado, @v_pagado)
					SET @p_salida=1	
				END

				ELSE 
				BEGIN

					SET @v_estado='C'

					INSERT INTO proyecto.CabeceraFactura
					(CodFactura, CodPedido, Fecha, Estado, Pagado)
					VALUES
					(@v_codfactura, @p_codPedido, @v_fecha, @v_estado, @v_pagado)
					SET @p_salida=2
				END
				/*Vamos a realizar un cursor para que fila por fila realice una insert en detalles de factura*/
				/*Declaramos el cursor*/
				DECLARE cur_FilasFactura CURSOR
				FOR SELECT CodPedido, N_Lineas, CodArticulo,PrecioUnitario, Cantidad
				FROM proyecto.DetallePedido
				WHERE CodPedido=@p_codPedido

		

				/*Abrimos el cursor*/
				OPEN cur_FilasFactura

				SET @v_SubTotal=0

				/*Recorremos el cursor*/
				FETCH NEXT FROM cur_FilasFactura INTO @v_codpedido, @v_nlinea, @v_codarticulo, @v_cantidad, @v_precio
				WHILE @@FETCH_STATUS=0

					BEGIN
							
						INSERT INTO proyecto.LineasFatura
						(CodFactura, CodPedido, NLineas, CodArticulo, Precio, Unidades)
						values
						(@v_codfactura, @v_codpedido, @v_nlinea, @v_codarticulo,@v_precio, @v_cantidad)

						FETCH NEXT FROM cur_FilasFactura INTO @v_codpedido, @v_nlinea, @v_codarticulo, @v_cantidad, @v_precio

						SET @v_resultado=@v_precio*@v_cantidad
						SET @v_SubTotal+= @v_resultado
					END

			CLOSE cur_FilasFactura
			DEALLOCATE cur_FilasFactura

			/*Creamos el codigo del libro de facturas emitidas*/

			SET @v_codlibro=(SELECT COUNT(CodLibro)+1
							FROM proyecto.Libro_Fact_Emi)

			SET @v_iva=0.21

			SET @v_total=@v_SubTotal*@v_iva+@v_SubTotal

			/*Por Ultimo insertamos en el libro de Facturas Emitidas la factura*/
			INSERT INTO proyecto.Libro_Fact_Emi
			(CodLibro, CodPedido, CodFactura, Fecha, SubTotal, IVA, Total)
			VALUES
			(@v_codlibro, @V_codpedido, @v_codfactura, @v_fecha, @v_SubTotal, @v_iva, @v_total)
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
				SET @p_salida=@@ERROR
				PRINT @p_salida
		END CATCH
END
			
	



END