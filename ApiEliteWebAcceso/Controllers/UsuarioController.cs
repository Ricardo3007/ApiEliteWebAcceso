using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Application.Services;
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

        [HttpGet("[action]/{idGrupoEmpresa}/{isSuperAdmin}")]
        public async Task<IActionResult> GetPermisoUsuarioEmpresaID(int idGrupoEmpresa, bool isSuperAdmin)
        {
            var result = await _usuarioService.GetPermisoUsuarioEmpresaID(idGrupoEmpresa, isSuperAdmin);
            return result.GetHttpResponse();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioInsertDto usuarioDto)
        {
            var result = await _usuarioService.CreateUsuario(usuarioDto);
            return result.GetHttpResponse();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUsuario([FromBody] UsuarioInsertDto usuarioDto)
        {
            var result = await _usuarioService.UpdateUsuario(usuarioDto);
            return result.GetHttpResponse();
        }

        [HttpDelete("[action]/{idUsuario}")]
        public async Task<IActionResult> DeleteUsuario(int idUsuario)
        {
            var result = await _usuarioService.DeleteUsuario(idUsuario);
            return result.GetHttpResponse();
        }

        /* [HttpPut("[action]")]
         public async Task<IActionResult> UpdateGrupoEmpresa([FromBody] UsuarioDto usuarioDto)
         {
             var result = await _usuarioService.UpdateUsuario(usuarioDto);
             return result.GetHttpResponse();
         }
        */
        /*
         [HttpDelete("DeleteGrupoEmpresaID/{idGrupoEmpresa}")]
         public async Task<IActionResult> DeleteGrupoEmpresaID(int idGrupoEmpresa)
         {
             var result = await _usuarioService.DeleteGrupoEmpresa(idGrupoEmpresa);
             return result.GetHttpResponse();
         }
        */
    }
}