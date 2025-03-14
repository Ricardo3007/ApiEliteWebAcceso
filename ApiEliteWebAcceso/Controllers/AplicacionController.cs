using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiEliteWebAcceso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacionController
    {
        private readonly IAplicacionService _aplicativoService;
        public AplicacionController(IAplicacionService aplicativoService)
        {
            _aplicativoService = aplicativoService;
        }


       [HttpGet("[action]")]
        public async Task<IActionResult> GetAplicativo()
        {
            var result = await _aplicativoService.GetAplicaciones();
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idAplicacion}")]
        public async Task<IActionResult> GetAplicacionID(int idAplicacion)
        {
            var result = await _aplicativoService.GetAplicacionID(idAplicacion);
            return result.GetHttpResponse();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAplicacion([FromBody] AplicativoDto aplicativoDto)
        {
            var result = await _aplicativoService.CreateAplicacion(aplicativoDto);
            return result.GetHttpResponse();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAplicacion([FromBody] AplicativoDto aplicativoDto)
        {
            var result = await _aplicativoService.UpdateAplicacion(aplicativoDto);
            return result.GetHttpResponse();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAplicacion(int idAplicacion)
        {
            var result = await _aplicativoService.DeleteAplicacion(idAplicacion);
            return result.GetHttpResponse();
        }


    }
}