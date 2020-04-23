/*Codigo de prueba*/
exec pr_P_Empleado 1

/*Codigo del procedimiento*/
CREATE PROCEDURE pr_P_empleado
@p_codigo tinyint
AS
BEGIN
SELECT IdEmpleado, (Nombre + ' '+ Apellido+' '+SegundoAp) as nombre
FROM proyecto.Empleados
WHERE IdEmpleado=@p_codigo
END


