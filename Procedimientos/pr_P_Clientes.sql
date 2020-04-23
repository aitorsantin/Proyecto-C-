/*Queremos cargar todos los clientes que tiene cada uno de los comerciales dependiendo del comercial seleccionado*/

/*Cpdigo de prueba*/
exec pr_P_Clientes '4'
/*Codigo de procedimiento*/
CREATE PROCEDURE pr_P_Clientes
@p_codEmpleado tinyint
AS
BEGIN
	SELECT cc.CodCliente, (Nombre+' '+CIF) as Nombre
	FROM proyecto.Clientes AS c
	JOIN proyecto.ComercialClientes AS cc
	ON c.CodCliente=cc.CodCliente
	WHERE cc.CodEmpleado=@p_codEmpleado
END