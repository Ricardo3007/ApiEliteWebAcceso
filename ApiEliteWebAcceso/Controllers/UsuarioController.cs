using ApiEliteWebAcceso.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ApiEliteWebAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


       [HttpGet("[action]")]
        public async Task<IActionResult> GetUsuario()
        {
            var result = await _usuarioService.GetUsuario();
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idUsuario}/{idGrupoEmpresa}")]
        public async Task<IActionResult> GetUsuarioID(int idUsuario, int idGrupoEmpresa)
        {
            var result = await _usuarioService.GetUsuarioID(idUsuario,idGrupoEmpresa);
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idUsuario}/{idGrupoEmpresa}/{isSuperAdmin}")]
        public async Task<IActionResult> GetPermisoUsuarioEmpresaID(int idUsuario, int idGrupoEmpresa, bool isSuperAdmin)
        {
            var result = await _usuarioService.GetPermisoUsuarioEmpresaID(idUsuario, idGrupoEmpresa, isSuperAdmin);
            return result.GetHttpResponse();
        }

        

    }
}