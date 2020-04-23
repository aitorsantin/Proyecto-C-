ALTER PROCEDURE pr_P_CabeceraPedido
@p_codempleado tinyint, @p_codcli char(6)
AS
BEGIN
	select CodPedido, Fecha, Estado, Pagado
	from proyecto.CabeceraPedido 
	where CodEmpleado = @p_codempleado AND CodCliente = @p_codcli 
END

