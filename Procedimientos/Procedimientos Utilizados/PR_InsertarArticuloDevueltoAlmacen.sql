/*Procedimiento almacenado que inserta una nueva entrada en el almacen por la devolucion de un articulo*/

/*Codigo de prueba*/
exec PR_InsertarArticuloDevueltoAlmacen 'ART001',20
/*Procedimiento Almacenado*/
CREATE PROCEDURE PR_InsertarArticuloDevueltoAlmacen
@p_codArticulo char(6),
@p_cantidad smallint
AS
DECLARE @v_nEntradas smallint,
		@v_canInicial smallint,
		@V_precio smallmoney
BEGIN
	BEGIN TRY
		SET @v_nEntradas=(SELECT COUNT(NumeroEntradas)+1
					FROM proyecto.MoviAlmacen
					WHERE FechaOperacion=CONVERT(date, GETDATE()))

	SET @v_canInicial=(SELECT  Stock
					FROM proyecto.Articulos
					WHERE CodArticulo=@p_codArticulo)

		SET @V_precio=(SELECT  PrecioCoste
						FROM proyecto.Articulos
						WHERE CodArticulo=@p_codArticulo)

		INSERT INTO proyecto.MoviAlmacen
		(CodArticulo, FechaOperacion, NumeroEntradas, Cantidad, CantidadInicial, Precio, EntradaSalida, Agotados, EstadoArticulo)
		VALUES
		(@p_codArticulo,CONVERT(date, GETDATE()), @v_nEntradas,  @p_cantidad, @v_canInicial, @V_precio, 'E', 0, 'D')
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH	
END



