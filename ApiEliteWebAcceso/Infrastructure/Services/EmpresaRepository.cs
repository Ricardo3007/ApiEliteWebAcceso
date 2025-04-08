using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using ApiEliteWebAcceso.Domain.Helpers.Enum;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace ApiEliteWebAcceso.Infrastructure.Services
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmpresaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<ACC_GRUPO_EMPRESAS>> GetGrupoEmpresa()
        {
            string consulta = @"SELECT PK_GRUPO_EMPRESA_C,NOMBRE_GRUPO_C,ESTADO_C FROM ACC_GRUPO_EMPRESAS";

            var result = await _dbConnection.QueryAsync<ACC_GRUPO_EMPRESAS>(consulta);

            return result.ToList();
        }

        public async Task<ACC_GRUPO_EMPRESAS> GetGrupoEmpresaID(int idGrupoEmpresa)
        {
            string consulta = @"SELECT PK_GRUPO_EMPRESA_C,NOMBRE_GRUPO_C,ESTADO_C FROM ACC_GRUPO_EMPRESAS WHERE PK_GRUPO_EMPRESA_C = @idGrupoEmpresa AND  ESTADO_C = @Estado ";

            ACC_GRUPO_EMPRESAS result = await _dbConnection.QueryFirstOrDefaultAsync<ACC_GRUPO_EMPRESAS>(consulta, new { idGrupoEmpresa = idGrupoEmpresa, Estado = EstadoGeneralEnum.ACTIVO });


            return result;
        }

        public async Task<GrupoEmpresaDto> CreateGrupoEmpresa(GrupoEmpresaDto createGrupoEmpresaDto)
        {
            // Crear la consulta SQL para insertar el nuevo registro
            var sqlInsert = @"
                    INSERT INTO ACC_GRUPO_EMPRESAS (NOMBRE_GRUPO_C, ESTADO_C)
                    VALUES (@NOMBRE_GRUPO_C, @ESTADO_C);
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                NOMBRE_GRUPO_C = createGrupoEmpresaDto.nombreGrupoDTO ?? string.Empty,
                ESTADO_C = createGrupoEmpresaDto.estadoDTO ?? string.Empty
            };

            // Ejecutar la consulta y obtener el ID del nuevo registro
            var newId = await _dbConnection.ExecuteScalarAsync<int>(sqlInsert, parameters);

            // Crear y devolver el nuevo objeto ACC_GRUPO_EMPRESAS
            var newGrupoEmpresa = new GrupoEmpresaDto
            {
                idGrupoEmpresaDTO = newId,
                nombreGrupoDTO = createGrupoEmpresaDto.nombreGrupoDTO ?? string.Empty,
                estadoDTO = createGrupoEmpresaDto.estadoDTO ?? string.Empty
            };

            return newGrupoEmpresa;
        }

        public async Task<GrupoEmpresaDto> UpdateGrupoEmpresa(GrupoEmpresaDto updateGrupoEmpresaDto)
        {
            // Crear la consulta SQL para actualizar el registro existente
            var sqlUpdate = @"
                UPDATE ACC_GRUPO_EMPRESAS
                SET NOMBRE_GRUPO_C = @NOMBRE_GRUPO_C,
                    ESTADO_C = @ESTADO_C
                WHERE PK_GRUPO_EMPRESA_C = @PK_GRUPO_EMPRESA_C;
            ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_GRUPO_EMPRESA_C = updateGrupoEmpresaDto.idGrupoEmpresaDTO,
                NOMBRE_GRUPO_C = updateGrupoEmpresaDto.nombreGrupoDTO ?? string.Empty,
                ESTADO_C = updateGrupoEmpresaDto.estadoDTO ?? string.Empty
            };

            // Ejecutar la consulta de actualización
            await _dbConnection.ExecuteAsync(sqlUpdate, parameters);

            // Crear y devolver el objeto actualizado ACC_GRUPO_EMPRESAS
            var updatedGrupoEmpresa = new GrupoEmpresaDto
            {
                idGrupoEmpresaDTO = updateGrupoEmpresaDto.idGrupoEmpresaDTO,
                nombreGrupoDTO = updateGrupoEmpresaDto.nombreGrupoDTO ?? string.Empty,
                estadoDTO = updateGrupoEmpresaDto.estadoDTO ?? string.Empty
            };

            return updatedGrupoEmpresa;
        }

        public async Task<bool> DeleteGrupoEmpresa(int idGrupoEmpresa)
        {
            // Crear la consulta SQL para eliminar el registro existente
            var sqlDelete = @"
        DELETE FROM ACC_GRUPO_EMPRESAS
        WHERE PK_GRUPO_EMPRESA_C = @PK_GRUPO_EMPRESA_C;
    ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_GRUPO_EMPRESA_C = idGrupoEmpresa
            };

            // Ejecutar la consulta de eliminación
            var rowsAffected = await _dbConnection.ExecuteAsync(sqlDelete, parameters);

            // Devolver true si se eliminó al menos un registro, de lo contrario false
            return rowsAffected > 0;
        }

        public async Task<List<ACC_EMPRESA>> GetEmpresa()
        {
            string consulta = @"SELECT 
                               [PK_EMPRESA_C]
                              ,[NOMBRE_EMPRESA_C]
                              ,[FK_GRUPO_EMPRESA_C]
                              ,[ID_EMPRESA_C]
                              ,[LOGO_EMPRESA_C]
                              ,[NOMBRE_BD_C]
                              ,[SERVIDOR_BD_C]
                              ,[USUARIO_BD_C]
                              ,[PASSWORD_BD_C]
                              ,AE.[ESTADO_C]
							  ,AGE.[NOMBRE_GRUPO_C]
							  FROM ACC_EMPRESA AE
							  INNER JOIN [ACC_GRUPO_EMPRESAS] AGE ON AGE.[PK_GRUPO_EMPRESA_C] = AE.[PK_EMPRESA_C];";

            var result = await _dbConnection.QueryAsync<ACC_EMPRESA>(consulta);

            return result.ToList();
        }

        public async Task<ACC_EMPRESA> GetEmpresaID(int idEmpresa)
        {
            string consulta = @"SELECT 
                                   [PK_EMPRESA_C]
                                  ,[NOMBRE_EMPRESA_C]
                                  ,[FK_GRUPO_EMPRESA_C]
                                  ,[ID_EMPRESA_C]
                                  ,[LOGO_EMPRESA_C]
                                  ,[NOMBRE_BD_C]
                                  ,[SERVIDOR_BD_C]
                                  ,[USUARIO_BD_C]
                                  ,[PASSWORD_BD_C]
                                  ,[ESTADO_C]
                                FROM ACC_EMPRESA WHERE PK_EMPRESA_C = @idEmpresa ";

            return await _dbConnection.QueryFirstOrDefaultAsync<ACC_EMPRESA>(consulta, new { idEmpresa = idEmpresa});

        }
        public async Task<int> CreateEmpresa(EmpresaInsertDto empresaInsertDto)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                 _dbConnection.Open();
            }

            using var transaction =  _dbConnection.BeginTransaction();

            try
            {
                // 1. Insertar la empresa
                var sqlInsertEmpresa = @"
            INSERT INTO [dbo].[ACC_EMPRESA]
               ([NOMBRE_EMPRESA_C], [FK_GRUPO_EMPRESA_C], [ID_EMPRESA_C], 
               [LOGO_EMPRESA_C], [NOMBRE_BD_C], [SERVIDOR_BD_C], 
               [USUARIO_BD_C], [PASSWORD_BD_C], [ESTADO_C])
            VALUES
               (@NOMBRE_EMPRESA_C, @FK_GRUPO_EMPRESA_C, @ID_EMPRESA_C, 
               @LOGO_EMPRESA_C, @NOMBRE_BD_C, @SERVIDOR_BD_C, 
               @USUARIO_BD_C, @PASSWORD_BD_C, @ESTADO_C);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var empresaId = await _dbConnection.ExecuteScalarAsync<int>(sqlInsertEmpresa, new
                {
                    NOMBRE_EMPRESA_C = empresaInsertDto.Nombre,
                    FK_GRUPO_EMPRESA_C = empresaInsertDto.GrupoEmpresaId,
                    ID_EMPRESA_C = empresaInsertDto.CodigoEmpresa ?? string.Empty,
                    LOGO_EMPRESA_C = empresaInsertDto.Logo ?? string.Empty,
                    NOMBRE_BD_C = empresaInsertDto.NombreBaseDatos,
                    SERVIDOR_BD_C = empresaInsertDto.ServidorBaseDatos,
                    USUARIO_BD_C = empresaInsertDto.UsuarioBaseDatos,
                    PASSWORD_BD_C = empresaInsertDto.PasswordBaseDatos,
                    ESTADO_C = empresaInsertDto.Estado
                }, transaction);

                // 2. Insertar aplicativos (versión corregida)
                if (empresaInsertDto.AplicativosIds?.Count > 0)
                {
                    var sqlInsertPermiso = @"
                INSERT INTO [dbo].[ACC_PERMISO_EMPRESA]
                   ([FK_APLICATIVO_C], [FK_EMPRESA_C], [ESTADO_C])
                VALUES
                   (@FK_APLICATIVO_C, @FK_EMPRESA_C, @ESTADO_C);";

                    // Versión simple que funciona con Dapper
                    foreach (var id in empresaInsertDto.AplicativosIds)
                    {
                        await _dbConnection.ExecuteAsync(sqlInsertPermiso, new
                        {
                            FK_APLICATIVO_C = id,
                            FK_EMPRESA_C = empresaId,
                            ESTADO_C = "A"
                        }, transaction);
                    }
                }

                 transaction.Commit();
                return empresaId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new ApplicationException($"Error al crear la empresa: {ex.Message}", ex);
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                     _dbConnection.Close();
                }
            }
        }
        public async Task<bool> UpdateEmpresa(EmpresaUpdateDto empresaUpdateDto)
        {
            // Validación básica del DTO
            if (empresaUpdateDto == null)
                throw new ArgumentNullException(nameof(empresaUpdateDto));

            if (empresaUpdateDto.Id <= 0)
                throw new ArgumentException("ID de empresa no válido");

            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using var transaction = _dbConnection.BeginTransaction();

            try
            {
                // 1. Actualizar datos principales de la empresa
                var sqlUpdateEmpresa = @"
            UPDATE [dbo].[ACC_EMPRESA]
            SET
                [NOMBRE_EMPRESA_C] = @Nombre,
                [FK_GRUPO_EMPRESA_C] = @GrupoEmpresaId,
                [ID_EMPRESA_C] = @CodigoEmpresa,
                [LOGO_EMPRESA_C] = @Logo,
                [NOMBRE_BD_C] = @NombreBaseDatos,
                [SERVIDOR_BD_C] = @ServidorBaseDatos,
                [USUARIO_BD_C] = @UsuarioBaseDatos,
                [PASSWORD_BD_C] = @PasswordBaseDatos,
                [ESTADO_C] = @Estado
            WHERE
                [PK_EMPRESA_C] = @Id";

                var rowsAffected = await _dbConnection.ExecuteAsync(sqlUpdateEmpresa, new
                {
                    empresaUpdateDto.Id,
                    empresaUpdateDto.Nombre,
                    empresaUpdateDto.GrupoEmpresaId,
                    CodigoEmpresa = empresaUpdateDto.CodigoEmpresa ?? string.Empty,
                    Logo = empresaUpdateDto.Logo ?? string.Empty,
                    empresaUpdateDto.NombreBaseDatos,
                    empresaUpdateDto.ServidorBaseDatos,
                    empresaUpdateDto.UsuarioBaseDatos,
                    empresaUpdateDto.PasswordBaseDatos,
                    Estado = empresaUpdateDto.Estado ?? "A"
                }, transaction);

                if (rowsAffected == 0)
                {
                    throw new KeyNotFoundException($"No se encontró la empresa con ID {empresaUpdateDto.Id}");
                }

                // 2. Sincronizar aplicativos asociados
                if (empresaUpdateDto.AplicativosIds != null && empresaUpdateDto.AplicativosIds.Count > 0)
                {
                    // 2.1 Eliminar permisos que ya no están en la lista
                    await _dbConnection.ExecuteAsync(
                        @"DELETE FROM [dbo].[ACC_PERMISO_EMPRESA] 
                  WHERE [FK_EMPRESA_C] = @EmpresaId 
                  AND [FK_APLICATIVO_C] NOT IN @AplicativosIds",
                        new
                        {
                            EmpresaId = empresaUpdateDto.Id,
                            AplicativosIds = empresaUpdateDto.AplicativosIds
                        },
                        transaction);

                    // 2.2 Insertar nuevos permisos (versión optimizada)
                    var insertQuery = new StringBuilder();
                    var parameters = new DynamicParameters();
                    parameters.Add("EmpresaId", empresaUpdateDto.Id);

                    for (int i = 0; i < empresaUpdateDto.AplicativosIds.Count; i++)
                    {
                        var aplicativoId = empresaUpdateDto.AplicativosIds[i];
                        parameters.Add($"AplicativoId_{i}", aplicativoId);

                        insertQuery.AppendLine($@"
                    IF NOT EXISTS (
                        SELECT 1 FROM [dbo].[ACC_PERMISO_EMPRESA] 
                        WHERE [FK_EMPRESA_C] = @EmpresaId 
                        AND [FK_APLICATIVO_C] = @AplicativoId_{i}
                    )
                    BEGIN
                        INSERT INTO [dbo].[ACC_PERMISO_EMPRESA]
                            ([FK_APLICATIVO_C], [FK_EMPRESA_C], [ESTADO_C])
                        VALUES (@AplicativoId_{i}, @EmpresaId, 'A')
                    END");
                    }

                    await _dbConnection.ExecuteAsync(insertQuery.ToString(), parameters, transaction);
                }

                 transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                 transaction.Rollback();
                throw new ApplicationException($"Error al actualizar la empresa: {ex.Message}", ex);
            }
        }
        public async Task<bool> DeleteEmpresa(int idEmpresaC)
        {
            // Validar que el ID de la empresa sea válido
            if (idEmpresaC <= 0)
            {
                throw new ArgumentException("El ID de la empresa debe ser un valor entero positivo.");
            }

            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using var transaction = _dbConnection.BeginTransaction();

            try
            {
                // 1. Primero eliminar los permisos asociados en ACC_PERMISO_EMPRESA
                var sqlDeletePermisos = @"
            DELETE FROM [dbo].[ACC_PERMISO_EMPRESA]
            WHERE [FK_EMPRESA_C] = @FK_EMPRESA_C;
        ";

                var permisosParams = new { FK_EMPRESA_C = idEmpresaC };
                await _dbConnection.ExecuteAsync(sqlDeletePermisos, permisosParams, transaction);

                // 2. Luego eliminar la empresa en ACC_EMPRESA
                var sqlDeleteEmpresa = @"
            DELETE FROM [dbo].[ACC_EMPRESA]
            WHERE [PK_EMPRESA_C] = @PK_EMPRESA_C;
        ";

                var empresaParams = new { PK_EMPRESA_C = idEmpresaC };
                var rowsAffected = await _dbConnection.ExecuteAsync(sqlDeleteEmpresa, empresaParams, transaction);

                // Confirmar la transacción si todo fue bien
                transaction.Commit();

                // Retornar true si se eliminó la empresa
                return rowsAffected > 0;
            }
            catch
            {
                // Revertir la transacción en caso de error
                transaction.Rollback();
                throw;
            }
        }

        public async Task<PermisoEmpresaDTO> CreatePermisoEmpresa(PermisoEmpresaDTO createPermisoEmpresaDto)
        {
            // Crear la consulta SQL para insertar el nuevo registro
            var sqlInsert = @"
                            INSERT INTO [dbo].[ACC_PERMISO_EMPRESA]
                               ([FK_APLICATIVO_C]
                               ,[FK_EMPRESA_C]
                               ,[ESTADO_C])
                         VALUES
                               (@FK_APLICATIVO_C
                               ,@FK_EMPRESA_C
                               ,@ESTADO_C);
                            SELECT CAST(SCOPE_IDENTITY() as int);
                        ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                FK_APLICATIVO_C = createPermisoEmpresaDto.IdAplicativoDTO,
                FK_EMPRESA_C = createPermisoEmpresaDto.IdEmpresaDTO,
                ESTADO_C = createPermisoEmpresaDto.EstadoDTO ?? string.Empty // Usar string.Empty si es nulo
            };

            // Ejecutar la consulta y obtener el ID del nuevo registro
            var newId = await _dbConnection.ExecuteScalarAsync<int>(sqlInsert, parameters);

            // Crear y devolver el nuevo objeto PermisoEmpresaDto
            var newPermisoEmpresa = new PermisoEmpresaDTO
            {
                IdPermisoEmpresaDTO = newId,
                IdAplicativoDTO = createPermisoEmpresaDto.IdAplicativoDTO,
                IdEmpresaDTO = createPermisoEmpresaDto.IdEmpresaDTO,
                EstadoDTO = createPermisoEmpresaDto.EstadoDTO ?? string.Empty
            };

            return newPermisoEmpresa;
        }
        public async Task<bool> UpdatePermisoEmpresa(PermisoEmpresaDTO updatePermisoEmpresaDto)
        {
            // Crear la consulta SQL para actualizar el registro
            var sqlUpdate = @"
                            UPDATE ACC_PERMISO_EMPRESA
                            SET
                                [FK_APLICATIVO_C] = @FK_APLICATIVO_C,
                                [FK_EMPRESA_C] = @FK_EMPRESA_C,
                                [ESTADO_C] = @ESTADO_C
                            WHERE
                                [PK_PERMISO_EMPRESA_C] = @PK_PERMISO_EMPRESA_C;
                            ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_PERMISO_EMPRESA_C = updatePermisoEmpresaDto.IdPermisoEmpresaDTO, // ID del registro a actualizar
                FK_APLICATIVO_C = updatePermisoEmpresaDto.IdAplicativoDTO,
                FK_EMPRESA_C = updatePermisoEmpresaDto.IdEmpresaDTO,
                ESTADO_C = updatePermisoEmpresaDto.EstadoDTO ?? string.Empty // Usar string.Empty si es nulo
            };

            // Ejecutar la consulta de actualización
            int rowsAffected = await _dbConnection.ExecuteAsync(sqlUpdate, parameters);

            // Retornar el DTO actualizado
            return rowsAffected > 0;
        }

        public async Task<List<ACC_PERMISO_EMPRESA>> GetPermisoEmpresa()
        {
            // Crear la consulta SQL para obtener todos los registros
            var sqlQuery = @"
                SELECT 
                    [PK_PERMISO_EMPRESA_C],
                    [FK_APLICATIVO_C],
                    [FK_EMPRESA_C],
                    [ESTADO_C]
                FROM 
                    [dbo].[ACC_PERMISO_EMPRESA];
            ";

            // Ejecutar la consulta y mapear los resultados a una lista de ACC_PERMISO_EMPRESA
            var permisosEmpresa = await _dbConnection.QueryAsync<ACC_PERMISO_EMPRESA>(sqlQuery);

            // Convertir el resultado a una lista y retornarla
            return permisosEmpresa.ToList();
        }

        public async Task<ACC_PERMISO_EMPRESA> GetPermisoEmpresaID(int idPermisoEmpresa)
        {
            // Crear la consulta SQL para obtener el registro por ID
            var sqlQuery = @"
                    SELECT 
                        [PK_PERMISO_EMPRESA_C],
                        [FK_APLICATIVO_C],
                        [FK_EMPRESA_C],
                        [ESTADO_C]
                    FROM 
                        [dbo].[ACC_PERMISO_EMPRESA]
                    WHERE
                        [PK_PERMISO_EMPRESA_C] = @PK_PERMISO_EMPRESA_C;
                ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_PERMISO_EMPRESA_C = idPermisoEmpresa // ID del permiso de empresa a buscar
            };

            // Ejecutar la consulta y obtener el registro
            var permisoEmpresa = await _dbConnection.QueryFirstOrDefaultAsync<ACC_PERMISO_EMPRESA>(sqlQuery, parameters);

            // Retornar el registro encontrado (puede ser null si no se encuentra)
            return permisoEmpresa;
        }
        public async Task<List<PermisoEmpresaAplicativoDTO>> GetPermisoEmpresaAplicativo(int idEmpresa)
        {
            // Crear la consulta SQL para obtener el registro por ID
            var sqlQuery = @"
                            SELECT  
                                PERM.PK_PERMISO_EMPRESA_C AS IdPermisoEmpresaDTO, 
                                PERM.FK_APLICATIVO_C AS IdAplicativoDTO,
                                APL.NOMBRE_APLICATIVO_C AS NombreAplicativoDTO,
                                APL.INICIALES_APLICATIVO_C AS InicialesAplicativoDTO, 
                                PERM.FK_EMPRESA_C AS IdEmpresaDTO,
                                EMP.NOMBRE_EMPRESA_C AS NombreEmpresaDTO, 
                                PERM.ESTADO_C AS EstadoDTO
                            FROM ACC_PERMISO_EMPRESA PERM
		                            INNER JOIN ACC_EMPRESA EMP ON EMP.PK_EMPRESA_C = PERM.FK_EMPRESA_C 
		                            INNER JOIN ACC_APLICACION APL ON APL.PK_APLICATIVO_C = PERM.FK_APLICATIVO_C 
	                            where PERM.FK_EMPRESA_C = @FK_EMPRESA_C AND PERM.ESTADO_C ='A'
                        ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                FK_EMPRESA_C = idEmpresa // ID del permiso de empresa a buscar
            };

            // Ejecutar la consulta y obtener el registro
            var permisoEmpresa = await _dbConnection.QueryAsync<PermisoEmpresaAplicativoDTO>(sqlQuery, parameters);

            // Retornar el registro encontrado (puede ser null si no se encuentra)
            return permisoEmpresa.ToList();
        }

        public async Task<bool> DeletePermisoEmpresa(int idPermisoEmpresa)
        {
            // Crear la consulta SQL para eliminar el registro
            var sqlDelete = @"
                DELETE FROM [dbo].[ACC_PERMISO_EMPRESA]
                WHERE [PK_PERMISO_EMPRESA_C] = @PK_PERMISO_EMPRESA_C;
            ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_PERMISO_EMPRESA_C = idPermisoEmpresa // ID del permiso de empresa a eliminar
            };

            // Ejecutar la consulta de eliminación
            int rowsAffected = await _dbConnection.ExecuteAsync(sqlDelete, parameters);

            // Retornar true si se eliminó al menos un registro, false en caso contrario
            return rowsAffected > 0;
        }

        public async Task<List<EmpresaPorGrupoDto>> GetEmpresaPorGrupo(int idGrupoEmpresa, bool isSuperAdmin)
        {
            var sqlQuery = @"
                            SELECT 
                                EMP.PK_EMPRESA_C, 
                                EMP.NOMBRE_EMPRESA_C, 
	                            GR.PK_GRUPO_EMPRESA_C,
	                            GR.NOMBRE_GRUPO_C
                            FROM ACC_EMPRESA EMP 
		                            INNER JOIN ACC_GRUPO_EMPRESAS GR ON GR.PK_GRUPO_EMPRESA_C = EMP.FK_GRUPO_EMPRESA_C 
                             WHERE EMP.ESTADO_C = 'A'
                                     AND (@superAdmin = 1 OR EMP.FK_GRUPO_EMPRESA_C = @FK_GRUPO_EMPRESA_C) 
                            ORDER BY EMP.NOMBRE_EMPRESA_C";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                FK_GRUPO_EMPRESA_C = idGrupoEmpresa, // ID del permiso de empresa a buscar
                superAdmin = isSuperAdmin
            };

            // Ejecutar la consulta y obtener el registro
            var result = await _dbConnection.QueryAsync(sqlQuery, parameters);


            // Retornar el registro encontrado (puede ser null si no se encuentra)
            return result.Select(e => new EmpresaPorGrupoDto
                    {
                        idEmpresaDTO = e.PK_EMPRESA_C,
                        nombreDTO = e.NOMBRE_EMPRESA_C,
                        idGrupoEmpresaDTO = e.PK_GRUPO_EMPRESA_C,
                        nombreGrupoEmpresaDTO = e.NOMBRE_GRUPO_C
                    }).ToList();
        }
    }
}
