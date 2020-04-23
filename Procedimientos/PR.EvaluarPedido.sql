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
--DECLARAMOS EL CURSOR PARA CUALCULAR 
	DECLARE CUR_EvaluarPedido CURSOR
	FOR SELECT Cantidad
	FROM proyecto.MoviAlmacen
	WHERE CodArticulo=@v_codArticulo
	
	OPEN CUR_EvaluarPedido

	SET @v_cantidad=(SELECT Cantidad
	FROM proyecto.MoviAlmacen
	WHERE CodArticulo=@v_codArticulo)

	FETCH NEXT FROM CUR_EvaluarPedido INTO @v_cantidad
	WHILE @@FETCH_STATUS=0

	FETCH NEXT FROM CUR_EvaluarPedido INTO @v_cantidad
	END
	CLOSE CUR_EvaluarPedido
	DEALLOCATE CUR_EvaluarPedido
END