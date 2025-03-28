using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.DTOs.Roles;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IRolesService
    {
        Task<Result<List<RolesDTO>>> GetRolGrupoEmpresa(int idGrupoEmpresa, bool isSuperAdmin);
        Task<Result<List<RolesOpcionMenu>>> GetRolOpcionesMenu(int idRol);

        Task<Result<List<RolesDTO>>> GetRoles();

        Task<Result<bool>> CreateRolAsync(RolDTO rolDTO);

        Task<Result<List<RolesOpcionMenu>>> GetRolById(int idRol);

        Task<Result<bool>> UpdateRol(RolDTO rolDTO);
        Task<Result<bool>> DeleteRol(int idRol);



    }
}
