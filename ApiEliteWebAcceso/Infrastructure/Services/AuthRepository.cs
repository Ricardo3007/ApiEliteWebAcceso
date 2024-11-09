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

        public async Task<Usuarios> ValidarLogin(string documento)
        {
            var query = "SELECT * FROM Usuarios WHERE documento_c = @Documento and estado_c = @Estado";

            var result = await _dbConnection.QueryFirstOrDefaultAsync<Usuarios>(query, new { Documento = documento, Estado = EstadoGeneralEnum.ACTIVO });

            return result;
        }

        public async Task<List<Empresas>> ObtenerEmpresasPorUsuario(int idUsuario)
        {
            var query = @"
                SELECT e.* 
                FROM Usuario_Empresa ue
                INNER JOIN Empresas e ON ue.fk_empresa_c = e.pk_empresa_c
                WHERE ue.fk_usuario_c = @IdUsuario and e.estado_c = @Estado";

            var result = await _dbConnection.QueryAsync<Empresas>(query, new { IdUsuario = idUsuario, Estado = EstadoGeneralEnum.ACTIVO });

            return result.ToList();
        }
    }
}
