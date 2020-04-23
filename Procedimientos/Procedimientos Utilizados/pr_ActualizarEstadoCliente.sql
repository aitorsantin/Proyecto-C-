/*Procedimiento almacenado para cambiar el estado del cliente dependiendo del numero de pedidos realizados*/


/*Codigo de prueba*/
exec pr_ActualizarEstadoCliente 'CLI003'

/*Codigo Procedimiento*/
CREATE PROCEDURE pr_ActualizarEstadoCliente
@p_codcliente char(6)
AS
DECLARE @v_contador tinyint
BEGIN
	SET @v_contador=(SELECT COUNT(Contador)
						FROM proyecto.ContadorPedidos
						WHERE CodCliente=@p_codcliente)

	/*Si ha realizado uno o mas pedidos ya es un cliente existente*/
	IF @v_contador>=1
	BEGIN
		UPDATE proyecto.Clientes
		SET TipoCliente='E'
		WHERE CodCliente=@p_codcliente
	END

	/*Si ha realizado 50 pedidos o mas ya es un cliente premium ahora podra realizar pedidos sin pago por adelantado*/
	ELSE IF @v_contador>=50
	BEGIN
		UPDATE proyecto.Clientes
		SET TipoCliente='P'
		WHERE CodCliente=@p_codcliente
	END
END