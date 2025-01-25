using ApiEliteWebAcceso.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ApiEliteWebAcceso.Controllers
{
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService )
        {
            _menuService = menuService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Menu(int idUsuario, int idEmpresa)
        {
            var result = await _menuService.ObtenerMenuUsuarioRolEmpresa(idUsuario, idEmpresa);
            return result.GetHttpResponse();
        }

    }
}
