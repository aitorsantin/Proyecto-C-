exec PR_L_DatosPersonales '98755420Y', 'comercial4 '

CREATE PROCEDURE PR_L_DatosPersonales
@p_user varchar(25), @p_pass char(16)
AS
BEGIN
	SELECT IdEmpleado, e.Nombre, (Apellido+' '+ SegundoAp) AS Apellidos, em.Nombre
	FROM proyecto.Empleados AS e
	JOIN proyecto.Login as l
	on l.CodLogin=e.CodLogin
	join proyecto.Empleo as em
	on em.CodEmpleo=e.Empleo
	WHERE Username=@p_user and Password=@p_pass
END

