/*Vamos a crear un procedimiento almacenado para dar de alta un albaran*/

/*Codigo de prueba*/
DECLARE @v_salida smallint
exec PR_AltaAlbaran '20180002 ', @v_salida output
print @v_salida

/*Procedimiento*/
ALTER PROCEDURE PR_AltaAlbaran
@p_codPedido char(9),
@p_salida tinyint output
AS
DECLARE @V_Ccodalbaran tinyint,
		@v_fecha date
BEGIN
	/*Asignamos el utlimo codigo de albaran*/
	SET @V_Ccodalbaran=(select COUNT(CodAlbaran)+1 as CodAlbaran
						from proyecto.CabeceraAlbaran)

	/*Asignamos la fecha actual*/
	SET @v_fecha=CONVERT(date,getdate())
	
	/*Comprobamos si el pedido existe*/
	if not exists(select CodPedido
					from proyecto.CabeceraPedido
					where CodPedido=@p_codPedido)
		BEGIN
			SET @p_salida=-1
			RETURN @p_salida
	
		END
	/*Si el pedido existe damos de alta la cabecera del albaran*/
	else
		BEGIN
			BEGIN TRANSACTION
			BEGIN TRY
			IF NOT EXISTS(SELECT CodPedido
							FROM proyecto.CabeceraAlbaran
							WHERE CodPedido=@p_codPedido)
				BEGIN
					INSERT INTO proyecto.CabeceraAlbaran
					(CodAlbaran, CodPedido, Fecha, Estado) VALUES (@V_Ccodalbaran,@p_codPedido,@v_fecha,'P')
				END
				/*Vamos a realizar un cursor para que fila por fila realice una insert en detalles de albaran*/
				/*Declaramos el cursor*/

				DECLARE cur_FilasAlbaran CURSOR
				FOR SELECT CodPedido, N_Lineas, CodArticulo, Cantidad
				FROM proyecto.DetallePedido
				WHERE CodPedido=@p_codPedido

		

				/*Abrimos el cursor*/
				OPEN cur_FilasAlbaran

				DECLARE 
				@v_codalbaran tinyint,
				@V_codpedido char(9),
				@v_codarticulo char(6),
				@v_cantidad tinyint,
				@v_nlinea tinyint

				SET @v_codalbaran=@V_Ccodalbaran

				/*Recorremos el cursor*/
				FETCH NEXT FROM cur_FilasAlbaran INTO @v_codpedido, @v_nlinea, @v_codarticulo, @v_cantidad
				WHILE @@FETCH_STATUS=0

					BEGIN
						BEGIN
							INSERT INTO proyecto.DetalleAlbaran
							(CodAlbaran, CodPedido, NLineas, CodArticulo, Cantidad) VALUES (@v_codalbaran, @V_codpedido, @v_nlinea, @v_codarticulo, @v_cantidad)
							print(@v_nlinea)
						END	
						FETCH NEXT FROM cur_FilasAlbaran INTO @v_codpedido, @v_nlinea, @v_codarticulo, @v_cantidad
					END
					CLOSE cur_FilasAlbaran
					DEALLOCATE cur_FilasAlbaran

			COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION
					SET @p_salida=@@ERROR
					RETURN @p_salida
			END CATCH
					SET @p_salida=0
					RETURN @p_salida
			END								
END