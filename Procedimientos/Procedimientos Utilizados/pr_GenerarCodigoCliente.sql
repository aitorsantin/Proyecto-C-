/*Procedimiento que genera el los numeros del nuevo codigo de cliente esto abra que concatenerlo posteriormente con un texto*/

/*Codigo de Prueba*/
declare @p_codEmpleado char(6)
exec pr_GenerarCodigoCliente @p_codEmpleado output
PRINT concat ('CLI', @p_codEmpleado)

/*Procedimiento Almacenado*/
ALTER PROCEDURE pr_GenerarCodigoCliente
@p_codEmpleado CHAR(6) output
AS
DECLARE @v_unidad char(1),
		@v_decena char(1),
		@v_centena char(1),
		@v_codigo char(6)
BEGIN

	SET @v_unidad=(SELECT  top 1 SUBSTRING(CodCliente, 6,1)
					FROM proyecto.Clientes
					ORDER BY CodCliente desc)


	IF @v_unidad<9
	BEGIN
		SET @V_unidad=@V_unidad+1

		SET @v_decena=(SELECT  top 1 SUBSTRING(CodCliente, 5,1)
					FROM proyecto.Clientes
					ORDER BY CodCliente desc)

		SET @v_centena=(SELECT  top 1 SUBSTRING(CodCliente, 4, 1)
					FROM proyecto.Clientes
					ORDER BY CodCliente desc)

		set @v_codigo=@v_centena+@v_decena+@V_unidad
		set @p_codEmpleado=@v_codigo

		RETURN @p_codEmpleado
	END

	ELSE IF @v_unidad=9
	BEGIN

		SET @v_decena=(SELECT  top 1 SUBSTRING(CodCliente, 5,1)
					FROM proyecto.Clientes
					ORDER BY CodCliente desc)

		IF @v_decena<9
		BEGIN
			SET @V_unidad=0

			SET @v_decena=@v_decena+1

			SET @v_centena=(SELECT  top 1 SUBSTRING(CodCliente, 4,1)
					FROM proyecto.Clientes
					ORDER BY CodCliente desc)

			set @v_codigo=@v_centena+@v_decena+@V_unidad
			SET @p_codEmpleado=@v_codigo
			RETURN @p_codEmpleado

		END

		ELSE IF @v_decena=9

		BEGIN

			SET @v_centena=(SELECT  top 1 SUBSTRING(CodCliente, 4,1)
					FROM proyecto.Clientes
					ORDER BY CodCliente desc)
			
			IF @v_centena<9

			BEGIN

				SET @V_unidad=0

				SET @v_decena=0

				SET @v_centena=@v_centena+1
				set @v_codigo=@v_centena+@v_decena+@V_unidad
				SET @p_codEmpleado=@v_codigo

				RETURN @p_codEmpleado
			END

		END

	END

END