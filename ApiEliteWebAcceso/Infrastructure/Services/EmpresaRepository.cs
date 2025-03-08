using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using ApiEliteWebAcceso.Domain.Helpers.Enum;
using Dapper;
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
    }
}
