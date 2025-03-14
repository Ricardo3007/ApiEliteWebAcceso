using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
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
    public class AplicativoRepository : IAplicativoRepository
    {
        private readonly IDbConnection _dbConnection;

        public AplicativoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<ACC_APLICACION>> GetAplicativo()
        {
            string consulta = @"SELECT [PK_APLICATIVO_C]
                                      ,[INICIALES_APLICATIVO_C]
                                      ,[NOMBRE_APLICATIVO_C]
                                      ,[ORDEN_C]
                                      ,[ESTADO_C]
                                  FROM ACC_APLICACION";

            var result = await _dbConnection.QueryAsync<ACC_APLICACION>(consulta);

            return result.ToList();
        }

        public async Task<ACC_APLICACION> GetAplicativoID(int idAplicativo)
        {
            string consulta = @"SELECT [PK_APLICATIVO_C]
                                      ,[INICIALES_APLICATIVO_C]
                                      ,[NOMBRE_APLICATIVO_C]
                                      ,[ORDEN_C]
                                      ,[ESTADO_C]
                                  FROM ACC_APLICACION WHERE PK_APLICATIVO_C = @idAplicativo ";

            return await _dbConnection.QueryFirstOrDefaultAsync<ACC_APLICACION>(consulta, new { idAplicativo = idAplicativo });

        }

        public async Task<AplicativoDto> CreateAplicativo(AplicativoDto createAplicativo)
        {
            // Consulta SQL para insertar el nuevo aplicativo
            var sqlInsert = @"
                                INSERT INTO [dbo].[ACC_APLICACION]
                                    ([INICIALES_APLICATIVO_C]
                                    ,[NOMBRE_APLICATIVO_C]
                                    ,[ORDEN_C]
                                    ,[ESTADO_C])
                                VALUES
                                    (@INICIALES_APLICATIVO_C
                                    ,@NOMBRE_APLICATIVO_C
                                    ,@ORDEN_C
                                    ,@ESTADO_C);
                                SELECT CAST(SCOPE_IDENTITY() as int);";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                INICIALES_APLICATIVO_C = createAplicativo.InicialesAplicativoDTO ?? string.Empty,
                NOMBRE_APLICATIVO_C = createAplicativo.NombreAplicativoDTO ?? string.Empty,
                ORDEN_C = createAplicativo.OrdenDTO ?? (byte)0, // Usar 0 si es nulo
                ESTADO_C = createAplicativo.EstadoDTO ?? string.Empty
            };

            // Ejecutar la consulta e insertar el nuevo registro, obteniendo el ID generado
            var newId = await _dbConnection.ExecuteScalarAsync<int>(sqlInsert, parameters);

            // Crear y devolver el nuevo objeto AplicativoDto con el ID asignado
            var newAplicativo = new AplicativoDto
            {
                IdAplicativoDTO = newId,
                InicialesAplicativoDTO = createAplicativo.InicialesAplicativoDTO,
                NombreAplicativoDTO = createAplicativo.NombreAplicativoDTO,
                OrdenDTO = createAplicativo.OrdenDTO,
                EstadoDTO = createAplicativo.EstadoDTO
            };

            return newAplicativo;
        }

        public async Task<bool> UpdateAplicativo(AplicativoDto updateAplicativo)
        {
            // Consulta SQL para actualizar el registro
            var sqlUpdate = @"
                        UPDATE [dbo].[ACC_APLICACION]
                        SET
                            [INICIALES_APLICATIVO_C] = ISNULL(@INICIALES_APLICATIVO_C, [INICIALES_APLICATIVO_C]),
                            [NOMBRE_APLICATIVO_C] = ISNULL(@NOMBRE_APLICATIVO_C, [NOMBRE_APLICATIVO_C]),
                            [ORDEN_C] = ISNULL(@ORDEN_C, [ORDEN_C]),
                            [ESTADO_C] = ISNULL(@ESTADO_C, [ESTADO_C])
                        WHERE
                            [PK_APLICATIVO_C] = @PK_APLICATIVO_C;
                    ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_APLICATIVO_C = updateAplicativo.IdAplicativoDTO ?? throw new ArgumentException("El ID del aplicativo es obligatorio para actualizar."),
                INICIALES_APLICATIVO_C = updateAplicativo.InicialesAplicativoDTO,
                NOMBRE_APLICATIVO_C = updateAplicativo.NombreAplicativoDTO,
                ORDEN_C = updateAplicativo.OrdenDTO,
                ESTADO_C = updateAplicativo.EstadoDTO
            };

            // Ejecutar la consulta y obtener el número de filas afectadas
            var rowsAffected = await _dbConnection.ExecuteAsync(sqlUpdate, parameters);

            // Retornar true si al menos una fila fue actualizada
            return rowsAffected > 0;
        }


        public async Task<bool> DeleteAplicativo(int idAplicativo)
        {
            // Validar que el ID de la empresa sea válido (mayor que 0)
            if (idAplicativo <= 0)
            {
                throw new ArgumentException("El ID de la empresa debe ser un valor entero positivo.");
            }

            // Crear la consulta SQL para eliminar el registro
            var sqlDelete = @"
                    DELETE FROM ACC_APLICACION
                    WHERE [PK_APLICATIVO_C] = @PK_APLICATIVO_C;
                ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                PK_APLICATIVO_C = idAplicativo
            };

            // Ejecutar la consulta y obtener el número de filas afectadas
            var rowsAffected = await _dbConnection.ExecuteAsync(sqlDelete, parameters);

            // Retornar true si al menos una fila fue eliminada
            return rowsAffected > 0;
        }


    }
}
