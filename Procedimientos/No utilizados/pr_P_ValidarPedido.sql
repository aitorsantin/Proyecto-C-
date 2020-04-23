exec pr_P_ValidarPedido

--Queremos validar los pedidos que se encuentran pendientes, Esto se realiza comprobando si hay suficiente stock para cada
--De las lineas de pedido
--Recorreremos cada una de las lineas de pedido y comprobaremos que hay suficiente unidades de cada uno de los productos

CREATE PROCEDURE pr_P_ValidarPedido
AS
BEGIN
	DECLARE @v_codpedido char(9), @v_nlineas tinyint, @v_codarticulo char(6), @v_cantidad tinyint, @v_precio smallmoney
	DECLARE @v_resto tinyint 

	DECLARE cur_ValidarPedido CURSOR
	FOR SELECT CodPedido, N_Lineas, CodArticulo,Cantidad, PrecioUnitario
		FROM proyecto.DetallePedido

	OPEN cur_ValidarPedido

	FETCH NEXT FROM cur_ValidarPedid INTO @v_codpedido, @v_nlineas, @v_codarticulo,@v_cantidad, @v_precio
	
	WHILE @@FETCH_STATUS=0
		BEGIN
		--Si la cantidad es mayor que el stock actualizaremos el pedido y lo enviaremos con la cantidad que dispongamos
		--Insetaremos un nuevo pedido con la cantidad que falta y se quedara pendiente de recibir el stock
		if(@v_cantidad>(SELECT Stock 
						FROM proyecto.Articulos
						WHERE CodArticulo=@v_codarticulo))
		BEGIN
			--Almacenamos la diferencia que sera lo que vamos a pedir
			SET @v_resto=@v_cantidad-(SELECT Stock 
							FROM proyecto.Articulos
							WHERE CodArticulo=@v_codarticulo)
		
		

			--Actualizamos la linea de pedido 
			UPDATE proyecto.DetallePedido
			SET Cantidad=(SELECT Stock 
							FROM proyecto.Articulos
							WHERE CodArticulo=@v_codarticulo)
			WHERE CodArticulo=@v_codarticulo

			--Cambiamos el estado del pedido a activo
			UPDATE proyecto.CabeceraPedido
			SET Estado='A'
			WHERE CodPedido=@v_codpedido

			--INSERTAMOS UN NUEVO PEDIDO
			INSERT INTO proyecto.CabeceraPedido () VALUES ()

			--INSERTAMOS LAS LINEAS DE PEDIDO CON LA CANTIDAD IGUAL A @v_resto
		
		END
		
		if(@v_cantidad<=(SELECT Stock 
						FROM proyecto.Articulos
						WHERE CodArticulo=@v_codarticulo))


	CLOSE cur_ValidarPedido
	DEALLOCATE cur_ValidarPedido	
	END	
END