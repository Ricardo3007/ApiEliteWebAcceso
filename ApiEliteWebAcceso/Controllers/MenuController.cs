using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace ApiEliteWebAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService )
        {
            _menuService = menuService;
        }

        [HttpGet("[action]/{idUsuario}/{idEmpresa}")]
        public async Task<IActionResult> GetMenuUsuario(int idUsuario, int idEmpresa)
        {
            var result = await _menuService.GetMenuUsuario(idUsuario, idEmpresa);
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idGrupoEmpresa}")]
        public async Task<IActionResult> GetMenuPermiso(int idGrupoEmpresa, int? idRol = null)
        {
            var result = await _menuService.GetMenuPermiso(idGrupoEmpresa, idRol);
            return result.GetHttpResponse();
        }

    }
}
