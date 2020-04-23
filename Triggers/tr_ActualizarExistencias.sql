--Codigo de prueba--
INSERT INTO proyecto.MoviAlmacen(CodArticulo,FechaOperacion,Cantidad,CantidadInicial,Precio,EntradaSalida,Agotados,EstadoArticulo)
VALUES('ART001',CONVERT(date,GETDATE()),'50','200','8,50','S','0','N')

--Nuestro tigger se ejecutara cada vez que se inserte una nueva fila en la tabla MoviAlmacen, en el caso de entrar stock actualizara  la tabla articulos

--Codigo del trigger
CREATE TRIGGER tr_ActualizarExistencias
ON proyecto.MoviAlmacen
AFTER INSERT
AS
BEGIN
	DECLARE @V_CodArticulo char(6), @v_resultado smallint, @v_EntradaSalida char(1)
	
	SELECT @V_CodArticulo=CodArticulo, @v_resultado=Cantidad,@v_EntradaSalida=EntradaSalida
	FROM inserted
	/*Si lo que recibimos es una entrada actualizamos el stock del articulo*/
	IF(@v_EntradaSalida LIKE 'E')
	BEGIN
		UPDATE proyecto.Articulos 
		SET Stock+=@v_resultado
		wHERE CodArticulo=@V_CodArticulo
	END

END

