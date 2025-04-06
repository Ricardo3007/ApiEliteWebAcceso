using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface ITipoIdentificacionRepository
    {
        Task<List<GRL_TIPO_IDENTIFICACION>> GetTipoIdentificacion();
        Task<GRL_TIPO_IDENTIFICACION> GetTipoIdentificacionID(int idTipoIdentificacion);

    }
}
