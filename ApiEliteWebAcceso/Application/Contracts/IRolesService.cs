using ApiEliteWebAcceso.Application.DTOs.Roles;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IRolesService
    {
        Task<Result<List<RolesDTO>>> GetRolGrupoEmpresa(int idGrupoEmpresa, bool isSuperAdmin);
        Task<Result<List<RolesOpcionMenu>>> GetRolOpcionesMenu(int idRol);
        
    }
}
