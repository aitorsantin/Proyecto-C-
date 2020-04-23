/*Codigo de prueba*/
exec pr_Login '44852133D','comercial2'       
   
/*Procedimiento almacenado*/
ALTER PROCEDURE pr_Login
@p_user varchar(25),
@p_pass char(16)
AS
BEGIN
SELECT Username, Password, Departamento, Empleo, IdEmpleado
FROM proyecto.Login as l
join proyecto.Empleados as e
on e.CodLogin=l.CodLogin
where Username=@p_user and Password=@p_pass
END

