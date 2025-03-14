using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IEmpresaService
    {
        Task<Result<List<GrupoEmpresaDto>>> GetGrupoEmpresa();
        Task<Result<GrupoEmpresaDto>> GetGrupoEmpresaID(int idUsuario);


        Task<Result<GrupoEmpresaDto>> CreateGrupoEmpresa(GrupoEmpresaDto createGrupoEmpresa);
        Task<Result<GrupoEmpresaDto>> UpdateGrupoEmpresa(GrupoEmpresaDto updateGrupoEmpresa);
        Task<Result<bool>> DeleteGrupoEmpresa(int idGrupoEmpresa);

    }
}
