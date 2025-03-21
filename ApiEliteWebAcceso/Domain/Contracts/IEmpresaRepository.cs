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

        Task<List<ACC_EMPRESA>> GetEmpresa();
        Task<ACC_EMPRESA> GetEmpresaID(int idEmpresa);
        Task<EmpresaDto> CreateEmpresa(EmpresaDto createEmpresa);
        Task<bool> UpdateEmpresa(EmpresaDto updateEmpresa);
        Task<bool> DeleteEmpresa(int idEmpresa);

        //permisos_empresas
        Task<PermisoEmpresaDTO> CreatePermisoEmpresa(PermisoEmpresaDTO permisoEmpresa);
        Task<bool> UpdatePermisoEmpresa(PermisoEmpresaDTO permisoEmpresa);
        Task<List<ACC_PERMISO_EMPRESA>> GetPermisoEmpresa();
        Task<ACC_PERMISO_EMPRESA> GetPermisoEmpresaID(int idPermisoEmpresa);
        Task<bool> DeletePermisoEmpresa(int idPermisoEmpresa);

        Task<List<EmpresaPorGrupoDto>> GetEmpresaPorGrupo(int idGrupoEmpresa, bool isSuperAdmin);

    }
}
