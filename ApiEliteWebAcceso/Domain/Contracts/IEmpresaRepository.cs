using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IEmpresaRepository
    {
        Task<List<ACC_GRUPO_EMPRESAS>> GetGrupoEmpresa();
        Task<ACC_GRUPO_EMPRESAS> GetGrupoEmpresaID(int idGrupoEmpresa);
    }
}
