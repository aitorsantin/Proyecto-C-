/*Procedimiento para dar de alta a un cliente y asignarlo a un empleado*/
/*Codigo de prueba*/
DECLARE @p_salida tinyint
exec pr_NuevoClienteRelacionComercial 3, 'CLI023', 'BAR PEDRO', 'B20717195', 'Calle Matia', '20100', 'Errenteria', '666100900', 
'', 'ES1120456523612520', @p_salida output
print @p_salida

/*Prpocedimiento Almacenado*/
ALTER PROCEDURE pr_NuevoClienteRelacionComercial
@p_codEmpleado smallint,
@p_codCliente char(6),
@p_nombre varchar(25),
@p_cif char(9),
@p_direccion varchar(50),
@_cp char(5),
@p_poblacion varchar(30),
@p_telefono char(9),
@p_email varchar(50),
@p_banco varchar(24),
@p_salida tinyint output
AS
DECLARE @v_empleo tinyint
BEGIN
	BEGIN TRANSACTION
	BEGIN TRY
		
		SET @v_empleo=(select Empleo
						from proyecto.Empleados
						WHERE IdEmpleado=@p_codEmpleado)

		IF @v_empleo!=1
		BEGIN
			/*Añadir cliente*/
			INSERT INTO proyecto.Clientes
			(CodCliente, Nombre, CIF, Direccion, CP, Poblacion, Telefono, Correo, Cuenta_Bancaria, TipoCliente)
			VALUES
			(@p_codCliente, @p_nombre, @p_cif, @p_direccion, @_cp, @p_poblacion, @p_telefono, @p_email, @p_banco, 'N' )

			INSERT INTO proyecto.ComercialClientes
			(CodEmpleado, CodCliente)
			VALUES
			(@p_codEmpleado, @p_codCliente)

			SET @p_salida=0;
		END

		/*Es un gerente*/
		IF @v_empleo=1
		BEGIN
			SET @p_salida=2;
		END
		
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @p_salida=1;
	END CATCH

	RETURN @p_salida;


END