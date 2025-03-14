using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using ApiEliteWebAcceso.Domain.Helpers.Enum;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
                              ,[ESTADO_C] FROM ACC_EMPRESA";

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

        public async Task<EmpresaDto> CreateEmpresa(EmpresaDto createEmpresaDto)
        {
            // Crear la consulta SQL para insertar el nuevo registro
            var sqlInsert = @"
                INSERT INTO [dbo].[ACC_EMPRESA]
                   ([NOMBRE_EMPRESA_C]
                   ,[FK_GRUPO_EMPRESA_C]
                   ,[ID_EMPRESA_C]
                   ,[LOGO_EMPRESA_C]
                   ,[NOMBRE_BD_C]
                   ,[SERVIDOR_BD_C]
                   ,[USUARIO_BD_C]
                   ,[PASSWORD_BD_C]
                   ,[ESTADO_C])
             VALUES
                   (@NOMBRE_EMPRESA_C
                   ,@FK_GRUPO_EMPRESA_C
                   ,@ID_EMPRESA_C
                   ,@LOGO_EMPRESA_C
                   ,@NOMBRE_BD_C
                   ,@SERVIDOR_BD_C
                   ,@USUARIO_BD_C
                   ,@PASSWORD_BD_C
                   ,@ESTADO_C);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                NOMBRE_EMPRESA_C = createEmpresaDto.nombreDTO ?? string.Empty,
                FK_GRUPO_EMPRESA_C = createEmpresaDto.grupoEmpresaDTO ?? (object)DBNull.Value, // Usar DBNull.Value si es nulo
                ID_EMPRESA_C = createEmpresaDto.idEmpresaC_DTO ?? string.Empty,
                LOGO_EMPRESA_C = createEmpresaDto.logoDTO ?? string.Empty,
                NOMBRE_BD_C = createEmpresaDto.nombreBdDTO ?? string.Empty,
                SERVIDOR_BD_C = createEmpresaDto.servidorBdDTO ?? string.Empty,
                USUARIO_BD_C = createEmpresaDto.usuarioBdDTO ?? string.Empty,
                PASSWORD_BD_C = createEmpresaDto.passwordBdDTO ?? string.Empty,
                ESTADO_C = createEmpresaDto.estadoDTO ?? string.Empty
            };

            // Ejecutar la consulta y obtener el ID del nuevo registro
            var newId = await _dbConnection.ExecuteScalarAsync<int>(sqlInsert, parameters);

            // Crear y devolver el nuevo objeto EmpresaDto
            var newEmpresa = new EmpresaDto
            {
                idEmpresaDTO = newId,
                nombreDTO = createEmpresaDto.nombreDTO,
                grupoEmpresaDTO = createEmpresaDto.grupoEmpresaDTO,
                idEmpresaC_DTO = createEmpresaDto.idEmpresaC_DTO,
                logoDTO = createEmpresaDto.logoDTO,
                nombreBdDTO = createEmpresaDto.nombreBdDTO,
                servidorBdDTO = createEmpresaDto.servidorBdDTO,
                usuarioBdDTO = createEmpresaDto.usuarioBdDTO,
                passwordBdDTO = createEmpresaDto.passwordBdDTO,
                estadoDTO = createEmpresaDto.estadoDTO
            };

            return newEmpresa;
        }
        public async Task<bool> UpdateEmpresa(EmpresaDto updateEmpresaDto)
        {
            // Crear la consulta SQL para actualizar el registro
             var sqlUpdate = @"
                UPDATE [dbo].[ACC_EMPRESA]
                SET
                    [NOMBRE_EMPRESA_C] = ISNULL(@NOMBRE_EMPRESA_C, [NOMBRE_EMPRESA_C]),
                    [FK_GRUPO_EMPRESA_C] = ISNULL(@FK_GRUPO_EMPRESA_C, [FK_GRUPO_EMPRESA_C]),
                    [ID_EMPRESA_C] = ISNULL(@ID_EMPRESA_C, [ID_EMPRESA_C]),
                    [LOGO_EMPRESA_C] = ISNULL(@LOGO_EMPRESA_C, [LOGO_EMPRESA_C]),
                    [NOMBRE_BD_C] = ISNULL(@NOMBRE_BD_C, [NOMBRE_BD_C]),
                    [SERVIDOR_BD_C] = ISNULL(@SERVIDOR_BD_C, [SERVIDOR_BD_C]),
                    [USUARIO_BD_C] = ISNULL(@USUARIO_BD_C, [USUARIO_BD_C]),
                    [PASSWORD_BD_C] = ISNULL(@PASSWORD_BD_C, [PASSWORD_BD_C]),
                    [ESTADO_C] = ISNULL(@ESTADO_C, [ESTADO_C])
                WHERE
                    [PK_EMPRESA_C] = @PK_EMPRESA_C;
            ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_EMPRESA_C =  updateEmpresaDto.idEmpresaDTO ?? throw new ArgumentException("El ID de la empresa es obligatorio para actualizar."), 
                NOMBRE_EMPRESA_C = updateEmpresaDto.nombreDTO,
                FK_GRUPO_EMPRESA_C = updateEmpresaDto.grupoEmpresaDTO,
                ID_EMPRESA_C = updateEmpresaDto.idEmpresaC_DTO,
                LOGO_EMPRESA_C = updateEmpresaDto.logoDTO,
                NOMBRE_BD_C = updateEmpresaDto.nombreBdDTO,
                SERVIDOR_BD_C = updateEmpresaDto.servidorBdDTO,
                USUARIO_BD_C = updateEmpresaDto.usuarioBdDTO,
                PASSWORD_BD_C = updateEmpresaDto.passwordBdDTO,
                ESTADO_C = updateEmpresaDto.estadoDTO
            };

            // Ejecutar la consulta y obtener el número de filas afectadas
            var rowsAffected = await _dbConnection.ExecuteAsync(sqlUpdate, parameters);

            // Retornar true si al menos una fila fue actualizada
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteEmpresa(int idEmpresaC)
        {
            // Validar que el ID de la empresa sea válido (mayor que 0)
            if (idEmpresaC <= 0)
            {
                throw new ArgumentException("El ID de la empresa debe ser un valor entero positivo.");
            }

            // Crear la consulta SQL para eliminar el registro
            var sqlDelete = @"
                    DELETE FROM [dbo].[ACC_EMPRESA]
                    WHERE [PK_EMPRESA_C] = @PK_EMPRESA_C;
                ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_EMPRESA_C = idEmpresaC
            };

            // Ejecutar la consulta y obtener el número de filas afectadas
            var rowsAffected = await _dbConnection.ExecuteAsync(sqlDelete, parameters);

            // Retornar true si al menos una fila fue eliminada
            return rowsAffected > 0;
        }

    }
}
