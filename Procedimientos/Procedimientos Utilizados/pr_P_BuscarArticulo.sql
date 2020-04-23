EXEC P_BuscarArticulos 'ch'

ALTER PROCEDURE P_BuscarArticulos
@p_parametro as varchar(50)
AS
BEGIN
SELECT *
FROM proyecto.Articulos
WHERE CodArticulo like '%'+@p_parametro+'%'or Descripcion like '%'+@p_parametro+'%'
END

