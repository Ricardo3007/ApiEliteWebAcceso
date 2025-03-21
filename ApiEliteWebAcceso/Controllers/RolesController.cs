using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
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
        

    }
}