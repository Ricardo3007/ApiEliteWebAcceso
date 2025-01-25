using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Login;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControlAccesoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _autenticacionService;

        public AuthController(IAuthService autenticacionService)
        {
            _autenticacionService = autenticacionService;
        }

        /// <summary>
        /// Login - acceso
        /// </summary>
        /// <param name="loginUsuario">loginUsuario dto para inicio de sesion</param>
        /// <author>Ricardo Ferrer</author>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUsuarioDto loginUsuario)
        {
            var result = await _autenticacionService.ValidarLogin(loginUsuario);
            return result.GetHttpResponse();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Menu(LoginUsuarioDto MenuEmpresa)
        {
            var result = await _autenticacionService.ValidarLogin(MenuEmpresa);
            return result.GetHttpResponse();
        }

    }
}
