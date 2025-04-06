using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IUsuarioRepository
    {
        Task<List<ACC_USUARIO>> GetUsuario(int tipoUsuario);
        Task<ACC_USUARIO> GetUsuarioID(int idUsuario);
        Task<List<ACC_PERMISO_USUARIO_DETALLE>> GetPermisoUsuarioID(int idUsuario,int idGrupoEmpresa);
        Task<int> CreateUsuario(UsuarioInsertDto createUsuario);
        Task<bool> UpdateUsuario(UsuarioInsertDto updateAplicativo);
        Task<bool> DeleteUsuario(int idUsuario);

        Task<bool> InsertPermisoEmpresa(PermisoEmpresaInsertDTO dto);
        Task<bool> UpdatePermisoEmpresa(PermisoEmpresaInsertDTO dto);
        Task<bool> DeletePermisoEmpresa(int idUsuario, int idEmpresa);
        Task<PermisoEmpresaInsertDTO> GetPermisoUsuarioID(int idUsuario);

        Task<List<ACC_PERMISO_USUARIO_EMPRESA>> GetPermisoUsuarioEmpresaID(int idGrupoEmpresa, bool isSuperAdmin);
    }
}
