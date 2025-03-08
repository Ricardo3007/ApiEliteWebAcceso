using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IEmpresaService
    {
        Task<Result<List<GrupoEmpresaDto>>> GetGrupoEmpresa();
        Task<Result<GrupoEmpresaDto>> GetGrupoEmpresaID(int idUsuario);
    }
}
