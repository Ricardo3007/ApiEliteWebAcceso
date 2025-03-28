using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Roles;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using Dapper;
using Microsoft.Data.SqlClient;
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

        public async Task<List<ACC_ROLES>> GetRoles()
        {
            string consulta = @"SELECT [PK_ROL_C]
                                  ,[NOMBRE_ROL_C]
                                  ,[FK_GRUPO_EMPRESA_C]
                                  ,[ESTADO_C]
                              FROM [Elite_Web].[dbo].[ACC_ROLES]";

            var result = await _dbConnection.QueryAsync<ACC_ROLES>(consulta);

            return result.ToList();
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
                                    MENU.FK_APLICATIVO_C,APL.INICIALES_APLICATIVO_C,APL.ORDEN_C
		                            FROM ACC_MENU_ELITE MENU  
			                             INNER JOIN ACC_OPCIONES_ROL OPC ON OPC.FK_OPCION_MENU_C = MENU.PK_OPCION_MENU_C
			                             INNER JOIN ACC_APLICACION	 APL ON APL.PK_APLICATIVO_C = MENU.FK_APLICATIVO_C  
			                             WHERE OPC.FK_ROL_C = @FK_ROL_C AND MENU.ESTADO_C = 'A'
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
                Orden = e.ORDEN_C
            }).ToList();
        }

        public Task<bool> UpdateRol(ACC_ROLES updateAplicativo)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> CreateRolAsync(RolDTO rolDTO)
        {

            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open(); // Abrir la conexión si está cerrada
            }

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Insertar el rol
                    string insertRolQuery = @"
                        INSERT INTO ACC_ROLES (NOMBRE_ROL_C, FK_GRUPO_EMPRESA_C, ESTADO_C)
                        VALUES (@NombreRol, @GrupoEmpresaId, @Estado);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    int rolId = await _dbConnection.ExecuteScalarAsync<int>(
                        insertRolQuery,
                        new
                        {
                            NombreRol = rolDTO.NombreRolDTO,
                            GrupoEmpresaId = rolDTO.GrupoEmpresaIdDTO,
                            Estado = rolDTO.EstadoDTO
                        },
                        transaction
                    );

                    // Insertar permisos
                    string insertPermisosQuery = @"
                        INSERT INTO ACC_OPCIONES_ROL (FK_OPCION_MENU_C, FK_ROL_C)
                        VALUES (@OpcionMenuId, @RolId);";

                    foreach (var permiso in rolDTO.Permisos)
                    {
                        await _dbConnection.ExecuteAsync(
                            insertPermisosQuery,
                            new { OpcionMenuId = permiso, RolId = rolId },
                            transaction
                        );
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }




        public async Task<bool> UpdateRol(RolDTO rolDTO)
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open(); // Abrir la conexión si está cerrada
            }

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // 1. Actualizar el rol
                    string updateRolQuery = @"
                        UPDATE ACC_ROLES 
                        SET NOMBRE_ROL_C = @NombreRol, 
                            FK_GRUPO_EMPRESA_C = @GrupoEmpresaId, 
                            ESTADO_C = @Estado
                        WHERE PK_ROL_C = @RolId;";

                    await _dbConnection.ExecuteAsync(
                        updateRolQuery,
                        new
                        {
                            RolId = rolDTO.IdRolDTO, // Asegúrate de que este ID venga en el DTO
                            NombreRol = rolDTO.NombreRolDTO,
                            GrupoEmpresaId = rolDTO.GrupoEmpresaIdDTO,
                            Estado = rolDTO.EstadoDTO
                        },
                        transaction
                    );

                    // 2. Eliminar los permisos existentes del rol
                    string deletePermisosQuery = @"
                        DELETE FROM ACC_OPCIONES_ROL 
                        WHERE FK_ROL_C = @RolId;";

                    await _dbConnection.ExecuteAsync(
                        deletePermisosQuery,
                        new { RolId = rolDTO.IdRolDTO },
                        transaction
                    );

                    // 3. Insertar los nuevos permisos
                    string insertPermisosQuery = @"
                        INSERT INTO ACC_OPCIONES_ROL (FK_OPCION_MENU_C, FK_ROL_C)
                        VALUES (@OpcionMenuId, @RolId);";

                    foreach (var permiso in rolDTO.Permisos)
                    {
                        await _dbConnection.ExecuteAsync(
                            insertPermisosQuery,
                            new { OpcionMenuId = permiso, RolId = rolDTO.IdRolDTO },
                            transaction
                        );
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }



        public async Task<bool> DeleteRol(int idRol)
        {
            if (idRol <= 0)
            {
                throw new ArgumentException("El ID del rol debe ser un valor entero positivo.");
            }

            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open(); // Abrir la conexión si está cerrada
            }

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Eliminar las opciones del rol
                    var sqlDeleteOpc = @"
                        DELETE FROM ACC_OPCIONES_ROL
                        WHERE FK_ROL_C = @FK_ROL_C;
                    ";
                    var parametersOpc = new { FK_ROL_C = idRol };
                    var rowsOpcDeleted = await _dbConnection.ExecuteAsync(sqlDeleteOpc, parametersOpc, transaction);

                    // Eliminar el rol
                    var sqlDelete = @"
                        DELETE FROM ACC_ROLES
                        WHERE PK_ROL_C = @PK_ROL_C;
                    ";
                    var parameters = new { PK_ROL_C = idRol };
                    var rowsRolDeleted = await _dbConnection.ExecuteAsync(sqlDelete, parameters, transaction);

                    // Confirmar la transacción solo si ambas eliminaciones son exitosas
                    if (rowsRolDeleted > 0 || rowsOpcDeleted > 0)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }





    }
}
