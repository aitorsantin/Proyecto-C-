/*Para poder pasar nuestro pedido del estado de pendiente a activado necesitamos revisar si tenemos suficiente stock para 
poder mandar el pedido al cliente*/
DECLARE @v_salida smallint
EXEC PR_ActivarPedidos '20180004',@v_salida output
print @v_salida

ALTER PROCEDURE PR_ActivarPedidos

@p_codpedido char(9),
@p_salida smallint output

AS
BEGIN
	
	BEGIN TRANSACTION
	BEGIN TRY

		DECLARE cur_EvaluarCantidad CURSOR
		FOR SELECT CodPedido,N_Lineas, CodArticulo,Cantidad, PrecioUnitario
		fROM proyecto.DetallePedido
		WHERE CodPedido=@p_codpedido


		OPEN cur_EvaluarCantidad

		DECLARE 
				@v_codpedido char(9),
				@v_codarticulo char(6),
				@V_nlinea tinyint,
				@v_cantidad smallint,
				@v_resultado smallint,
				@v_resto smallint,
				@v_stock smallint,
				@V_contador tinyint,
				@v_precio smallmoney,
				@v_restante smallint,
				@v_sumador smallint,
				@v_nentrada tinyint

				set @V_contador=1	

			
				/*Vamos a ir evaluando cada uno de los articulos del cliente*/
					FETCH NEXT FROM cur_EvaluarCantidad INTO @v_codpedido, @V_nlinea, @v_codarticulo, @v_cantidad, @v_precio
						WHILE @@FETCH_STATUS=0
						BEGIN

				
							/*Evaluaremos si el stock que tenemos es mayor que la cantidad del pedido*/

							SET @v_stock=(select Stock
								from proyecto.Articulos
								WHERE CodArticulo=@v_codarticulo)

								IF @v_stock>=@v_cantidad
								BEGIN
										SET @v_resultado= @v_stock-@v_cantidad
							
										/*Actualizaremos el articulo de la tabla de Articulos*/
										UPDATE proyecto.Articulos
										SET Stock=@v_resultado
										where CodArticulo=@v_codarticulo

										set @p_salida=1

										/*Contamos el numero de entrada*/

										SET @v_nentrada=(SELECT COUNT(NumeroEntradas)+1
															from proyecto.MoviAlmacen
															WHERE FechaOperacion=CONVERT(date,GETDATE()) AND
															CodArticulo=@v_codarticulo)

									/*INSERTAMOS UNA NUEVA LINEA EN LA TABLA MOVIMIENTOS ALMACEN*/
									INSERT INTO proyecto.MoviAlmacen
									(CodArticulo, FechaOperacion,NumeroEntradas, Cantidad, CantidadInicial,
									 Precio, EntradaSalida, Agotados, EstadoArticulo) 
									VALUES 
									(@v_codarticulo, CONVERT(date,GETDATE()),@v_nentrada, @v_cantidad, @v_stock,
									 @v_precio, 'S', 0,'N')
								END
								ELSE 
									BEGIN
											SET @v_resto=@v_cantidad-@v_stock
											SET @v_restante= @v_cantidad-@v_resto

											/*Actualizamos la cantidad del articulo en la tabla DetallesPedido*/

											UPDATE proyecto.DetallePedido
											SET Cantidad=@v_restante
											WHERE CodPedido=@v_codpedido AND CodArticulo=@v_codarticulo

											/*Poner a 0 la cantidad del articulo en la tabla Articulos*/
											UPDATE proyecto.Articulos
											SET Stock=0
											WHERE CodArticulo=@v_codarticulo

											/*Insertar en la tabla pendientes la linea de pedido con la cantidad del resto para realizar un nuevo pedido*/
											INSERT INTO proyecto.LineasPendientes
											(CodPedido, N_Lineas, CodArticulo, Cantidad, PrecioUnitario)
											VALUES
											(@v_codpedido,@V_contador, @v_codarticulo, @v_resto, @v_precio)

											/*Para poder contar cada linea que insertamos en LineasPendientes*/
											/*Creamos este estilo de contador*/
											SET @v_sumador=@V_contador+1
											SET @V_contador=@v_sumador

											SET @v_nentrada=(SELECT COUNT(NumeroEntradas)+1
															from proyecto.MoviAlmacen
															WHERE FechaOperacion=CONVERT(date,GETDATE()) AND
															CodArticulo=@v_codarticulo)

											/*INSERTAMOS UNA NUEVA LINEA EN LA TABLA MOVIMIENTOS ALMACEN*/

											INSERT INTO proyecto.MoviAlmacen
											(CodArticulo, FechaOperacion,NumeroEntradas, Cantidad, CantidadInicial,
											 Precio, EntradaSalida, Agotados, EstadoArticulo) 
											VALUES 
											(@v_codarticulo, CONVERT(date,GETDATE()),@v_nentrada, @v_stock, @v_stock,
											 @v_precio, 'S', 1,'N')

								
											set @p_salida=2
									END
							FETCH NEXT FROM cur_EvaluarCantidad INTO @v_codpedido, @V_nlinea, @v_codarticulo, @v_cantidad, @v_precio
						END
			
	/*Actualizamos el pedido poniendo el estado a Activo*/
		UPDATE proyecto.CabeceraPedido
		SET Estado='A'
		WHERE CodPedido=@p_codpedido
		
		CLOSE cur_EvaluarCantidad
		DEALLOCATE cur_EvaluarCantidad	
	COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
		ROLLBACK TRANSACTION
				SET @p_salida=@@ERROR
				RETURN @p_salida
		END CATCH
			return @p_salida
END