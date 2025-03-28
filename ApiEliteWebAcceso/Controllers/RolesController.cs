using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.DTOs.Roles;
using ApiEliteWebAcceso.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiEliteWebAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }


        [HttpGet("[action]/{idGrupoEmpresa}/{isSuperAdmin}")]
        public async Task<IActionResult> GetRolGrupoEmpresa(int idGrupoEmpresa, bool isSuperAdmin)
        {
            var result = await _rolesService.GetRolGrupoEmpresa(idGrupoEmpresa, isSuperAdmin);
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idRol}")]
        public async Task<IActionResult> GetRolOpcionesMenu(int idRol)
        {
            var result = await _rolesService.GetRolOpcionesMenu(idRol);
            return result.GetHttpResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _rolesService.GetRoles();
            return result.GetHttpResponse();
        }

        [HttpPost("CreateRol")]
        public async Task<IActionResult> CreateRol([FromBody] RolDTO rolDTO)
        {
            var result = await _rolesService.CreateRolAsync(rolDTO);

            return result.GetHttpResponse();
        }


        [HttpGet("[action]/{idRol}")]
        public async Task<IActionResult> GetRolById(int idRol)
        {
            var result = await _rolesService.GetRolById(idRol);
            return result.GetHttpResponse();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRol([FromBody] RolDTO rolDTO)
        {
            var result = await _rolesService.UpdateRol(rolDTO);

            return result.GetHttpResponse();
        }


        [HttpDelete("DeleteRol/{idRol}")]
        public async Task<IActionResult> DeleteRol(int idRol)
        {
            var result = await _rolesService.DeleteRol(idRol);
            return result.GetHttpResponse();
        }



    }
}