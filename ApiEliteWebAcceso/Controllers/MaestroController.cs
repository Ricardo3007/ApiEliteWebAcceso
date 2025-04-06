using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiEliteWebAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MaestroController
    {
        private readonly ITipoIdentificacioService _tipoIdentificacioService;
        public MaestroController(ITipoIdentificacioService tipoIdentificacioService)
        {
            _tipoIdentificacioService = tipoIdentificacioService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> getTipoIdentificacion()
        {
            var result = await _tipoIdentificacioService.GetTipoIdentificacion();
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idTipoIdentificacion}")]
        public async Task<IActionResult> getTipoIdentificacionID(int idTipoIdentificacion)
        {
            var result = await _tipoIdentificacioService.GetTipoIdentificacionID(idTipoIdentificacion);
            return result.GetHttpResponse();
        }
    }

}
