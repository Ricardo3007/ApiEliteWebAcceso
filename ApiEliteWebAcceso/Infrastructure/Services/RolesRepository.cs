using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Roles;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using Dapper;
using System.Data;

namespace ApiEliteWebAcceso.Infrastructure.Services
{
    public class RolesRepository : IRolesRepository
    {

        private readonly IDbConnection _dbConnection;

        public RolesRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public Task<ACC_ROLES> CreateRol(ACC_ROLES createAplicativo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRol(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<ACC_ROLES>> GetRoles()
        {
            throw new NotImplementedException();
        }

        public Task<ACC_ROLES> GetRolesID(int idRol)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ACC_ROLES_GRUPOEMPRESA>> GetRolGrupoEmpresa(int idGrupoEmpresa, bool isSuperAdmin)
        {
            var sqlQuery = @"
                            SELECT 
                                ROL.PK_ROL_C,ROL.NOMBRE_ROL_C,ROL.FK_GRUPO_EMPRESA_C,GR.NOMBRE_GRUPO_C
			                FROM ACC_ROLES  ROL
					             INNER JOIN ACC_GRUPO_EMPRESAS GR ON GR.PK_GRUPO_EMPRESA_C = ROL.FK_GRUPO_EMPRESA_C 
			                  WHERE 1 = 1
				                     AND (@isSuperAdmin = 1 OR ROL.FK_GRUPO_EMPRESA_C = @PK_GRUPO_EMPRESA_C) AND ROL.ESTADO_C = 'A'
                             ORDER BY ROL.NOMBRE_ROL_C";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                isSuperAdmin = isSuperAdmin ? 1 : 0,
                PK_GRUPO_EMPRESA_C = idGrupoEmpresa
            };

            // Ejecutar la consulta y obtener el registro
            var permisoMenu = await _dbConnection.QueryAsync<ACC_ROLES_GRUPOEMPRESA>(sqlQuery, parameters);

            // Convertir el resultado a una lista y retornarla
            return permisoMenu.ToList();
        }

        public async Task<List<RolesOpcionMenu>> GetRolOpcionesMenu(int idRol)
        {
            var sqlQuery = @"
                            SELECT MENU.PK_OPCION_MENU_C,MENU.PARENT_C,MENU.TEXT_C,
                                    MENU.FK_APLICATIVO_C,APL.INICIALES_APLICATIVO_C,APL.ORDEN_C,
		                            CASE 
			                            WHEN OPC.PK_OPCION_ROL_C IS NULL THEN 0 
			                            ELSE 1 
		                            END AS CHECK_C -- INDICA SI EL USUARIO TIENE EL PERMISO (1) O NO (0) 
		                            FROM ACC_MENU_ELITE MENU  
			                                INNER JOIN ACC_APLICACION	APL ON APL.PK_APLICATIVO_C = MENU.FK_APLICATIVO_C  
				                             LEFT JOIN ACC_OPCIONES_ROL OPC ON OPC.FK_OPCION_MENU_C = MENU.PK_OPCION_MENU_C AND OPC.FK_ROL_C = @FK_ROL_C
			                                WHERE MENU.ESTADO_C = 'A'
			                                ORDER BY APL.ORDEN_C,APL.INICIALES_APLICATIVO_C";

            // Ejecutar la consulta y obtener el registro
            var result = await _dbConnection.QueryAsync(sqlQuery,new { FK_ROL_C = idRol });

            // Convertir el resultado a una lista y retornarla
            return result.Select(e => new RolesOpcionMenu
            {
                OpcionMenuId = e.PK_OPCION_MENU_C,
                ParentId = e.PARENT_C,
                Texto = e.TEXT_C,
                AplicativoId = e.FK_APLICATIVO_C,
                InicialesAplicativo = e.INICIALES_APLICATIVO_C,
                Orden = e.ORDEN_C,
                Check = e.CHECK_C
            }).ToList();
        }

        public Task<bool> UpdateRol(ACC_ROLES updateAplicativo)
        {
            throw new NotImplementedException();
        }
    }
}
