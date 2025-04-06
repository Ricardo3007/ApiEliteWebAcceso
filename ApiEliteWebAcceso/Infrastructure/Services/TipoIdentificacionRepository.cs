using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using Dapper;
using System.Data;

namespace ApiEliteWebAcceso.Infrastructure.Services
{
    public class TipoIdentificacionRepository : ITipoIdentificacionRepository
    {
        private readonly IDbConnection _dbConnection;

        public TipoIdentificacionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<GRL_TIPO_IDENTIFICACION>> GetTipoIdentificacion()
        {
            string consulta = @"
                                SELECT 
                                    PK_TDI_C, CODIGO_TDI_C, NOMBRE_TDI_C, CLASE_TDI_C, SW_EXPEDIDA_C, 
                                    SIGLAS_C, CARACTERES_ALFANUMERICOS_C
                                    FROM GRL_TIPO_IDENTIFICACION ";

            var result = await _dbConnection.QueryAsync<GRL_TIPO_IDENTIFICACION>(consulta);

            return result.ToList();
        }

        public async Task<GRL_TIPO_IDENTIFICACION> GetTipoIdentificacionID(int idTipoIdentificacion)
        {
            var sqlQuery = @"
                            SELECT
                                    PK_TDI_C, CODIGO_TDI_C, NOMBRE_TDI_C, CLASE_TDI_C,
                                    SW_EXPEDIDA_C, SIGLAS_C, CARACTERES_ALFANUMERICOS_C
                            FROM 
                                    GRL_TIPO_IDENTIFICACION
                            WHERE  
                                (PK_TDI_C = @IdTipoIdentificacion) ";

            // Crear los parámetros para la consulta
            var parameters = new
            {
                IdTipoIdentificacion = idTipoIdentificacion
            };

            // Convertir el resultado a una lista y retornarla
            return await _dbConnection.QueryFirstOrDefaultAsync<GRL_TIPO_IDENTIFICACION>(sqlQuery, parameters);
        }
    }
}
