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

        public async Task<List<Menu_Usuario>>  ObtenerMenuUsuarioRolEmpresa(int idUsuario, int idEmpresa)
        {
            string consulta = @"
                                    SELECT 
                                        usu.nombre_c AS Usuario,
                                        rol.nombre_c AS Rol,
                                        em.nombre_c AS Empresa,
                                        menu.nombre_c AS Menu,
                                        menu.parent_c AS Menu_Padre,
                                        menu.url_c AS URL
                                    FROM 
                                        Usuarios usu
                                    INNER JOIN 
                                        Usuario_Empresa usem 
                                        ON usu.pk_usuario_c = usem.fk_usuario_c 
                                    INNER JOIN 
                                        Empresas em 
                                        ON em.pk_empresa_c = usem.fk_empresa_c 
                                        AND em.estado_c = 'activo'
                                    INNER JOIN 
                                        Roles rol 
                                        ON rol.fk_empresa_c = em.pk_empresa_c
                                    INNER JOIN 
                                        Usuario_Rol usrol 
                                        ON usrol.fk_usuario_c = usu.pk_usuario_c 
                                        AND usrol.fk_rol_c = rol.pk_rol_c
                                    INNER JOIN 
                                        Rol_Menu mero 
                                        ON mero.fk_rol_c = rol.pk_rol_c
                                    INNER JOIN 
                                        Menus menu 
                                        ON menu.pk_menu_c = mero.fk_menu_c
                                    WHERE 
                                        usu.pk_usuario_c = @idUsuario 
                                        AND em.pk_empresa_c = @idEmpresa 
                                        AND usu.estado_c = @Estado;
                                    ";
            
           var result = await _dbConnection.QueryAsync<Menu_Usuario>(consulta, new { idUsuario = idUsuario, idEmpresa = idEmpresa, Estado = EstadoGeneralEnum.ACTIVO });

            return result.ToList();
        }
    }
}
