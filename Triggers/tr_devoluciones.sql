--Codigo de prueba
insert into proyecto.DetDevoluciones(CodDevolucion, CodAlbaran, CodPedido,NLineas,CodArticulo,Cantidad,Desechable)
VALUES('D000001','1','20180001','2','ART004','5','0')

--Hemos creado un trigger que cada vez que nos realicen una devolucion, si el articulo se encuentra
--en buen estado volvera al almacen. Este insertara una nueva fila con el codigo del articulo,
--y la fecha en que se tramito el traspaso de la tabla de devoluciones a la tabla MoviAlmacen

--Codigo trigger
USE DAM_AitorSantin
GO
--Creamos un trigger
CREATE TRIGGER proyecto.tr_devoluciones
ON proyecto.DetDevoluciones
AFTER UPDATE
AS
BEGIN
--Declaramos las variables necesarias
	DECLARE @v_CodArticulo char(6),@v_cantidad smallint, @V_desechable char(1),@v_fecha date, @v_precio money, @v_reacondicionado money='20', @v_resultado money, @v_catnecesaria smallint
	
	SELECT @v_CodArticulo=CodArticulo, @v_cantidad=cantidad, @V_desechable=Desechable
	FROM inserted
--Si el Articulo es desechable no se realizara ninguna accion
	IF(@V_desechable=-1)
	BEGIN
		PRINT 'Articulo desechable'
		RETURN
	END
--Si no lo es 
	IF(@V_desechable=1)
	BEGIN
--Si no tenemos un articulo Con el mismo codigo de articulo y la fecha sea igual a la fecha actual
		IF not EXISTS (select FechaOperacion 
						FROM MoviAlmacen 
						WHERE CodArticulo=@v_CodArticulo AND FechaOperacion=convert(date,GETDATE()))
		BEGIN

		SET @v_precio=(Select PrecioUnitario From proyecto.DetallePedido Where CodArticulo=@v_CodArticulo)
		SET	@v_resultado=@v_precio-((@v_precio*@v_reacondicionado)/100)
		--Insertamos el articulo devuelto
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
			(@v_CodArticulo,
				convert(date,GETDATE()),
				@v_cantidad,
				@v_cantidad,
				@v_resultado,
				'E',
				'0',
				'D')
		END
--Si nos devuelven varias veces el mismo producto el mismo día
		ELSE
		BEGIN
		SET @v_catnecesaria=(SELECT Cantidad FROM MoviAlmacen WHERE CodArticulo=@v_CodArticulo AND FechaOperacion=CONVERT(date,GETDATE()))
		SET @v_cantidad=@v_cantidad+@v_catnecesaria
		--Actualizamos las existencias
		UPDATE proyecto.MoviAlmacen	
		SET
		Cantidad=@v_cantidad
		WHERE CodArticulo=@v_CodArticulo and FechaOperacion=CONVERT(date,GETDATE())
		END
	END
END