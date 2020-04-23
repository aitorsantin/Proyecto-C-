--Queremos que cada 7 dias se nos actualicen las columnas de stock_min,Stock_max y Stock_medio
--Para ello crearemos un porcedimiento almacenado con un cursor en su interior que ira recorriendo fila por fila
--y actualizara las columnas indicadas siendo el stock_minimo=stock/7 cantiad de unidades de ese producto
--entre los dias hasta la reposicion de las existencias. stock_max es la multiplicacion del stock_min*15
--y el stock medio es la suma del stock_maximo mas el minimo entre 2.


--Codigo del Procedimiento
CREATE PROCEDURE proyecto.pr_ActualizarStockMiMaMe
@p_salida smallint output
AS
--Declaramos las variables que vamos a necesitar
DECLARE @v_codigo char(6),@v_stock smallint, @v_max smallint, @v_min smallint, @v_medio smallint
--Declaramos el cursor
--he indicamos cuales son las columnas con las que vamos a trabajar
DECLARE cur_ActualizarStock CURSOR
	For select CodArticulo,Stock,Stock_Minimo,Stock_Maximo,Stock_Medido
		from proyecto.Articulos
--Abrimos el cursor
OPEN cur_ActualizarStock
--Ejecutamos la transaccion
BEGIN TRANSACTION
BEGIN TRY
--Indicamos que las columnas CodArticulo,Stock,Stock_Minimo,Stock_Maximo,Stock_Medido
--Estaran almacenadas en las variables @v_codigo, @v_stock, @v_max,  @v_min, @v_medio
	FETCH NEXT FROM cur_ActualizarStock INTO @v_codigo, @v_stock, @v_max,  @v_min, @v_medio
--Recorremos la tabla fila por fila
	WHILE @@FETCH_STATUS=0
BEGIN
	BEGIN
--Realizamos los calculos arriba mencionados
		SET @v_min=@v_stock/7
		SET @v_max=@v_min*15
		SET @v_medio=(@v_max+@v_min)/2
--Actualizamos las columnas de la tabla articulos pasandoles el valor de las variables a sus
--correspondientes filas
		UPDATE proyecto.Articulos 
		SET Stock=@v_stock,
		Stock_Minimo=@v_min,
		Stock_Maximo=@v_max,
		Stock_Medido=@v_medio
		WHERE CodArticulo=@v_codigo
		FETCH cur_ActualizarStock INTO @v_codigo, @v_max,  @v_min, @v_medio
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
