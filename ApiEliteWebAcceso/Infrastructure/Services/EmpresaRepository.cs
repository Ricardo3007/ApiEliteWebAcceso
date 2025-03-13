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


    }
}
