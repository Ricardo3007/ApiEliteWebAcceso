using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Maestros;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Infrastructure.Services;

namespace ApiEliteWebAcceso.Application.Services
{
    public class TipoIdentificacioService : ITipoIdentificacioService
    {
        private readonly IConfiguration _config;
        private readonly ITipoIdentificacionRepository _tipoIdentificacionRepository;

        public TipoIdentificacioService(IConfiguration config, ITipoIdentificacionRepository tipoIdentificacionRepository)
        {
            _config = config;
            _tipoIdentificacionRepository = tipoIdentificacionRepository;
        }

        public async Task<Result<List<TipoIdentificacionDto>>> GetTipoIdentificacion()
        {
            try
            {
                // Obtener la lista de empresas desde el repositorio
                var result = await _tipoIdentificacionRepository.GetTipoIdentificacion();

                // Mapear la lista de ACC_EMPRESA a EmpresaDto
                var resultDto = result.Select(e => new TipoIdentificacionDto
                {
                    IdDto = e.PK_TDI_C,
                    CodigoDto = e.CODIGO_TDI_C,
                    NombreDto = e.NOMBRE_TDI_C,
                    ClaseDto = e.CLASE_TDI_C,
                    EsExpedidaDto = e.SW_EXPEDIDA_C,
                    SiglasDto = e.SIGLAS_C,
                    CaracteresAlfanumericosDto = e.CARACTERES_ALFANUMERICOS_C
                }).ToList();


                return Result<List<TipoIdentificacionDto>>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return Result<List<TipoIdentificacionDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<TipoIdentificacionDto>> GetTipoIdentificacionID(int idTipoIdentificacion)
        {
            try
            {
                // Obtener la lista de desde el repositorio
                var result = await _tipoIdentificacionRepository.GetTipoIdentificacionID(idTipoIdentificacion);


                if (result == null)
                {
                    return Result<TipoIdentificacionDto>.Failure("Empresa no encontrada.");
                }

                // Mapear la lista 
                var resultDto = new TipoIdentificacionDto
                {
                    IdDto = result.PK_TDI_C,
                    CodigoDto = result.CODIGO_TDI_C,
                    NombreDto = result.NOMBRE_TDI_C,
                    ClaseDto = result.CLASE_TDI_C,
                    EsExpedidaDto = result.SW_EXPEDIDA_C,
                    SiglasDto = result.SIGLAS_C,
                    CaracteresAlfanumericosDto = result.CARACTERES_ALFANUMERICOS_C
                };


                return Result<TipoIdentificacionDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return Result<TipoIdentificacionDto>.Failure(ex.Message);
            }
        }
    }
}
