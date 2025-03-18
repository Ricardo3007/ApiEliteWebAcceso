using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IUsuarioRepository
    {
        Task<List<ACC_USUARIO>> GetUsuario();
        Task<ACC_USUARIO> GetUsuarioID(int idUsuario);
        Task<List<ACC_PERMISO_USUARIO_DETALLE>> GetPermisoUsuarioID(int idUsuario,int idGrupoEmpresa);
        Task<UsuarioDto> CreateUsuario(UsuarioDto createAplicativo);
        Task<bool> UpdateUsuario(UsuarioDto updateAplicativo);
        Task<bool> DeleteUsuario(int idUsuario);

        Task<List<ACC_PERMISO_USUARIO_EMPRESA>> GetPermisoUsuarioEmpresaID(int idUsuario, int idGrupoEmpresa, bool isSuperAdmin);
    }
}
