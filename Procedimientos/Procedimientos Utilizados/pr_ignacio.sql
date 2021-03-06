USE [PRU_IgnacioGonzalo]
GO
/****** Object:  StoredProcedure [OE].[pa_ImportaPedido]    Script Date: 08/11/2018 10:00:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [OE].[pa_ImportaPedido] 
	@IdPed smallint,
    @Fecha smalldatetime,
	@IdCliente int,
	@IdOperador int,
	@lineas importPedLins READONLY,
	@resultado int output,
    @resPedido varchar(250) output
AS

BEGIN
	DECLARE @IdPedido int, @contadorLin int = 0
	DECLARE @salida TABLE (CodProd smallint, err varchar(250))
	
	-- Cursor que recupera las lineas del pedido
	DECLARE lecLin CURSOR FOR
		SELECT CodProd,Precio,Cantidad FROM @lineas
    --
	BEGIN TRANSACTION
	BEGIN TRY		
		IF @IdPed is null or @Fecha is null or
			@IdCliente is null or @IdOperador is null
			Raiserror('Faltan datos del pedido.', 16,1)

		If exists(Select ORDER_ID from oe.ORDERS Where ORDER_ID = @IdPed)
				Raiserror('Pedido existente.', 16,1)
		ELSE
			INSERT INTO oe.ORDERS ([ORDER_ID]
								  ,[ORDER_DATE]
								  ,[ORDER_MODE]
								  ,[CUSTOMER_ID]
								  ,[ORDER_STATUS]
								  ,[ORDER_TOTAL]
								  ,[SALES_REP_ID]
								  ,[PROMOTION_ID])
             VALUES (@IdPed,
					 @Fecha,
					 null,
					 @IdCliente,
					 null,
					 0,
					 @IdOperador,
					 null)		
	
		OPEN lecLin
		DECLARE     @CodProd smallint,
					@Precio decimal(8,2),
					@Cantidad smallint
		
		DECLARE @TOTAL decimal(8,2)
		SET @TOTAL = 0

		FETCH NEXT FROM lecLin INTO
					@CodProd,
					@Precio,
					@Cantidad
		WHILE @@FETCH_STATUS = 0
		BEGIN
			BEGIN TRY
				If @Cantidad = 0
					Raiserror('La cantidad es cero.', 16,1)

				If not exists(Select PRODUCT_ID From oe.PRODUCT_INFORMATION Where PRODUCT_ID = @CodProd)
					Raiserror('No existe el codigo de producto en la tabla oe.PRODUCT_INFORMATION', 16,1)

				-- Control de suficiencia de Stocks
				If @Cantidad > (SELECT SUM(ISNULL(QUANTITY_ON_HAND,0))
								FROM oe.INVENTORIES
								WHERE PRODUCT_ID = @CodProd)
					Raiserror('No hay suficiente stock.', 16,1)
				
				set @TOTAL = @TOTAL + (@Cantidad * @Precio)

				DECLARE lecWareH CURSOR FOR
					SELECT QUANTITY_ON_HAND 
					FROM oe.INVENTORIES
					WHERE PRODUCT_ID = @CodProd
					ORDER BY QUANTITY_ON_HAND DESC
				FOR UPDATE OF QUANTITY_ON_HAND

				OPEN lecWareH

				DECLARE @QUANTITY_ON_HAND smallint
				DECLARE @CONT smallint
				
				SET @CONT = @Cantidad
				
				FETCH NEXT FROM lecWareH INTO @QUANTITY_ON_HAND

				WHILE @@FETCH_STATUS = 0
				BEGIN
					IF @CONT <= @QUANTITY_ON_HAND 
					begin
						UPDATE oe.INVENTORIES 
						SET QUANTITY_ON_HAND = QUANTITY_ON_HAND - @CONT  
						WHERE CURRENT OF lecWareH
						BREAK
					end
					else
					begin
						UPDATE oe.INVENTORIES SET QUANTITY_ON_HAND = 0 
						WHERE CURRENT OF lecWareH
						SET @CONT = @CONT - @QUANTITY_ON_HAND
					end
					FETCH NEXT FROM lecWareH INTO
						@QUANTITY_ON_HAND
				END

				CLOSE lecWareH
				DEALLOCATE lecWareH

				INSERT INTO oe.ORDER_ITEMS([ORDER_ID]
										  ,[LINE_ITEM_ID]
										  ,[PRODUCT_ID]
										  ,[UNIT_PRICE]
										  ,[QUANTITY])
				VALUES (@IdPed,
						(Select isnull(Max(LINE_ITEM_ID),0)+1
						 From oe.ORDER_ITEMS
						 Where ORDER_ID = @IdPed),
						@CodProd,
						@Precio,
						@Cantidad
						)
			END TRY
			BEGIN CATCH
				INSERT INTO @salida(CodProd,err) VALUES 
				(@CodProd,ERROR_MESSAGE())
			END CATCH
			
			SET @contadorLin = @contadorLin +1

			FETCH NEXT FROM lecLin INTO
						@CodProd,
						@Precio,
						@Cantidad
		END
		CLOSE lecLin

		IF exists(SELECT CodProd FROM @salida)
			Raiserror('Error(es) en lineas de pedido.', 16,1)

		UPDATE oe.ORDERS SET ORDER_TOTAL = @TOTAL
		WHERE ORDER_ID = @IdPed

		COMMIT TRANSACTION
		SET @resultado = @contadorLin
	END TRY
	BEGIN CATCH
		SET @resPedido = ERROR_MESSAGE()
		SET @resultado = -2
		ROLLBACK TRANSACTION				
	END CATCH

	IF CURSOR_STATUS('global','lecLin') = 1
		CLOSE lecLin

	DEALLOCATE lecLin

	IF exists(SELECT CodProd FROM @salida)
		SELECT CodProd,err FROM @salida
END
