using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IUsuarioService
    {
        Task<Result<List<UsuarioDto>>> GetUsuario(int tipoUsuario);
        Task<Result<UsuarioDto>> GetUsuarioID(int idUsuario, int idGrupoEmpresa);
        Task<Result<int>> CreateUsuario(UsuarioInsertDto usuarioDto);
        Task<Result<bool>> UpdateUsuario(UsuarioInsertDto usuarioDto);
        Task<Result<bool>> DeleteUsuario(int idUsuario);

        Task<Result<bool>> InsertPermisoEmpresa(PermisoEmpresaInsertDTO dto);
        Task<Result<bool>> UpdatePermisoEmpresa(PermisoEmpresaInsertDTO dto);
        Task<Result<bool>> DeletePermisoEmpresa(int idUsuario, int idEmpresa);
        Task<Result<PermisoEmpresaInsertDTO>> GetPermisoUsuarioID(int idUsuario);

        Task<Result<List<UsuarioEmpresaPermisoDto>>> GetPermisoUsuarioEmpresaID(int idGrupoEmpresa, bool isSuperAdmin);
    }
}
