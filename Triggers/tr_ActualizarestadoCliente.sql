ALTER TRIGGER proyecto.tr_ActualizarestadoCliente
ON proyecto.CabeceraPedido
AFTER insert
AS
BEGIN
	DECLARE @v_codPedido char(9), @v_codCliente char(6)
	SELECT @v_codPedido=CodPedido, @v_codCliente=CodCliente
	FROM inserted

	IF NOT EXISTS(SELECT * FROM proyecto.Clientes where CodCliente=@v_codCliente)
	BEGIN
		PRINT 'El cliente no existe'
	END
	/*Si el cliente no tiene ningun pedido realizado*/
	IF (SELECT COUNT(*) FROM proyecto.CabeceraPedido
		WHERE CodCliente=@v_codCliente)=0
	BEGIN
		PRINT 'El cliente es nuevo'
	END

	IF (SELECT COUNT(*) FROM proyecto.CabeceraPedido
		WHERE CodCliente=@v_codCliente)>=1
	BEGIN
		UPDATE proyecto.Clientes
		SET TipoCliente='E'
		WHERE CodCliente=@v_codCliente
	END
	IF (SELECT COUNT(*) FROM proyecto.CabeceraPedido
		WHERE CodCliente=@v_codCliente)>=50
	BEGIN
	UPDATE proyecto.Clientes
		SET TipoCliente='P'
		WHERE CodCliente=@v_codCliente
	END
END