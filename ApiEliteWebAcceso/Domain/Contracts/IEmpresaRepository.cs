using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IEmpresaRepository
    {
        Task<List<ACC_GRUPO_EMPRESAS>> GetGrupoEmpresa();
        Task<ACC_GRUPO_EMPRESAS> GetGrupoEmpresaID(int idGrupoEmpresa);

        Task<GrupoEmpresaDto> CreateGrupoEmpresa(GrupoEmpresaDto createGrupoEmpresa);
        Task<GrupoEmpresaDto> UpdateGrupoEmpresa(GrupoEmpresaDto updateGrupoEmpresa);
        Task<bool> DeleteGrupoEmpresa(int idGrupoEmpresa);

    }
}
