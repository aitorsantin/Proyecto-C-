ALTER PROCEDURE proyecto.pr_NuevoPedido

--vARIABLES DE LA TABLA PEDIDOS
	@p_CodArticulo as char(6),
	@p_codpedido as char(9),
	@p_cantidad as smallint,
	@p_pagado char(1),
	@p_codCliente char(6),
	@p_salida smallint output
	
AS
DECLARE @v_cantidad as smallint
	DECLARE @v_codArticulo as char(6)
	declare @v_fecha as date
	DECLARE @v_agotado as char(1)
	DECLARE @v_estado as char(1)
	DECLARE @v_ES as char(1)
	DECLARE @V_resto as smallint
	DECLARE @v_precio as smallmoney
	DECLARE @v_total as smallmoney

BEGIN
--Si el cliente es nuevo o existente y no ha esta pagado
	IF (@p_codCliente=(SELECT CLI.CodCliente
		FROM proyecto.Clientes as Cli
		join proyecto.CabeceraPedido AS CP
		ON Cli.CodCliente=CP.CodCliente
		WHERE CLI.CodCliente='N' OR Cli.CodCliente='E' AND CP.Pagado='N'))
	BEGIN
	 SET @p_salida-=1
	END
--SI LA CANTIDAD DE LAS LINEAS DE PEDIDO ES MAYOR QUE LA SUMA DE LAS EXISTENCIAS DE ESE ARTICULO
	IF(SELECT SUM(Cantidad)
		FROM proyecto.MoviAlmacen
		WHERE CodArticulo=@p_CodArticulo)>@p_cantidad
	BEGIN
		--DECLARAMOS EL CURSOR PARA CALCULAR 
		DECLARE cur_movimientosAlmacen1 CURSOR
		FOR SELECT Cantidad
		FROM proyecto.MoviAlmacen
		WHERE CodArticulo=@v_codArticulo
		ORDER BY Cantidad DESC
	
		OPEN cur_movimientosAlmacen1

		BEGIN TRANSACTION
		BEGIN TRY

		SET @v_cantidad=(SELECT Cantidad
		FROM proyecto.MoviAlmacen
		WHERE CodArticulo=@v_codArticulo
		)

		FETCH NEXT FROM cur_movimientosAlmacen1 INTO @v_cantidad
		WHILE @@FETCH_STATUS=0
		BEGIN
			BEGIN
			--SI TENEMOS EXISTENCIAS SUFICIENTES TENEMOS QUE IR FILA POR FILA COMPROBANDO SI LA CANTIDAD DE EXISTENCIAS
			--DE ESA FILA ES MENOR O ES MAYOR
			SET @V_resto=@p_cantidad
			--SI ES MENOR
			IF @v_cantidad<@V_resto
			BEGIN
			--QUITAMOS A RESTO LA CANTIDAD DE ESA FILA
			SET @V_resto-=@v_cantidad
			--PONEMOS LAS EXISTENCIAS DE ESE PRODUCTO A 0
			UPDATE proyecto.MoviAlmacen
				SET Cantidad=0, Agotados=1
				WHERE CodArticulo=@v_codArticulo

			--Actualizamos el estado del articulo		
			UPDATE proyecto.CabeceraPedido
			SET Estado='A'
			WHERE CodPedido=@p_codpedido

			END
		--SI HAY MAS
			ELSE
			BEGIN
			--ACTUALIZAMOS LAS EXISTENCIAS DE ESA FILA RESTANDOLAS CON LAS EXISTENCIAS DEL PEDIDO
				SET @v_cantidad=@v_cantidad-@V_resto
				UPDATE proyecto.MoviAlmacen
				SET Cantidad=@v_cantidad
				WHERE CodArticulo=@v_codArticulo
				BREAK
			END
		-- INSERTAMOS UNA NUEVA FILA EN MOVIMIENTOS ALMACEN
				SET @v_precio=(Select TOP 1 Precio from proyecto.MoviAlmacen where CodArticulo=@v_codArticulo)

				INSERT INTO proyecto.MoviAlmacen
				(CodArticulo, 
				FechaOperacion, 
				Cantidad, 
				CantidadInicial,
				Precio,
				EntradaSalida,
				Agotados,
				EstadoArticulo)
				 VALUES 
				 (@v_codArticulo,
					CONVERT(date,GETDATE()),
					@v_cantidad,
					@v_cantidad,
					@v_precio,
					'S',
					'0',
					'N')
			--Actualizamos el estado del pedido
			UPDATE proyecto.CabeceraPedido
			SET Estado='A'
			WHERE CodPedido=@p_codpedido
			FETCH NEXT FROM cur_movimientosAlmacen1 INTO @v_cantidad
	END
	DEALLOCATE cur_ActualizarStock
	COMMIT TRANSACTION
			SET @p_salida=@@ERROR
			END
	END TRY
	BEGIN CATCH
			SET @p_salida=@@ERROR
			ROLLBACK TRANSACTION
	END CATCH
	END

	ELSE
	BEGIN
		BEGIN
		BEGIN
		DECLARE cur_movimientosAlmacen2 CURSOR
		FOR SELECT Cantidad
		FROM proyecto.MoviAlmacen
		WHERE CodArticulo=@v_codArticulo
		ORDER BY Cantidad DESC
	
		OPEN cur_movimientosAlmacen2
	
		BEGIN TRANSACTION
		BEGIN TRY

		SET @v_cantidad=(SELECT Cantidad
		FROM proyecto.MoviAlmacen
		WHERE CodArticulo=@v_codArticulo
		)

		FETCH NEXT FROM cur_movimientosAlmacen2 INTO @v_cantidad
		WHILE @@FETCH_STATUS=0
		BEGIN
			BEGIN
				SET @v_precio=(SELECT Precio FROM proyecto.MoviAlmacen WHERE CodArticulo=@v_codArticulo)
				--LA CANTIDAD DE LAS LINEAS DE PEDIDO ES MAYOR QUE LA SUMA DE LAS EXISTENCIAS DE ESE ARTICULO

				SET @V_resto=@p_cantidad
				--VAMOS A COMPROBAR CUANTAS UNIDADES PODEMOS MANDAR HASTA QUE SE AGOTEN
				SET @V_resto-=@v_cantidad
				--PONEMOS LAS EXISTENCIAS DE ESE PRODUCTO A 0
				UPDATE proyecto.MoviAlmacen
				SET Cantidad=0, Agotados=1
				WHERE CodArticulo=@v_codArticulo	
			END
			FETCH NEXT FROM cur_movimientosAlmacen2 INTO @v_cantidad
		DEALLOCATE cur_ActualizarStock
		COMMIT TRANSACTION
				SET @p_salida=@@ERROR
				END
		END TRY
		BEGIN CATCH
				SET @p_salida=@@ERROR
				ROLLBACK TRANSACTION
		END CATCH
		END
	END
	--ACTUALIZAMOS EL PEDIDO INDICANDO QUE LA CANTIDAD QUE SE VA A ENVIAR ES LA TOTALIDAD DE LAS 
				--EXISTENCIAS DE ESE PRODUCTO QUE DISPONEMOS EN EL ALMACEN
	UPDATE proyecto.DetallePedido
			SET Cantidad=@p_cantidad-@V_resto
			WHERE CodPedido=@p_codpedido AND CodArticulo=@p_CodArticulo	

			UPDATE proyecto.CabeceraPedido
			SET Estado='A'
			WHERE CodPedido=@p_codpedido

			-- INSERTAMOS UNA NUEVA FILA EN MOVIMIENTOS ALMACEN

			INSERT INTO proyecto.MoviAlmacen
			(CodArticulo, 
			FechaOperacion, 
			Cantidad, 
			CantidadInicial,
			Precio,
			EntradaSalida,
			Agotados,
			EstadoArticulo)
				VALUES 
				(@v_codArticulo,
				CONVERT(date,GETDATE()),
				@v_cantidad,
				@v_cantidad,
				@v_total,
				'S',
				'0',
				'N')
	END
END