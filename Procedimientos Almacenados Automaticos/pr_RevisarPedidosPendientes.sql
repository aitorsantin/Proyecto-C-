/*Este procedimiento se ejecutara automaticamente todos los dias*/
/*Se utilizara para revisar el stock de los articulos ya que si tenemos un pedido pendiente de que entre nuevas unidades*/
/*Esto nos lo mirara todos los días y el día que se reponga el pedido pendiente se eliminara y se generara un nuevo pedido*/
/*Dicho pedido tendra preferencia de salida ya que se le asignara la misma fecha que obtuvo su pedido*/

EXEC pr_RevisarPedidosPendientes

ALTER PROCEDURE pr_RevisarPedidosPendientes
AS
DECLARE @v_codarticulo char(6),
					@v_cantidad smallint,
					@v_fecha date,
					@v_año char(4),
					@V_cero char(5),
					@v_pendiente char(9),
					@v_codEmpleado tinyint,
					@v_codCliente char(6),
					@v_pagado char(1),
					@V_contador tinyint,
					@v_precio smallmoney,
					@v_nlinea tinyint,
					@v_codpedido char(9),
					@v_stock smallint,
					@v_resultado smallint
					
			
BEGIN
	BEGIN TRANSACTION
	BEGIN TRY

		DECLARE cur_RevisarLineasPendientes CURSOR
		FOR SELECT CodPedido,N_Lineas, CodArticulo, Cantidad, PrecioUnitario
		FROM proyecto.LineasPendientes


		OPEN cur_RevisarLineasPendientes

			

					set @V_contador=0

		FETCH NEXT FROM cur_RevisarLineasPendientes INTO @v_pendiente,@v_nlinea, @v_codarticulo, @v_cantidad, @v_precio
		WHILE @@FETCH_STATUS=0

		BEGIN
			BEGIN
				/*Evaluaremos si el stock del articulo tiene la cantidad suficiente*/

				if (SELECT Stock
					FROM proyecto.Articulos
					WHERE CodArticulo=@v_codarticulo)>=@v_cantidad
					BEGIN
						SET @V_contador+=1
					END
				ELSE
					BEGIN
						SET @V_contador+=-1
					END
			END			
			FETCH NEXT FROM cur_RevisarLineasPendientes INTO @v_pendiente,@v_nlinea, @v_codarticulo, @v_cantidad, @v_precio
		END
		
		BEGIN
			IF @V_contador=@v_nlinea
			BEGIN
					/*obtenemos la fecha del pedido*/
					SET @v_fecha=(SELECT Fecha
								FROM proyecto.CabeceraPedido
								WHERE CodPedido=@v_pendiente)

				/*Generamos un nuevo codigo de pedido*/
				SET @v_año=(SELECT YEAR(GETDATE()))

				SET @V_cero='0001'


				SET @v_codpedido=CONCAT(@v_año, @V_cero)

				/*Si ya existe la primera factura del año*/
				IF EXISTS (SELECT CodPedido
					FROM proyecto.CabeceraPedido
					WHERE CodPedido=@v_codpedido)
					BEGIN
						SET @v_codpedido=(SELECT MAX(CodPedido)+1
											FROM proyecto.CabeceraPedido)
					END

					ELSE
					BEGIN
						SET @v_codpedido=@v_año+@V_cero
					END
				/*obtenemos el codigo del cliente*/
				SET @v_codCliente=(select CodCliente
									from proyecto.CabeceraPedido
									where CodPedido=@v_pendiente)

				/*Obtenemos el codigo del empleado*/
				SET @v_codEmpleado=(select CodEmpleado
									from proyecto.CabeceraPedido
									where CodPedido=@v_pendiente)
			
				/*Vemos si esta pagado o no*/
				SET @v_pagado=(select Pagado
									from proyecto.CabeceraPedido
									where CodPedido=@v_pendiente)

				/*Insertar un nuevo pedido como ya pasa automaticamente la revision del stock el estado sera activo*/
				INSERT INTO proyecto.CabeceraPedido
				(CodPedido, CodEmpleado, CodCliente, Fecha, Estado, Pagado)
				VALUES
				(@v_codpedido, @v_codEmpleado, @v_codCliente, @v_fecha, 'A', @v_pagado)

				PRINT @v_codpedido
			END
		END

	DECLARE cur_InsertarLineasPedido CURSOR
		FOR SELECT CodPedido,N_Lineas, CodArticulo, Cantidad, PrecioUnitario
		FROM proyecto.LineasPendientes


		OPEN cur_InsertarLineasPedido

		DECLARE @V_codigo char(9),
				@v_codArt char(6),
				@v_cant smallint,
				@v_n tinyint,
				@v_pre smallmoney

				
		FETCH NEXT FROM cur_InsertarLineasPedido INTO @V_codigo,@v_n, @v_codArt, @v_cant, @v_pre
		WHILE @@FETCH_STATUS=0
		BEGIN
			BEGIN
					IF EXISTS (SELECT CodPedido
							FROM proyecto.CabeceraPedido
							WHERE CodPedido=@v_codpedido)
				BEGIN
							
					/*Insertamos las lineas de pedido*/
					INSERT INTO proyecto.DetallePedido
					(CodPedido, N_Lineas, CodArticulo, Cantidad, PrecioUnitario)
					VALUES
					(@v_codpedido, @v_n, @v_codArt, @v_cant, @v_pre)

					PRINT @v_codArt

					/*Actualizamos el stock de la tabla articulos*/
					SET @v_stock=(SELECT Stock
									FROM proyecto.Articulos
									WHERE CodArticulo=@v_codArt)

					SET @v_resultado=@v_stock-@v_cant

					UPDATE proyecto.Articulos
					SET Stock=@v_resultado
					WHERE CodArticulo=@v_codArt

					PRINT '3'

					/*Insertamos una nueva fila en la tabla moviAlmacen*/
					if @v_stock>@v_cant
					BEGIN
						INSERT INTO proyecto.MoviAlmacen
						(CodArticulo, FechaOperacion, Cantidad, CantidadInicial,
						Precio, EntradaSalida, Agotados, EstadoArticulo) 
						values
						(@v_codArt, CONVERT(date,GETDATE()), @v_cant, @v_stock, @v_pre, 'S', 0, 'N')

						PRINT '4'
					END

					ELSE IF @v_stock=@v_cant
					BEGIN
						INSERT INTO proyecto.MoviAlmacen
						(CodArticulo, FechaOperacion, Cantidad, CantidadInicial,
						Precio, EntradaSalida, Agotados, EstadoArticulo) 
						values
						(@v_codArt, CONVERT(date,GETDATE()), @v_cant, @v_stock, @v_pre, 'S', 1, 'N')

						PRINT '5'
					END
				END
				ELSE
				BEGIN
					PRINT '-1'
				END
			END
			FETCH NEXT FROM cur_InsertarLineasPedido INTO @V_codigo,@v_n, @v_codArt, @v_cant, @v_pre
		END
		CLOSE cur_InsertarLineasPedido
		DEALLOCATE cur_InsertarLineasPedido
		CLOSE cur_RevisarLineasPendientes
		DEALLOCATE cur_RevisarLineasPendientes


		/*Eliminamos las lineas de pedido*/
					DELETE proyecto.LineasPendientes
					WHERE CodPedido=@V_codigo

					PRINT '6'
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END



		

	

		