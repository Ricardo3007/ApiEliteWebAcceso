using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Application.Services;
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

        [HttpGet("[action]/{idAplicativo}")]
        public async Task<IActionResult> GetMenuPadre(int idAplicativo)
        {
            var result = await _menuService.GetMenuPadre(idAplicativo);
            return result.GetHttpResponse();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMenu()
        {
            var result = await _menuService.GetMenu();
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idMenu}")]
        public async Task<IActionResult> GetMenuID(int idMenu)
        {
            var result = await _menuService.GetMenuID(idMenu);
            return result.GetHttpResponse();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateMenu([FromBody] MenuPrincipalDTO menu)
        {
            var result = await _menuService.CreateMenu(menu);
            return result.GetHttpResponse();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateMenu([FromBody] MenuPrincipalDTO menu)
        {
            var result = await _menuService.UpdateMenu(menu);
            return result.GetHttpResponse();
        }

        [HttpDelete("[action]/{idMenu}")]
        public async Task<IActionResult> DeleteMenu(int idMenu)
        {
            var result = await _menuService.DeleteMenu(idMenu);
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idEmpresa}")]
        public async Task<IActionResult> GetMenuUsuarioEmpresa(int idEmpresa, int? idRol = null)
        {
            var result = await _menuService.GetMenuUsuarioEmpresa(idEmpresa, idRol);
            return result.GetHttpResponse();
        }

    }
}
