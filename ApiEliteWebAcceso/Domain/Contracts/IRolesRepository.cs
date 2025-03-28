using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.DTOs.Roles;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IRolesRepository
    {

        Task<List<ACC_ROLES>> GetRoles();
        Task<ACC_ROLES> GetRolesID(int idRol);
        Task<ACC_ROLES> CreateRol(ACC_ROLES createRol);
        Task<bool> UpdateRol(ACC_ROLES updateRol);

        Task<List<ACC_ROLES_GRUPOEMPRESA>> GetRolGrupoEmpresa(int idGrupoEmpresa, bool isSuperAdmin);
        Task<List<RolesOpcionMenu>> GetRolOpcionesMenu(int idRol);

        Task<bool> CreateRolAsync(RolDTO rolDTO);

        Task<bool> UpdateRol(RolDTO rolDTO);
        Task<bool> DeleteRol(int idRol);
    }
}
