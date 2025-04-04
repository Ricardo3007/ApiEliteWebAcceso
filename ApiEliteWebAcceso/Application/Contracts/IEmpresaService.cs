using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using System.Threading.Tasks;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IEmpresaService
    {
        Task<Result<List<GrupoEmpresaDto>>> GetGrupoEmpresa();
        Task<Result<GrupoEmpresaDto>> GetGrupoEmpresaID(int idUsuario);


        Task<Result<GrupoEmpresaDto>> CreateGrupoEmpresa(GrupoEmpresaDto createGrupoEmpresa);
        Task<Result<GrupoEmpresaDto>> UpdateGrupoEmpresa(GrupoEmpresaDto updateGrupoEmpresa);
        Task<Result<bool>> DeleteGrupoEmpresa(int idGrupoEmpresa);

        Task<Result<List<EmpresaDto>>> GetEmpresa();
        Task<Result<EmpresaDto>> GetEmpresaID(int idUsuario);


        Task<Result<int>> CreateEmpresa(EmpresaInsertDto createEmpresa);
        Task<Result<bool>> UpdateEmpresa(EmpresaUpdateDto updateEmpresa);
        Task<Result<bool>> DeleteEmpresa(int idEmpresa);

        Task<Result<List<EmpresaPorGrupoDto>>> GetEmpresaPorGrupo(int idGrupoEmpresa, bool isSuperAdmin);

    }
}
