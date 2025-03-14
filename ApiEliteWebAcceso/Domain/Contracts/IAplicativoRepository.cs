using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IAplicativoRepository
    {

        Task<List<ACC_APLICACION>> GetAplicativo();
        Task<ACC_APLICACION> GetAplicativoID(int idAplicativo);
        Task<AplicativoDto> CreateAplicativo(AplicativoDto createAplicativo);
        Task<bool> UpdateAplicativo(AplicativoDto updateAplicativo);
        Task<bool> DeleteAplicativo(int idAplicativo);

    }
}
