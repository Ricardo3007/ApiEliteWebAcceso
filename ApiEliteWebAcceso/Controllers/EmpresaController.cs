using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiEliteWebAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController
    {
        private readonly IEmpresaService _empresaService;
        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGrupoEmpresa()
        {
            var result = await _empresaService.GetGrupoEmpresa();
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idGrupoEmpresa}")]
        public async Task<IActionResult> GetGrupoEmpresaID(int idGrupoEmpresa)
        {
            var result = await _empresaService.GetGrupoEmpresaID(idGrupoEmpresa);
            return result.GetHttpResponse();
        }

    }
}