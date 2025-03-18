using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IRolesRepository
    {

        Task<List<ACC_ROLES>> GetRoles();
        Task<ACC_ROLES> GetRolesID(int idRol);
        Task<ACC_ROLES> CreateRol(ACC_ROLES createRol);
        Task<bool> UpdateRol(ACC_ROLES updateRol);
        Task<bool> DeleteRol(int idRol);

        Task<List<ACC_ROLES_GRUPOEMPRESA>> GetRolGrupoEmpresa(int idGrupoEmpresa, bool isSuperAdmin);
    }
}
