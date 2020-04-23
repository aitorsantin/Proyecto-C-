/*Este trigger se ejecutara cada vez que demos de alta un nuevo pedido actualizando la tabla de contarpedidos*/

CREATE TRIGGER TR_ContarPedidos
ON proyecto.CabeceraPedido
AFTER INSERT
AS
BEGIN
	DECLARE @v_codpedido char(9),
			@v_codcliente char(6),
			@v_contador tinyint


			SELECT CodPedido=@v_codpedido, CodCliente=@v_codcliente
			FROM inserted

			
			SET @v_contador=(SELECT COUNT(Contador)+1 
								FROM proyecto.ContadorPedidos
								where CodCliente=@v_codcliente)

			UPDATE proyecto.ContadorPedidos
			SET Contador=@v_contador
			WHERE CodCliente=@v_codcliente

END
