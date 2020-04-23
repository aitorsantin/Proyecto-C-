/*Procedimiento almacenado para contar cuantos pedidos ha realizado el cliente que ha echo el utlimo pedido*/

/*Codigo de prueba*/
exec pr_ContarPedidos 'CLI003'

/*Codigo Procedimiento*/
ALTER PROCEDURE pr_ContarPedidos
@p_codcliente char(6)
AS
	DECLARE @v_contador tinyint
BEGIN
	SET @v_contador=(SELECT COUNT(CodPedido)
						FROM proyecto.CabeceraPedido
						WHERE CodCliente=@p_codcliente)

	UPDATE proyecto.ContadorPedidos
	SET Contador=@v_contador
	WHERE CodCliente=@p_codcliente

	print @v_contador
END