/*Este procedimiento simula la reposicion de las existencias de los articulos del almacen,
a mi empresa los proveedores tardan unos 7 días en reponernos el stock, este procedimiento que se 
ejecuta de forma automatica todos los días a las 8 de la mañana revisa si los articulos solicitados su fecha es menor a 7 dias comparando
con la fecha actual en caso afirmativo realiza una insert en movimientos almacen y elimina la fila de dicha fecha,*/

/*Codigo de prueba*/
exec PR_ActualizarUnidadesArticulos 

/*Procedimiento almacenado*/
ALTER PROCEDURE PR_ActualizarUnidadesArticulos
AS
DECLARE @v_codArticulo char(6),
		@v_cantidad smallint,
		@v_fecha date,
		@v_nSolicitud tinyint,
		@v_dia int, 
		@v_fin int,
		@v_fechaMasSieteDias date,

		@v_fechaOperacion date,
		@v_numeroEntradas int,
		@v_canInicial smallint,
		@v_precio smallmoney,
		@v_cant smallint,
		@v_suma smallint
BEGIN
	BEGIN TRANSACTION
	BEGIN TRY

		DECLARE Cur_RecorrerPedidoArticulosPendientes CURSOR
		FOR SELECT CodArticulo, NSolicitud, Cantidad, Fecha
		FROM proyecto.P_ArticulosPendientes

		OPEN Cur_RecorrerPedidoArticulosPendientes

		FETCH NEXT FROM Cur_RecorrerPedidoArticulosPendientes 
		INTO @v_codArticulo, @v_nSolicitud,@v_cantidad, @v_fecha
		WHILE @@FETCH_STATUS=0
		BEGIN
				/*Si la fecha del articulo solicitado es mayor a 7 dias comparando con la fecha actual*/
				SET @v_fechaMasSieteDias=DATEADD(DAY,7,@v_fecha)

				IF @v_fechaMasSieteDias<CONVERT(date, GETDATE())
				BEGIN
					SET @v_fechaOperacion=CONVERT(date,GETDATE())

					SET @v_numeroEntradas=(SELECT COUNT(NumeroEntradas)+1
											FROM proyecto.MoviAlmacen
											WHERE FechaOperacion=@v_fechaOperacion)

					set @v_canInicial=(SELECT Stock
										FROM proyecto.Articulos
										where CodArticulo=@v_codArticulo)

					set @v_precio=(SELECT PrecioCoste
										FROM proyecto.Articulos
										where CodArticulo=@v_codArticulo)


					/*INSERTYAMOS NUEVA FILA EN MOVIMIENTOS ALMACEN*/
					INSERT INTO proyecto.MoviAlmacen
					(CodArticulo, FechaOperacion, NumeroEntradas, Cantidad, CantidadInicial, Precio, EntradaSalida, Agotados, EstadoArticulo)
					VALUES
					(@v_codArticulo, @v_fechaOperacion, @v_numeroEntradas, @v_cantidad, @v_canInicial, @v_precio, 'E', 0, 'N')

					DELETE FROM proyecto.P_ArticulosPendientes
					WHERE CodArticulo=@v_codArticulo AND Fecha=@v_fecha
				END
			FETCH NEXT FROM Cur_RecorrerPedidoArticulosPendientes 
			INTO @v_codArticulo, @v_nSolicitud,@v_cantidad, @v_fecha
		END
		CLOSE Cur_RecorrerPedidoArticulosPendientes
		DEALLOCATE Cur_RecorrerPedidoArticulosPendientes	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END