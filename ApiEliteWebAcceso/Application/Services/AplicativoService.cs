using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Login;
using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Resource;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using ApiEliteWebAcceso.Infrastructure.Services;

namespace ApiEliteWebAcceso.Application.Services
{
    public class AplicativoService : IAplicacionService
    {

        private readonly IConfiguration _config;
        private readonly IAplicativoRepository _aplicacionRepository;

        public AplicativoService(IConfiguration config, IAplicativoRepository aplicativoRepository)
        {
            _config = config;
            _aplicacionRepository = aplicativoRepository;
        }

        // ✅ Crear una nueva aplicación
        public async Task<Result<AplicativoDto>> CreateAplicacion(AplicativoDto createAplicacion)
        {
            try
            {
                var newAplicacion = await _aplicacionRepository.CreateAplicativo(createAplicacion);
                return Result<AplicativoDto>.Success(newAplicacion);
            }
            catch (Exception ex)
            {
                return Result<AplicativoDto>.Failure(ex.Message);
            }
        }

        // ✅ Obtener todas las aplicaciones
        public async Task<Result<List<AplicativoDto>>> GetAplicaciones()
        {
            try
            {
                // Obtener lista de modelos desde el repositorio
                var aplicaciones = await _aplicacionRepository.GetAplicativo();

                // Mapear lista de modelos a lista de DTOs
                var aplicacionesDto = aplicaciones.Select(a => new AplicativoDto
                {
                    IdAplicativoDTO = a.PK_APLICATIVO_C,
                    InicialesAplicativoDTO = a.INICIALES_APLICATIVO_C,
                    NombreAplicativoDTO = a.NOMBRE_APLICATIVO_C,
                    OrdenDTO = a.ORDEN_C,
                    EstadoDTO = a.ESTADO_C
                }).ToList();

                return Result<List<AplicativoDto>>.Success(aplicacionesDto);
            }
            catch (Exception ex)
            {
                return Result<List<AplicativoDto>>.Failure(ex.Message);
            }
        }


        // ✅ Obtener una aplicación por ID
        public async Task<Result<AplicativoDto>> GetAplicacionID(int idAplicacion)
        {
            try
            {
                // Obtener el modelo desde el repositorio
                var aplicacion = await _aplicacionRepository.GetAplicativoID(idAplicacion);

                // Validar si no se encontró el registro
                if (aplicacion == null)
                    return Result<AplicativoDto>.Failure("Aplicación no encontrada.");

                // Mapear el modelo a DTO
                var aplicacionDto = new AplicativoDto
                {
                    IdAplicativoDTO = aplicacion.PK_APLICATIVO_C,
                    InicialesAplicativoDTO = aplicacion.INICIALES_APLICATIVO_C,
                    NombreAplicativoDTO = aplicacion.NOMBRE_APLICATIVO_C,
                    OrdenDTO = aplicacion.ORDEN_C,
                    EstadoDTO = aplicacion.ESTADO_C
                };

                return Result<AplicativoDto>.Success(aplicacionDto);
            }
            catch (Exception ex)
            {
                return Result<AplicativoDto>.Failure(ex.Message);
            }
        }

    

        // ✅ Actualizar una aplicación
        public async Task<Result<bool>> UpdateAplicacion(AplicativoDto updateAplicacion)
            {
                try
                {
                    var updated = await _aplicacionRepository.UpdateAplicativo(updateAplicacion);
                    if (!updated)
                        return Result<bool>.Failure("No se pudo actualizar la aplicación.");

                    return Result<bool>.Success(true);
                }
                catch (Exception ex)
                {
                    return Result<bool>.Failure(ex.Message);
                }
            }

       // ✅ Eliminar una aplicación
       public async Task<Result<bool>> DeleteAplicacion(int idAplicacion)
            {
                try
                {
                    var deleted = await _aplicacionRepository.DeleteAplicativo(idAplicacion);
                    if (!deleted)
                        return Result<bool>.Failure("No se pudo eliminar la aplicación.");

                    return Result<bool>.Success(true);
                }
                catch (Exception ex)
                {
                    return Result<bool>.Failure(ex.Message);
                }
            }

    
    }
}
