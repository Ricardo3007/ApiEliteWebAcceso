using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApiEliteWebAcceso.Infrastructure.Services
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection;

        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateUsuario(UsuarioInsertDto usuarioInsertDto)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using (var transaction = _dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Insertar en ACC_USUARIO
                        var insertUsuarioSql = @"
                                                INSERT INTO [dbo].[ACC_USUARIO]
                                                    ([USUARIO_C],
                                                     [NOMBRE_USUARIO_C],
                                                     [FK_TDI_C],
                                                     [ID_USUARIO_C],
                                                     [PASSWORD_C],
                                                     [MAIL_USUARIO_C],
                                                     [ESTADO_C],
                                                     [TIPO_USUARIO_C])
                                                VALUES
                                                    (@Usuario,
                                                     @Nombre,
                                                     @TipoDocumento,
                                                     @Documento,
                                                     @Password,
                                                     @Email,
                                                     @Estado,
                                                     @TipoUsuario);
                                                SELECT SCOPE_IDENTITY();";

                        var idUsuario = await _dbConnection.ExecuteScalarAsync<int>(insertUsuarioSql, new
                        {
                            Usuario = usuarioInsertDto.UsuarioDTO,
                            Nombre = usuarioInsertDto.NombreDTO,
                            TipoDocumento = 1, // Asumo que este valor va fijo o debes mapearlo si está en el DTO
                            Documento = usuarioInsertDto.DocumentoDTO,
                            Password = usuarioInsertDto.PasswordDTO,
                            Email = usuarioInsertDto.EmailDTO,
                            Estado = usuarioInsertDto.EstadoDTO,
                            TipoUsuario = usuarioInsertDto.TipoUsuarioDTO
                        }, transaction);

                        transaction.Commit();
                        return idUsuario;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // Acá podrías loguear el error si es necesario
                        throw new Exception("Error al insertar el usuario", ex);
                    }
                    finally
                    {
                        if (_dbConnection.State == ConnectionState.Open)
                        {
                            _dbConnection.Close();
                        }
                    }
            }
            
        }

        public async Task<bool> DeleteUsuario(int idUsuario)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Eliminar permisos del usuario
                    var deletePermisosSql = @"
                                                DELETE FROM [dbo].[ACC_PERMISO_USUARIO]
                                                WHERE FK_USUARIO_C = @UsuarioId;";

                    await _dbConnection.ExecuteAsync(deletePermisosSql, new
                    {
                        UsuarioId = idUsuario
                    }, transaction);

                    // Eliminar el usuario
                    var deleteUsuarioSql = @"
                                            DELETE FROM [dbo].[ACC_USUARIO]
                                            WHERE PK_USUARIO_C = @UsuarioId;";

                    var rowsAffected = await _dbConnection.ExecuteAsync(deleteUsuarioSql, new
                    {
                        UsuarioId = idUsuario
                    }, transaction);

                    transaction.Commit();

                    // Devuelve true si se eliminó al menos un usuario
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al eliminar el usuario", ex);
                }
                finally
                {
                    if (_dbConnection.State == ConnectionState.Open)
                    {
                        _dbConnection.Close();
                    }
                }
            }
        }

        public async Task<List<ACC_PERMISO_USUARIO_EMPRESA>> GetPermisoUsuarioEmpresaID(int idGrupoEmpresa,bool isSuperAdmin)
        {

            var sqlQuery = @"
                            SELECT 
                                EMP.PK_EMPRESA_C, 
                                EMP.NOMBRE_EMPRESA_C, 
	                            GR.PK_GRUPO_EMPRESA_C,
	                            GR.NOMBRE_GRUPO_C,
                                CASE 
                                    WHEN MAX(PER.PK_PERMISO_EMPRESA_C) IS NULL THEN 0 
                                    ELSE 1 
                                END AS TIENE_PERMISO, -- INDICA SI EL USUARIO TIENE EL PERMISO (1) O NO (0)
	                            CASE 
                                    WHEN MAX(MENU.PK_OPCION_MENU_C) IS NULL THEN 0 
                                    ELSE 1 
                                END AS TIENE_MENU -- INDICA SI EL USUARIO TIENE EL PERMISO (1) O NO (0)
                            FROM ACC_EMPRESA EMP 
		                            INNER JOIN ACC_GRUPO_EMPRESAS GR ON GR.PK_GRUPO_EMPRESA_C = EMP.FK_GRUPO_EMPRESA_C
		                            LEFT JOIN ACC_PERMISO_EMPRESA PER 
                                ON EMP.PK_EMPRESA_C = PER.FK_EMPRESA_C 
		                            LEFT JOIN ACC_MENU_ELITE MENU 
                                ON MENU.FK_APLICATIVO_C = PER.FK_APLICATIVO_C  
                            WHERE 1 = 1
                                    AND (@isSuperAdmin = 1 OR EMP.FK_GRUPO_EMPRESA_C = @PK_GRUPO_EMPRESA_C)
                            GROUP BY EMP.PK_EMPRESA_C, EMP.NOMBRE_EMPRESA_C,GR.PK_GRUPO_EMPRESA_C,GR.NOMBRE_GRUPO_C
                            ORDER BY EMP.NOMBRE_EMPRESA_C";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                isSuperAdmin = isSuperAdmin ? 1 : 0,
                PK_GRUPO_EMPRESA_C = idGrupoEmpresa
            };

            // Ejecutar la consulta y obtener el registro
            var permisoMenu = await _dbConnection.QueryAsync<ACC_PERMISO_USUARIO_EMPRESA>(sqlQuery, parameters);

            // Convertir el resultado a una lista y retornarla
            return permisoMenu.ToList();
        }

        public async Task<List<ACC_PERMISO_USUARIO_DETALLE>> GetPermisoUsuarioID(int idUsuario, int idGrupoEmpresa)
        {
            // Crear la consulta SQL para obtener todos los registros
            var sqlQuery = @"
                            SELECT DISTINCT
                                EMP.PK_EMPRESA_C, 
                                EMP.NOMBRE_EMPRESA_C, 
                                EMP.FK_GRUPO_EMPRESA_C, 
                                APL.INICIALES_APLICATIVO_C, 
                                APL.NOMBRE_APLICATIVO_C, 
                                APL.ORDEN_C, 
                                MENU.PK_OPCION_MENU_C, 
                                MENU.DESCRIPCION_C, 
                                MENU.URL_C, 
                                MENU.ICONO_C, 
                                MENU.ESTADO_C,
	                            PU.PK_PERMISO_USUARIO_C,
	                            PE.PK_PERMISO_EMPRESA_C,
                                CASE 
                                    WHEN PU.PK_PERMISO_USUARIO_C IS NOT NULL  THEN 1
                                    ELSE 0 
                                END AS PERMISO_C
                            FROM ACC_EMPRESA EMP
                            JOIN ACC_PERMISO_EMPRESA PE 
                                ON EMP.PK_EMPRESA_C = PE.FK_EMPRESA_C
                                AND EMP.FK_GRUPO_EMPRESA_C = @FK_GRUPO_EMPRESA_C
                            JOIN ACC_APLICACION APL 
                                ON PE.FK_APLICATIVO_C = APL.PK_APLICATIVO_C
                            JOIN ACC_MENU_ELITE MENU 
                                ON MENU.FK_APLICATIVO_C = APL.PK_APLICATIVO_C
                            LEFT JOIN ACC_PERMISO_USUARIO PU 
                                ON PU.FK_OPCION_MENU_C = MENU.PK_OPCION_MENU_C 
                                AND PU.FK_USUARIO_C = @PK_USUARIO_C
                                AND PU.FK_EMPRESA_C = EMP.PK_EMPRESA_C
                            ORDER BY EMP.PK_EMPRESA_C, APL.ORDEN_C, MENU.PK_OPCION_MENU_C";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_USUARIO_C = idUsuario, // ID del permiso de empresa a buscar
                FK_GRUPO_EMPRESA_C = idGrupoEmpresa, // ID del permiso de empresa a buscar
            };

            // Ejecutar la consulta y obtener el registro
            var permisoMenu = await _dbConnection.QueryAsync<ACC_PERMISO_USUARIO_DETALLE>(sqlQuery, parameters);

            // Convertir el resultado a una lista y retornarla
            return permisoMenu.ToList();
        }

        public async Task<List<ACC_USUARIO>> GetUsuario(int tipoUsuario)
        {
            // Crear la consulta SQL para obtener todos los registros
            var sqlQuery = @"
                            SELECT [PK_USUARIO_C]
                                  ,[USUARIO_C]
                                  ,[NOMBRE_USUARIO_C]
                                  ,[FK_TDI_C]
                                  ,[ID_USUARIO_C]
                                  ,[PASSWORD_C]
                                  ,[MAIL_USUARIO_C]
                                  ,[ESTADO_C]
                                  ,[TIPO_USUARIO_C]
                              FROM [dbo].[ACC_USUARIO]
                              WHERE 
                                    (@TipoUsuario = 1)
                                    OR (@TipoUsuario = 2 AND TIPO_USUARIO_C = 3)
                                    OR (@TipoUsuario = 3 AND 1 = 0) -- Esto fuerza que no se devuelva nada cuando sea 3";

            // Ejecutar la consulta y mapear los resultados a una lista de ACC_USUARIO
            var usuario = await _dbConnection.QueryAsync<ACC_USUARIO>(sqlQuery,new { TipoUsuario  = tipoUsuario });

            // Convertir el resultado a una lista y retornarla
            return usuario.ToList();
        }

        public async Task<ACC_USUARIO> GetUsuarioID(int idUsuario)
        {
            // Crear la consulta SQL para obtener todos los registros
            var sqlQuery = @"
                            SELECT [PK_USUARIO_C]
                                  ,[USUARIO_C]
                                  ,[NOMBRE_USUARIO_C]
                                  ,[FK_TDI_C]
                                  ,[ID_USUARIO_C]
                                  ,[PASSWORD_C]
                                  ,[MAIL_USUARIO_C]
                                  ,[ESTADO_C]
                                  ,[TIPO_USUARIO_C]
                              FROM [dbo].[ACC_USUARIO]  WHERE PK_USUARIO_C = @PK_USUARIO_C";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_USUARIO_C = idUsuario // ID del permiso de empresa a buscar
            };

            // Ejecutar la consulta y obtener el registro
            var usuario = await _dbConnection.QueryFirstOrDefaultAsync<ACC_USUARIO>(sqlQuery, parameters);

            // Retornar el registro encontrado (puede ser null si no se encuentra)
            return usuario;
        }

        public async Task<bool> UpdateUsuario(UsuarioInsertDto usuarioInsertDto)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Actualizar ACC_USUARIO
                    var updateUsuarioSql = @"
                                            UPDATE [dbo].[ACC_USUARIO]
                                            SET 
                                                USUARIO_C = @Usuario,
                                                NOMBRE_USUARIO_C = @Nombre,
                                                FK_TDI_C = @TipoDocumento,
                                                ID_USUARIO_C = @Documento,
                                                PASSWORD_C = @Password,
                                                MAIL_USUARIO_C = @Email,
                                                ESTADO_C = @Estado,
                                                TIPO_USUARIO_C = @TipoUsuario
                                            WHERE PK_USUARIO_C = @IdUsuario;";

                    await _dbConnection.ExecuteAsync(updateUsuarioSql, new
                    {
                        Usuario = usuarioInsertDto.UsuarioDTO,
                        Nombre = usuarioInsertDto.NombreDTO,
                        TipoDocumento = 1, // Fijo o desde DTO si lo tenés
                        Documento = usuarioInsertDto.DocumentoDTO,
                        Password = usuarioInsertDto.PasswordDTO,
                        Email = usuarioInsertDto.EmailDTO,
                        Estado = usuarioInsertDto.EstadoDTO,
                        TipoUsuario = usuarioInsertDto.TipoUsuarioDTO,
                        IdUsuario = usuarioInsertDto.IdUsuarioDTO
                    }, transaction);

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al actualizar el usuario", ex);
                }
                finally
                {
                    if (_dbConnection.State == ConnectionState.Open)
                    {
                        _dbConnection.Close();
                    }
                }
            }
        }

        public async Task<bool> InsertPermisoEmpresa(PermisoEmpresaInsertDTO dto)
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    foreach (var empresa in dto.PermisosPorEmpresa)
                    {
                        foreach (var permiso in empresa.Permisos)
                        {
                            var insertSql = @"
                        INSERT INTO [dbo].[ACC_PERMISO_USUARIO]
                            (FK_USUARIO_C, FK_OPCION_MENU_C, FK_EMPRESA_C, FK_ROL_C)
                        VALUES
                            (@UsuarioId, @PermisoId, @EmpresaId, @RolId);";

                            await _dbConnection.ExecuteAsync(insertSql, new
                            {
                                UsuarioId = dto.IdUsuarioDTO,
                                PermisoId = permiso,
                                EmpresaId = empresa.IdEmpresaDTO,
                                RolId = empresa.IdRolDTO
                            }, transaction);
                        }
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al insertar permisos por empresa", ex);
                }
                finally
                {
                    if (_dbConnection.State == ConnectionState.Open)
                        _dbConnection.Close();
                }
            }
        }

        public async Task<bool> UpdatePermisoEmpresa(PermisoEmpresaInsertDTO dto)
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var deleteSql = @"
                                    DELETE FROM [dbo].[ACC_PERMISO_USUARIO]
                                    WHERE FK_USUARIO_C = @UsuarioId;";

                    await _dbConnection.ExecuteAsync(deleteSql, new { UsuarioId = dto.IdUsuarioDTO }, transaction);

                    // Insertamos los nuevos permisos
                    foreach (var empresa in dto.PermisosPorEmpresa)
                    {
                        foreach (var permiso in empresa.Permisos)
                        {
                            var insertSql = @"
                        INSERT INTO [dbo].[ACC_PERMISO_USUARIO]
                            (FK_USUARIO_C, FK_OPCION_MENU_C, FK_EMPRESA_C, FK_ROL_C)
                        VALUES
                            (@UsuarioId, @PermisoId, @EmpresaId, @RolId);";

                            await _dbConnection.ExecuteAsync(insertSql, new
                            {
                                UsuarioId = dto.IdUsuarioDTO,
                                PermisoId = permiso,
                                EmpresaId = empresa.IdEmpresaDTO,
                                RolId = empresa.IdRolDTO
                            }, transaction);
                        }
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al actualizar permisos por empresa", ex);
                }
                finally
                {
                    if (_dbConnection.State == ConnectionState.Open)
                        _dbConnection.Close();
                }
            }
        }

        public async Task<bool> DeletePermisoEmpresa(int idUsuario, int idEmpresa)
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var deleteSql = @"
                DELETE FROM [dbo].[ACC_PERMISO_USUARIO]
                WHERE FK_USUARIO_C = @UsuarioId AND FK_EMPRESA_C = @EmpresaId";

                    var rowsAffected = await _dbConnection.ExecuteAsync(deleteSql, new
                    {
                        UsuarioId = idUsuario,
                        EmpresaId = idEmpresa
                    }, transaction);

                    transaction.Commit();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al eliminar permisos por empresa", ex);
                }
                finally
                {
                    if (_dbConnection.State == ConnectionState.Open)
                        _dbConnection.Close();
                }
            }
        }

        public async Task<PermisoEmpresaInsertDTO> GetPermisoUsuarioID(int idUsuario)
        {
            var sql = @"
                        SELECT 
                            FK_USUARIO_C,
                            FK_EMPRESA_C,
                            FK_ROL_C,
                            FK_OPCION_MENU_C
                        FROM ACC_PERMISO_USUARIO
                        WHERE FK_USUARIO_C = @IdUsuario
                    ";

            var parametros = new { IdUsuario = idUsuario };

            var permisos = (await _dbConnection.QueryAsync<dynamic>(sql, parametros)).ToList();

            var permisosPorEmpresa = permisos
                .GroupBy(p => new { Empresa = (int)p.FK_EMPRESA_C, Rol = (int)p.FK_ROL_C })
                .Select(g => new PermisoEmpresaUsuarioDTO
                {
                    IdEmpresaDTO = g.Key.Empresa,
                    IdRolDTO = g.Key.Rol,
                    Permisos = g.Select(p => (int)p.FK_OPCION_MENU_C).Distinct().ToList()
                })
                .ToList();

            var resultado = new PermisoEmpresaInsertDTO
            {
                IdUsuarioDTO = idUsuario,
                PermisosPorEmpresa = permisosPorEmpresa
            };

            return resultado;
        }
    }
}
