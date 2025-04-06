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


       [HttpGet("[action]/{tipoUsuario}")]
        public async Task<IActionResult> GetUsuario(int tipoUsuario)
        {
            var result = await _usuarioService.GetUsuario(tipoUsuario);
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

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePermisoUsuario([FromBody] PermisoEmpresaInsertDTO usuarioDto)
        {
            var result = await _usuarioService.InsertPermisoEmpresa(usuarioDto);
            return result.GetHttpResponse();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdatePermisoUsuario([FromBody] PermisoEmpresaInsertDTO usuarioDto)
        {
            var result = await _usuarioService.UpdatePermisoEmpresa(usuarioDto);
            return result.GetHttpResponse();
        }

        [HttpDelete("[action]/{idUsuario}/{idEmpresa}")]
        public async Task<IActionResult> DeletePermisoUsuario(int idUsuario, int idEmpresa)
        {
            var result = await _usuarioService.DeletePermisoEmpresa(idUsuario, idEmpresa);
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idUsuario}")]
        public async Task<IActionResult> GetPermisoUsuarioID(int idUsuario)
        {
            var result = await _usuarioService.GetPermisoUsuarioID(idUsuario);
            return result.GetHttpResponse();
        }
    }
}