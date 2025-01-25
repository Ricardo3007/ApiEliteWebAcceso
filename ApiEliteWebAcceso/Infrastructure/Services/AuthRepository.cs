using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using System.Data;
using Dapper;
using ApiEliteWebAcceso.Domain.Helpers.Enum;

namespace ApiEliteWebAcceso.Infrastructure.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _dbConnection;

        public AuthRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ACC_USUARIO> ValidarLogin(string usuario)
        {
            var query = "SELECT * FROM ACC_USUARIO WHERE usuario_c = @Usuario and estado_c = @Estado";

            var result = await _dbConnection.QueryFirstOrDefaultAsync<ACC_USUARIO>(query, new { Usuario = usuario, Estado = EstadoGeneralEnum.ACTIVO });

            return result;
        }

        public async Task<List<ACC_EMPRESA>> ObtenerEmpresasPorUsuario(int idUsuario)
        {

            var query = @"
                SELECT DISTINCT FK_EMPRESA_C, ae.*
                    FROM acc_permiso_usuario apu
                    INNER JOIN ACC_EMPRESA ae on ae.PK_EMPRESA_C = apu.FK_EMPRESA_C
                    WHERE apu.FK_USUARIO_C = @IdUsuario AND ae.ESTADO_C = @Estado";

            var result = await _dbConnection.QueryAsync<ACC_EMPRESA>(query, new { IdUsuario = idUsuario, Estado = EstadoGeneralEnum.ACTIVO });

            return result.ToList();
        }
    }
}
