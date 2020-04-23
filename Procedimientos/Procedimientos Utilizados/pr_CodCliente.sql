
--Creamos un procedimiento almacenado
--Creamos una sentencia select que separa el del codigo del Cliente coge la parte numerica
--Recuperamos el ultmo numero del Codigo Cliente

select top 1 SUBSTRING (CodCliente,4,5) AS Numero
from proyecto.Clientes
where CodCliente LIKE 'CLI%'
order by CodCliente desc



CREATE PROCEDURE pr_generarCodCliente
--Creamos una variable donde almacenaremos el ultimo dato de la tabla
@v_codigo as char(6)
AS
BEGIN
--Si no tenemos ningun dato introducimos añadimos a @v_codigo el valor CLI001
	if(select top 1 SUBSTRING (CodCliente,4,5) AS Numero
		from proyecto.Clientes
		where CodCliente LIKE 'CLI%'
		order by CodCliente desc)=000
	set @v_codigo=001
	ELSE
	set
--Si ya tenemos valores introducidos cogeremos el ultmo valor de codCliente y le sumaremos uno mas
		@v_codigo=(select top 1 SUBSTRING (CodCliente,4,5) AS Numero
		from proyecto.Clientes
		where CodCliente LIKE 'CLI%'
		order by CodCliente desc)+1
END