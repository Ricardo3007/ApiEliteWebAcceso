using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.DTOs.Roles;
using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
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

        public async Task<Result<List<RolesOpcionMenu>>> GetRolOpcionesMenu(int idRol)
        {
            try
            {
                return Result<List<RolesOpcionMenu>>.Success(await _rolesRepository.GetRolOpcionesMenu(idRol));
            }
            catch (Exception ex)
            {
                return Result<List<RolesOpcionMenu>>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<RolesDTO>>> GetRoles()
        {
            try
            {
                // Obtener lista de modelos desde el repositorio
                var roles = await _rolesRepository.GetRoles();

                // Mapear lista de modelos a lista de DTOs
                var rolesDto = roles.Select(a => new RolesDTO
                {
                    IdRolDTO = a.PK_ROL_C,
                    NombreRolDTO = a.NOMBRE_ROL_C,
                    GrupoEmpresaIdDTO = a.FK_GRUPO_EMPRESA_C,
                    EstadoDTO = a.ESTADO_C
                }).ToList();

                return Result<List<RolesDTO>>.Success(rolesDto);
            }
            catch (Exception ex)
            {
                return Result<List<RolesDTO>>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> CreateRolAsync(RolDTO rolDTO)
        {
            try
            {
                var rsta = await _rolesRepository.CreateRolAsync(rolDTO);
                return Result<bool>.Success(rsta);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

        }

        public Task<Result<List<RolesOpcionMenu>>> GetRolById(int idRol)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> UpdateRol(RolDTO rolDTO)
        {
            try
            {
                var rsta = await _rolesRepository.UpdateRol(rolDTO);
                return Result<bool>.Success(rsta);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

        }


        public async Task<Result<bool>> DeleteRol(int idRol)
        {
            try
            {
                var deleted = await _rolesRepository.DeleteRol(idRol);
                if (!deleted)
                    return Result<bool>.Failure("No se pudo eliminar el registro.");

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }


    }
}
