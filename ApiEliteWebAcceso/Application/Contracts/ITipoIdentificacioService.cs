using ApiEliteWebAcceso.Application.DTOs.Maestros;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface ITipoIdentificacioService
    {
        Task<Result<List<TipoIdentificacionDto>>> GetTipoIdentificacion();
        Task<Result<TipoIdentificacionDto>> GetTipoIdentificacionID(int idTipoIdentificacion);
    }
}
