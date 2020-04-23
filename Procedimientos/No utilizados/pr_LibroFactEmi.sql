--Tenemos un procedimiento que ejecutaremos cada vez que tengamos una factura pagada.
--Hay ocasiones en que las facturas ya nos llegan pagadas y otras que se quedan pendientes 


--Codigo del procedimiento

ALTER PROCEDURE proyecto.Pr_LibroFactEmi
--Creamos el procedimiento y las variables
@p_Codfact char(8), @p_CodPed char(9),
@p_fecha date, @p_IVA smallmoney='0,21', @p_salida smallint OUTPUT
AS	
BEGIN
DECLARE @V_codigo tinyint 
DECLARE @V_SubTotal smallmoney
DECLARE @v_total smallmoney
DECLARE @V_fechapago date
DECLARE @v_fecha date
DECLARE @v_pagado char(1)

BEGIN
	--Tenemos que introducir en la columna subtotal la suma de las unidades * el precio de cada una de las facturas
		SET @V_SubTotal=(SELECT SUM(Unidades*Precio) as SubTotal
		FROM proyecto.LineasFatura
		WHERE CodFactura=@p_Codfact AND CodPedido=@p_CodPed
		GROUP BY CodFactura,CodPedido)
	--El @p_codigo son el codigo del libro de facturas emitidas
	--como es un tinyint iremos sumando una a una cada vez que realicemos una insert
		SET @V_codigo=(SELECT count(CodLibro)+1 AS CodLibro
						FROM proyecto.Libro_Fact_Emi)
	--La variable @p_pagado almacena el estado de la factura
		SET @v_pagado=(SELECT Pagado
						FROM proyecto.CabeceraFactura
						WHERE CodFactura=@p_Codfact AND CodPedido=@p_CodPed
						GROUP BY CodFactura,CodPedido,Pagado)

	--la variable fecha almacena la fecha actual
		SET @v_fecha=CONVERT(date,GETDATE())
	-- La variable fechapago almacena la fecha de la cabeceraFactura
		SET @v_fechapago=(SELECT Fecha
							FROM proyecto.CabeceraFactura
							WHERE CodFactura=@p_Codfact AND CodPedido=@p_CodPed AND Pagado LIKE 'S')
	--El total es el calculo de la variable subtotal* la variable iva + el subtotal
		SET @v_total=(@v_SubTotal*@p_IVA)+@v_SubTotal
		BEGIN TRANSACTION
		BEGIN TRY
			BEGIN
	--Si la factura esta pagada y la fecha actual es la misma que la fecha en la que se genero la factura
			IF(@v_pagado LIKE 'S' AND @p_fecha=@v_fechapago)
	
			INSERT INTO proyecto.Libro_Fact_Emi 
			(CodLibro,
			CodFactura,
			CodPedido,
			Fecha,
			SubTotal,
			IVA,
			Total)
			VALUES
			(@v_codigo,@p_Codfact,@p_CodPed,@v_fechapago,@v_SubTotal,@p_IVA,@v_total)

		--Si la factura esta pagada y la fecha actual es mayor que la fecha de pago
			IF(@v_pagado LIKE 'S' AND @p_fecha>@v_fechapago)
			BEGIN
			INSERT INTO proyecto.Libro_Fact_Emi 
			(CodLibro,
			CodFactura,
			CodPedido,
			Fecha,
			SubTotal,
			IVA,
			Total)
			VALUES
			(@v_codigo,@p_Codfact,@p_CodPed,@v_fecha,@v_SubTotal,@p_IVA,@v_total)
			END
	
		COMMIT TRANSACTION
				SET @p_salida=@@ERROR
				END
		END TRY
		BEGIN CATCH
				SET @p_salida=@@ERROR
				ROLLBACK TRANSACTION
		END CATCH
	END
END


	





