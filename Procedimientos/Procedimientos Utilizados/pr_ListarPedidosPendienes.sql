/*Procedimiento para mostrar todos los pedidos pendientes de los clientes nuevos y existentes que han pagado por adelantado*/
/*O de los clientes premium que esten pagados o no */


/*Codigo de prueba*/
EXEC pr_ListarPedidosPendienes

/*Procedimiento almacenado*/
ALTER PROCEDURE pr_ListarPedidosPendienes
AS
BEGIN
	select CodPedido, CodEmpleado,cp.CodCliente,Fecha, Estado, Pagado 
	from proyecto.CabeceraPedido CP
	JOIN proyecto.Clientes as C
	on c.CodCliente=cp.CodCliente
	where
	c.TipoCliente='N' AND Pagado='S' AND Estado = 'P' OR Estado = 'A'
	or
	c.TipoCliente='E' AND Pagado='S' AND Estado = 'P'OR Estado = 'A'
	or
	c.TipoCliente='P' AND Estado = 'P' OR Estado = 'A'
	Order by Fecha
END