/*Este procedimiento evalua semanalmente todos las ventas de los articulos para calcular cual tiene que ser su stock minimo
maximo y medio para que en ningun momento tengamos demasiadas unidades en stock que son unidades que se pueden perder ni tener poco
stock y quedarse sin poder servir a los clientes*/

/*El stock minimo es el calculo de las unidades venidas en la semana de lunes a viernes por el tiempo que nos tardan en servir a nosotros
7 dias por el tiempo que nosotros tardamos en servir al cliente 2 dias*/

/*El stock maximo es el stock minimo por los dias que tardamos en servir a los clientes 2 días*/

/*El stock medio es la suma de el minimo y el maximo entre 2*/

/*Codigo de prueba*/
EXEC PR_Stock

/*Codigo del procedimiento*/
ALTER PROCEDURE PR_Stock
AS	
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY
				
				DECLARE cur_ActualizarStock CURSOR
				FOR SELECT CodArticulo, Stock_Minimo, Stock_Maximo, Stock_Medido
					FROM proyecto.Articulos

					OPEN cur_ActualizarStock

					DECLARE @v_codarticulo char(6),
							@v_minimo smallint,
							@v_maximo smallint,
							@v_medio smallint

				 FETCH cur_ActualizarStock INTO @v_codarticulo, @v_minimo, @v_maximo, @v_medio
				 WHILE @@FETCH_STATUS = 0
					BEGIN
						BEGIN

							SET @v_minimo=(SELECT (SUM(Cantidad)*7)*2 as total 
										FROM proyecto.DetallePedido AS D
										JOIN proyecto.CabeceraPedido AS C
										ON C.CodPedido=D.CodPedido
										WHERE Fecha BETWEEN dateadd(week, datediff(week, 0, getdate()), 0) AND DATEADD(wk,DATEDIFF(wk,0,GETDATE()),6) and CodArticulo=@v_codarticulo
										group by CodArticulo)
								
							IF NOT EXISTS (SELECT CodArticulo
										FROM proyecto.DetallePedido AS D
										JOIN proyecto.CabeceraPedido AS C
										ON C.CodPedido=D.CodPedido
										WHERE Fecha BETWEEN dateadd(week, datediff(week, 0, getdate()), 0) AND DATEADD(wk,DATEDIFF(wk,0,GETDATE()),6) and CodArticulo=@v_codarticulo
										group by CodArticulo)
							begin
								print 'No esta'
							end
							else
							begin
								SET @v_maximo=(SELECT((SUM(Cantidad)*7)*2)*2 as total 
												FROM proyecto.DetallePedido AS D
												JOIN proyecto.CabeceraPedido AS C
												ON C.CodPedido=D.CodPedido
												WHERE Fecha BETWEEN dateadd(week, datediff(week, 0, getdate()), 0) AND DATEADD(wk,DATEDIFF(wk,0,GETDATE()),6)  and CodArticulo=@v_codarticulo
												group by CodArticulo)

								SET @v_medio=(SELECT((((SUM(Cantidad)*7)*2)*2)+(SUM(Cantidad)*7)*2)/2  as total 
												FROM proyecto.DetallePedido AS D
												JOIN proyecto.CabeceraPedido AS C
												ON C.CodPedido=D.CodPedido
												WHERE Fecha BETWEEN dateadd(week, datediff(week, 0, getdate()), 0) AND DATEADD(wk,DATEDIFF(wk,0,GETDATE()),6)  and CodArticulo=@v_codarticulo
												group by CodArticulo)


								UPDATE proyecto.Articulos
								SET Stock_Minimo=@v_minimo, Stock_Maximo=@v_maximo, Stock_Medido=@v_medio
								WHERE CodArticulo=@v_codarticulo
								END
								FETCH cur_ActualizarStock INTO @v_codarticulo, @v_minimo, @v_maximo, @v_medio
							end

						
					END
					CLOSE cur_ActualizarStock
					DEALLOCATE cur_ActualizarStock
		COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION
			END CATCH
END