/*codigo de prueba*/
exec pr_P_Comercial

/*Codigo del procedimiento*/
CREATE PROCEDURE pr_P_Comercial
AS
BEGIN
	SELECT IdEmpleado, (Nombre +' '+ Apellido +' ' + SegundoAp) as nombre, Empleo
	FROM proyecto.Empleados
	WHERE Departamento='COM'
END