DECLARE @v_salida char(9)
exec pr_CodigoPedido @v_salida output
PRINT @v_salida

ALTER PROCEDURE pr_CodigoPedido
@p_salida char(9) output
AS
DECLARE @v_año char(4)
DECLARE @V_cero char(5)
DECLARE @v_codpedido CHAR(9)
BEGIN


	SET @v_año=(SELECT YEAR(GETDATE()))

	SET @V_cero='0001'


	SET @v_codpedido=CONCAT(@v_año, @V_cero)

	/*Si ya existe la primera factura del año*/
	IF EXISTS (SELECT CodPedido
				FROM proyecto.CabeceraPedido
				WHERE CodPedido=@v_codpedido)
				BEGIN
					SET @v_codpedido=(SELECT MAX(CodPedido)+1
										FROM proyecto.CabeceraPedido)
					SET @p_salida=@v_codpedido
					return @p_salida
				END
				ELSE
				BEGIN
					SET @v_codpedido=@v_año+@V_cero
					SET @p_salida=@v_codpedido
					return @p_salida
				END
END