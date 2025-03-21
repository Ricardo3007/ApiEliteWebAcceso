using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
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

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateGrupoEmpresa([FromBody] GrupoEmpresaDto grupoEmpresaDto)
        {
            var result = await _empresaService.CreateGrupoEmpresa(grupoEmpresaDto);
            return result.GetHttpResponse();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateGrupoEmpresa([FromBody] GrupoEmpresaDto grupoEmpresaDto)
        {
            var result = await _empresaService.UpdateGrupoEmpresa(grupoEmpresaDto);
            return result.GetHttpResponse();
        }

        [HttpDelete("DeleteGrupoEmpresaID/{idGrupoEmpresa}")]
        public async Task<IActionResult> DeleteGrupoEmpresaID(int idGrupoEmpresa)
        {
            var result = await _empresaService.DeleteGrupoEmpresa(idGrupoEmpresa);
            return result.GetHttpResponse();
        }

        //EMPRESA

        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmpresa()
        {
            var result = await _empresaService.GetEmpresa();
            return result.GetHttpResponse();
        }

        [HttpGet("[action]/{idEmpresa}")]
        public async Task<IActionResult> GetEmpresaID(int idEmpresa)
        {
            var result = await _empresaService.GetEmpresaID(idEmpresa);
            return result.GetHttpResponse();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateEmpresa([FromBody] EmpresaDto empresaDto)
        {
            var result = await _empresaService.CreateEmpresa(empresaDto);
            return result.GetHttpResponse();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateEmpresa([FromBody] EmpresaDto empresaDto)
        {
            var result = await _empresaService.UpdateEmpresa(empresaDto);
            return result.GetHttpResponse();
        }

        [HttpDelete("DeleteEmpresaID/{idEmpresa}")]
        public async Task<IActionResult> DeleteEmpresaID(int idEmpresa)
        {
            var result = await _empresaService.DeleteEmpresa(idEmpresa);
            return result.GetHttpResponse();
        }

        
        [HttpGet("[action]/{idGrupoEmpresa}/{isSuperAdmin}")]
        public async Task<IActionResult> GetEmpresaPorGrupo(int idGrupoEmpresa,bool isSuperAdmin)
        {
            var result = await _empresaService.GetEmpresaPorGrupo(idGrupoEmpresa,isSuperAdmin);
            return result.GetHttpResponse();
        }

    }
}