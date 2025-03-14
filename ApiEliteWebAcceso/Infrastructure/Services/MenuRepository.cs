using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using ApiEliteWebAcceso.Domain.Helpers.Enum;
using Dapper;
using System.Data;

namespace ApiEliteWebAcceso.Infrastructure.Services
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IDbConnection _dbConnection;

        public MenuRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<MenuOption>>  ObtenerMenuUsuarioRolEmpresa(int idUsuario, int idEmpresa)
        {
            string consulta = @"
                                WITH MenuHierarchy AS (
                                    -- CTE inicial: selecciona los elementos raíz (PADRES)
                                    SELECT 
                                        B.PK_OPCION_MENU_C,
                                        B.FK_APLICATIVO_C,
                                        B.URL_C,
                                        B.PARENT_C,
                                        B.TEXT_C,
                                        B.DESCRIPCION_C,
                                        B.ICONO_C,
                                        B.ESTADO_C,
                                        D.INICIALES_APLICATIVO_C,
                                        CAST(B.PK_OPCION_MENU_C AS NVARCHAR(MAX)) AS Jerarquia, -- Inicializamos la jerarquía con el PK del padre
                                        0 AS Nivel -- Nivel inicial
                                    FROM ACC_PERMISO_USUARIO A
                                    INNER JOIN ACC_MENU_ELITE B ON A.FK_OPCION_MENU_C = B.PK_OPCION_MENU_C
                                    INNER JOIN ACC_PERMISO_EMPRESA C ON A.FK_EMPRESA_C = C.FK_EMPRESA_C AND C.FK_APLICATIVO_C = B.FK_APLICATIVO_C
                                    INNER JOIN ACC_APLICACION D ON D.PK_APLICATIVO_C = C.FK_APLICATIVO_C
                                    WHERE A.FK_USUARIO_C =  @idUsuario
                                      AND A.FK_EMPRESA_C =  @idEmpresa 
                                      AND B.PARENT_C IS NULL -- Selecciona solo los elementos raíz
                                    UNION ALL
                                    SELECT 
                                        B.PK_OPCION_MENU_C,
                                        B.FK_APLICATIVO_C,
                                        B.URL_C,
                                        B.PARENT_C,
                                        B.TEXT_C,
                                        B.DESCRIPCION_C,
                                        B.ICONO_C,
                                        B.ESTADO_C,
                                        D.INICIALES_APLICATIVO_C,
                                        CAST(MH.Jerarquia + '>' + CAST(B.PK_OPCION_MENU_C AS NVARCHAR(MAX)) AS NVARCHAR(MAX)) AS Jerarquia, -- Construimos la jerarquía
                                        MH.Nivel + 1 AS Nivel -- Incrementa el nivel
                                    FROM MenuHierarchy MH
                                    INNER JOIN ACC_MENU_ELITE B ON MH.PK_OPCION_MENU_C = B.PARENT_C
                                    INNER JOIN ACC_PERMISO_EMPRESA C ON C.FK_APLICATIVO_C = B.FK_APLICATIVO_C
                                    INNER JOIN ACC_APLICACION D ON D.PK_APLICATIVO_C = C.FK_APLICATIVO_C
	                                INNER JOIN ACC_PERMISO_USUARIO perus on perus.FK_OPCION_MENU_C = B.PK_OPCION_MENU_C AND C.FK_EMPRESA_C = perus.FK_EMPRESA_C 
                                    WHERE B.ESTADO_C = 'A' AND perus.FK_USUARIO_C = @idUsuario AND perus.FK_EMPRESA_C =  @idEmpresa 
                                )
                                SELECT 
                                    PK_OPCION_MENU_C,
                                    FK_APLICATIVO_C,
                                    URL_C,
                                    PARENT_C,
                                    TEXT_C,
                                    DESCRIPCION_C,
                                    ICONO_C,
                                    ESTADO_C,
                                    INICIALES_APLICATIVO_C,
                                    Nivel,
                                    Jerarquia
                                FROM MenuHierarchy
                                ORDER BY Jerarquia;";
            
           var result = await _dbConnection.QueryAsync<MenuOption>(consulta, new { idUsuario = idUsuario, idEmpresa = idEmpresa, Estado = EstadoGeneralEnum.ACTIVO });

            return result.ToList();
        }
    }
}
