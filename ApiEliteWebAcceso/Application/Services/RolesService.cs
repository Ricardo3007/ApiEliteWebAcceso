using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Roles;
using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Infrastructure.Services;

namespace ApiEliteWebAcceso.Application.Services
{
    public class RolesService : IRolesService
    {

        private readonly IConfiguration _config;
        private readonly IRolesRepository _rolesRepository;

        public RolesService(IConfiguration config, IRolesRepository rolesRepository)
        {
            _config = config;
            _rolesRepository = rolesRepository;
        }

        public async Task<Result<List<RolesDTO>>> GetRolGrupoEmpresa(int idGrupoEmpresa, bool isSuperAdmin)
        {
            try
            {
                // Obtener la lista de empresas desde el repositorio
                var permisoRolEmpresa = await _rolesRepository.GetRolGrupoEmpresa(idGrupoEmpresa, isSuperAdmin);

                var permisoRolEmpresaDTO = permisoRolEmpresa.Select(e => new RolesDTO
                {
                    IdRolDTO = e.PK_ROL_C,
                    NombreRolDTO = e.NOMBRE_ROL_C,
                    NombreGrupoDTO = e.NOMBRE_GRUPO_C,
                    GrupoEmpresaIdDTO = e.FK_GRUPO_EMPRESA_C
                }).ToList();

                return Result<List<RolesDTO>>.Success(permisoRolEmpresaDTO);
            }
            catch (Exception ex)
            {
                return Result<List<RolesDTO>>.Failure(ex.Message);
            }
        }
    }
}
